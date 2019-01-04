using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for piston blocks
        /// </summary>
        public class Piston : Block
        {
            /// <summary>
            /// Creates a new piston block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Piston(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Piston(Group group) : base(group) { }

            /// <summary>
            /// The direction the piston will push.
            /// </summary>
            [BlockData("facing")]
            public ID.FacingFull? SFacing { get; set; }

            /// <summary>
            /// If the piston is extendend
            /// </summary>
            [BlockData("extended")]
            public bool? SExtended { get; set; }
        }
    }
}
