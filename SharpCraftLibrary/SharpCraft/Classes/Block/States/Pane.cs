using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for pane blocks
        /// </summary>
        public class Pane : Block, IBlock.IConnectedCardinal, IBlock.IWaterLogged
        {
            /// <summary>
            /// Creates a new pane block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Pane(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Pane(Group group) : base(group) { }

            /// <summary>
            /// If the block is connected in this direction
            /// </summary>
            [BlockData("east")]
            public bool? SEast { get; set; }

            /// <summary>
            /// If the block is connected in this direction
            /// </summary>
            [BlockData("north")]
            public bool? SNorth { get; set; }

            /// <summary>
            /// If the block is connected in this direction
            /// </summary>
            [BlockData("south")]
            public bool? SSouth { get; set; }

            /// <summary>
            /// If the block is connected in this direction
            /// </summary>
            [BlockData("west")]
            public bool? SWest { get; set; }

            /// <summary>
            /// If the block is water logged
            /// </summary>
            [BlockData("waterlogged")]
            public bool? SWaterLogged { get; set; }
        }
    }
}
