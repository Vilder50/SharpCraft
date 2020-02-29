using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for loom blocks
        /// </summary>
        public class Loom : Block, IBlock.IFacing
        {
            /// <summary>
            /// Creates a loom block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Loom(BlockType? type) : base(type) { }

            /// <summary>
            /// Creates a loom block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Loom(ID.Block type = SharpCraft.ID.Block.loom) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.loom;
            }

            /// <summary>
            /// The direction the block is facing
            /// </summary>
            [BlockState("facing")]
            public ID.Facing? SFacing { get; set; }
        }
    }
}
