﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for tnt blocks
    /// </summary>
    public class TNT : Block
    {
        /// <summary>
        /// Creates a tnt block
        /// </summary>
        /// <param name="type">The type of block</param>
        public TNT(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public TNT() : base(SharpCraft.ID.Block.tnt) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.tnt;
        }

        /// <summary>
        /// If the tnt gets ignited when broken
        /// </summary>
        [BlockState("unstable")]
        public bool? SUnstable { get; set; }
    }
}
