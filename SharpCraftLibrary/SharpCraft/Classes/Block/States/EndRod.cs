using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for end rod blocks
        /// </summary>
        public class EndRod : Block
        {
            /// <summary>
            /// Creates a new end rod block
            /// </summary>
            /// <param name="type">The type of block</param>
            public EndRod(ID.Block? type = SharpCraft.ID.Block.end_rod) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public EndRod(Group group) : base(group) { }

            /// <summary>
            /// The direction of the block its attached to.
            /// </summary>
            [BlockData("facing")]
            public ID.FacingFull? SAttached { get; set; }
        }
    }
}
