using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for wall torch blocks
        /// </summary>
        public class WallTorch : Block
        {
            /// <summary>
            /// Creates a new wall torch block
            /// </summary>
            /// <param name="type">The type of block</param>
            public WallTorch(ID.Block? type = SharpCraft.ID.Block.wall_torch) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public WallTorch(Group group) : base(group) { }

            /// <summary>
            /// The way the torch is facing. (The way it points)
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? SFacing { get; set; }
        }
    }
}
