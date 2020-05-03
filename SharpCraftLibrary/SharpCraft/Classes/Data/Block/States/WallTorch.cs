﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for wall torch blocks
        /// </summary>
        public class WallTorch : Block, IBlock.IFacing
        {
            /// <summary>
            /// Creates a torch block
            /// </summary>
            /// <param name="type">The type of block</param>
            public WallTorch(BlockType type) : base(type) { }

            /// <summary>
            /// Creates a torch block
            /// </summary>
            /// <param name="type">The type of block</param>
            public WallTorch(ID.Block type = SharpCraft.ID.Block.wall_torch) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.wall_torch || block == SharpCraft.ID.Block.soul_wall_touch;
            }

            /// <summary>
            /// The way the torch is facing. (The way it points)
            /// </summary>
            [BlockState("facing")]
            public ID.Facing? SFacing { get; set; }
        }
    }
}
