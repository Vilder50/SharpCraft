using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for stair blocks
        /// </summary>
        public class Stair : Block, IBlock.IFacing, IBlock.IWaterLogged
        {
            /// <summary>
            /// Creates a stair block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Stair(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Stair(Group group) : base(group) { }

            /// <summary>
            /// If the stair is water logged
            /// </summary>
            [BlockData("waterlogged")]
            public bool? SWaterLogged { get; set; }

            /// <summary>
            /// The shape of the stair
            /// </summary>
            [BlockData("shape")]
            public ID.StateStairShape? SShape { get; set; }

            /// <summary>
            /// The block the stair is placed on
            /// </summary>
            [BlockData("half")]
            public ID.StateSimplePlaced? SPlaced { get; set; }

            /// <summary>
            /// The direction the full stair part is facing
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? SFacing { get; set; }
        }
    }
}
