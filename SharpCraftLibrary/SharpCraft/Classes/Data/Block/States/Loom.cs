﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for loom blocks
    /// </summary>
    public class Loom : Block, Interfaces.IFacing
    {
        /// <summary>
        /// Creates a loom block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Loom(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Loom() : base(SharpCraft.ID.Block.loom) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.loom;
        }

        /// <summary>
        /// The direction the block is facing
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }
    }
}
