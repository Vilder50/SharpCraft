﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
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
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public BaseContainer(Group group) : base(group) { }

            /// <summary>
            /// The loot table with items to put into the chest when opened
            /// </summary>
            [Data.DataTag("LootTable", ForceType = SharpCraft.ID.NBTTagType.TagString)]
            public Loottable DLootTable { get; set; }

            /// <summary>
            /// The seed used to generate the loot
            /// </summary>
            [Data.DataTag("LootTableSeed")]
            public long? DLootTableSeed { get; set; }
        }
    }
}
