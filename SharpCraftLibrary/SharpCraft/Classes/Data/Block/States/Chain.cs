using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for chain blocks
        /// </summary>
        public class Chain : Block, IBlock.IWaterLogged
        {
            /// <summary>
            /// Creates a chain block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Chain(BlockType type) : base(type) { }

            /// <summary>
            /// Creates a chain block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Chain(ID.Block type = SharpCraft.ID.Block.chain) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.chain;
            }

            /// <summary>
            /// If the chain is water logged
            /// </summary>
            [BlockState("waterlogged")]
            public bool? SWaterLogged { get; set; }
        }
    }
}
