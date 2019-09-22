namespace SharpCraft.FunctionWriters
{
    /// <summary>
    /// All the block commands
    /// </summary>
    public class BlockCommands
    {
        readonly Function.FunctionWriter writer;
        internal BlockCommands(Function.FunctionWriter commandsList)
        {
            writer = commandsList;
            Data = new ClassData(writer);
        }

        /// <summary>
        /// Adds a <see cref="SharpCraft.Block"/> at the given <see cref="Coords"/>
        /// </summary>
        /// <param name="addBlock">the <see cref="SharpCraft.Block"/> to add</param>
        /// <param name="blockCoords">the <see cref="Coords"/> to add it at</param>
        /// <param name="type">how to place the block</param>
        public void Add(Block addBlock, Coords blockCoords, ID.BlockAdd type = ID.BlockAdd.replace)
        {
            writer.Add("setblock " + blockCoords + " " + addBlock);
            if (type != ID.BlockAdd.replace) { writer.Add(" " + type.ToString()); }
            writer.NewLine();
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
            writer.Add("fill " + corner1 + " " + corner2 + " " + fillBlock);
            if (type != ID.BlockFill.replace) { writer.Add(" " + type.ToString()); }
            if (type == ID.BlockFill.replace && replaceBlock != null) { writer.Add(" " + type.ToString() + " " + replaceBlock); }
            writer.NewLine();
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
            writer.Add("clone " + corner1 + " " + corner2 + " " + copyTo);
            if (type != ID.BlockClone.replace || way != ID.BlockCloneWay.normal) { writer.Add(" " + type.ToString()); }
            if (type == ID.BlockClone.filtered && filteredBlock != null) { writer.Add(" " + filteredBlock); }
            if (way != ID.BlockCloneWay.normal) { writer.Add(" " + way); }
            writer.NewLine();
        }

        /// <summary>
        /// Inserts the given <see cref="Item"/> into the <see cref="SharpCraft.Block"/>
        /// </summary>
        /// <param name="BlockCoords">The <see cref="SharpCraft.Block"/> to insert into</param>
        /// <param name="AddItem">The <see cref="Item"/> to insert (<see cref="Item.Slot"/> choses the slot)</param>
        public void AddItem(Coords BlockCoords, Item AddItem)
        {
            if (AddItem.Slot == null || AddItem.Slot > 53 || AddItem.Slot < 0)
            {
                AddItem.Slot = 0;
            }
            writer.Add("replaceitem block " + BlockCoords + " container." + AddItem.Slot + " " + AddItem.IDDataString + " " + (AddItem.Count ?? 1));
            writer.NewLine();
        }

        /// <summary>
        /// Inserts the given <see cref="Loottable"/> into the <see cref="SharpCraft.Block"/>
        /// </summary>
        /// <param name="block">the <see cref="SharpCraft.Block"/> to input into</param>
        /// <param name="loot">the <see cref="Loottable"/> to input</param>
        /// <param name="slot">the slot to insert the <see cref="Loottable"/> at</param>
        public void Loot(Coords block, Loottable loot, int? slot = null)
        {
            writer.Add($"loot {(slot is null ? "insert" : "replace block")} {block}{(slot is null ? "" : $" container.{slot}")} loot {loot}");
            writer.NewLine();
        }

        /// <summary>
        /// Inserts the given <see cref="Entity"/>'s <see cref="Loottable"/> into the <see cref="SharpCraft.Block"/>
        /// </summary>
        /// <param name="block">the <see cref="SharpCraft.Block"/> to input into</param>
        /// <param name="kill">the <see cref="Entity"/>'s <see cref="Loottable"/> to input</param>
        /// <param name="slot">the slot to insert the <see cref="Loottable"/> at</param>
        public void Loot(Coords block, Selector kill, int? slot = null)
        {
            writer.Add($"loot {(slot is null ? "insert" : "replace block")} {block}{(slot is null ? "" : $" container.{slot}")} kill {kill}");
            writer.NewLine();
        }

        /// <summary>
        /// Inserts the given <see cref="SharpCraft.Block"/>'s <see cref="Loottable"/> into the <see cref="SharpCraft.Block"/>
        /// </summary>
        /// <param name="block">the <see cref="SharpCraft.Block"/> to input into</param>
        /// <param name="breakBlock">the <see cref="SharpCraft.Block"/>'s <see cref="Loottable"/> to input</param>
        /// <param name="breakWith">the tool used to break the <see cref="SharpCraft.Block"/></param>
        /// <param name="slot">the slot to insert the <see cref="Loottable"/> at</param>
        public void Loot(Coords block, Coords breakBlock, Item breakWith = null, int slot = -1)
        {
            writer.Add($"loot {(slot == -1 ? "insert" : "replace block")} {block}{(slot == -1 ? "" : $" container.{slot}")} mine {breakBlock}");
            if (breakWith != null)
            {
                writer.Add(" " + breakWith);
            }
            writer.NewLine();
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
            readonly Function.FunctionWriter writer;
            internal ClassData(Function.FunctionWriter commandsList)
            {
                writer = commandsList;
            }

            /// <summary>
            /// Adds the given data to the <see cref="SharpCraft.Block"/>
            /// </summary>
            /// <param name="data">The data to give to the <see cref="SharpCraft.Block"/></param>
            /// <param name="place">the <see cref="Coords"/> of the <see cref="SharpCraft.Block"/> to give the data to</param>
            public void Change(Coords place, Block data)
            {
                if (!data.HasData) { throw new System.ArgumentException(nameof(data) + " was supposed to have data"); }
                writer.Add("data merge block " + place + " " + data.GetDataString());
                writer.NewLine();
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
                writer.Add($"data modify block {toBlock} {toDataPath} {modifierType} value {copyData.GetDataString()}");
                writer.NewLine();
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
                writer.Add($"data modify block {toBlock} {toDataPath} insert {System.Math.Abs(index)} value {copyData.GetDataString()}");
                writer.NewLine();
            }

            /// <summary>
            /// Removes the data from the <see cref="SharpCraft.Block"/> at the given datapath
            /// </summary>
            /// <param name="dataPath">The datapath</param>
            /// <param name="place">the <see cref="Coords"/> of the <see cref="SharpCraft.Block"/> to remove the data from</param>
            public void Remove(Coords place, string dataPath)
            {
                writer.Add("data remove block " + place + " " + dataPath);
                writer.NewLine();
            }

            /// <summary>
            /// Gets the numeric data from the datapath from the <see cref="SharpCraft.Block"/>
            /// </summary>
            /// <param name="place">the <see cref="Coords"/> of the <see cref="SharpCraft.Block"/> to get the data from</param>
            /// <param name="dataPath">the datapath to the data</param>
            /// <param name="scale">the number to multiply the numeric value with</param>
            public void Get(Coords place, string dataPath, double scale = 1)
            {
                writer.Add("data get block " + place + " " + dataPath);
                if (scale != 1) { writer.Add(" " + scale.ToMinecraftDouble()); }
                writer.NewLine();
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
                writer.Add($"data modify block {toBlock} {toDataPath} {modifierType} from entity {fromSelector} {fromDataPath}");
                writer.NewLine();
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
                writer.Add($"data modify block {toBlock} {toDataPath} {modifierType} from block {fromBlock} {fromDataPath}");
                writer.NewLine();
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
                writer.Add($"data modify block {toBlock} {toDataPath} insert {System.Math.Abs(index)} from block {fromBlock} {fromDataPath}");
                writer.NewLine();
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
                writer.Add($"data modify block {toBlock} {toDataPath} insert {System.Math.Abs(index)} from entity {fromSelector} {fromDataPath}");
                writer.NewLine();
            }
        }
    }
}