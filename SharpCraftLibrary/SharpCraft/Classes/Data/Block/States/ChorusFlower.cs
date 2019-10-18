using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    /// <summary>
    /// A object for minecraft blocks
    /// </summary>
    public partial class Block
    {
        /// <summary>
        /// An object for chorus flower blocks
        /// </summary>
        public class ChorusFlower : Block, IBlock.IAge
        {
            private int? _sAge;

            /// <summary>
            /// Creates a chorus flower block
            /// </summary>
            /// <param name="type">The type of block</param>
            public ChorusFlower(BlockType type) : base(type) { }

            /// <summary>
            /// Creates a chorus flower block
            /// </summary>
            /// <param name="type">The type of block</param>
            public ChorusFlower(ID.Block type = SharpCraft.ID.Block.chorus_flower) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.chorus_flower;
            }

            /// <summary>
            /// The age of the flower.
            /// (0-5. 1-4 = still growin. 5 = fully grown)
            /// </summary>
            [BlockState("age")]
            [BlockIntStateRange(0, 5)]
            public int? SAge
            {
                get => _sAge;
                set
                {
                    if (value != null && (value < 0 || value > 5))
                    {
                        throw new ArgumentException(nameof(SAge) + " has to be equel to or between 0 and 5");
                    }
                    _sAge = value;
                }
            }
        }
    }
}
