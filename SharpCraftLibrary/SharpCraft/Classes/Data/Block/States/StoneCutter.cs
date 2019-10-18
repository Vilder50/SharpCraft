using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for anvil blocks
        /// </summary>
        public class Stonecutter : Block, IBlock.IFacing
        {

            /// <summary>
            /// Creates a stonecutter block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Stonecutter(BlockType type) : base(type) { }

            /// <summary>
            /// Creates a stonecutter block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Stonecutter(ID.Block type = SharpCraft.ID.Block.stonecutter) : base(type) { }

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
}
