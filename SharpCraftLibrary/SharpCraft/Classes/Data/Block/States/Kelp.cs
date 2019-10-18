using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for krlp blocks
        /// </summary>
        public class Kelp : Block, IBlock.IAge
        {
            private int? _sAge;

            /// <summary>
            /// Creates a kelp block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Kelp(BlockType type) : base(type) { }

            /// <summary>
            /// Creates a kelp block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Kelp(ID.Block type = SharpCraft.ID.Block.kelp) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.kelp;
            }

            /// <summary>
            /// The age of the kelp.
            /// (0-25. 1-24 == will try to grow. 25 = won't try to grow)
            /// </summary>
            [BlockState("age")]
            [BlockIntStateRange(0, 25)]
            public int? SAge
            {
                get => _sAge;
                set
                {
                    if (value != null && (value < 0 || value > 25))
                    {
                        throw new ArgumentException(nameof(SAge) + " has to be equel to or between 0 and 25");
                    }
                    _sAge = value;
                }
            }
        }
    }
}
