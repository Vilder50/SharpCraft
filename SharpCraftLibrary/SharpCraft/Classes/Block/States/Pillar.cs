using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for pillar blocks.
        /// (Blocks like bone blocks and purpur pillars)
        /// </summary>
        public class Pillar : Block
        {
            /// <summary>
            /// Creates a new pillar block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Pillar(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Pillar(Group group) : base(group) { }

            /// <summary>
            /// The axis the pillar is parallel to
            /// </summary>
            [BlockData("axis")]
            public ID.Axis? SAxis { get; set; }
        }
    }
}
