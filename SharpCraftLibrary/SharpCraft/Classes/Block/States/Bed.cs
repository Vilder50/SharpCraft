using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for anvil blocks
        /// </summary>
        public class Bed : Block
        {
            /// <summary>
            /// Creates a new bed block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Bed(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Bed(Group group) : base(group) { }

            /// <summary>
            /// The direction the bed is facing
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? SFacing { get; set; }

            /// <summary>
            /// If the bed is occupied
            /// </summary>
            [BlockData("occupied")]
            public bool? SOccupied { get; set; }

            /// <summary>
            /// The direction the bed is facing
            /// </summary>
            [BlockData("part")]
            public ID.StateBedPart? SPart { get; set; }
        }
    }
}
