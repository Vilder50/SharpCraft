namespace SharpCraft.FunctionWriters
{
    /// <summary>
    /// All the player commands
    /// </summary>
    public class PlayerCommands
    {
        readonly Function.FunctionWriter Writer;
        /// <summary>
        /// All commands for levels and xp
        /// </summary>
        public ClassXP XP;
        /// <summary>
        /// All commands for levels and xp
        /// </summary>
        public class ClassXP
        {
            readonly Function.FunctionWriter Writer;
            internal ClassXP(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
            }
            /// <summary>
            /// Adds the specified amount of levels to the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="levels">The amount of levels to add. If this is negative levels will be removed</param>
            public void LevelsAdd(Selector player, int levels)
            {
                Writer.Add("experience add " + player + " " + levels + " levels");
                Writer.NewLine();
            }
            /// <summary>
            /// Sets the selected players' levels to the specified amount
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="levels">The amount to set the levels to</param>
            public void LevelsSet(Selector player, int levels)
            {
                Writer.Add("experience set " + player + " " + levels + " levels");
                Writer.NewLine();
            }
            /// <summary>
            /// Outputs the amount of levels the selected player has
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            public void LevelsGet(Selector player)
            {
                player.Limited();
                Writer.Add("experience quary " + player + " levels");
                Writer.NewLine();
            }
            /// <summary>
            /// Adds the specified amount of points to the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="points">The amount of points to add. If this is negative points will be removed</param>
            public void PointsAdd(Selector player, int points)
            {
                Writer.Add("experience add " + player + " " + points + " points");
                Writer.NewLine();
            }
            /// <summary>
            /// Sets the selected players' points to the specified amount
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="points">The amount to set the points to</param>
            public void PointsSet(Selector player, int points)
            {
                Writer.Add("experience set " + player + " " + points + " points");
                Writer.NewLine();
            }
            /// <summary>
            /// Outputs the amount of points the selected player has
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            public void PointsGet(Selector player)
            {
                player.Limited();
                Writer.Add("experience quary " + player + " points");
                Writer.NewLine();
            }
        }
        internal PlayerCommands(Function.FunctionWriter CommandsList)
        {
            Writer = CommandsList;
            XP = new ClassXP(CommandsList);
            Particle = new ClassParticle(CommandsList);
            Item = new ClassItem(CommandsList);
            Sound = new ClassSound(CommandsList);
            Title = new ClassTitle(CommandsList);
            Recipe = new ClassRecipe(CommandsList);
            Advancement = new ClassAdvancement(CommandsList);
        }

        /// <summary>
        /// Changes the selected players gamemode
        /// </summary>
        /// <param name="player">the <see cref="Selector"/> to use</param>
        /// <param name="mode">the gamemode to change to</param>
        public void Gamemode(Selector player, ID.Gamemode mode)
        {
            Writer.Add("gamemode " + mode + " " + player);
            Writer.NewLine();
        }

        /// <summary>
        /// Changes the executing player's score in the given <see cref="ScoreObject"/>
        /// </summary>
        /// <param name="scoreObject">the <see cref="ScoreObject"/> to change in</param>
        /// <param name="number">The number to add/set it to</param>
        /// <param name="set">If the score should be set to the given number. If false it will be added instead</param>
        public void Trigger(ScoreObject scoreObject, int number, bool set = true)
        {
            Writer.Add("trigger " + scoreObject + " " + (set ? "set" : "add") + " " + number);
        }

        /// <summary>
        /// Whispers the specified message to the selected players
        /// </summary>
        /// <param name="player">the <see cref="Selector"/> to use</param>
        /// <param name="message">The message to tell the player</param>
        public void Tell(Selector player, string message)
        {
            Writer.Add("msg " + player + " " + message);
            Writer.NewLine();
        }

        /// <summary>
        /// Changes the selected players' spawnpoint to the given location
        /// </summary>
        /// <param name="player">the <see cref="Selector"/> to use</param>
        /// <param name="spawn">The new spawnpoint location</param>
        public void Spawnpoint(Selector player, Coords spawn = null)
        {
            Writer.Add("spawnpoint " + player);
            if (spawn != null) { Writer.Add(" " + spawn); }
            Writer.NewLine();
        }

        /// <summary>
        /// Enables a trigger for the selected players
        /// </summary>
        /// <param name="player">the <see cref="Selector"/> to use</param>
        /// <param name="objective">The trigger (<see cref="ScoreObject"/>) to enable</param>
        public void EnableTrigger(Selector player, ScoreObject objective)
        {
            Writer.Add("scoreboard players enable " + player + " " + objective);
            Writer.NewLine();
        }

        /// <summary>
        /// Tells the selected players a message in chat
        /// </summary>
        /// <param name="player">the <see cref="Selector"/> to use</param>
        /// <param name="message">The message to tell the players</param>
        public void Tellraw(Selector player, JSON[] message)
        {
            Writer.Add("tellraw " + player + " " + message.GetString());
            Writer.NewLine();
        }

        /// <summary>
        /// Shows a message in the selected players actionbar
        /// </summary>
        /// <param name="player">the <see cref="Selector"/> to use</param>
        /// <param name="message">The message to show</param>
        public void Actionbar(Selector player, JSON[] message)
        {
            Writer.Add("title " + player + " actionbar " + message.GetString());
            Writer.NewLine();
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
            readonly Function.FunctionWriter Writer;
            internal ClassParticle(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
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
                Writer.Add("particle " + particle + " " + displayCoords + " " + size.X.ToString().Replace(",", ".") + " " + size.Y.ToString().Replace(",", ".") + " " + size.Z.ToString().Replace(",", ".") + " " + speed.ToString().Replace(",", ".") + " " + count);
                if (force) { Writer.Add(" force"); } else { Writer.Add(" normal"); }
                if (player != null) { Writer.Add(" " + player); }
                Writer.NewLine();
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
                Writer.Add("particle dust " + (decimal.Divide(color.Red, 255).ToString().Replace(",", ".")) + " " + (decimal.Divide(color.Green, 255).ToString().Replace(",", ".")) + " " + (decimal.Divide(color.Blue, 255).ToString().Replace(",", ".")) + " " + particleSize.ToString().Replace(",", ".") + " " + displayCoords + " " + size.X.ToString().Replace(",", ".") + " " + size.Y.ToString().Replace(",", ".") + " " + size.Z.ToString().Replace(",", ".") + " " + speed.ToString().Replace(",", ".") + " " + count);
                if (force) { Writer.Add(" force"); } else { Writer.Add(" normal"); }
                if (player != null) { Writer.Add(" " + player); }
                Writer.NewLine();
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
                if (dust)
                {
                    Writer.Add("particle falling_dust ");
                }
                else
                {
                    Writer.Add("particle block ");
                }
                Writer.Add(block + " " + displayCoords + " " + size.X.ToString().Replace(",", ".") + " " + size.Y.ToString().Replace(",", ".") + " " + size.Z.ToString().Replace(",", ".") + " " + speed.ToString().Replace(",", ".") + " " + count);
                if (force) { Writer.Add(" force"); } else { Writer.Add(" normal"); }
                if (player != null) { Writer.Add(" " + player); }
                Writer.NewLine();
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
                Writer.Add("particle item " + item.IDDataString + " " + displayCoords + " " + size.X.ToString().Replace(",", ".") + " " + size.Y.ToString().Replace(",", ".") + " " + size.Z.ToString().Replace(",", ".") + " " + speed.ToString().Replace(",", ".") + " " + count);
                if (force) { Writer.Add(" force"); } else { Writer.Add(" normal"); }
                if (player != null) { Writer.Add(" " + player); }
                Writer.NewLine();
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
            readonly Function.FunctionWriter Writer;
            internal ClassItem(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
            }

            /// <summary>
            /// Gives an item to the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="giveItem">The <see cref="Item"/> to give to the players</param>
            public void GiveItem(Selector player, Item giveItem)
            {
                if (giveItem.Slot != null)
                {
                    if (giveItem.Slot > 26) { giveItem.Slot = 26; }
                    Writer.Add("replaceitem entity " + player + " inventory." + giveItem.Slot + " " + giveItem.IDDataString + " " + (giveItem.Count ?? 1));
                    Writer.NewLine();
                }
                else
                {
                    Writer.Add("give " + player + " " + giveItem.IDDataString);
                    Writer.NewLine();
                }
            }

            /// <summary>
            /// Gives the loot from a loottable to the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="loot">the <see cref="Loottable"/> to give the player</param>
            public void GiveItem(Selector player, Loottable loot)
            {
                Writer.Add($"loot give {player} loot {loot}");
                Writer.NewLine();
            }

            /// <summary>
            /// Gives the loot which the selected entity would drop if killed to the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="kill">the selector selecting the entity whose loot should be dropped</param>
            public void GiveItem(Selector player, Selector kill)
            {
                kill.Limited();
                Writer.Add($"loot give {player} kill {kill}");
                Writer.NewLine();
            }

            /// <summary>
            /// Gives the loot which the block at the given coords would drop of broken to the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="breakBlock">the coords of the block</param>
            /// <param name="breakWith">the item used to break the block</param>
            public void GiveItem(Selector player, Coords breakBlock, Item breakWith)
            {
                Writer.Add($"loot give {player} mine {breakBlock}");
                if (breakWith != null)
                {
                    Writer.Add(" " + breakWith.IDDataString);
                }
                Writer.NewLine();
            }

            /// <summary>
            /// Puts an item into the selected players' enderchests
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="giveItem">The item to insert into the enderchest. <see cref="Item.Slot"/> choses the slot.</param>
            public void GiveEnderChest(Selector player, Item giveItem)
            {
                if (giveItem.Slot == null || giveItem.Slot > 26 || giveItem.Slot < 0)
                {
                    giveItem.Slot = 0;
                }
                Writer.Add("replaceitem entity " + player + " enderchest." + giveItem.Slot + " " + giveItem.IDDataString + " " + (giveItem.Count ?? 1));
                Writer.NewLine();
            }
            /// <summary>
            /// Puts an item into the selected players' hotbars
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="giveItem">The item to insert into the hotbar. <see cref="Item.Slot"/> choses the slot.</param>
            public void GiveHotbar(Selector player, Item giveItem)
            {
                if (giveItem.Slot == null || giveItem.Slot > 8 || giveItem.Slot < 0)
                {
                    giveItem.Slot = 0;
                }
                Writer.Add("replaceitem entity " + player + " hotbar." + giveItem.Slot + " " + giveItem.IDDataString + " " + (giveItem.Count ?? 1));
                Writer.NewLine();
            }

            /// <summary>
            /// Puts the item from the loot table into the players hotbar
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="loot">the <see cref="Loottable"/> to give the player</param>
            /// <param name="slot">The hotbar slot to put the item in</param>
            public void GiveHotbar(Selector player, Loottable loot, int slot)
            {
                Writer.Add($"loot replace entity {player} hotbar.{slot} loot {loot}");
                Writer.NewLine();
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
                Writer.Add($"loot replace entity {player} hotbar.{slot} mine {breakBlock}");
                if (breakWith != null)
                {
                    Writer.Add(" " + breakWith.IDDataString);
                }
                Writer.NewLine();
            }

            /// <summary>
            /// Inserts an item into the selected players' selected slot
            /// </summary>
            /// <param name="selector">the <see cref="Selector"/> to use</param>
            /// <param name="giveItem">The item to insert</param>
            /// <param name="offHand">If it should insert into the offhand instead</param>
            public void GiveWeapon(Selector selector, Item giveItem, bool offHand = false)
            {
                if (offHand)
                {
                    Writer.Add("replaceitem entity " + selector + " weapon.offhand" + " " + giveItem.IDDataString + " " + (giveItem.Count ?? 1));
                }
                else
                {
                    Writer.Add("replaceitem entity " + selector + " weapon" + " " + giveItem.IDDataString + " " + (giveItem.Count ?? 1));
                }
                Writer.NewLine();
            }
            /// <summary>
            /// Clears an item from the selected players' inventories
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="item">The item to clear</param>
            /// <param name="amount">The maximum amount of the item to clear. null clears all</param>
            public void Clear(Selector player, Item item = null, int? amount = null)
            {
                Writer.Add("clear " + player);
                if (item != null) { Writer.Add(" " + item.IDDataString); }
                if (amount != null) { Writer.Add(" " + amount); }
                Writer.NewLine();
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
            readonly Function.FunctionWriter Writer;
            internal ClassSound(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
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
            public void Play(Selector player, string sound, ID.SoundSource source, Coords location = null, double volume = 1, double speed = 1, double minValue = 0)
            {
                Writer.Add("playsound " + sound + " " + source + " " + player);
                if (location != null) { Writer.Add(" " + location); }
                if (location == null && (volume != 1 || speed != 1 || minValue != 0)) { Writer.Add(" ~ ~ ~"); }
                if (volume != 1 || speed != 1 || minValue != 0) { Writer.Add(" " + volume.ToMinecraftDouble()); }
                if (speed != 1 || minValue != 0) { Writer.Add(" " + speed.ToMinecraftDouble()); }
                if (minValue != 0) { Writer.Add(" " + minValue.ToMinecraftDouble()); }
                Writer.NewLine();
            }

            /// <summary>
            /// Stops sounds for the selected players
            /// (If no source and sound is specified it will stop all sounds)
            /// </summary>
            /// <param name="Player">the <see cref="Selector"/> to use</param>
            /// <param name="Source">the source to stop sounds at. Null will stop the sound from any source</param>
            /// <param name="sound">the sound to stop. Null will stop any sound in the given source</param>
            public void Stop(Selector Player, ID.SoundSource? Source = null, string sound = null)
            {
                Writer.Add("stopsound " + Player);
                if (Source != null) { Writer.Add(" " + Source); }
                if (Source == null && sound != null) { Writer.Add(" *"); }
                if (sound != null) { Writer.Add(" " + sound); }
                Writer.NewLine();
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
            readonly Function.FunctionWriter Writer;
            internal ClassTitle(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
            }

            /// <summary>
            /// displays a title for the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="message">The message to show the players</param>
            public void Title(Selector player, JSON[] message)
            {
                Writer.Add("title " + player + " title " + message.GetString());
                Writer.NewLine();
            }

            /// <summary>
            /// displays a subtitle for the selected players
            /// Note: the subtitle is first shown when the title command is ran
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="message">The message to show the players</param>
            public void SubTitle(Selector player, JSON[] message)
            {
                Writer.Add("title " + player + " subtitle " + message.GetString());
                Writer.NewLine();
            }

            /// <summary>
            /// choses how long the titles should be shown for the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="startFade">The amount of ticks it takes for the title to fade in</param>
            /// <param name="stay">The amount of ticks the title stays on screen</param>
            /// <param name="endFade">The amount of ticks it takes for the title to fade out</param>
            public void Time(Selector player, int startFade, int stay, int endFade)
            {
                Writer.Add("title " + player + " times " + startFade + " " + stay + " " + endFade);
                Writer.NewLine();
            }

            /// <summary>
            /// clears the shown title on the selected players' screens
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            public void Clear(Selector player)
            {
                Writer.Add("title " + player + " clear");
                Writer.NewLine();
            }

            /// <summary>
            /// resets all the title values for the selected players' screens
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            public void Reset(Selector player)
            {
                Writer.Add("title " + player + " reset");
                Writer.NewLine();
            }

            /// <summary>
            /// Displays a whole title for the selected players
            /// </summary>
            /// <param name="Player">the <see cref="Selector"/> to use</param>
            /// <param name="TopMessage">The main title message</param>
            /// <param name="BottomMessage">the bottom part of the title message</param>
            /// <param name="StartFade">The amount of ticks it takes for the title to fade in</param>
            /// <param name="Stay">The amount of ticks the title stays on screen</param>
            /// <param name="EndFade">The amount of ticks it takes for the title to fade out</param>
            public void FullTitle(Selector Player, JSON[] TopMessage, JSON[] BottomMessage, int StartFade, int Stay, int EndFade)
            {
                Writer.CopyState();
                Writer.Add("title " + Player + " times " + StartFade + " " + Stay + " " + EndFade);
                Writer.NewLine();

                Writer.PasteState();
                if (BottomMessage != null)
                {
                    Writer.Add("title " + Player + " subtitle " + BottomMessage.GetString());
                    Writer.NewLine();
                }

                Writer.PasteState();
                if (TopMessage != null)
                {
                    Writer.Add("title " + Player + " title " + TopMessage.GetString());
                    Writer.NewLine();
                }
                else
                {
                    Writer.Add("title " + Player + " title " + new JSON[] { new JSON("") }.GetString());
                    Writer.NewLine();
                }

                Writer.NewLine();
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
            readonly Function.FunctionWriter Writer;
            internal ClassRecipe(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
            }

            /// <summary>
            /// Give a recipe to the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="giveRecipe">The <see cref="Recipe"/> to give</param>
            public void Give(Selector player, Recipe giveRecipe)
            {
                Writer.Add("recipe give " + player + " " + giveRecipe);
                Writer.NewLine();
            }
            /// <summary>
            /// Gives all recipes to the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            public void GiveAll(Selector player)
            {
                Writer.Add("recipe give " + player + " *");
                Writer.NewLine();
            }
            /// <summary>
            /// Removes a recipe from the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="giveRecipe">the <see cref="Recipe"/> to remove</param>
            public void Remove(Selector player, Recipe giveRecipe)
            {
                Writer.Add("recipe take " + player + " " + giveRecipe);
                Writer.NewLine();
            }
            /// <summary>
            /// removes all recipes from the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            public void RemoveAll(Selector player)
            {
                Writer.Add("recipe take " + player + " *");
                Writer.NewLine();
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
            readonly Function.FunctionWriter Writer;
            internal ClassAdvancement(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
            }
            /// <summary>
            /// grants/evokes all advancements for the selected players
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="revoke">if the advancement should be revoked instead of granted</param>
            public void Everything(Selector player, bool revoke = false)
            {
                Writer.Add("advancement " + GrantRevoke(revoke) + " " + player + " everything");
                Writer.NewLine();
            }
            /// <summary>
            /// Grants/revokes all advancements up to the specified advancement for the selected players
            /// (it also grants/revokes the specified advancement)
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="advancement">the advancement to grant/revoke up to</param>
            /// <param name="revoke">if the advancement should be revoked instead of granted</param>
            public void Untill(Selector player, Advancement advancement, bool revoke = false)
            {
                Writer.Add("advancement " + GrantRevoke(revoke) + " " + player + " until " + advancement);
                Writer.NewLine();
            }
            /// <summary>
            /// grants/revokes all advancements after the specified advancement for the selected players
            /// (it also grants/revokes the specified advancement)
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="advancement">the advancement to grant/revoke from</param>
            /// <param name="revoke">if the advancement should be revoked instead of granted</param>
            public void From(Selector player, Advancement advancement, bool revoke = false)
            {
                Writer.Add("advancement " + GrantRevoke(revoke) + " " + player + " from " + advancement);
                Writer.NewLine();
            }

            /// <summary>
            /// grants/revokes all advancements in the same branch as the specified advancement for the selected players
            /// (it also grants/revokes the specified advancement)
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="advancement">an advancement in the branch to grant/revoke</param>
            /// <param name="revoke">if the advancement should be revoked instead of granted</param>
            public void Branch(Selector player, Advancement advancement, bool revoke = false)
            {
                Writer.Add("advancement " + GrantRevoke(revoke) + " " + player + " through " + advancement);
                Writer.NewLine();
            }

            /// <summary>
            /// grants/revokes the specified the advancement for the selected players
            /// (it also grants/revokes the specified advancement)
            /// </summary>
            /// <param name="player">the <see cref="Selector"/> to use</param>
            /// <param name="advancement">the advancement to grant/revoke</param>
            /// <param name="revoke">if the advancement should be revoked instead of granted</param>
            /// <param name="trigger">the trigger in the advancement to revoke/grant. Null means the advancement itself will be granted/revoked</param>
            public void Only(Selector player, Advancement advancement, bool revoke = false, Advancement.Trigger trigger = null)
            {
                Writer.Add("advancement " + GrantRevoke(revoke) + " " + player + " only " + advancement);
                if (trigger != null) { Writer.Add(" " + trigger); }
                Writer.NewLine();
            }

            private string GrantRevoke(bool revoke)
            {
                return revoke ? "revoke" : "grant";
            }
        }
    }
}

