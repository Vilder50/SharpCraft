﻿using SharpCraft.Commands;

namespace SharpCraft.FunctionWriters
{
    /// <summary>
    /// All the block commands
    /// </summary>
    public class BlockCommands
    {
        readonly Function function;
        internal BlockCommands(Function function)
        {
            this.function = function;
            Data = new ClassData(function);
        }

        /// <summary>
        /// Adds a <see cref="SharpCraft.Block"/> at the given <see cref="Coords"/>
        /// </summary>
        /// <param name="addBlock">the <see cref="SharpCraft.Block"/> to add</param>
        /// <param name="blockCoords">the <see cref="Coords"/> to add it at</param>
        /// <param name="type">how to place the block</param>
        public void Add(Block addBlock, Coords blockCoords, ID.BlockAdd type = ID.BlockAdd.replace)
        {
            function.AddCommand(new SetblockCommand(blockCoords, addBlock, type));
        }

        /// <summary>
        /// Fills in the <see cref="SharpCraft.Block"/> between the two corner <see cref="Coords"/>
        /// </summary>
        /// <param name="fillBlock">the <see cref="SharpCraft.Block"/> to fill with</param>
        /// <param name="corner1">the first corner <see cref="Coords"/></param>
        /// <param name="corner2">the second corner <see cref="Coords"/></param>
        /// <param name="type">how to fill the <see cref="SharpCraft.Block"/>s</param>
        /// <param name="replaceBlock">the <see cref="SharpCraft.Block"/>s to replace</param>
        public void Fill(Block fillBlock, Coords corner1, Coords corner2, ID.BlockFill type = ID.BlockFill.replace, Block replaceBlock = null)
        {
            if (replaceBlock is null)
            {
                function.AddCommand(new FillCommand(corner1, corner2, fillBlock, type));
            }
            else
            {
                function.AddCommand(new FillReplaceCommand(corner1, corner2, fillBlock, replaceBlock));
            }
        }

        /// <summary>
        /// Copies the <see cref="SharpCraft.Block"/>s between the two corner <see cref="Coords"/> to another <see cref="Coords"/>
        /// </summary>
        /// <param name="corner1">the first corner <see cref="Coords"/></param>
        /// <param name="corner2">the second corner <see cref="Coords"/></param>
        /// <param name="copyTo">the place to copy to</param>
        /// <param name="type">how to copy the <see cref="SharpCraft.Block"/>s</param>
        /// <param name="way">copy rules</param>
        /// <param name="filteredBlock">the <see cref="SharpCraft.Block"/> to copy</param>
        public void Clone(Coords corner1, Coords corner2, Coords copyTo, ID.BlockClone type = ID.BlockClone.replace, ID.BlockCloneWay way = ID.BlockCloneWay.normal, Block filteredBlock = null)
        {
            if (filteredBlock is null)
            {
                function.AddCommand(new CloneCommand(corner1, corner2, copyTo, (type == ID.BlockClone.masked), way));
            }
            else
            {
                function.AddCommand(new FilteredCloneCommand(corner1, corner2, copyTo, filteredBlock, way));
            }
        }

        /// <summary>
        /// Inserts the given <see cref="Item"/> into the <see cref="SharpCraft.Block"/>
        /// </summary>
        /// <param name="BlockCoords">The <see cref="SharpCraft.Block"/> to insert into</param>
        /// <param name="AddItem">The <see cref="Item"/> to insert (<see cref="Item.Slot"/> choses the slot)</param>
        public void AddItem(Coords BlockCoords, Item AddItem)
        {
            function.AddCommand(new ReplaceitemBlockCommand(BlockCoords, new Slots.ContainerSlot(AddItem.Slot ?? 0), AddItem, AddItem.Count ?? 1));
        }

        /// <summary>
        /// Inserts the given <see cref="ILootTable"/> into the <see cref="SharpCraft.Block"/>
        /// </summary>
        /// <param name="block">the <see cref="SharpCraft.Block"/> to input into</param>
        /// <param name="loot">the <see cref="ILootTable"/> to input</param>
        /// <param name="slot">the slot to insert the <see cref="ILootTable"/> at</param>
        public void Loot(Coords block, ILootTable loot, int? slot = null)
        {
            if (slot is null)
            {
                function.AddCommand(new LootCommand(new LootTargets.InsertTarget(block), new LootSources.LoottableSource(loot)));
            }
            else
            {
                function.AddCommand(new LootCommand(new LootTargets.BlockTarget(block, new Slots.ContainerSlot(slot.Value)), new LootSources.LoottableSource(loot)));
            }
        }

        /// <summary>
        /// Inserts the given <see cref="Entity"/>'s <see cref="LootTable"/> into the <see cref="SharpCraft.Block"/>
        /// </summary>
        /// <param name="block">the <see cref="SharpCraft.Block"/> to input into</param>
        /// <param name="kill">the <see cref="Entity"/>'s <see cref="LootTable"/> to input</param>
        /// <param name="slot">the slot to insert the <see cref="LootTable"/> at</param>
        public void Loot(Coords block, Selector kill, int? slot = null)
        {
            if (slot is null)
            {
                function.AddCommand(new LootCommand(new LootTargets.InsertTarget(block), new LootSources.KillSource(kill)));
            }
            else
            {
                function.AddCommand(new LootCommand(new LootTargets.BlockTarget(block, new Slots.ContainerSlot(slot.Value)), new LootSources.KillSource(kill)));
            }
        }

        /// <summary>
        /// Inserts the given <see cref="SharpCraft.Block"/>'s <see cref="LootTable"/> into the <see cref="SharpCraft.Block"/>
        /// </summary>
        /// <param name="block">the <see cref="SharpCraft.Block"/> to input into</param>
        /// <param name="breakBlock">the <see cref="SharpCraft.Block"/>'s <see cref="LootTable"/> to input</param>
        /// <param name="breakWith">the tool used to break the <see cref="SharpCraft.Block"/></param>
        /// <param name="slot">the slot to insert the <see cref="LootTable"/> at</param>
        public void Loot(Coords block, Coords breakBlock, Item breakWith = null, int? slot = null)
        {
            LootTargets.ILootTarget target;
            LootSources.ILootSource source;
            if (slot is null)
            {
                target = new LootTargets.InsertTarget(block);
            }
            else
            {
                target = new LootTargets.BlockTarget(block, new Slots.ContainerSlot(slot.Value));
            }
            if (breakWith is null)
            {
                source = new LootSources.MineItemSource(breakBlock, breakWith);
            }
            else
            {
                source = new LootSources.MineHandSource(breakBlock, true);
            }

            function.AddCommand(new LootCommand(target, source));
        }

        /// <summary>
        /// The data commands
        /// </summary>
        public ClassData Data;

        /// <summary>
        /// The data commands
        /// </summary>
        public class ClassData
        {
            readonly Function function;
            internal ClassData(Function function)
            {
                this.function = function;
            }

            /// <summary>
            /// Adds the given data to the <see cref="SharpCraft.Block"/>
            /// </summary>
            /// <param name="data">The data to give to the <see cref="SharpCraft.Block"/></param>
            /// <param name="place">the <see cref="Coords"/> of the <see cref="SharpCraft.Block"/> to give the data to</param>
            public void Change(Coords place, Data.SimpleDataHolder data)
            {
                function.AddCommand(new DataMergeBlockCommand(place, data));
            }

            /// <summary>
            /// Copies data into the <see cref="SharpCraft.Block"/>
            /// </summary>
            /// <param name="toBlock">the <see cref="Coords"/> of the <see cref="SharpCraft.Block"/> to copy the data to</param>
            /// <param name="toDataPath">the datapath to copy to</param>
            /// <param name="modifierType">The way to data should be copied in</param>
            /// <param name="copyData">The data to insert</param>
            public void Change(Coords toBlock, string toDataPath, ID.EntityDataModifierType modifierType, Data.SimpleDataHolder copyData)
            {
                function.AddCommand(new DataModifyWithDataCommand(new BlockDataLocation(toBlock, toDataPath), modifierType, copyData));
            }

            /// <summary>
            /// Copies data into the <see cref="SharpCraft.Block"/>
            /// </summary>
            /// <param name="toBlock">the <see cref="Coords"/> of the <see cref="SharpCraft.Block"/> to copy the data to</param>
            /// <param name="toDataPath">the datapath to copy to</param>
            /// <param name="index">The index to copy the data to</param>
            /// <param name="copyData">The data to insert</param>
            public void Change(Coords toBlock, string toDataPath, int index, Data.SimpleDataHolder copyData)
            {
                function.AddCommand(new DataModifyInsertDataCommand(new BlockDataLocation(toBlock, toDataPath), index, copyData));
            }

            /// <summary>
            /// Removes the data from the <see cref="SharpCraft.Block"/> at the given datapath
            /// </summary>
            /// <param name="dataPath">The datapath</param>
            /// <param name="place">the <see cref="Coords"/> of the <see cref="SharpCraft.Block"/> to remove the data from</param>
            public void Remove(Coords place, string dataPath)
            {
                function.AddCommand(new DataDeleteCommand(new BlockDataLocation(place, dataPath)));
            }

            /// <summary>
            /// Gets the numeric data from the datapath from the <see cref="SharpCraft.Block"/>
            /// </summary>
            /// <param name="place">the <see cref="Coords"/> of the <see cref="SharpCraft.Block"/> to get the data from</param>
            /// <param name="dataPath">the datapath to the data</param>
            /// <param name="scale">the number to multiply the numeric value with</param>
            public void Get(Coords place, string dataPath, double scale = 1)
            {
                function.AddCommand(new DataGetCommand(new BlockDataLocation(place, dataPath), scale));
            }

            /// <summary>
            /// Copies data into the <see cref="SharpCraft.Block"/>
            /// </summary>
            /// <param name="toBlock">the <see cref="Coords"/> of the <see cref="SharpCraft.Block"/> to copy the data to</param>
            /// <param name="toDataPath">the datapath to copy to</param>
            /// <param name="modifierType">The way to data should be copied in</param>
            /// <param name="fromSelector">the <see cref="Entity"/> to copy the data from</param>
            /// <param name="fromDataPath">The datapath to copy from</param>
            public void Copy(Coords toBlock, string toDataPath, ID.EntityDataModifierType modifierType, Selector fromSelector, string fromDataPath)
            {
                fromSelector.Limited();
                function.AddCommand(new DataModifyWithLocationCommand(new BlockDataLocation(toBlock, toDataPath), modifierType, new EntityDataLocation(fromSelector, fromDataPath)));
            }

            /// <summary>
            /// Copies data into the <see cref="SharpCraft.Block"/>
            /// </summary>
            /// <param name="toBlock">the <see cref="Coords"/> of the <see cref="SharpCraft.Block"/> to copy the data to</param>
            /// <param name="toDataPath">the datapath to copy to</param>
            /// <param name="modifierType">The way to data should be copied in</param>
            /// <param name="fromBlock">the <see cref="Coords"/> of the <see cref="SharpCraft.Block"/> to copy the data from</param>
            /// <param name="fromDataPath">The datapath to copy from</param>
            public void Copy(Coords toBlock, string toDataPath, ID.EntityDataModifierType modifierType, Coords fromBlock, string fromDataPath)
            {
                function.AddCommand(new DataModifyWithLocationCommand(new BlockDataLocation(toBlock, toDataPath), modifierType, new BlockDataLocation(fromBlock, fromDataPath)));
            }

            /// <summary>
            /// Copies data into the <see cref="SharpCraft.Block"/>
            /// </summary>
            /// <param name="toBlock">the <see cref="Coords"/> of the <see cref="SharpCraft.Block"/> to copy the data to</param>
            /// <param name="toDataPath">the datapath to copy to</param>
            /// <param name="index">The index to copy the data to</param>
            /// <param name="fromBlock">the <see cref="Coords"/> of the <see cref="SharpCraft.Block"/> to copy the data from</param>
            /// <param name="fromDataPath">The datapath to copy from</param>
            public void Copy(Coords toBlock, string toDataPath, int index, Coords fromBlock, string fromDataPath)
            {
                function.AddCommand(new DataModifyInsertLocationCommand(new BlockDataLocation(toBlock, toDataPath), index, new BlockDataLocation(fromBlock, fromDataPath)));
            }

            /// <summary>
            /// Copies data into the <see cref="SharpCraft.Block"/>
            /// </summary>
            /// <param name="toBlock">the <see cref="Coords"/> of the <see cref="SharpCraft.Block"/> to copy the data to</param>
            /// <param name="toDataPath">the datapath to copy to</param>
            /// <param name="index">The index to copy the data to</param>
            /// <param name="fromSelector">the <see cref="Entity"/> to copy the data from</param>
            /// <param name="fromDataPath">The datapath to copy from</param>
            public void Copy(Coords toBlock, string toDataPath, int index, Selector fromSelector, string fromDataPath)
            {
                fromSelector.Limited();
                function.AddCommand(new DataModifyInsertLocationCommand(new BlockDataLocation(toBlock, toDataPath), index, new EntityDataLocation(fromSelector, fromDataPath)));
            }
        }
    }
}