using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for grindstone blocks
        /// </summary>
        public class Grindstone : Block
        {
            /// <summary>
            /// Creates a grindstone block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Grindstone(ID.Block? type = SharpCraft.ID.Block.grindstone) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Grindstone(Group group) : base(group) { }

            /// <summary>
            /// The way the grindstone is placed
            /// </summary>
            [BlockData("face")]
            public ID.StatePlaced? SPlaced { get; set; }

            /// <summary>
            /// The way the grindstone faces
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? SFacing { get; set; }
        }
    }
}
