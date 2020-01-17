using SharpCraft.Commands;
using System.Collections.Generic;
using System.Linq;

namespace SharpCraft.FunctionWriters
{
    /// <summary>
    /// All the entity commands
    /// </summary>
    public class EntityCommands
    {
        /// <summary>
        /// The function to write onto
        /// </summary>
        public Function Function { get; private set; }

        internal EntityCommands(Function function)
        {
            Function = function;
            Data = new ClassData(function);
            Tag = new ClassTag(function);
            Score = new ClassScore(function);
            Effect = new ClassEffect(function);
            Item = new ClassItem(function);
        }

        /// <summary>
        /// Adds the specified entity to the world at the specified location
        /// </summary>
        /// <param name="addEntity">The entity to add to the world</param>
        /// <param name="coords">The coords to add the entity at</param>
        public void Add(Entity.BaseEntity addEntity, Coords coords = null)
        {
            Function.AddCommand(new SummonCommand(addEntity, coords ?? new Coords()));
        }

        /// <summary>
        /// Kills all entities selected by the selector
        /// </summary>
        /// <param name="selector">The <see cref="BaseSelector"/> to use</param>
        public void Kill(BaseSelector selector)
        {
            Function.AddCommand(new KillCommand(selector));
        }

        /// <summary>
        /// Spreads all entities selected by the selector around the specified location
        /// </summary>
        /// <param name="selector">The <see cref="BaseSelector"/> to use</param>
        /// <param name="center">The center to spread around</param>
        /// <param name="minDistance">the minimum distance the player can be spreaded away from each other</param>
        /// <param name="spreadDistanceMax">the maximum distance the player can be spreaded from the <paramref name="center"/></param>
        /// <param name="spreadTeams">If teams should be placed close to each other</param>
        public void Spread(BaseSelector selector, Coords center, int minDistance, int spreadDistanceMax, bool spreadTeams = false)
        {
            Function.AddCommand(new SpreadPlayersCommand(center, selector, minDistance, spreadDistanceMax, spreadTeams));
        }

        /// <summary>
        /// Makes the selected entities join the specified <see cref="Team"/>
        /// </summary>
        /// <param name="selector">The <see cref="BaseSelector"/> to use</param>
        /// <param name="team">The team they should join. Leave null to make them leave their team</param>
        public void JoinTeam(BaseSelector selector, Team team)
        {
            if (team != null)
            {
                Function.AddCommand(new TeamJoinCommand(team, selector));
            }
            else
            {
                Function.AddCommand(new TeamLeaveCommand(selector));
            }
        }

        /// <summary>
        /// Enchants the selected entities hand item
        /// </summary>
        /// <param name="selector">The <see cref="BaseSelector"/> to use</param>
        /// <param name="enchant">the enchantment to enchant with</param>
        /// <param name="level">the level of the enchantment</param>
        public void Enchant(BaseSelector selector, ID.Enchant enchant, int level)
        {
            Function.AddCommand(new EnchantCommand(selector, level, enchant));
        }

        /// <summary>
        /// Teleports the selected entities to the specified location
        /// </summary>
        /// <param name="selector">The <see cref="BaseSelector"/> to use</param>
        /// <param name="tpTo">The location to teleport the entities to. Leave null to teleport to executed position</param>
        public void Teleport(BaseSelector selector, Coords tpTo = null)
        {
            Function.AddCommand(new TeleportToCommand(tpTo ?? new Coords(), selector));
        }

        /// <summary>
        /// Teleports the selected entities to the specified location
        /// </summary>
        /// <param name="selector">The <see cref="BaseSelector"/> to use</param>
        /// <param name="tpTo">The location to teleport the entities to</param>
        /// <param name="rotation">The rotation to teleport the selected entities to</param>
        public void Teleport(BaseSelector selector, Coords tpTo, Rotation rotation)
        {
            Function.AddCommand(new TeleportToRotationCommand(tpTo ?? new Coords(), selector, rotation ?? new Rotation(true, 0, 0)));
        }
        /// <summary>
        /// Teleports the selected entities to the specified location facing another entity
        /// </summary>
        /// <param name="selector">The <see cref="BaseSelector"/> to use</param>
        /// <param name="tpTo">The location to teleport the entities to</param>
        /// <param name="facing">The selector the entities should look at</param>
        /// <param name="facingPart">The part of the entity to look at</param>
        public void Teleport(BaseSelector selector, Coords tpTo, BaseSelector facing, ID.FacingAnchor facingPart = ID.FacingAnchor.feet)
        {
            facing.LimitSelector();
            Function.AddCommand(new TeleportToFacingEntityCommand(tpTo ?? new Coords(), selector, facing, facingPart));
        }

        /// <summary>
        /// Teleports the selected entities to the specified location facing a location
        /// </summary>
        /// <param name="selector">The <see cref="BaseSelector"/> to use</param>
        /// <param name="tpTo">The location to teleport the entities to</param>
        /// <param name="facing">The block to look at</param>
        public void Teleport(BaseSelector selector, Coords tpTo, Coords facing)
        {
            Function.AddCommand(new TeleportToFacingCommand(tpTo ?? new Coords(), selector, facing ?? new Coords()));
        }

        /// <summary>
        /// Teleports the selected entities to another entity
        /// </summary>
        /// <param name="selector">The <see cref="BaseSelector"/> to use</param>
        /// <param name="toSelector">The entity to teleport to</param>
        public void Teleport(BaseSelector selector, BaseSelector toSelector)
        {
            toSelector.LimitSelector();
            Function.AddCommand(new TeleportToEntityCommand(selector, toSelector));
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
            /// <summary>
            /// The function to write onto
            /// </summary>
            public Function Function { get; private set; }
            internal ClassData(Function function)
            {
                this.Function = function;
            }

            /// <summary>
            /// Adds the data from <paramref name="newEntity"/> to the selected entity
            /// </summary>
            /// <param name="selector">The <see cref="BaseSelector"/> to use</param>
            /// <param name="newEntity">the new data to add to the entity</param>
            public void Change(BaseSelector selector, Data.SimpleDataHolder newEntity)
            {
                selector.LimitSelector();
                Function.AddCommand(new DataMergeEntityCommand(selector, newEntity));
            }

            /// <summary>
            /// Adds the <paramref name="copyData"/> to the entity's data at the specified data path
            /// </summary>
            /// <param name="toSelector">The entity to copy to</param>
            /// <param name="toDataPath">The data path to copy to</param>
            /// <param name="copyData">The data to insert</param>
            /// <param name="modifierType">The way to add the data</param>
            public void Change(BaseSelector toSelector, string toDataPath, ID.EntityDataModifierType modifierType, Data.SimpleDataHolder copyData)
            {
                toSelector.LimitSelector();
                Function.AddCommand(new DataModifyWithDataCommand(new EntityDataLocation(toSelector, toDataPath), modifierType, copyData));
            }

            /// <summary>
            /// Adds the <paramref name="copyData"/> to the entity's data at the specified data path at the specified index of the array
            /// </summary>
            /// <param name="toSelector">The entity to copy to</param>
            /// <param name="toDataPath">The data path to copy to</param>
            /// <param name="copyData">The data to insert</param>
            /// <param name="index">the index to insert the data at</param>
            public void Change(BaseSelector toSelector, string toDataPath, int index, Data.SimpleDataHolder copyData)
            {
                toSelector.LimitSelector();
                Function.AddCommand(new DataModifyInsertDataCommand(new EntityDataLocation(toSelector, toDataPath), index, copyData));
            }

            /// <summary>
            /// Gets a numeric value from the selected entity's data at the given path
            /// </summary>
            /// <param name="selector">The <see cref="BaseSelector"/> to use</param>
            /// <param name="dataPath">The path to the data to get the number from</param>
            /// <param name="scale">The number to multiply the output number by</param>
            public void Get(BaseSelector selector, string dataPath, double scale = 1)
            {
                selector.LimitSelector();
                Function.AddCommand(new DataGetCommand(new EntityDataLocation(selector, dataPath), scale));
            }

            /// <summary>
            /// Removes the data from the <see cref="SharpCraft.Entity"/> at the given datapath
            /// </summary>
            /// <param name="selector">The <see cref="BaseSelector"/> to remove data from</param>
            /// <param name="dataPath">The datapath</param>
            public void Remove(BaseSelector selector, string dataPath)
            {
                Function.AddCommand(new DataDeleteCommand(new EntityDataLocation(selector, dataPath)));
            }

            /// <summary>
            /// Copies data from a place to an entity
            /// </summary>
            /// <param name="toSelector">The entity to copy the data to</param>
            /// <param name="toDataPath">The data path to copy to</param>
            /// <param name="dataLocation">The place to copy the data from</param>
            /// <param name="modifierType">The way to add the data</param>
            public void Copy(BaseSelector toSelector, string toDataPath, ID.EntityDataModifierType modifierType, IDataLocation dataLocation)
            {
                toSelector.LimitSelector();
                Function.AddCommand(new DataModifyWithLocationCommand(new EntityDataLocation(toSelector, toDataPath), modifierType, dataLocation));
            }

            /// <summary>
            /// Copies data from a place to an entity at the data path's specified index
            /// </summary>
            /// <param name="toSelector">The entity to copy the data to</param>
            /// <param name="toDataPath">The data path to copy to</param>
            /// <param name="dataLocation">The place to copy the data from</param>
            /// <param name="index">the index to copy to</param>
            public void Copy(BaseSelector toSelector, string toDataPath, IDataLocation dataLocation, int index)
            {
                toSelector.LimitSelector();
                Function.AddCommand(new DataModifyInsertLocationCommand(new EntityDataLocation(toSelector, toDataPath), index, dataLocation));
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

            /// <summary>
            /// The function to write onto
            /// </summary>
            public Function Function { get; private set; }
            internal ClassTag(Function function)
            {
                this.Function = function;
            }

            /// <summary>
            /// Adds the specified tag the selected entities
            /// </summary>
            /// <param name="selector">the <see cref="BaseSelector"/> to use</param>
            /// <param name="tagName">The <see cref="Tag"/> to add</param>
            public void Add(BaseSelector selector, Tag tagName)
            {
                Function.AddCommand(new TagCommand(selector, tagName, true));
            }

            /// <summary>
            /// removes the specified tag the selected entities
            /// </summary>
            /// <param name="selector">the <see cref="BaseSelector"/> to use</param>
            /// <param name="tagName">The <see cref="Tag"/> to remove</param>
            public void Remove(BaseSelector selector, Tag tagName)
            {
                Function.AddCommand(new TagCommand(selector, tagName, false));
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
            /// <summary>
            /// The function to write onto
            /// </summary>
            public Function Function { get; private set; }
            internal ClassScore(Function function)
            {
                this.Function = function;
            }

            /// <summary>
            /// adds the <paramref name="amount"/> to the selected entities' score in <paramref name="objective"/>
            /// </summary>
            /// <param name="selector">the <see cref="BaseSelector"/> to use</param>
            /// <param name="objective">the <see cref="Objective"/> to change the score in</param>
            /// <param name="amount">the amount to add to the score. If the number is negative its removed instead</param>
            public void Add(BaseSelector selector, Objective objective, int amount)
            {
                Function.AddCommand(new ScoreboardValueChangeCommand(selector, objective, ID.ScoreChange.add, amount));
            }

            /// <summary>
            /// sets the selected entities' score in <paramref name="objective"/> to the specified <paramref name="amount"/>
            /// </summary>
            /// <param name="selector">the <see cref="BaseSelector"/> to use</param>
            /// <param name="objective">the <see cref="Objective"/> to change the score in</param>
            /// <param name="amount">the amount to set the score to</param>
            public void Set(BaseSelector selector, Objective objective, int amount)
            {
                Function.AddCommand(new ScoreboardValueChangeCommand(selector, objective, ID.ScoreChange.set, amount));
            }

            /// <summary>
            /// Does math with a score and a number and saves the result in the entity's score
            /// </summary>
            /// <param name="mainSelector">The entity score to do math on (Result will be saved in here)</param>
            /// <param name="mainObjective">The <see cref="Objective"/> to get the value from to do math on</param>
            /// <param name="operationType">The operation to do between the numbers</param>
            /// <param name="number">The number to do math with</param>
            public void Operation(BaseSelector mainSelector, Objective mainObjective, ID.Operation operationType, int number)
            {
                mainSelector.LimitSelector();
                Function.AddCommand(new ScoreboardOperationCommand(mainSelector, mainObjective, operationType, AddConstantNumber(number), constantObjective));
            }

            /// <summary>
            /// Does math with two scores and saves the result in one of the entities' score
            /// </summary>
            /// <param name="mainSelector">The first entity (The result will be stored in this entity's score)</param>
            /// <param name="mainObjective">The first entity's <see cref="Objective"/> (The result will be stored in here)</param>
            /// <param name="operationType">The operation to do between the numbers</param>
            /// <param name="otherSelector">The other entity</param>
            /// <param name="otherObjective">The other entity's <see cref="Objective"/></param>
            public void Operation(BaseSelector mainSelector, Objective mainObjective, ID.Operation operationType, BaseSelector otherSelector, Objective otherObjective)
            {
                mainSelector.LimitSelector();
                otherSelector.LimitSelector();
                Function.AddCommand(new ScoreboardOperationCommand(mainSelector, mainObjective, operationType, otherSelector, otherObjective));
            }

            /// <summary>
            /// Resets the selected entities scores
            /// </summary>
            /// <param name="selector">the <see cref="BaseSelector"/> to use</param>
            /// <param name="objective">if a <see cref="Objective"/> is specified only the score in the specified <see cref="Objective"/> will be reseted</param>
            public void Reset(BaseSelector selector, Objective objective = null)
            {
                Function.AddCommand(new ScoreboardResetCommand(selector, objective));
            }

            /// <summary>
            /// Gets the selected entity's score and outputs it
            /// </summary>
            /// <param name="selector">the <see cref="BaseSelector"/> to use</param>
            /// <param name="objective">the <see cref="Objective"/> to take the score from</param>
            public void Get(BaseSelector selector, Objective objective)
            {
                selector.LimitSelector();
                Function.AddCommand(new ScoreboardValueGetCommand(selector, objective));
            }

            #region make constants
            private const string constantFileName = "constants";
            private static readonly Objective constantObjective = new Objective("constants");
            private static Function constantNumberFile;
            private BaseSelector AddConstantNumber(int number)
            {
                 //Get constant making function file
                 if (constantNumberFile is null)
                 {
                     constantNumberFile = (SharpCraftFiles.SetupFunction.Commands.SingleOrDefault(c => c is RunFunctionCommand functionCommand && functionCommand.Function.Name == constantFileName) as RunFunctionCommand)?.Function as Function;
                     
                     if (constantNumberFile is null)
                     {
                         constantNumberFile = SharpCraftFiles.SharpCraftNamespace.Function(constantFileName, BaseFile.WriteSetting.OnDispose);
                         constantNumberFile.AddCommand(new ScoreboardObjectiveAddCommand(constantObjective, "dummy", null));
                         SharpCraftFiles.SetupFunction.AddCommand(new RunFunctionCommand(constantNumberFile));
                     }
                 }
                 
                 //Check if constant already is added. If not add it to the constant function file.
                 if (!(constantNumberFile.Commands.SingleOrDefault(c => c is ScoreboardValueChangeCommand command && command.ChangeNumber == number) is ScoreboardValueChangeCommand existingCommand))
                 {
                     NameSelector selector = new NameSelector(number.ToString(), true);
                     constantNumberFile.AddCommand(new ScoreboardValueChangeCommand(selector, constantObjective, ID.ScoreChange.set, number));
                     return selector;
                 }
                 else
                 {
                     return existingCommand.Selector;
                 }
            }
            #endregion
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
            /// <summary>
            /// The function to write onto
            /// </summary>
            public Function Function { get; private set; }
            internal ClassEffect(Function function)
            {
                this.Function = function;
            }

            /// <summary>
            /// Clears the specified effect from the selected entities
            /// </summary>
            /// <param name="selector">the <see cref="BaseSelector"/> to use</param>
            /// <param name="effect">the effect to remove</param>
            public void Clear(BaseSelector selector, ID.Effect? effect = null)
            {
                Function.AddCommand(new EffectClearCommand(selector, effect));
            }

            /// <summary>
            /// Gives the specified effect to the selected entities
            /// </summary>
            /// <param name="selector">the <see cref="BaseSelector"/> to use</param>
            /// <param name="effect">the effect to give</param>
            /// <param name="time">the duration of the effect</param>
            /// <param name="amplifier">the amplifier of the effect (0 = level 1)</param>
            /// <param name="hideParticles">if the particles from the effect should be hidden or not</param>
            public void Give(BaseSelector selector, ID.Effect effect, int time, byte amplifier = 0, bool hideParticles = true)
            {
                Function.AddCommand(new EffectGiveCommand(selector,effect, time, amplifier, hideParticles));
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
            /// <summary>
            /// The function to write onto
            /// </summary>
            public Function Function { get; private set; }
            internal ClassItem(Function function)
            {
                this.Function = function;
                Horse = new ClassHorse(function);
            }

            /// <summary>
            /// Changes the selected entities' armor to the specified item
            /// </summary>
            /// <param name="selector">the <see cref="BaseSelector"/> to use</param>
            /// <param name="giveItem">the <see cref="Item"/> to give</param>
            /// <param name="armorSlot">the armor slot to put the item in</param>
            public void Armor(BaseSelector selector, Item giveItem, ID.ArmorSlot armorSlot)
            {
                Function.AddCommand(new ReplaceitemEntityCommand(selector, new Slots.ArmorSlot(armorSlot), giveItem, giveItem.Count ?? 1));
            }

            /// <summary>
            /// Changes the selected entities' weapon to the specified item
            /// </summary>
            /// <param name="selector">the <see cref="BaseSelector"/> to use</param>
            /// <param name="giveItem">the <see cref="Item"/> to give</param>
            /// <param name="offHand">If its the offhand weapon which should change</param>
            public void Weapon(BaseSelector selector, Item giveItem, bool offHand = false)
            {
                Function.AddCommand(new ReplaceitemEntityCommand(selector, new Slots.WeaponSlot(!offHand), giveItem, giveItem.Count ?? 1));
            }

            /// <summary>
            /// Adds the specified item to the selected entities' inventory
            /// </summary>
            /// <param name="selector">the <see cref="BaseSelector"/> to use</param>
            /// <param name="addItem">the item to add to the entities. <see cref="Item.Slot"/> is used to specify the slot</param>
            public void Container(BaseSelector selector, Item addItem)
            {
                Function.AddCommand(new ReplaceitemEntityCommand(selector, new Slots.ContainerSlot(addItem.Slot ?? 0), addItem, addItem.Count ?? 1));
            }

            /// <summary>
            /// Adds the specified item to the selected villagers' inventory
            /// </summary>
            /// <param name="selector">the <see cref="BaseSelector"/> to use</param>
            /// <param name="addItem">the item to add to the villager. <see cref="Item.Slot"/> is used to specify the slot</param>
            public void VillagerInventory(BaseSelector selector, Item addItem)
            {
                Function.AddCommand(new ReplaceitemEntityCommand(selector, new Slots.VillagerInventorySlot(addItem.Slot ?? 0), addItem, addItem.Count ?? 1));
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
                readonly Function function;
                internal ClassHorse(Function function)
                {
                    this.function = function;
                }

                /// <summary>
                /// Adds the specified item to the selected horses' inventory
                /// </summary>
                /// <param name="selector">the <see cref="BaseSelector"/> to use</param>
                /// <param name="giveItem">the item to add to the horses. <see cref="Item.Slot"/> is used to specify the slot</param>
                public void Inventory(BaseSelector selector, Item giveItem)
                {
                    function.AddCommand(new ReplaceitemEntityCommand(selector, new Slots.HorseInventorySlot(giveItem.Slot ?? 0), giveItem, giveItem.Count ?? 1));
                }

                /// <summary>
                /// Makes the specified item the selected horses' saddles
                /// </summary>
                /// <param name="selector">the <see cref="BaseSelector"/> to use</param>
                /// <param name="giveItem">the item to add to the horses.</param>
                public void Saddle(BaseSelector selector, Item giveItem)
                {
                    function.AddCommand(new ReplaceitemEntityCommand(selector, new Slots.HorseSlot(ID.HorseSlot.saddle), giveItem, giveItem.Count ?? 1));
                }
                /// <summary>
                /// Makes the specified item the selected horses' armor
                /// </summary>
                /// <param name="selector">the <see cref="BaseSelector"/> to use</param>
                /// <param name="giveItem">the item to add to the horses.</param>
                public void Armor(BaseSelector selector, Item giveItem)
                {
                    function.AddCommand(new ReplaceitemEntityCommand(selector, new Slots.HorseSlot(ID.HorseSlot.armor), giveItem, giveItem.Count ?? 1));
                }
                /// <summary>
                /// Makes the specified item the selected horses' chest
                /// </summary>
                /// <param name="selector">the <see cref="BaseSelector"/> to use</param>
                /// <param name="giveItem">the item to add to the horses.</param>
                public void Chest(BaseSelector selector, Item giveItem)
                {
                    function.AddCommand(new ReplaceitemEntityCommand(selector, new Slots.HorseSlot(ID.HorseSlot.chest), giveItem, giveItem.Count ?? 1));
                }
            }
        }
    }
}
