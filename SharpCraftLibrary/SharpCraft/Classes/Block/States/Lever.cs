using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for lever blocks
        /// </summary>
        public class Lever : Block, IBlock.IPlacedOn, IBlock.IFacing, IBlock.IPowered
        {
            /// <summary>
            /// Creates a new lever block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Lever(ID.Block? type = SharpCraft.ID.Block.lever) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Lever(Group group) : base(group) { }

            /// <summary>
            /// The way the lever is placed
            /// </summary>
            [BlockData("face")]
            public ID.StatePlaced? SPlacedOn { get; set; }

            /// <summary>
            /// The way the lever is facing (oppesite of the direction of the block its placed on)
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? SFacing { get; set; }

            /// <summary>
            /// If the lever is turned on
            /// </summary>
            [BlockData("powered")]
            public bool? SPowered { get; set; }
        }
    }
}
