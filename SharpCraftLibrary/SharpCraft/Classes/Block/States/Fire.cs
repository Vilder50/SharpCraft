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
        public class Fire : Block
        {
            private int? _sAge;

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
            /// The age of the fire.
            /// (0-15. Has a 1/3 chance of going up each tick)
            /// </summary>
            [BlockData("age")]
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
            [BlockData("east")]
            public bool? SEast { get; set; }

            /// <summary>
            /// If the fire texture shows that way
            /// (If there is a flameable block there)
            /// </summary>
            [BlockData("north")]
            public bool? SNorth { get; set; }

            /// <summary>
            /// If the fire texture shows that way
            /// (If there is a flameable block there)
            /// </summary>
            [BlockData("south")]
            public bool? SSouth { get; set; }

            /// <summary>
            /// If the fire texture shows that way
            /// (If there is a flameable block there)
            /// </summary>
            [BlockData("west")]
            public bool? SWest { get; set; }

            /// <summary>
            /// If the fire texture shows that way
            /// (If there is a flameable block there)
            /// </summary>
            [BlockData("up")]
            public bool? SUp { get; set; }
        }
    }
}
