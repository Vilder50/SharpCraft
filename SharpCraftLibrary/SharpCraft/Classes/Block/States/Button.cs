using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for button blocks.
        /// </summary>
        public class Button : Block
        {
            /// <summary>
            /// Creates a new button block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Button(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Button(Group group) : base(group) { }

            /// <summary>
            /// The way the button is placed
            /// </summary>
            [BlockData("face")]
            public ID.StatePlaced? PlacedOn { get; set; }

            /// <summary>
            /// The way the button is facing
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? Facing { get; set; }

            /// <summary>
            /// If the button is clicked
            /// </summary>
            [BlockData("powered")]
            public bool? Clicked { get; set; }
        }
    }
}
