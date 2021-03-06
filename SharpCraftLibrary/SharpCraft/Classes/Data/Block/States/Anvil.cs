﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for anvil blocks
    /// </summary>
    public class Anvil : Block, Interfaces.IFacing
    {
        /// <summary>
        /// Creates a new anvil block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Anvil(IBlockType? type) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.anvil || block == SharpCraft.ID.Block.chipped_anvil || block == SharpCraft.ID.Block.damaged_anvil;
        }

        /// <summary>
        /// The direction the block is facing
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }
    }
}
