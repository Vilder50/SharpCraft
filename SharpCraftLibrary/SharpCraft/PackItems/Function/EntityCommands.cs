namespace SharpCraft.FunctionWriters
{
    /// <summary>
    /// All the entity commands
    /// </summary>
    public class EntityCommands
    {
        readonly Function.FunctionWriter Writer;

        internal EntityCommands(Function.FunctionWriter CommandsList)
        {
            Writer = CommandsList;
            Data = new ClassData(CommandsList);
            Tag = new ClassTag(CommandsList);
            Score = new ClassScore(CommandsList);
            Effect = new ClassEffect(CommandsList);
            Item = new ClassItem(CommandsList);
        }

        /// <summary>
        /// Adds the specified entity to the world
        /// </summary>
        /// <param name="addEntity">The entity to add to the world</param>
        public void Add(Entity.BaseEntity addEntity)
        {
            if (addEntity.HasData)
            {
                Writer.Add("summon " + addEntity.EntityType + " " + new Coords() + " " + addEntity.GetDataWithoutID());
            }
            else
            {
                Writer.Add("summon " + addEntity.EntityType);
            }
            Writer.NewLine();
        }

        /// <summary>
        /// Adds the specified entity to the world at the specified location
        /// </summary>
        /// <param name="addEntity">The entity to add to the world</param>
        /// <param name="coords">The coords to add the entity at</param>
        public void Add(Entity.BaseEntity addEntity, Coords coords)
        {
            if (addEntity.HasData)
            {
                Writer.Add("summon " + addEntity.EntityType + " " + coords + " " + addEntity.GetDataWithoutID());
            }
            else
            {
                Writer.Add("summon " + addEntity.EntityType + " " + coords);
            }
            Writer.NewLine();
        }

        /// <summary>
        /// Kills all entities selected by the selector
        /// </summary>
        /// <param name="selector">The <see cref="Selector"/> to use</param>
        public void Kill(Selector selector)
        {
            Writer.Add("kill " + selector);
            Writer.NewLine();
        }

        /// <summary>
        /// Spreads all entities selected by the selector around the specified location
        /// </summary>
        /// <param name="selector">The <see cref="Selector"/> to use</param>
        /// <param name="center">The center to spread around</param>
        /// <param name="spreadDistanceMin">the minimum distance the player can be spreaded from the <paramref name="center"/></param>
        /// <param name="spreadDistanceMax">the maximum distance the player can be spreaded from the <paramref name="center"/></param>
        /// <param name="spreadTeams">If teams should be placed close to each other</param>
        public void Spread(Selector selector, Coords center, int spreadDistanceMin, int spreadDistanceMax, bool spreadTeams = false)
        {
            Writer.Add("spreadplayers " + center.StringX + " " + center.StringZ + " " + spreadDistanceMin + " " + spreadDistanceMax + " " + spreadTeams.ToMinecraftBool() + " " + selector);
            Writer.NewLine();
        }

        /// <summary>
        /// Makes the selected entities join the specified <see cref="Team"/>
        /// </summary>
        /// <param name="selector">The <see cref="Selector"/> to use</param>
        /// <param name="team">The team they should join</param>
        public void JoinTeam(Selector selector, Team team)
        {
            if (team != null)
            {
                Writer.Add("team join " + team + " " + selector);
            }
            else
            {
                Writer.Add("team leave " + selector);
            }
            Writer.NewLine();
        }

        /// <summary>
        /// Enchants the selected entities hand item
        /// </summary>
        /// <param name="selector">The <see cref="Selector"/> to use</param>
        /// <param name="enchant">the enchantment to enchant with</param>
        /// <param name="level">the level of the enchantment</param>
        public void Enchant(Selector selector, ID.Enchant enchant, int level)
        {
            Writer.Add("enchant " + selector + " " + enchant + " " + level);
            Writer.NewLine();
        }

        /// <summary>
        /// Teleports the selected entities to location executed from
        /// </summary>
        /// <param name="selector">The <see cref="Selector"/> to use</param>
        public void Teleport(Selector selector)
        {
            Writer.Add("teleport " + selector);
            Writer.NewLine();
        }
        /// <summary>
        /// Teleports the selected entities to the specified location
        /// </summary>
        /// <param name="selector">The <see cref="Selector"/> to use</param>
        /// <param name="tpTo">The location to teleport the entities to</param>
        public void Teleport(Selector selector, Coords tpTo)
        {
            Writer.Add("teleport " + selector + " " + tpTo);
            Writer.NewLine();
        }
        /// <summary>
        /// Teleports the selected entities to the specified location
        /// </summary>
        /// <param name="selector">The <see cref="Selector"/> to use</param>
        /// <param name="tpTo">The location to teleport the entities to</param>
        /// <param name="rotation">The rotation to teleport the selected entities to</param>
        public void Teleport(Selector selector, Coords tpTo, Rotation rotation)
        {
            Writer.Add("teleport " + selector + " " + tpTo + " " + rotation);
            Writer.NewLine();
        }
        /// <summary>
        /// Teleports the selected entities to the specified location facing another entity
        /// </summary>
        /// <param name="selector">The <see cref="Selector"/> to use</param>
        /// <param name="tpTo">The location to teleport the entities to</param>
        /// <param name="facing">The selector the entities should look at</param>
        /// <param name="facingPart">The part of the entity to look at</param>
        public void Teleport(Selector selector, Coords tpTo, Selector facing, ID.FacingAnchor? facingPart = null)
        {
            facing.Limited();
            Writer.Add("teleport " + selector + " " + tpTo + " facing entity " + facing);
            if (facingPart != null) { Writer.Add(" " + facingPart); }
            Writer.NewLine();
        }

        /// <summary>
        /// Teleports the selected entities to the specified location facing a location
        /// </summary>
        /// <param name="selector">The <see cref="Selector"/> to use</param>
        /// <param name="tpTo">The location to teleport the entities to</param>
        /// <param name="facing">The block to look at</param>
        public void Teleport(Selector selector, Coords tpTo, Coords facing)
        {
            Writer.Add("teleport " + selector + " " + tpTo);
            if (facing != null) { Writer.Add(" facing " + facing); }
            Writer.NewLine();
        }

        /// <summary>
        /// Teleports the selected entities to another entity
        /// </summary>
        /// <param name="selector">The <see cref="Selector"/> to use</param>
        /// <param name="toSelector">The entity to teleport to</param>
        public void Teleport(Selector selector, Selector toSelector)
        {
            toSelector.Limited();
            Writer.Add("teleport " + selector + " " + toSelector);
            Writer.NewLine();
        }
        /// <summary>
        /// Teleports the selected entities to another entity facing another entity
        /// </summary>
        /// <param name="selector">The <see cref="Selector"/> to use</param>
        /// <param name="toSelector">The entity to teleport to</param>
        /// <param name="facing">The entity to look at</param>
        /// <param name="facingPart">The part to look at</param>
        public void Teleport(Selector selector, Selector toSelector, Selector facing, ID.FacingAnchor? facingPart = null)
        {
            facing.Limited();
            toSelector.Limited();
            Writer.Add("teleport " + selector + " " + toSelector + " facing entity " + facing);
            if (facingPart != null) { Writer.Add(" " + facingPart); }
            Writer.NewLine();
        }
        /// <summary>
        /// Teleports the selected entities to another entity facing a block
        /// </summary>
        /// <param name="selector">The <see cref="Selector"/> to use</param>
        /// <param name="toSelector">The entity to teleport to</param>
        /// <param name="facing">The block to look at</param>
        public void Teleport(Selector selector, Selector toSelector, Coords facing)
        {
            toSelector.Limited();
            Writer.Add("teleport " + selector + " " + toSelector);
            if (facing != null) { Writer.Add(" facing " + facing); }
            Writer.NewLine();
        }

        /// <summary>
        /// All commands using entity data
        /// </summary>
        public ClassData Data;
        /// <summary>
        /// All commands using entity data
        /// </summary>
        public class ClassData
        {
            readonly Function.FunctionWriter Writer;
            internal ClassData(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
            }

            /// <summary>
            /// Adds the data from <paramref name="newEntity"/> to the selected entity
            /// </summary>
            /// <param name="selector">The <see cref="Selector"/> to use</param>
            /// <param name="newEntity">the new data to add to the entity</param>
            public void Change(Selector selector, Entity.BaseEntity newEntity)
            {
                selector.Limited();
                Writer.Add("data merge entity " + selector + " " + newEntity.GetDataString());
                Writer.NewLine();
            }

            /// <summary>
            /// Adds the <paramref name="copyData"/> to the entity's data at the specified data path
            /// </summary>
            /// <param name="toSelector">The entity to copy to</param>
            /// <param name="toDataPath">The data path to copy to</param>
            /// <param name="copyData">The data to insert</param>
            /// <param name="modifierType">The way to add the data</param>
            public void Change(Selector toSelector, string toDataPath, ID.EntityDataModifierType modifierType, Data.SimpleDataHolder copyData)
            {
                toSelector.Limited();
                Writer.Add($"data modify entity {toSelector} {toDataPath} {modifierType} value {copyData.GetDataString()}");
                Writer.NewLine();
            }

            /// <summary>
            /// Adds the <paramref name="copyData"/> to the entity's data at the specified data path at the specified index of the array
            /// </summary>
            /// <param name="toSelector">The entity to copy to</param>
            /// <param name="toDataPath">The data path to copy to</param>
            /// <param name="copyData">The data to insert</param>
            /// <param name="index">the index to insert the data at</param>
            public void Change(Selector toSelector, string toDataPath, int index, Data.SimpleDataHolder copyData)
            {
                toSelector.Limited();
                Writer.Add($"data modify entity {toSelector} {toDataPath} insert {System.Math.Abs(index)} value {copyData.GetDataString()}");
                Writer.NewLine();
            }

            /// <summary>
            /// Gets a numeric value from the selected entity's data at the given path
            /// </summary>
            /// <param name="selector">The <see cref="Selector"/> to use</param>
            /// <param name="dataPath">The path to the data to get the number from</param>
            /// <param name="scale">The number to multiply the output number by</param>
            public void Get(Selector selector, string dataPath, double scale = 1)
            {
                selector.Limited();
                Writer.Add("data get entity " + selector + " " + dataPath);
                if (scale != 1) { Writer.Add(" " + scale.ToMinecraftDouble()); }
                Writer.NewLine();
            }

            /// <summary>
            /// Removes the data from the <see cref="SharpCraft.Entity"/> at the given datapath
            /// </summary>
            /// <param name="selector">The <see cref="Selector"/> to remove data from</param>
            /// <param name="dataPath">The datapath</param>
            public void Remove(Selector selector, string dataPath)
            {
                Writer.Add("data remove entity " + selector + " " + dataPath);
                Writer.NewLine();
            }

            /// <summary>
            /// Copies data from one entity to another entity
            /// </summary>
            /// <param name="toSelector">The entity to copy the data to</param>
            /// <param name="toDataPath">The data path to copy to</param>
            /// <param name="fromSelector">The entity to copy from</param>
            /// <param name="fromDataPath">The data path to copy from</param>
            /// <param name="modifierType">The way to add the data</param>
            public void Copy(Selector toSelector, string toDataPath, ID.EntityDataModifierType modifierType, Selector fromSelector, string fromDataPath)
            {
                toSelector.Limited();
                fromSelector.Limited();
                Writer.Add($"data modify entity {toSelector} {toDataPath} {modifierType} from entity {fromSelector} {fromDataPath}");
                Writer.NewLine();
            }

            /// <summary>
            /// Copies data from a block to an entity
            /// </summary>
            /// <param name="toSelector">The entity to copy the data to</param>
            /// <param name="toDataPath">The data path to copy to</param>
            /// <param name="fromBlock">The block to copy from</param>
            /// <param name="fromDataPath">The data path to copy from</param>
            /// <param name="modifierType">The way to add the data</param>
            public void Copy(Selector toSelector, string toDataPath, ID.EntityDataModifierType modifierType, Coords fromBlock, string fromDataPath)
            {
                toSelector.Limited();
                Writer.Add($"data modify entity {toSelector} {toDataPath} {modifierType} from block {fromBlock} {fromDataPath}");
                Writer.NewLine();
            }

            /// <summary>
            /// Copies data from a block to an entity at the data path's specified index
            /// </summary>
            /// <param name="toSelector">The entity to copy the data to</param>
            /// <param name="toDataPath">The data path to copy to</param>
            /// <param name="fromBlock">The block to copy from</param>
            /// <param name="fromDataPath">The data path to copy from</param>
            /// <param name="index">the index to copy to</param>
            public void Copy(Selector toSelector, string toDataPath, Coords fromBlock, string fromDataPath, int index)
            {
                toSelector.Limited();
                Writer.Add($"data modify entity {toSelector} {toDataPath} insert {System.Math.Abs(index)} from block {fromBlock} {fromDataPath}");
                Writer.NewLine();
            }

            /// <summary>
            /// Copies data from one entity to another entity at the data path's specified index
            /// </summary>
            /// <param name="toSelector">The entity to copy the data to</param>
            /// <param name="toDataPath">The data path to copy to</param>
            /// <param name="fromSelector">The entity to copy from</param>
            /// <param name="fromDataPath">The data path to copy from</param>
            /// <param name="index">the index to copy to</param>
            public void Copy(Selector toSelector, string toDataPath, Selector fromSelector, string fromDataPath, int index)
            {
                toSelector.Limited();
                fromSelector.Limited();
                Writer.Add($"data modify entity {toSelector} {toDataPath} insert {System.Math.Abs(index)} from entity {fromSelector} {fromDataPath}");
                Writer.NewLine();
            }
        }

        /// <summary>
        /// All commands for entity tags
        /// </summary>
        public ClassTag Tag;
        /// <summary>
        /// All commands for entity tags
        /// </summary>
        public class ClassTag
        {

            readonly Function.FunctionWriter Writer;
            internal ClassTag(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
            }

            /// <summary>
            /// Adds the specified tag the selected entities
            /// </summary>
            /// <param name="selector">the <see cref="Selector"/> to use</param>
            /// <param name="tagName">The <see cref="Tag"/> to add</param>
            public void Add(Selector selector, Tag tagName)
            {
                Writer.Add("tag " + selector + " add " + tagName);
                Writer.NewLine();

            }

            /// <summary>
            /// removes the specified tag the selected entities
            /// </summary>
            /// <param name="selector">the <see cref="Selector"/> to use</param>
            /// <param name="tagName">The <see cref="Tag"/> to remove</param>
            public void Remove(Selector selector, Tag tagName)
            {
                Writer.Add("tag " + selector + " remove " + tagName);
                Writer.NewLine();
            }
        }

        /// <summary>
        /// All commands for entity scores
        /// </summary>
        public ClassScore Score;
        /// <summary>
        /// All commands for entity scores
        /// </summary>
        public class ClassScore
        {
            readonly Function.FunctionWriter Writer;
            internal ClassScore(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
            }

            /// <summary>
            /// adds the <paramref name="amount"/> to the selected entities' score in <paramref name="objective"/>
            /// </summary>
            /// <param name="selector">the <see cref="Selector"/> to use</param>
            /// <param name="objective">the <see cref="ScoreObject"/> to change the score in</param>
            /// <param name="amount">the amount to add to the score. If the number is negative its removed instead</param>
            public void Add(Selector selector, ScoreObject objective, int amount)
            {
                if (amount >= 0)
                {
                    Writer.Add("scoreboard players add " + selector + " " + objective + " " + amount);
                }
                else
                {
                    amount *= -1;
                    Writer.Add("scoreboard players remove " + selector + " " + objective + " " + amount);
                }
                Writer.NewLine();
            }

            /// <summary>
            /// sets the selected entities' score in <paramref name="objective"/> to the specified <paramref name="amount"/>
            /// </summary>
            /// <param name="selector">the <see cref="Selector"/> to use</param>
            /// <param name="objective">the <see cref="ScoreObject"/> to change the score in</param>
            /// <param name="amount">the amount to set the score to</param>
            public void Set(Selector selector, ScoreObject objective, int amount)
            {
                Writer.Add("scoreboard players set " + selector + " " + objective + " " + amount);
                Writer.NewLine();
            }

            /// <summary>
            /// Does math with two scores and saves the result in one of the entities' score
            /// </summary>
            /// <param name="mainSelector">The first entity (The result will be stored in this entity's score)</param>
            /// <param name="mainObjective">The first entity's <see cref="ScoreObject"/> (The result will be stored in here)</param>
            /// <param name="operationType">The operation to do between the numbers</param>
            /// <param name="otherSelector">The other entity</param>
            /// <param name="otherObjective">The other entity's <see cref="ScoreObject"/></param>
            public void Operation(Selector mainSelector, ScoreObject mainObjective, ID.Operation operationType, Selector otherSelector, ScoreObject otherObjective)
            {
                mainSelector.Limited();
                otherSelector.Limited();
                Writer.Add("scoreboard players operation " + mainSelector + " " + mainObjective + " ");
                if (operationType == ID.Operation.Add) { Writer.Add("+="); }
                if (operationType == ID.Operation.Divide) { Writer.Add("/="); }
                if (operationType == ID.Operation.Equel) { Writer.Add("="); }
                if (operationType == ID.Operation.GetHigher) { Writer.Add(">"); }
                if (operationType == ID.Operation.GetLowest) { Writer.Add("<"); }
                if (operationType == ID.Operation.Multiply) { Writer.Add("*="); }
                if (operationType == ID.Operation.Remainder) { Writer.Add("%="); }
                if (operationType == ID.Operation.Subtract) { Writer.Add("-="); }
                if (operationType == ID.Operation.Switch) { Writer.Add("><"); }
                Writer.Add(" " + otherSelector + " " + otherObjective);
                Writer.NewLine();
            }

            /// <summary>
            /// Resets the selected entities scores
            /// </summary>
            /// <param name="selector">the <see cref="Selector"/> to use</param>
            /// <param name="objective">if a <see cref="ScoreObject"/> is specified only the score in the specified <see cref="ScoreObject"/> will be reseted</param>
            public void Reset(Selector selector, ScoreObject objective = null)
            {
                Writer.Add("scoreboard players reset " + selector);
                if (objective != null) { Writer.Add(" " + objective); }
                Writer.NewLine();
            }

            /// <summary>
            /// Gets the selected entity's score and outputs it
            /// </summary>
            /// <param name="selector">the <see cref="Selector"/> to use</param>
            /// <param name="objective">the <see cref="ScoreObject"/> to take the score from</param>
            public void Get(Selector selector, ScoreObject objective)
            {
                selector.Limited();
                Writer.Add("scoreboard players get " + selector + " " + objective);
                Writer.NewLine();
            }
        }

        /// <summary>
        /// All commands for effects
        /// </summary>
        public ClassEffect Effect;
        /// <summary>
        /// All commands for effects
        /// </summary>
        public class ClassEffect
        {
            readonly Function.FunctionWriter Writer;
            internal ClassEffect(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
            }

            /// <summary>
            /// Clears the specified effect from the selected entities
            /// </summary>
            /// <param name="selector">the <see cref="Selector"/> to use</param>
            /// <param name="effect">the effect to remove</param>
            public void Clear(Selector selector, ID.Effect? effect = null)
            {
                Writer.Add("effect clear " + selector);
                if (effect != null) { Writer.Add(" " + effect); }
                Writer.NewLine();
            }

            /// <summary>
            /// Gives the specified effect to the selected entities
            /// </summary>
            /// <param name="selector">the <see cref="Selector"/> to use</param>
            /// <param name="effect">the effect to give</param>
            /// <param name="time">the duration of the effect</param>
            /// <param name="amplifier">the amplifier of the effect (0 = level 1)</param>
            /// <param name="hideParticles">if the particles from the effect should be hidden or not</param>
            public void Give(Selector selector, ID.Effect effect, int time, int amplifier = 0, bool? hideParticles = false)
            {
                Writer.Add("effect give " + selector + " " + effect + " " + time);
                if (amplifier != 0 || hideParticles == true) { Writer.Add(" " + amplifier); }
                if (hideParticles == true) { Writer.Add(" " + hideParticles.ToMinecraftBool()); }
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
                Horse = new ClassHorse(CommandsList);
            }

            /// <summary>
            /// Changes the selected entities' armor to the specified item
            /// </summary>
            /// <param name="selector">the <see cref="Selector"/> to use</param>
            /// <param name="giveItem">the <see cref="Item"/> to give</param>
            /// <param name="armorSlot">the armor slot to put the item in</param>
            public void Armor(Selector selector, Item giveItem, ID.ArmorSlot armorSlot)
            {
                Writer.Add("replaceitem entity " + selector + " armor." + armorSlot + " " + giveItem.IDDataString + " " + (giveItem.Count ?? 1));
                Writer.NewLine();
            }

            /// <summary>
            /// Changes the selected entities' weapon to the specified item
            /// </summary>
            /// <param name="selector">the <see cref="Selector"/> to use</param>
            /// <param name="giveItem">the <see cref="Item"/> to give</param>
            /// <param name="offHand">If its the offhand weapon which should change</param>
            public void Weapon(Selector selector, Item giveItem, bool offHand = false)
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
            /// Adds the specified item to the selected entities' inventory
            /// </summary>
            /// <param name="selector">the <see cref="Selector"/> to use</param>
            /// <param name="addItem">the item to add to the entities. <see cref="Item.Slot"/> is used to specify the slot</param>
            public void Container(Selector selector, Item addItem)
            {
                if (addItem.Slot == null || addItem.Slot > 53 || addItem.Slot < 0)
                {
                    addItem.Slot = 0;
                }
                Writer.Add("replaceitem entity " + selector + " container." + addItem.Slot + " " + addItem.IDDataString + " " + (addItem.Count ?? 1));
                Writer.NewLine();
            }

            /// <summary>
            /// Adds the specified item to the selected villagers' inventory
            /// </summary>
            /// <param name="selector">the <see cref="Selector"/> to use</param>
            /// <param name="addItem">the item to add to the villager. <see cref="Item.Slot"/> is used to specify the slot</param>
            public void VillagerInventory(Selector selector, Item addItem)
            {
                if (addItem.Slot == null || addItem.Slot > 7 || addItem.Slot < 0)
                {
                    addItem.Slot = 0;
                }
                Writer.Add("replaceitem entity " + selector + "villager." + addItem.Slot + " " + addItem.IDDataString + " " + (addItem.Count ?? 1));
                Writer.NewLine();
            }

            /// <summary>
            /// all commands for items in horses
            /// </summary>
            public ClassHorse Horse;
            /// <summary>
            /// all commands for items in horses
            /// </summary>
            public class ClassHorse
            {
                readonly Function.FunctionWriter Writer;
                internal ClassHorse(Function.FunctionWriter commandsList)
                {
                    Writer = commandsList;
                }

                /// <summary>
                /// Adds the specified item to the selected horses' inventory
                /// </summary>
                /// <param name="selector">the <see cref="Selector"/> to use</param>
                /// <param name="giveItem">the item to add to the horses. <see cref="Item.Slot"/> is used to specify the slot</param>
                public void Inventory(Selector selector, Item giveItem)
                {
                    if (giveItem.Slot == null || giveItem.Slot > 14 || giveItem.Slot < 0)
                    {
                        giveItem.Slot = 0;
                    }
                    Writer.Add("replaceitem entity " + selector + "horse." + giveItem.Slot + " " + giveItem.IDDataString + " " + (giveItem.Count ?? 1));
                    Writer.NewLine();
                }

                /// <summary>
                /// Makes the specified item the selected horses' saddles
                /// </summary>
                /// <param name="selector">the <see cref="Selector"/> to use</param>
                /// <param name="giveItem">the item to add to the horses.</param>
                public void Saddle(Selector selector, Item giveItem)
                {
                    Writer.Add("replaceitem entity " + selector + "horse.saddle " + giveItem.IDDataString + " " + (giveItem.Count ?? 1));
                    Writer.NewLine();
                }
                /// <summary>
                /// Makes the specified item the selected horses' armor
                /// </summary>
                /// <param name="selector">the <see cref="Selector"/> to use</param>
                /// <param name="giveItem">the item to add to the horses.</param>
                public void Armor(Selector selector, Item giveItem)
                {
                    Writer.Add("replaceitem entity " + selector + "horse.armor " + giveItem.IDDataString + " " + (giveItem.Count ?? 1));
                    Writer.NewLine();
                }
                /// <summary>
                /// Makes the specified item the selected horses' chest
                /// </summary>
                /// <param name="selector">the <see cref="Selector"/> to use</param>
                /// <param name="giveItem">the item to add to the horses.</param>
                public void Chest(Selector selector, Item giveItem)
                {
                    Writer.Add("replaceitem entity " + selector + "horse.chest " + giveItem.IDDataString + " " + (giveItem.Count ?? 1));
                    Writer.NewLine();
                }
            }
        }
    }
}
