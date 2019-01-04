using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for vine blocks
        /// </summary>
        public class Vines : Block
        {
            /// <summary>
            /// Creates a new vine block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Vines(ID.Block? type = SharpCraft.ID.Block.vine) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Vines(Group group) : base(group) { }

            /// <summary>
            /// If the vine should be displayed in this direction
            /// </summary>
            [BlockData("down")]
            public bool? SDown { get; set; }

            /// <summary>
            /// If the vine should be displayed in this direction
            /// </summary>
            [BlockData("up")]
            public bool? SUp { get; set; }

            /// <summary>
            /// If the vine should be displayed in this direction
            /// </summary>
            [BlockData("east")]
            public bool? SEast { get; set; }

            /// <summary>
            /// If the vine should be displayed in this direction
            /// </summary>
            [BlockData("north")]
            public bool? SNorth { get; set; }

            /// <summary>
            /// If the vine should be displayed in this direction
            /// </summary>
            [BlockData("south")]
            public bool? SSouth { get; set; }

            /// <summary>
            /// If the vine should be displayed in this direction
            /// </summary>
            [BlockData("west")]
            public bool? SWest { get; set; }
        }
    }
}
