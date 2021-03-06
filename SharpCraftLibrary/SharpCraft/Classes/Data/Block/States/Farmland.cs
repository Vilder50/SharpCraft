﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for farm land blocks
    /// </summary>
    public class Farmland : Block
    {
        private int? _sHydration;

        /// <summary>
        /// Creates a farmland block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Farmland(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Farmland() : base(SharpCraft.ID.Block.farmland) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.farmland;
        }

        /// <summary>
        /// How hydrated the farmland is.
        /// (0-7. 7 == fully hydrated)
        /// </summary>
        [BlockState("moisture")]
        public int? SHydration
        {
            get => _sHydration;
            set
            {
                if (value != null && (value < 0 || value > 7))
                {
                    throw new ArgumentException(nameof(SHydration) + " has to be equel to or between 0 and 7");
                }
                _sHydration = value;
            }
        }
    }
}
