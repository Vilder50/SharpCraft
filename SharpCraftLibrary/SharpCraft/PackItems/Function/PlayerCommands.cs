﻿using SharpCraft.Commands;
using System.Linq;

namespace SharpCraft.FunctionWriters
{
    /// <summary>
    /// All the player commands
    /// </summary>
    public class PlayerCommands : CommandList
    {
        /// <summary>
        /// All commands for levels and xp
        /// </summary>
        public ClassXP XP;
        /// <summary>
        /// All commands for levels and xp
        /// </summary>
        public class ClassXP : CommandList
        {
            internal ClassXP(Function function) : base(function)
            {
                
            }
            /// <summary>
            /// Adds the specified amount of levels to the selected players
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="levels">The amount of levels to add. If this is negative levels will be removed</param>
            public void LevelsAdd(BaseSelector player, int levels)
            {
                ForFunction.AddCommand(new ExperienceModifyCommand(player, true, ID.AddSetModifier.add, levels));
            }
            /// <summary>
            /// Sets the selected players' levels to the specified amount
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="levels">The amount to set the levels to</param>
            public void LevelsSet(BaseSelector player, int levels)
            {
                ForFunction.AddCommand(new ExperienceModifyCommand(player, true, ID.AddSetModifier.set, levels));
            }
            /// <summary>
            /// Outputs the amount of levels the selected player has
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            public void LevelsGet(BaseSelector player)
            {
                player.LimitSelector();
                ForFunction.AddCommand(new ExperienceGetCommand(player, true));
            }
            /// <summary>
            /// Adds the specified amount of points to the selected players
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="points">The amount of points to add. If this is negative points will be removed</param>
            public void PointsAdd(BaseSelector player, int points)
            {
                ForFunction.AddCommand(new ExperienceModifyCommand(player, false, ID.AddSetModifier.add, points));
            }
            /// <summary>
            /// Sets the selected players' points to the specified amount
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="points">The amount to set the points to</param>
            public void PointsSet(BaseSelector player, int points)
            {
                ForFunction.AddCommand(new ExperienceModifyCommand(player, false, ID.AddSetModifier.set, points));
            }
            /// <summary>
            /// Outputs the amount of points the selected player has
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            public void PointsGet(BaseSelector player)
            {
                player.LimitSelector();
                ForFunction.AddCommand(new ExperienceGetCommand(player, false));
            }
        }
        internal PlayerCommands(Function function) : base(function)
        {
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
        /// <param name="player">the <see cref="BaseSelector"/> to use</param>
        /// <param name="mode">the gamemode to change to</param>
        public void Gamemode(BaseSelector player, ID.Gamemode mode)
        {
            ForFunction.AddCommand(new GamemodeCommand(player, mode));
        }

        /// <summary>
        /// Forces a player to spectate an entity. The player has to be in spectator mode. Leave both params empty to make the executing player stop spectating
        /// </summary>
        /// <param name="spectate">The entity to spectate</param>
        /// <param name="spectator">The spectating player</param>
        public void Spectate(BaseSelector spectate, BaseSelector spectator)
        {
            if (spectate is null)
            {
                ForFunction.AddCommand(new SpectateStopCommand());
            }
            else
            {
                spectate.LimitSelector();
                spectator.LimitSelector();
                ForFunction.AddCommand(new SpectateCommand(spectate, spectator));
            }
        }

        /// <summary>
        /// Changes the executing player's score in the given <see cref="Objective"/>
        /// </summary>
        /// <param name="scoreObject">the <see cref="Objective"/> to change in</param>
        /// <param name="number">The number to add/set it to</param>
        /// <param name="set">If the score should be set to the given number. If false it will be added instead</param>
        public void Trigger(Objective scoreObject, int number, bool set = true)
        {
            ForFunction.AddCommand(new TriggerCommand(scoreObject, set, number));
        }

        /// <summary>
        /// Whispers the specified message to the selected players
        /// </summary>
        /// <param name="player">the <see cref="BaseSelector"/> to use</param>
        /// <param name="message">The message to tell the player</param>
        public void Tell(BaseSelector player, string message)
        {
            ForFunction.AddCommand(new MsgCommand(player, message));
        }

        /// <summary>
        /// Changes the selected players' spawnpoint to the given location
        /// </summary>
        /// <param name="player">the <see cref="BaseSelector"/> to use</param>
        /// <param name="spawn">The new spawnpoint location</param>
        public void Spawnpoint(BaseSelector player, Vector? spawn = null)
        {
            ForFunction.AddCommand(new SpawnPointCommand(spawn ?? new Coords(), player));
        }

        /// <summary>
        /// Enables a trigger for the selected players
        /// </summary>
        /// <param name="player">the <see cref="BaseSelector"/> to use</param>
        /// <param name="objective">The trigger (<see cref="Objective"/>) to enable</param>
        public void EnableTrigger(BaseSelector player, Objective objective)
        {
            ForFunction.AddCommand(new ScoreboardEnableTriggerCommand(player, objective));
        }

        /// <summary>
        /// Tells the selected players a message in chat
        /// </summary>
        /// <param name="player">the <see cref="BaseSelector"/> to use</param>
        /// <param name="message">The message to tell the players</param>
        public void Tellraw(BaseSelector player, BaseJsonText message)
        {
            ForFunction.AddCommand(new TellrawCommand(player, message));
        }

        /// <summary>
        /// Shows a message in the selected players actionbar
        /// </summary>
        /// <param name="player">the <see cref="BaseSelector"/> to use</param>
        /// <param name="message">The message to show</param>
        public void Actionbar(BaseSelector player, BaseJsonText message)
        {
            ForFunction.AddCommand(new TitleActionbarCommand(player, message));
        }

        /// <summary>
        /// All commands for particles
        /// </summary>
        public ClassParticle Particle;
        /// <summary>
        /// All commands for particles
        /// </summary>
        public class ClassParticle : CommandList
        {
            internal ClassParticle(Function function) : base(function)
            {
                
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
            public void Normal(ID.Particle particle, Vector displayCoords, Vector size, double speed, int count, bool force = false, BaseSelector? player = null)
            {
                ForFunction.AddCommand(new ParticleNormalCommand(particle, displayCoords, size, speed, count, force, player));
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
            public void ColoredDust(RGBColor color, double particleSize, Vector displayCoords, Vector size, double speed, int count, bool force = false, BaseSelector? player = null)
            {
                ForFunction.AddCommand(new ParticleColoredDustCommand(color, particleSize, displayCoords, size, speed, count, force, player));
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
            public void Block(Block block, Vector displayCoords, Vector size, double speed, int count, bool dust = false, bool force = false, BaseSelector? player = null)
            {
                ForFunction.AddCommand(new ParticleBlockCommand(block, displayCoords, size, speed, count, dust, force, player));
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
            public void Item(Item item, Vector displayCoords, Vector size, double speed, int count, bool force = false, BaseSelector? player = null)
            {
                ForFunction.AddCommand(new ParticleItemCommand(item, displayCoords, size, speed, count, force, player));
            }
        }

        /// <summary>
        /// All commands for items
        /// </summary>
        public ClassItem Item;
        /// <summary>
        /// All commands for items
        /// </summary>
        public class ClassItem : CommandList
        {
            internal ClassItem(Function function) : base(function)
            {
                
            }

            /// <summary>
            /// Gives an item to the selected players
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="giveItem">The <see cref="Item"/> to give to the players</param>
            public void GiveItem(BaseSelector player, Item giveItem)
            {
                if (giveItem.Slot is null)
                {
                    ForFunction.AddCommand(new GiveCommand(player, giveItem, giveItem.Count ?? 1));
                }
                else
                {
                    ForFunction.AddCommand(new ReplaceitemEntityCommand(player, new Slots.InventorySlot((int)giveItem.Slot), giveItem, giveItem.Count ?? 1));
                }
            }

            /// <summary>
            /// Gives the loot from a loottable to the selected players
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="loot">the <see cref="LootTable"/> to give the player</param>
            public void GiveItem(BaseSelector player, ILootTable loot)
            {
                ForFunction.AddCommand(new LootCommand(new LootTargets.GiveTarget(player), new LootSources.LoottableSource(loot)));
            }

            /// <summary>
            /// Gives the loot which the selected entity would drop if killed to the selected players
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="kill">the selector selecting the entity whose loot should be dropped</param>
            public void GiveItem(BaseSelector player, BaseSelector kill)
            {
                kill.LimitSelector();
                ForFunction.AddCommand(new LootCommand(new LootTargets.GiveTarget(player), new LootSources.KillSource(kill)));
            }

            /// <summary>
            /// Gives the loot which the block at the given coords would drop of broken to the selected players
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="breakBlock">the coords of the block</param>
            /// <param name="breakWith">the item used to break the block</param>
            public void GiveItem(BaseSelector player, Vector breakBlock, Item? breakWith)
            {
                if (breakWith is null)
                {
                    ForFunction.AddCommand(new LootCommand(new LootTargets.GiveTarget(player), new LootSources.MineHandSource(breakBlock, true)));
                }
                else
                {
                    ForFunction.AddCommand(new LootCommand(new LootTargets.GiveTarget(player), new LootSources.MineItemSource(breakBlock, breakWith)));
                }
            }

            /// <summary>
            /// Puts an item into the selected players' enderchests
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="giveItem">The item to insert into the enderchest. <see cref="Item.Slot"/> choses the slot.</param>
            public void GiveEnderChest(BaseSelector player, Item giveItem)
            {
                ForFunction.AddCommand(new ReplaceitemEntityCommand(player, new Slots.EnderChestSlot(giveItem.Slot ?? 0), giveItem, giveItem.Count ?? 1));
            }
            /// <summary>
            /// Puts an item into the selected players' hotbars
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="giveItem">The item to insert into the hotbar. <see cref="Item.Slot"/> choses the slot.</param>
            public void GiveHotbar(BaseSelector player, Item giveItem)
            {
                ForFunction.AddCommand(new ReplaceitemEntityCommand(player, new Slots.HotbarSlot(giveItem.Slot ?? 0), giveItem, giveItem.Count ?? 1));
            }

            /// <summary>
            /// Puts the item from the loot table into the players hotbar
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="loot">the <see cref="LootTable"/> to give the player</param>
            /// <param name="slot">The hotbar slot to put the item in</param>
            public void GiveHotbar(BaseSelector player, ILootTable loot, int slot)
            {
                ForFunction.AddCommand(new LootCommand(new LootTargets.EntityTarget(player, new Slots.HotbarSlot(slot)), new LootSources.LoottableSource(loot)));
            }

            /// <summary>
            /// Puts the item from the loot table into the players hotbar
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="breakBlock">the coords of the block</param>
            /// <param name="breakWith">the item used to break the block</param>
            /// <param name="slot">The hotbar slot to put the item in</param>
            public void GiveHotbar(BaseSelector player, Vector breakBlock, Item? breakWith, int slot)
            {
                if (breakWith is null)
                {
                    ForFunction.AddCommand(new LootCommand(new LootTargets.EntityTarget(player, new Slots.HotbarSlot(slot)), new LootSources.MineHandSource(breakBlock, true)));
                }
                else
                {
                    ForFunction.AddCommand(new LootCommand(new LootTargets.EntityTarget(player, new Slots.HotbarSlot(slot)), new LootSources.MineItemSource(breakBlock, breakWith)));
                }
            }

            /// <summary>
            /// Inserts an item into the selected players' selected slot
            /// </summary>
            /// <param name="selector">the <see cref="BaseSelector"/> to use</param>
            /// <param name="giveItem">The item to insert</param>
            /// <param name="offHand">If it should insert into the offhand instead</param>
            public void GiveWeapon(BaseSelector selector, Item giveItem, bool offHand = false)
            {
                ForFunction.AddCommand(new ReplaceitemEntityCommand(selector, new Slots.WeaponSlot(!offHand), giveItem, giveItem.Count ?? 1));
            }
            /// <summary>
            /// Clears an item from the selected players' inventories
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="item">The item to clear</param>
            /// <param name="amount">The maximum amount of the item to clear. null clears all</param>
            public void Clear(BaseSelector player, Item? item = null, int? amount = null)
            {
                ForFunction.AddCommand(new ClearCommand(player, item, amount));
            }
        }

        /// <summary>
        /// All commands for sounds
        /// </summary>
        public ClassSound Sound;
        /// <summary>
        /// All commands for sounds
        /// </summary>
        public class ClassSound : CommandList
        {
            internal ClassSound(Function function) : base(function)
            {
                
            }

            /// <summary>
            /// Plays a sound for the selected players
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="sound">the sound to play</param>
            /// <param name="source">the source to play the sound as</param>
            /// <param name="location">the location to play the sound at</param>
            /// <param name="volume">the maximum volume of the sound</param>
            /// <param name="speed">the speed of the sound (0-2)</param>
            /// <param name="minValue">the minimum volume of the sound (0-2)</param>
            public void Play(BaseSelector player, string sound, ID.SoundSource source, Vector location, double volume = 1, double speed = 1, double minValue = 0)
            {
                ForFunction.AddCommand(new PlaySoundCommand(sound, source, player, location, volume, speed, minValue));
            }

            /// <summary>
            /// Stops sounds for the selected players
            /// (If no source and sound is specified it will stop all sounds)
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="source">the source to stop sounds at. Null will stop the sound from any source</param>
            /// <param name="sound">the sound to stop. Null will stop any sound in the given source</param>
            public void Stop(BaseSelector player, ID.SoundSource? source = null, string? sound = null)
            {
                ForFunction.AddCommand(new StopSoundCommand(player, sound, source));
            }
        }

        /// <summary>
        /// All commands for titles
        /// </summary>
        public ClassTitle Title;
        /// <summary>
        /// All commands for titles
        /// </summary>
        public class ClassTitle : CommandList
        {
            internal ClassTitle(Function function): base(function)
            {
                
            }

            /// <summary>
            /// displays a title for the selected players
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="message">The message to show the players</param>
            public void Title(BaseSelector player, BaseJsonText message)
            {
                ForFunction.AddCommand(new TitleCommand(player, message));
            }

            /// <summary>
            /// displays a subtitle for the selected players
            /// Note: the subtitle is first shown when the title command is ran
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="message">The message to show the players</param>
            public void SubTitle(BaseSelector player, BaseJsonText message)
            {
                ForFunction.AddCommand(new TitleSubtitleCommand(player, message));
            }

            /// <summary>
            /// choses how long the titles should be shown for the selected players
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="startFade">The amount of ticks it takes for the title to fade in</param>
            /// <param name="stay">The amount of ticks the title stays on screen</param>
            /// <param name="endFade">The amount of ticks it takes for the title to fade out</param>
            public void Time(BaseSelector player, NoneNegativeTime<int> startFade, NoneNegativeTime<int> stay, NoneNegativeTime<int> endFade)
            {
                ForFunction.AddCommand(new TitleTimesCommand(player, startFade, stay, endFade));
            }

            /// <summary>
            /// clears the shown title on the selected players' screens
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            public void Clear(BaseSelector player)
            {
                ForFunction.AddCommand(new TitleClearCommand(player));
            }

            /// <summary>
            /// resets all the title values for the selected players' screens
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            public void Reset(BaseSelector player)
            {
                ForFunction.AddCommand(new TitleResetCommand(player));
            }

            /// <summary>
            /// Displays a whole title for the selected players
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="topMessage">The main title message</param>
            /// <param name="bottomMessage">the bottom part of the title message</param>
            /// <param name="startFade">The amount of ticks it takes for the title to fade in</param>
            /// <param name="stay">The amount of ticks the title stays on screen</param>
            /// <param name="endFade">The amount of ticks it takes for the title to fade out</param>
            public void FullTitle(BaseSelector player, BaseJsonText topMessage, BaseJsonText bottomMessage, NoneNegativeTime<int> startFade, NoneNegativeTime<int> stay, NoneNegativeTime<int> endFade)
            {
                ForFunction.Custom.GroupCommands(f => 
                {
                    ForFunction.AddCommand(new TitleTimesCommand(player, startFade, stay, endFade));
                    if (!(bottomMessage is null))
                    {
                        ForFunction.AddCommand(new TitleSubtitleCommand(player, bottomMessage));
                    }
                    if (topMessage is null)
                    {
                        ForFunction.AddCommand(new TitleCommand(player, new JsonText.Text("")));
                    }
                    else
                    {
                        ForFunction.AddCommand(new TitleCommand(player, topMessage));
                    }
                });
            }
        }

        /// <summary>
        /// All commands for recipes
        /// </summary>
        public ClassRecipe Recipe;
        /// <summary>
        /// All commands for recipes
        /// </summary>
        public class ClassRecipe : CommandList
        {
            internal ClassRecipe(Function function) : base(function)
            {
                
            }

            /// <summary>
            /// Give a recipe to the selected players
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="giveRecipe">The recipe to give</param>
            public void Give(BaseSelector player, IRecipe giveRecipe)
            {
                ForFunction.AddCommand(new RecipeCommand(giveRecipe, player, true));
            }
            /// <summary>
            /// Gives all recipes to the selected players
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            public void GiveAll(BaseSelector player)
            {
                ForFunction.AddCommand(new RecipeAllCommand(player, true));
            }
            /// <summary>
            /// Removes a recipe from the selected players
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="giveRecipe">the recipe to remove</param>
            public void Remove(BaseSelector player, IRecipe giveRecipe)
            {
                ForFunction.AddCommand(new RecipeCommand(giveRecipe, player, false));
            }
            /// <summary>
            /// removes all recipes from the selected players
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            public void RemoveAll(BaseSelector player)
            {
                ForFunction.AddCommand(new RecipeAllCommand(player, false));
            }
        }

        /// <summary>
        /// All commands for advancements
        /// </summary>
        public ClassAdvancement Advancement;
        /// <summary>
        /// All commands for advancements
        /// </summary>
        public class ClassAdvancement : CommandList
        {
            internal ClassAdvancement(Function function) : base(function)
            {
               
            }
            /// <summary>
            /// grants/evokes all advancements for the selected players
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="revoke">if the advancement should be revoked instead of granted</param>
            public void Everything(BaseSelector player, bool revoke = false)
            {
                ForFunction.AddCommand(new AdvancementAllCommand(player, !revoke));
            }
            /// <summary>
            /// Grants/revokes all advancements up to the specified advancement for the selected players
            /// (it also grants/revokes the specified advancement)
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="advancement">the advancement to grant/revoke up to</param>
            /// <param name="revoke">if the advancement should be revoked instead of granted</param>
            public void Until(BaseSelector player, IAdvancement advancement, bool revoke = false)
            {
                ForFunction.AddCommand(new AdvancementSomeCommand(player, advancement, ID.RelativeAdvancement.until, !revoke));
            }
            /// <summary>
            /// grants/revokes all advancements after the specified advancement for the selected players
            /// (it also grants/revokes the specified advancement)
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="advancement">the advancement to grant/revoke from</param>
            /// <param name="revoke">if the advancement should be revoked instead of granted</param>
            public void From(BaseSelector player, IAdvancement advancement, bool revoke = false)
            {
                ForFunction.AddCommand(new AdvancementSomeCommand(player, advancement, ID.RelativeAdvancement.from, !revoke));
            }

            /// <summary>
            /// grants/revokes all advancements in the same branch as the specified advancement for the selected players
            /// (it also grants/revokes the specified advancement)
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="advancement">an advancement in the branch to grant/revoke</param>
            /// <param name="revoke">if the advancement should be revoked instead of granted</param>
            public void Branch(BaseSelector player, IAdvancement advancement, bool revoke = false)
            {
                ForFunction.AddCommand(new AdvancementSomeCommand(player, advancement, ID.RelativeAdvancement.through, !revoke));
            }

            /// <summary>
            /// grants/revokes the specified the advancement for the selected players
            /// (it also grants/revokes the specified advancement)
            /// </summary>
            /// <param name="player">the <see cref="BaseSelector"/> to use</param>
            /// <param name="advancement">the advancement to grant/revoke</param>
            /// <param name="revoke">if the advancement should be revoked instead of granted</param>
            /// <param name="trigger">the trigger in the advancement to revoke/grant. Null means the advancement itself will be granted/revoked</param>
            public void Only(BaseSelector player, IAdvancement advancement, bool revoke = false, AdvancementObjects.ITrigger? trigger = null)
            {
                ForFunction.AddCommand(new AdvancementSingleCommand(player, advancement, trigger, !revoke));
            }
        }
    }
}

