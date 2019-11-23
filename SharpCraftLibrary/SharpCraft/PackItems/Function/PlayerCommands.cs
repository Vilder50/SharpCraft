using SharpCraft.Commands;
using System.Linq;

namespace SharpCraft.FunctionWriters
{
    /// <summary>
    /// All the player commands
    /// </summary>
    public class PlayerCommands
    {
        readonly Function function;
        /// <summary>
        /// All commands for levels and xp
        /// </summary>
        public ClassXP XP;
        /// <summary>
        /// All commands for levels and xp
        /// </summary>
        public class ClassXP
        {
            readonly Function function;
            internal ClassXP(Function function)
            {
                this.function = function;
            }
            /// <summary>
            /// Adds the specified amount of levels to the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="levels">The amount of levels to add. If this is negative levels will be removed</param>
            public void LevelsAdd(Selector player, int levels)
            {
                function.AddCommand(new ExperienceModifyCommand(player, true, ID.AddSetModifier.add, levels));
            }
            /// <summary>
            /// Sets the selected players' levels to the specified amount
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="levels">The amount to set the levels to</param>
            public void LevelsSet(Selector player, int levels)
            {
                function.AddCommand(new ExperienceModifyCommand(player, true, ID.AddSetModifier.set, levels));
            }
            /// <summary>
            /// Outputs the amount of levels the selected player has
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            public void LevelsGet(Selector player)
            {
                player.Limited();
                function.AddCommand(new ExperienceGetCommand(player, true));
            }
            /// <summary>
            /// Adds the specified amount of points to the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="points">The amount of points to add. If this is negative points will be removed</param>
            public void PointsAdd(Selector player, int points)
            {
                function.AddCommand(new ExperienceModifyCommand(player, false, ID.AddSetModifier.add, points));
            }
            /// <summary>
            /// Sets the selected players' points to the specified amount
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="points">The amount to set the points to</param>
            public void PointsSet(Selector player, int points)
            {
                function.AddCommand(new ExperienceModifyCommand(player, false, ID.AddSetModifier.set, points));
            }
            /// <summary>
            /// Outputs the amount of points the selected player has
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            public void PointsGet(Selector player)
            {
                player.Limited();
                function.AddCommand(new ExperienceGetCommand(player, false));
            }
        }
        internal PlayerCommands(Function function)
        {
            this.function = function;
            XP = new ClassXP(function);
            Particle = new ClassParticle(function);
            Item = new ClassItem(function);
            Sound = new ClassSound(function);
            Title = new ClassTitle(function);
            Recipe = new ClassRecipe(function);
            Advancement = new ClassAdvancement(function);
        }

        /// <summary>
        /// Changes the selected players gamemode
        /// </summary>
        /// <param name="player">the <see cref="Selector"/> to use</param>
        /// <param name="mode">the gamemode to change to</param>
        public void Gamemode(Selector player, ID.Gamemode mode)
        {
            function.AddCommand(new GamemodeCommand(player, mode));
        }

        /// <summary>
        /// Changes the executing player's score in the given <see cref="ScoreObject"/>
        /// </summary>
        /// <param name="scoreObject">the <see cref="ScoreObject"/> to change in</param>
        /// <param name="number">The number to add/set it to</param>
        /// <param name="set">If the score should be set to the given number. If false it will be added instead</param>
        public void Trigger(ScoreObject scoreObject, int number, bool set = true)
        {
            function.AddCommand(new TriggerCommand(scoreObject, set, number));
        }

        /// <summary>
        /// Whispers the specified message to the selected players
        /// </summary>
        /// <param name="player">the <see cref="Selector"/> to use</param>
        /// <param name="message">The message to tell the player</param>
        public void Tell(Selector player, string message)
        {
            function.AddCommand(new MsgCommand(player, message));
        }

        /// <summary>
        /// Changes the selected players' spawnpoint to the given location
        /// </summary>
        /// <param name="player">the <see cref="Selector"/> to use</param>
        /// <param name="spawn">The new spawnpoint location</param>
        public void Spawnpoint(Selector player, Coords spawn = null)
        {
            function.AddCommand(new SpawnPointCommand(spawn ?? new Coords(), player));
        }

        /// <summary>
        /// Enables a trigger for the selected players
        /// </summary>
        /// <param name="player">the <see cref="Selector"/> to use</param>
        /// <param name="objective">The trigger (<see cref="ScoreObject"/>) to enable</param>
        public void EnableTrigger(Selector player, ScoreObject objective)
        {
            function.AddCommand(new ScoreboardEnableTriggerCommand(player, objective));
        }

        /// <summary>
        /// Tells the selected players a message in chat
        /// </summary>
        /// <param name="player">the <see cref="Selector"/> to use</param>
        /// <param name="message">The message to tell the players</param>
        public void Tellraw(Selector player, JSON[] message)
        {
            function.AddCommand(new TellrawCommand(player, message));
        }

        /// <summary>
        /// Shows a message in the selected players actionbar
        /// </summary>
        /// <param name="player">the <see cref="Selector"/> to use</param>
        /// <param name="message">The message to show</param>
        public void Actionbar(Selector player, JSON[] message)
        {
            function.AddCommand(new TitleActionbarCommand(player, message));
        }

        /// <summary>
        /// All commands for particles
        /// </summary>
        public ClassParticle Particle;
        /// <summary>
        /// All commands for particles
        /// </summary>
        public class ClassParticle
        {
            readonly Function function;
            internal ClassParticle(Function function)
            {
                this.function = function;
            }

            /// <summary>
            /// Makes particles appear in the world
            /// </summary>
            /// <param name="particle">The type of particles</param>
            /// <param name="displayCoords">The place to show the particles at</param>
            /// <param name="size">The radius to spread the particles in</param>
            /// <param name="speed">The speed of the particles</param>
            /// <param name="count">The amount of particles</param>
            /// <param name="force">If the particles should be shown no mater what</param>
            /// <param name="player">The players to show the particles to. If null the particles are shown to everyone</param>
            public void Normal(ID.Particle particle, Coords displayCoords, Coords size, double speed, int count, bool force = false, Selector player = null)
            {
                function.AddCommand(new ParticleNormalCommand(particle, displayCoords, size, speed, count, force, player));
            }

            /// <summary>
            /// Makes dust particles appear in the world
            /// </summary>
            /// <param name="color">The color of the particles</param>
            /// <param name="particleSize">The size of the particles</param>
            /// <param name="displayCoords">The place to show the particles at</param>
            /// <param name="size">The radius to spread the particles in</param>
            /// <param name="speed">The speed of the particles</param>
            /// <param name="count">The amount of particles</param>
            /// <param name="force">If the particles should be shown no mater what</param>
            /// <param name="player">The players to show the particles to. If null the particles are shown to everyone</param>
            public void ColoredDust(HexColor color, double particleSize, Coords displayCoords, Coords size, double speed, int count, bool force = false, Selector player = null)
            {
                function.AddCommand(new ParticleColoredDustCommand(color, particleSize, displayCoords, size, speed, count, force, player));
            }

            /// <summary>
            /// Makes block particles appear in the world
            /// </summary>
            /// <param name="block">The block's particles to show</param>
            /// <param name="displayCoords">The place to show the particles at</param>
            /// <param name="size">The radius to spread the particles in</param>
            /// <param name="speed">The speed of the particles</param>
            /// <param name="count">The amount of particles</param>
            /// <param name="dust">If it should be dust or squares</param>
            /// <param name="force">If the particles should be shown no mater what</param>
            /// <param name="player">The players to show the particles to. If null the particles are shown to everyone</param>
            public void Block(Block block, Coords displayCoords, Coords size, double speed, int count, bool dust = false, bool force = false, Selector player = null)
            {
                function.AddCommand(new ParticleBlockCommand(block, displayCoords, size, speed, count, dust, force, player));
            }

            /// <summary>
            /// Makes item particles appear in the world
            /// </summary>
            /// <param name="item">The item's particles to show</param>
            /// <param name="displayCoords">The place to show the particles at</param>
            /// <param name="size">The radius to spread the particles in</param>
            /// <param name="speed">The speed of the particles</param>
            /// <param name="count">The amount of particles</param>
            /// <param name="force">If the particles should be shown no mater what</param>
            /// <param name="player">The players to show the particles to. If null the particles are shown to everyone</param>
            public void Item(Item item, Coords displayCoords, Coords size, double speed, int count, bool force = false, Selector player = null)
            {
                function.AddCommand(new ParticleItemCommand(item, displayCoords, size, speed, count, force, player));
            }
        }

        /// <summary>
        /// All commands for items
        /// </summary>
        public ClassItem Item;
        /// <summary>
        /// All commands for items
        /// </summary>
        public class ClassItem
        {
            readonly Function function;
            internal ClassItem(Function function)
            {
                this.function = function;
            }

            /// <summary>
            /// Gives an item to the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="giveItem">The <see cref="Item"/> to give to the players</param>
            public void GiveItem(Selector player, Item giveItem)
            {
                if (giveItem.Slot is null)
                {
                    function.AddCommand(new GiveCommand(player, giveItem, giveItem.Count ?? 1));
                }
                else
                {
                    function.AddCommand(new ReplaceitemEntityCommand(player, new Slots.InventorySlot((int)giveItem.Slot), giveItem, giveItem.Count ?? 1));
                }
            }

            /// <summary>
            /// Gives the loot from a loottable to the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="loot">the <see cref="Loottable"/> to give the player</param>
            public void GiveItem(Selector player, ILoottable loot)
            {
                function.AddCommand(new LootCommand(new LootTargets.GiveTarget(player), new LootSources.LoottableSource(loot)));
            }

            /// <summary>
            /// Gives the loot which the selected entity would drop if killed to the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="kill">the selector selecting the entity whose loot should be dropped</param>
            public void GiveItem(Selector player, Selector kill)
            {
                kill.Limited();
                function.AddCommand(new LootCommand(new LootTargets.GiveTarget(player), new LootSources.KillSource(kill)));
            }

            /// <summary>
            /// Gives the loot which the block at the given coords would drop of broken to the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="breakBlock">the coords of the block</param>
            /// <param name="breakWith">the item used to break the block</param>
            public void GiveItem(Selector player, Coords breakBlock, Item breakWith)
            {
                if (breakWith is null)
                {
                    function.AddCommand(new LootCommand(new LootTargets.GiveTarget(player), new LootSources.MineHandSource(breakBlock, true)));
                }
                else
                {
                    function.AddCommand(new LootCommand(new LootTargets.GiveTarget(player), new LootSources.MineItemSource(breakBlock, breakWith)));
                }
            }

            /// <summary>
            /// Puts an item into the selected players' enderchests
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="giveItem">The item to insert into the enderchest. <see cref="Item.Slot"/> choses the slot.</param>
            public void GiveEnderChest(Selector player, Item giveItem)
            {
                function.AddCommand(new ReplaceitemEntityCommand(player, new Slots.EnderChestSlot(giveItem.Slot ?? 0), giveItem, giveItem.Count ?? 1));
            }
            /// <summary>
            /// Puts an item into the selected players' hotbars
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="giveItem">The item to insert into the hotbar. <see cref="Item.Slot"/> choses the slot.</param>
            public void GiveHotbar(Selector player, Item giveItem)
            {
                function.AddCommand(new ReplaceitemEntityCommand(player, new Slots.HotbarSlot(giveItem.Slot ?? 0), giveItem, giveItem.Count ?? 1));
            }

            /// <summary>
            /// Puts the item from the loot table into the players hotbar
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="loot">the <see cref="Loottable"/> to give the player</param>
            /// <param name="slot">The hotbar slot to put the item in</param>
            public void GiveHotbar(Selector player, ILoottable loot, int slot)
            {
                function.AddCommand(new LootCommand(new LootTargets.EntityTarget(player, new Slots.HotbarSlot(slot)), new LootSources.LoottableSource(loot)));
            }

            /// <summary>
            /// Puts the item from the loot table into the players hotbar
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="breakBlock">the coords of the block</param>
            /// <param name="breakWith">the item used to break the block</param>
            /// <param name="slot">The hotbar slot to put the item in</param>
            public void GiveHotbar(Selector player, Coords breakBlock, Item breakWith, int slot)
            {
                if (breakWith is null)
                {
                    function.AddCommand(new LootCommand(new LootTargets.EntityTarget(player, new Slots.HotbarSlot(slot)), new LootSources.MineHandSource(breakBlock, true)));
                }
                else
                {
                    function.AddCommand(new LootCommand(new LootTargets.EntityTarget(player, new Slots.HotbarSlot(slot)), new LootSources.MineItemSource(breakBlock, breakWith)));
                }
            }

            /// <summary>
            /// Inserts an item into the selected players' selected slot
            /// </summary>
            /// <param name="selector">the <see cref="Selector"/> to use</param>
            /// <param name="giveItem">The item to insert</param>
            /// <param name="offHand">If it should insert into the offhand instead</param>
            public void GiveWeapon(Selector selector, Item giveItem, bool offHand = false)
            {
                function.AddCommand(new ReplaceitemEntityCommand(selector, new Slots.WeaponSlot(!offHand), giveItem, giveItem.Count ?? 1));
            }
            /// <summary>
            /// Clears an item from the selected players' inventories
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="item">The item to clear</param>
            /// <param name="amount">The maximum amount of the item to clear. null clears all</param>
            public void Clear(Selector player, Item item = null, int? amount = null)
            {
                function.AddCommand(new ClearCommand(player, item, amount));
            }
        }

        /// <summary>
        /// All commands for sounds
        /// </summary>
        public ClassSound Sound;
        /// <summary>
        /// All commands for sounds
        /// </summary>
        public class ClassSound
        {
            readonly Function function;
            internal ClassSound(Function function)
            {
                this.function = function;
            }

            /// <summary>
            /// Plays a sound for the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="sound">the sound to play</param>
            /// <param name="source">the source to play the sound as</param>
            /// <param name="location">the location to play the sound at</param>
            /// <param name="volume">the maximum volume of the sound</param>
            /// <param name="speed">the speed of the sound (0-2)</param>
            /// <param name="minValue">the minimum volume of the sound (0-2)</param>
            public void Play(Selector player, string sound, ID.SoundSource source, Coords location, double volume = 1, double speed = 1, double minValue = 0)
            {
                function.AddCommand(new PlaySoundCommand(sound, source, player, location, volume, speed, minValue));
            }

            /// <summary>
            /// Stops sounds for the selected players
            /// (If no source and sound is specified it will stop all sounds)
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="source">the source to stop sounds at. Null will stop the sound from any source</param>
            /// <param name="sound">the sound to stop. Null will stop any sound in the given source</param>
            public void Stop(Selector player, ID.SoundSource? source = null, string sound = null)
            {
                function.AddCommand(new StopSoundCommand(player, sound, source));
            }
        }

        /// <summary>
        /// All commands for titles
        /// </summary>
        public ClassTitle Title;
        /// <summary>
        /// All commands for titles
        /// </summary>
        public class ClassTitle
        {
            readonly Function function;
            internal ClassTitle(Function function)
            {
                this.function = function;
            }

            /// <summary>
            /// displays a title for the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="message">The message to show the players</param>
            public void Title(Selector player, JSON[] message)
            {
                function.AddCommand(new TitleCommand(player, message));
            }

            /// <summary>
            /// displays a subtitle for the selected players
            /// Note: the subtitle is first shown when the title command is ran
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="message">The message to show the players</param>
            public void SubTitle(Selector player, JSON[] message)
            {
                function.AddCommand(new TitleSubtitleCommand(player, message));
            }

            /// <summary>
            /// choses how long the titles should be shown for the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="startFade">The amount of ticks it takes for the title to fade in</param>
            /// <param name="stay">The amount of ticks the title stays on screen</param>
            /// <param name="endFade">The amount of ticks it takes for the title to fade out</param>
            public void Time(Selector player, Time startFade, Time stay, Time endFade)
            {
                function.AddCommand(new TitleTimesCommand(player, startFade, stay, endFade));
            }

            /// <summary>
            /// clears the shown title on the selected players' screens
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            public void Clear(Selector player)
            {
                function.AddCommand(new TitleClearCommand(player));
            }

            /// <summary>
            /// resets all the title values for the selected players' screens
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            public void Reset(Selector player)
            {
                function.AddCommand(new TitleResetCommand(player));
            }

            /// <summary>
            /// Displays a whole title for the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="topMessage">The main title message</param>
            /// <param name="bottomMessage">the bottom part of the title message</param>
            /// <param name="startFade">The amount of ticks it takes for the title to fade in</param>
            /// <param name="stay">The amount of ticks the title stays on screen</param>
            /// <param name="endFade">The amount of ticks it takes for the title to fade out</param>
            public void FullTitle(Selector player, JSON[] topMessage, JSON[] bottomMessage, Time startFade, Time stay, Time endFade)
            {
                BaseExecuteCommand executeCommand = null;
                if (function.Commands.Count != 0 && function.Commands.Last() is BaseExecuteCommand execute && !execute.DoneChanging)
                {
                    executeCommand = (BaseExecuteCommand)execute.ShallowClone();
                }
                function.AddCommand(new TitleTimesCommand(player, startFade, stay, endFade));
                if (!(bottomMessage is null))
                {
                    if (!(executeCommand is null))
                    {
                        function.AddCommand(executeCommand);
                    }
                    function.AddCommand(new TitleSubtitleCommand(player, bottomMessage));
                }
                if (!(executeCommand is null))
                {
                    function.AddCommand(executeCommand);
                }
                if (topMessage is null)
                {
                    function.AddCommand(new TitleCommand(player, new JSON("")));
                }
                else
                {
                    function.AddCommand(new TitleCommand(player, topMessage));
                }
            }
        }

        /// <summary>
        /// All commands for recipes
        /// </summary>
        public ClassRecipe Recipe;
        /// <summary>
        /// All commands for recipes
        /// </summary>
        public class ClassRecipe
        {
            readonly Function function;
            internal ClassRecipe(Function function)
            {
                this.function = function;
            }

            /// <summary>
            /// Give a recipe to the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="giveRecipe">The <see cref="Recipe"/> to give</param>
            public void Give(Selector player, IRecipe giveRecipe)
            {
                function.AddCommand(new RecipeCommand(giveRecipe, player, true));
            }
            /// <summary>
            /// Gives all recipes to the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            public void GiveAll(Selector player)
            {
                function.AddCommand(new RecipeAllCommand(player, true));
            }
            /// <summary>
            /// Removes a recipe from the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="giveRecipe">the <see cref="Recipe"/> to remove</param>
            public void Remove(Selector player, Recipe giveRecipe)
            {
                function.AddCommand(new RecipeCommand(giveRecipe, player, false));
            }
            /// <summary>
            /// removes all recipes from the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            public void RemoveAll(Selector player)
            {
                function.AddCommand(new RecipeAllCommand(player, false));
            }
        }

        /// <summary>
        /// All commands for advancements
        /// </summary>
        public ClassAdvancement Advancement;
        /// <summary>
        /// All commands for advancements
        /// </summary>
        public class ClassAdvancement
        {
            readonly Function function;
            internal ClassAdvancement(Function function)
            {
                this.function = function;
            }
            /// <summary>
            /// grants/evokes all advancements for the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="revoke">if the advancement should be revoked instead of granted</param>
            public void Everything(Selector player, bool revoke = false)
            {
                function.AddCommand(new AdvancementAllCommand(player, !revoke));
            }
            /// <summary>
            /// Grants/revokes all advancements up to the specified advancement for the selected players
            /// (it also grants/revokes the specified advancement)
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="advancement">the advancement to grant/revoke up to</param>
            /// <param name="revoke">if the advancement should be revoked instead of granted</param>
            public void Until(Selector player, IAdvancement advancement, bool revoke = false)
            {
                function.AddCommand(new AdvancementSomeCommand(player, advancement, ID.RelativeAdvancement.until, !revoke));
            }
            /// <summary>
            /// grants/revokes all advancements after the specified advancement for the selected players
            /// (it also grants/revokes the specified advancement)
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="advancement">the advancement to grant/revoke from</param>
            /// <param name="revoke">if the advancement should be revoked instead of granted</param>
            public void From(Selector player, IAdvancement advancement, bool revoke = false)
            {
                function.AddCommand(new AdvancementSomeCommand(player, advancement, ID.RelativeAdvancement.from, !revoke));
            }

            /// <summary>
            /// grants/revokes all advancements in the same branch as the specified advancement for the selected players
            /// (it also grants/revokes the specified advancement)
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="advancement">an advancement in the branch to grant/revoke</param>
            /// <param name="revoke">if the advancement should be revoked instead of granted</param>
            public void Branch(Selector player, IAdvancement advancement, bool revoke = false)
            {
                function.AddCommand(new AdvancementSomeCommand(player, advancement, ID.RelativeAdvancement.through, !revoke));
            }

            /// <summary>
            /// grants/revokes the specified the advancement for the selected players
            /// (it also grants/revokes the specified advancement)
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="advancement">the advancement to grant/revoke</param>
            /// <param name="revoke">if the advancement should be revoked instead of granted</param>
            /// <param name="trigger">the trigger in the advancement to revoke/grant. Null means the advancement itself will be granted/revoked</param>
            public void Only(Selector player, IAdvancement advancement, bool revoke = false, Advancement.Trigger trigger = null)
            {
                function.AddCommand(new AdvancementSingleCommand(player, advancement, trigger, !revoke));
            }
        }
    }
}

