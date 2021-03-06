﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for sea picle blocks
    /// </summary>
    public class SeaPicles : Block, Interfaces.IWaterLogged
    {
        private int? _sPicles;

        /// <summary>
        /// Creates a sea picle block
        /// </summary>
        /// <param name="type">The type of block</param>
        public SeaPicles(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public SeaPicles() : base(SharpCraft.ID.Block.sea_pickle) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.sea_pickle;
        }

        /// <summary>
        /// The amount of picles in the block
        /// (1-4)
        /// </summary>
        [BlockState("pickles")]
        [BlockIntStateRange(1, 4)]
        public int? SPicles
        {
            get => _sPicles;
            set
            {
                if (value != null && (value < 1 || value > 4))
                {
                    throw new ArgumentException(nameof(SPicles) + " has to be equel to or between 1 and 4");
                }
                _sPicles = value;
            }
        }

        /// <summary>
        /// If the picles are water logged
        /// </summary>
        [BlockState("waterlogged")]
        public bool? SWaterLogged { get; set; }
    }
}
