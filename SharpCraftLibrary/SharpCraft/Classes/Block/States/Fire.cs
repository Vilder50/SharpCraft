using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for fire blocks
        /// </summary>
        public class Fire : Block, IBlock.IAge
        {
            private int? _sAge;

            /// <summary>
            /// Intilizes a new block object
            /// </summary>
            public Fire()
            {
                ID = null;
            }

            /// <summary>
            /// Creates a new fire block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Fire(ID.Block? type = SharpCraft.ID.Block.fire) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Fire(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.fire;
            }

            /// <summary>
            /// The age of the fire.
            /// (0-15. Has a 1/3 chance of going up each tick)
            /// </summary>
            [BlockState("age")]
            public int? SAge
            {
                get => _sAge;
                set
                {
                    if (value != null && (value < 0 || value > 15))
                    {
                        throw new ArgumentException(nameof(SAge) + " has to be equel to or between 0 and 15");
                    }
                    _sAge = value;
                }
            }

            /// <summary>
            /// If the fire texture shows that way
            /// (If there is a flameable block there)
            /// </summary>
            [BlockState("east")]
            public bool? SEast { get; set; }

            /// <summary>
            /// If the fire texture shows that way
            /// (If there is a flameable block there)
            /// </summary>
            [BlockState("north")]
            public bool? SNorth { get; set; }

            /// <summary>
            /// If the fire texture shows that way
            /// (If there is a flameable block there)
            /// </summary>
            [BlockState("south")]
            public bool? SSouth { get; set; }

            /// <summary>
            /// If the fire texture shows that way
            /// (If there is a flameable block there)
            /// </summary>
            [BlockState("west")]
            public bool? SWest { get; set; }

            /// <summary>
            /// If the fire texture shows that way
            /// (If there is a flameable block there)
            /// </summary>
            [BlockState("up")]
            public bool? SUp { get; set; }
        }
    }
}
