using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for pane blocks
        /// </summary>
        public class Pane : Fence
        {
            /// <summary>
            /// Creates a new pane block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Pane(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Pane(Group group) : base(group) { }
        }
    }
}
