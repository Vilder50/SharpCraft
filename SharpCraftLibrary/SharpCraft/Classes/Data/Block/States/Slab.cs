﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for slab blocks
    /// </summary>
    public class Slab : Block, Interfaces.IWaterLogged
    {

        /// <summary>
        /// Creates a new slab block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Slab(IBlockType? type) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            string blockName = block.ToString();
            return (blockName.Contains("slab"));
        }

        /// <summary>
        /// If the slab is water logged
        /// </summary>
        [BlockState("waterlogged")]
        public bool? SWaterLogged { get; set; }

        /// <summary>
        /// The type of slab
        /// </summary>
        [BlockState("type")]
        public ID.StateSlabPart? SPart { get; set; }
    }
}
