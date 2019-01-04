using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for carrot, potato or wheat blocks
        /// </summary>
        public class CarrotPotatoWheat : Block
        {
            private int? _sAge;

            /// <summary>
            /// Creates a new carrot, potato or wheat block
            /// </summary>
            /// <param name="type">The type of block</param>
            public CarrotPotatoWheat(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public CarrotPotatoWheat(Group group) : base(group) { }

            /// <summary>
            /// How much the carrot / potato / wheat block has grown
            /// (0-7. 7 == fully grown)
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
        }
    }
}
