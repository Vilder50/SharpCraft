using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for door blocks
        /// </summary>
        public class Door : Block
        {
            /// <summary>
            /// Creates a new door block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Door(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Door(Group group) : base(group) { }

            /// <summary>
            /// The direction the door is placed in.
            /// (The direction the door will fill less in.)
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? SFacing { get; set; }

            /// <summary>
            /// The part of the door
            /// </summary>
            [BlockData("half")]
            public ID.StatePart? SPart { get; set; }

            /// <summary>
            /// What side the hinges are on
            /// </summary>
            [BlockData("hinge")]
            public ID.StateDoorHinge? SHingeLocation { get; set; }

            /// <summary>
            /// If the door is open
            /// </summary>
            [BlockData("open")]
            public bool? SOpen { get; set; }

            /// <summary>
            /// If the door is powered by redstone
            /// </summary>
            [BlockData("powered")]
            public bool? SPowered { get; set; }
        }
    }
}
