using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for terracotta blocks
        /// </summary>
        public class Terracotta : Block, IBlock.IFacing
        {
            /// <summary>
            /// Creates a new terracotta block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Terracotta(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Terracotta(Group group) : base(group) { }

            /// <summary>
            /// The direction the block is facing
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? SFacing { get; set; }
        }
    }
}
