using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for anvil blocks
    /// </summary>
    public class Stonecutter : Block, Interfaces.IFacing
    {

        /// <summary>
        /// Creates a stonecutter block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Stonecutter(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Stonecutter() : base(SharpCraft.ID.Block.stonecutter) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.stonecutter;
        }

        /// <summary>
        /// The direction the block is facing
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }
    }
}
