using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for redstone lamp and ore blocks
        /// </summary>
        public class RedstoneLit : Block
        {
            /// <summary>
            /// Creates a new redstone lamp  or ore block
            /// </summary>
            /// <param name="type">The type of block</param>
            public RedstoneLit(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public RedstoneLit(Group group) : base(group) { }


            /// <summary>
            /// If the block is lit up
            /// </summary>
            [BlockData("lit")]
            public bool? SLit { get; set; }
        }
    }
}
