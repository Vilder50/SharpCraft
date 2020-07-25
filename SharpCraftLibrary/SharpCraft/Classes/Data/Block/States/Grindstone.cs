using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for grindstone blocks
    /// </summary>
    public class Grindstone : Block, Interfaces.IPlacedOn, Interfaces.IFacing
    {
        /// <summary>
        /// Creates a grindstone block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Grindstone(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Grindstone() : base(SharpCraft.ID.Block.grindstone) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.grindstone;
        }

        /// <summary>
        /// The way the grindstone is placed
        /// </summary>
        [BlockState("face")]
        public ID.StatePlaced? SPlacedOn { get; set; }

        /// <summary>
        /// The way the grindstone faces
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }
    }
}
