﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for lever blocks
    /// </summary>
    public class Lever : Block, Interfaces.IPlacedOn, Interfaces.IFacing, Interfaces.IPowered
    {
        /// <summary>
        /// Creates a lever block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Lever(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Lever() : base(SharpCraft.ID.Block.lever) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.lever;
        }

        /// <summary>
        /// The way the lever is placed
        /// </summary>
        [BlockState("face")]
        public ID.StatePlaced? SPlacedOn { get; set; }

        /// <summary>
        /// The way the lever is facing (oppesite of the direction of the block its placed on)
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }

        /// <summary>
        /// If the lever is turned on
        /// </summary>
        [BlockState("powered")]
        public bool? SPowered { get; set; }
    }
}
