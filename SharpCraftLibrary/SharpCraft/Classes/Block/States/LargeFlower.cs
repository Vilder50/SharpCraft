using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for large flower blocks
        /// </summary>
        public class LargeFlower : Block
        {
            /// <summary>
            /// Creates a new large flower block
            /// </summary>
            /// <param name="type">The type of block</param>
            public LargeFlower(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public LargeFlower(Group group) : base(group) { }

            /// <summary>
            /// The part of the flower
            /// </summary>
            [BlockData("half")]
            public ID.StatePart? SPart { get; set; }
        }
    }
}
