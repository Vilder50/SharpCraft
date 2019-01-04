using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for tripwire blocks
        /// </summary>
        public class Tripwire : Block
        {
            /// <summary>
            /// Creates a tripwire block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Tripwire(ID.Block? type = SharpCraft.ID.Block.tripwire) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Tripwire(Group group) : base(group) { }

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
            /// If the tripwire is attached to tripwire hook
            /// </summary>
            [BlockData("attached")]
            public bool? SAttached { get; set; }

            /// <summary>
            /// If the tripwire has been disarmed
            /// </summary>
            [BlockData("disarmed")]
            public bool? SDisarmed { get; set; }

            /// <summary>
            /// If the tripwire is being stepped on
            /// </summary>
            [BlockData("powered")]
            public bool? SSteppedDown { get; set; }
        }
    }
}
