using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for pumpkin blocks
        /// </summary>
        public class Pumpkin : Block
        {
            /// <summary>
            /// Creates a new pumpkin block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Pumpkin(ID.Block? type = SharpCraft.ID.Block.pumpkin) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Pumpkin(Group group) : base(group) { }

            /// <summary>
            /// The direction the pumpkin is facing
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? SFacing { get; set; }
        }
    }
}
