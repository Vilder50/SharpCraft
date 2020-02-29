﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for rail blocks
        /// </summary>
        public class Rail : Block
        {
            /// <summary>
            /// Creates a rail block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Rail(BlockType? type) : base(type) { }

            /// <summary>
            /// Creates a rail block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Rail(ID.Block type = SharpCraft.ID.Block.rail) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.rail;
            }

            /// <summary>
            /// The direction the rail is going in
            /// </summary>
            [BlockState("direction")]
            public ID.StateRailShape? SDirection { get; set; }
        }
    }
}
