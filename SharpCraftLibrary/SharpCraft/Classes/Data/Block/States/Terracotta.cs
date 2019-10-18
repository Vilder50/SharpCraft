using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for terracotta blocks
        /// </summary>
        public class Terracotta : Block, IBlock.IFacing
        {
            /// <summary>
            /// Creates a new terracotta block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Terracotta(BlockType type) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                string blockName = block.ToString();
                return (blockName.Contains("glazed"));
            }

            /// <summary>
            /// The direction the block is facing
            /// </summary>
            [BlockState("facing")]
            public ID.Facing? SFacing { get; set; }
        }
    }
}
