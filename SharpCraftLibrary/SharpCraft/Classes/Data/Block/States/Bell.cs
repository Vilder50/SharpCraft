using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for bell blocks
    /// </summary>
    public class Bell : Block, Interfaces.IFacing
    {
        /// <summary>
        /// Creates a bell block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Bell(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Bell() : base(SharpCraft.ID.Block.bell) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.bell;
        }

        /// <summary>
        /// The way the bell is attached
        /// </summary>
        [BlockState("attachment")]
        public ID.StateBellAttachment? SAttachement { get; set; }

        /// <summary>
        /// The way the bell faces
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }
    }
}
