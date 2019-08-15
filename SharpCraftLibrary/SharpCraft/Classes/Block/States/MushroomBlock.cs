using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for mushroom blocks
        /// </summary>
        public class MushroomBlock : Block, IBlock.IConnected
        {
            /// <summary>
            /// Creates a new mushroom block
            /// </summary>
            /// <param name="type">The type of block</param>
            public MushroomBlock(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public MushroomBlock(Group group) : base(group) { }

            /// <summary>
            /// If the texture should be shown downwards.
            /// False will show pores texture
            /// </summary>
            [BlockData("down")]
            public bool? SDown { get; set; }

            /// <summary>
            /// If the texture should be shown upwards.
            /// False will show pores texture
            /// </summary>
            [BlockData("up")]
            public bool? SUp { get; set; }

            /// <summary>
            /// If the texture should be in east.
            /// False will show pores texture
            /// </summary>
            [BlockData("east")]
            public bool? SEast { get; set; }

            /// <summary>
            /// If the texture should be in north.
            /// False will show pores texture
            /// </summary>
            [BlockData("north")]
            public bool? SNorth { get; set; }

            /// <summary>
            /// If the texture should be in south.
            /// False will show pores texture
            /// </summary>
            [BlockData("south")]
            public bool? SSouth { get; set; }

            /// <summary>
            /// If the texture should be in west.
            /// False will show pores texture
            /// </summary>
            [BlockData("west")]
            public bool? SWest { get; set; }
        }
    }
}
