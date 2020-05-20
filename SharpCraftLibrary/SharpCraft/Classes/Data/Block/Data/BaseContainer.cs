using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// A base class ofr blocks with inventories
    /// </summary>
    public abstract class BaseContainer : BaseInventory
    {
        /// <summary>
        /// Creates a new inventory block
        /// </summary>
        /// <param name="type">The type of block</param>
        public BaseContainer(ID.Block? type) : base(type) { }

        /// <summary>
        /// Creates a new inventory block
        /// </summary>
        /// <param name="type">The type of block</param>
        public BaseContainer(BlockType? type) : base(type) { }

        /// <summary>
        /// The loot table with items to put into the chest when opened
        /// </summary>
        [Data.DataTag("LootTable", ForceType = SharpCraft.ID.NBTTagType.TagString)]
        public LootTable? DLootTable { get; set; }

        /// <summary>
        /// The seed used to generate the loot
        /// </summary>
        [Data.DataTag("LootTableSeed")]
        public long? DLootTableSeed { get; set; }
    }
}
