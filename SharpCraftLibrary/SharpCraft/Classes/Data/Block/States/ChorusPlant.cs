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
        public class ChorusPlant : Block, IBlock.IConnected
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
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.chorus_plant;
            }

            /// <summary>
            /// If the plant is connected downwards
            /// </summary>
            [BlockState("down")]
            public bool? SDown { get; set; }

            /// <summary>
            /// If the plant is connected upwards
            /// </summary>
            [BlockState("up")]
            public bool? SUp { get; set; }

            /// <summary>
            /// If the plant is connected in east
            /// </summary>
            [BlockState("east")]
            public bool? SEast { get; set; }

            /// <summary>
            /// If the plant is connected in north
            /// </summary>
            [BlockState("north")]
            public bool? SNorth { get; set; }

            /// <summary>
            /// If the plant is connected in south
            /// </summary>
            [BlockState("south")]
            public bool? SSouth { get; set; }

            /// <summary>
            /// If the plant is connected in west
            /// </summary>
            [BlockState("west")]
            public bool? SWest { get; set; }
        }
    }
}
