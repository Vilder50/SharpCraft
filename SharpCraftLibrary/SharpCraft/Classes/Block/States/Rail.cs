using System;
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
            /// Creates a new rail block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Rail(ID.Block? type = SharpCraft.ID.Block.rail) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Rail(Group group) : base(group) { }

            /// <summary>
            /// The direction the rail is going in
            /// </summary>
            [BlockData("direction")]
            public ID.StateRailShape? SDirection { get; set; }
        }
    }
}
