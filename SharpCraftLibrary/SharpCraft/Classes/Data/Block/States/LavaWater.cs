using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for lava and water blocks
        /// </summary>
        public class LavaWater : Block, IBlock.ILevel
        {
            private int? _sLevel;

            /// <summary>
            /// Creates a new lava or water block
            /// </summary>
            /// <param name="type">The type of block</param>
            public LavaWater(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public LavaWater(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.water || block == SharpCraft.ID.Block.lava;
            }

            /// <summary>
            /// The type of lava / water.
            /// (0-15. 0 == source block. 1-7 = flowing. 8-15 = falling (the number is 8+the number from the source its falling from))
            /// </summary>
            [BlockState("level")]
            [BlockIntStateRange(0, 15)]
            public int? SLevel
            {
                get => _sLevel;
                set
                {
                    if (value != null && (value < 0 || value > 15))
                    {
                        throw new ArgumentException(nameof(SLevel) + " has to be equel to or between 0 and 15");
                    }
                    _sLevel = value;
                }
            }
        }
    }
}
