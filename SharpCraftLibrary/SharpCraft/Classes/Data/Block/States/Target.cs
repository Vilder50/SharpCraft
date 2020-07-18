using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for anvil blocks
    /// </summary>
    public class Target : Block, Interfaces.IPower
    {
        private int? _sPower;

        /// <summary>
        /// Creates a daylight detector block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Target(IBlockType type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Target() : base(SharpCraft.ID.Block.target) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.target;
        }

        /// <summary>
        /// How much redstone power it outputs
        /// (0-15)
        /// </summary>
        [BlockState("power")]
        public int? SPower
        {
            get => _sPower;
            set
            {
                if (value != null && (value < 0 || value > 15))
                {
                    throw new ArgumentException(nameof(SPower) + " has to be equel to or between 0 and 15");
                }
                _sPower = value;
            }
        }
    }
}
