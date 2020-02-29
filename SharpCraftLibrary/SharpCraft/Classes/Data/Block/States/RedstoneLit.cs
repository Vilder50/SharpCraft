using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for redstone lamp, ground torches and ore blocks
        /// </summary>
        public class RedstoneLit : Block
        {
            /// <summary>
            /// Creates a new redstone lamp, ground torches or ore block
            /// </summary>
            /// <param name="type">The type of block</param>
            public RedstoneLit(BlockType? type) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.redstone_torch || block == SharpCraft.ID.Block.redstone_lamp || block == SharpCraft.ID.Block.redstone_ore;
            }

            /// <summary>
            /// If the block is lit up
            /// </summary>
            [BlockState("lit")]
            public bool? SLit { get; set; }
        }
    }
}
