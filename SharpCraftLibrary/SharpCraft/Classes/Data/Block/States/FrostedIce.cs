using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for frosted ice blocks
        /// </summary>
        public class FrostedIce : Block, IBlock.IAge
        {
            private int? _sAge;

            /// <summary>
            /// Creates a frosted ice block
            /// </summary>
            /// <param name="type">The type of block</param>
            public FrostedIce(BlockType? type) : base(type) { }

            /// <summary>
            /// Creates a frosted ice block
            /// </summary>
            /// <param name="type">The type of block</param>
            public FrostedIce(ID.Block type = SharpCraft.ID.Block.frosted_ice) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.frosted_ice;
            }

            /// <summary>
            /// The age of the ice.
            /// (0-3. When 3 it has a chance to break)
            /// </summary>
            [BlockState("age")]
            [BlockIntStateRange(0, 3)]
            public int? SAge
            {
                get => _sAge;
                set
                {
                    if (value != null && (value < 0 || value > 3))
                    {
                        throw new ArgumentException(nameof(SAge) + " has to be equel to or between 0 and 3");
                    }
                    _sAge = value;
                }
            }
        }
    }
}
