using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for end rod blocks
    /// </summary>
    public class EndRod : Block, Interfaces.IFacingFull
    {
        /// <summary>
        /// Creates an end rod block
        /// </summary>
        /// <param name="type">The type of block</param>
        public EndRod(BlockType? type) : base(type) { }

        /// <summary>
        /// Creates an end rod block
        /// </summary>
        /// <param name="type">The type of block</param>
        public EndRod(ID.Block type = SharpCraft.ID.Block.end_rod) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.end_rod;
        }

        /// <summary>
        /// The direction of the block its attached to.
        /// </summary>
        [BlockState("facing")]
        public ID.FacingFull? SFacing { get; set; }
    }
}
