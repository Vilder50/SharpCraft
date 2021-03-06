﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for ladder blocks
    /// </summary>
    public class Ladder : Block, Interfaces.IFacing, Interfaces.IWaterLogged
    {
        /// <summary>
        /// Creates a ladder block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Ladder(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Ladder() : base(SharpCraft.ID.Block.ladder) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.ladder;
        }

        /// <summary>
        /// The direction of the block the ladder is on
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }

        /// <summary>
        /// If the ladder is water logged
        /// </summary>
        [BlockState("waterlogged")]
        public bool? SWaterLogged { get; set; }
    }
}
