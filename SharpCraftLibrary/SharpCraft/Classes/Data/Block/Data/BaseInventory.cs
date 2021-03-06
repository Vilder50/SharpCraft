﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// A base class ofr blocks with inventories
    /// </summary>
    public abstract class BaseInventory : BaseBlockEntity
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<BaseInventory> PathCreator => new Data.DataPathCreator<BaseInventory>();

        /// <summary>
        /// Creates a new inventory block
        /// </summary>
        /// <param name="type">The type of block</param>
        public BaseInventory(ID.Block? type) : base(type) { }

        /// <summary>
        /// Creates a new inventory block
        /// </summary>
        /// <param name="type">The type of block</param>
        public BaseInventory(IBlockType? type) : base(type) { }

        /// <summary>
        /// The inventory's lock.
        /// (Players have to use an item with the given name to open the block)
        /// </summary>
        [Data.DataTag("Lock")]
        public string? DLock { get; set; }
        /// <summary>
        /// The custom name displayed at the top left corner when you open the block
        /// </summary>

        [Data.DataTag("CustomName", ForceType = SharpCraft.ID.NBTTagType.TagString)]
        public BaseJsonText? DCustomName { get; set; }
        /// <summary>
        /// The items in the inventory
        /// </summary>

        [Data.DataTag("Items")]
        public abstract Item[]? DItems { get; set; }
    }
}
