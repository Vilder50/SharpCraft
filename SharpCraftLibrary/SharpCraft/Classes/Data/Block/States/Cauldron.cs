using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for cauldron blocks
        /// </summary>
        public class Cauldron : Block, IBlock.ILevel
        {
            private int? _sLevel;

            /// <summary>
            /// Creates a cauldron block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Cauldron(BlockType type) : base(type) { }

            /// <summary>
            /// Creates a cauldron block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Cauldron(ID.Block type = SharpCraft.ID.Block.cauldron) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.cauldron;
            }

            /// <summary>
            /// How much water there is in the cauldron
            /// (0-3. 0 = no water. 3 = full)
            /// </summary>
            [BlockState("level")]
            [BlockIntStateRange(0, 3)]
            public int? SLevel
            {
                get => _sLevel;
                set
                {
                    if (value != null && (value < 0 || value > 3))
                    {
                        throw new ArgumentException(nameof(SLevel) + " has to be equel to or between 0 and 3");
                    }
                    _sLevel = value;
                }
            }
        }
    }
}
