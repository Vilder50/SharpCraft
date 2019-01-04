using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for special rail blocks
        /// </summary>
        public class SpecialRail : Block
        {
            /// <summary>
            /// Creates a new special rail block
            /// </summary>
            /// <param name="type">The type of block</param>
            public SpecialRail(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public SpecialRail(Group group) : base(group) { }

            /// <summary>
            /// The direction the rail is going in.
            /// </summary>
            [BlockData("direction")]
            public ID.StateSpecailRailShape? SDirection { get; set; }

            /// <summary>
            /// If the rail is activated
            /// </summary>
            [BlockData("powered")]
            public bool? SPowered { get; set; }
        }
    }
}
