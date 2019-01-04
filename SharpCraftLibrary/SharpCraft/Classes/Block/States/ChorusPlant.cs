using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for chorus plant blocks
        /// </summary>
        public class ChorusPlant : Block
        {
            /// <summary>
            /// Creates a new chorus plant block
            /// </summary>
            /// <param name="type">The type of block</param>
            public ChorusPlant(ID.Block? type = SharpCraft.ID.Block.chorus_plant) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public ChorusPlant(Group group) : base(group) { }

            /// <summary>
            /// If the plant is connected downwards
            /// </summary>
            [BlockData("down")]
            public bool? SDown { get; set; }

            /// <summary>
            /// If the plant is connected upwards
            /// </summary>
            [BlockData("up")]
            public bool? SUp { get; set; }

            /// <summary>
            /// If the plant is connected in east
            /// </summary>
            [BlockData("east")]
            public bool? SEast { get; set; }

            /// <summary>
            /// If the plant is connected in north
            /// </summary>
            [BlockData("north")]
            public bool? SNorth { get; set; }

            /// <summary>
            /// If the plant is connected in south
            /// </summary>
            [BlockData("south")]
            public bool? SSouth { get; set; }

            /// <summary>
            /// If the plant is connected in west
            /// </summary>
            [BlockData("west")]
            public bool? SWest { get; set; }
        }
    }
}
