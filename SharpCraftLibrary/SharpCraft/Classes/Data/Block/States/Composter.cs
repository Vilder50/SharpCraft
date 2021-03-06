﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for composter blocks
    /// </summary>
    public class Composter : Block, Interfaces.ILevel
    {
        private int? _sLevel;

        /// <summary>
        /// Creates a composter block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Composter(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Composter() : base(SharpCraft.ID.Block.composter) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.composter;
        }

        /// <summary>
        /// How much compost there is in the composter
        /// (0-8. 0 = compost. 8 = full)
        /// </summary>
        [BlockState("level")]
        [BlockIntStateRange(0, 8)]
        public int? SLevel
        {
            get => _sLevel;
            set
            {
                if (value != null && (value < 0 || value > 8))
                {
                    throw new ArgumentException(nameof(SLevel) + " has to be equel to or between 0 and 8");
                }
                _sLevel = value;
            }
        }
    }
}
