using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for composter blocks
        /// </summary>
        public class Composter : Block, IBlock.ILevel
        {
            private int? _sLevel;

            /// <summary>
            /// Intilizes a new block object
            /// </summary>
            public Composter()
            {
                ID = null;
            }

            /// <summary>
            /// Creates a new composter block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Composter(ID.Block? type = SharpCraft.ID.Block.composter) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Composter(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.composter;
            }

            /// <summary>
            /// How much compost there is in the composter
            /// (0-8. 0 = compost. 8 = full)
            /// </summary>
            [BlockState("level")]
            [BlockIntStateRange(0, 8)]
            public int? SLevel
            {
                get => _sLevel;
                set
                {
                    if (value != null && (value < 0 || value > 8))
                    {
                        throw new ArgumentException(nameof(SLevel) + " has to be equel to or between 0 and 8");
                    }
                    _sLevel = value;
                }
            }
        }
    }
}
