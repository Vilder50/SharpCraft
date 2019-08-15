using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for loom blocks
        /// </summary>
        public class Loom : Block, IBlock.IFacing
        {
            /// <summary>
            /// Creates a new loom block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Loom(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Loom(Group group) : base(group) { }

            /// <summary>
            /// The direction the block is facing
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? SFacing { get; set; }
        }
    }
}
