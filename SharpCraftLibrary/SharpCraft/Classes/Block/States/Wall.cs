using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for wall blocks
        /// </summary>
        public class Wall : Fence
        {
            /// <summary>
            /// Creates a new wall block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Wall(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Wall(Group group) : base(group) { }

            /// <summary>
            /// If the block is connected in this direction
            /// </summary>
            [BlockData("up")]
            public bool? SUp { get; set; }
        }
    }
}
