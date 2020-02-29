using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for pumpkin blocks
    /// </summary>
    public class Pumpkin : Block, Interfaces.IFacing
    {
        /// <summary>
        /// Creates a pumpkin block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Pumpkin(BlockType? type) : base(type) { }

        /// <summary>
        /// Creates a pumpkin block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Pumpkin(ID.Block type = SharpCraft.ID.Block.carved_pumpkin) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.carved_pumpkin;
        }

        /// <summary>
        /// The direction the pumpkin is facing
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }
    }
}
