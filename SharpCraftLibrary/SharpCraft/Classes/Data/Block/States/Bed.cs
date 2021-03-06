﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for anvil blocks
    /// </summary>
    public class Bed : Block, Interfaces.IFacing
    {
        /// <summary>
        /// Creates a new bed block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Bed(IBlockType? type) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block.ToString().Contains("_bed");
        }

        /// <summary>
        /// The direction the bed is facing
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }

        /// <summary>
        /// If the bed is occupied
        /// </summary>
        [BlockState("occupied")]
        public bool? SOccupied { get; set; }

        /// <summary>
        /// The part of the bed this block is
        /// </summary>
        [BlockState("part")]
        public ID.StateBedPart? SPart { get; set; }
    }
}
