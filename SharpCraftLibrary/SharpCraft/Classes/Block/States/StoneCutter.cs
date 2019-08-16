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
        public class StoneCutter : Block, IBlock.IFacing
        {
            /// <summary>
            /// Creates a new anvil block
            /// </summary>
            /// <param name="type">The type of block</param>
            public StoneCutter(ID.Block? type = SharpCraft.ID.Block.stonecutter) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public StoneCutter(Group group) : base(group) { }

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
            [BlockData("facing")]
            public ID.Facing? SFacing { get; set; }
        }
    }
}
