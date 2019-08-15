using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for cactus and sugar cane blocks
        /// </summary>
        public class CactusSugarCane : Block, IBlock.IAge
        {
            private int? _sAge;

            /// <summary>
            /// Creates a new cactus or sugar cane block
            /// </summary>
            /// <param name="type">The type of block</param>
            public CactusSugarCane(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public CactusSugarCane(Group group) : base(group) { }

            /// <summary>
            /// How much the cactus / sugar cane has grown
            /// (0-15. 1-4 == will not grow. 15 == will try to grow)
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
