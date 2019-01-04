using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for observer blocks
        /// </summary>
        public class Observer : Block
        {
            /// <summary>
            /// Creates a new observer block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Observer(ID.Block? type = SharpCraft.ID.Block.observer) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Observer(Group group) : base(group) { }

            /// <summary>
            /// The direction the observer is observing.
            /// </summary>
            [BlockData("facing")]
            public ID.FacingFull? SFacing { get; set; }

            /// <summary>
            /// If the observer has observed a change
            /// </summary>
            [BlockData("powered")]
            public bool? SPowered { get; set; }
        }
    }
}
