using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for respawn anchor blocks
    /// </summary>
    public class RespawnAnchor : Block
    {
        private int? sCharges;

        /// <summary>
        /// Creates a respawn anchor block
        /// </summary>
        /// <param name="type">The type of block</param>
        public RespawnAnchor(IBlockType type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public RespawnAnchor() : base(SharpCraft.ID.Block.respawn_anchor) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.respawn_anchor;
        }

        /// <summary>
        /// How many charges the anchor has left
        /// </summary>
        [BlockState("charges")]
        [BlockIntStateRange(0, 4)]
        public int? SCharges
        {
            get => sCharges;
            set
            {
                if (value != null && (value < 0 || value > 4))
                {
                    throw new ArgumentException(nameof(SCharges) + " has to be equel to or between 0 and 4");
                }
                sCharges = value;
            }
        }
    }
}
