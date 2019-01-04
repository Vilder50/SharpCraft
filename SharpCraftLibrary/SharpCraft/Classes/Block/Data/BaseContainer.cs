using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// A base class ofr blocks with inventories
        /// </summary>
        public abstract class BaseContainer<T> : BaseInventory<T> where T : Block
        {
            /// <summary>
            /// Creates a new inventory block
            /// </summary>
            /// <param name="type">The type of block</param>
            public BaseContainer(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public BaseContainer(Group group) : base(group) { }

            /// <summary>
            /// The loot table with items to put into the chest when opened
            /// </summary>
            [BlockData]
            public Loottable DLootTable { get; set; }
            /// <summary>
            /// The seed used to generate the loot
            /// </summary>
            [BlockData]
            public long? DLootTableSeed { get; set; }

            /// <summary>
            /// Gets the raw data for the inventory data
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string GetDataString()
            {
                List<string> TempList = new List<string>();

                string InventoryData = base.GetDataString();
                if (InventoryData.Length != 0) { TempList.Add(InventoryData); }
                if (DLootTable != null) { TempList.Add("LootTable:\"" + DLootTable + "\""); }
                if (DLootTableSeed != null) { TempList.Add("LootTableSeed:" + DLootTableSeed + "L"); }

                return string.Join(",", TempList);
            }
        }
    }
}
