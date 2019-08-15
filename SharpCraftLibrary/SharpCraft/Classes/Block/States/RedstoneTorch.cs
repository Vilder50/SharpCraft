using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for redstone torches
        /// </summary>
        public class RedstoneWallTorch : Block, IBlock.IFacing, IBlock.ILit
        {
            /// <summary>
            /// Creates a new redstone torch
            /// </summary>
            /// <param name="type">The type of block</param>
            public RedstoneWallTorch(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public RedstoneWallTorch(Group group) : base(group) { }
            
            /// <summary>
            /// The way the torch is facing. (The way it points)
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? SFacing { get; set; }

            /// <summary>
            /// If the block is lit up
            /// </summary>
            [BlockData("lit")]
            public bool? SLit { get; set; }
        }
    }
}
