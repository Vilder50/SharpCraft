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
        public class Button : Block, IBlock.IFacing, IBlock.IPlacedOn, IBlock.IPowered
        {
            /// <summary>
            /// Intilizes a new block object
            /// </summary>
            public Button()
            {
                ID = null;
            }

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
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block.ToString().Contains("_button");
            }

            /// <summary>
            /// The way the button is placed
            /// </summary>
            [BlockState("face")]
            public ID.StatePlaced? SPlacedOn { get; set; }

            /// <summary>
            /// The way the button is facing
            /// </summary>
            [BlockState("facing")]
            public ID.Facing? SFacing { get; set; }

            /// <summary>
            /// If the button is clicked
            /// </summary>
            [BlockState("powered")]
            public bool? SPowered { get; set; }
        }
    }
}
