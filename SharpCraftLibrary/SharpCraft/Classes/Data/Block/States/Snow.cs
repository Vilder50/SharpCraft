using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for snow blocks
        /// </summary>
        public class Snow : Block
        {
            private int? _sLayers;

            /// <summary>
            /// Creates a snow block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Snow(ID.Block? type = SharpCraft.ID.Block.snow) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Snow(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.snow;
            }

            /// <summary>
            /// The amount of snow layers
            /// (1-8)
            /// </summary>
            [BlockState("layers")]
            [BlockIntStateRange(1, 8)]
            public int? SLayers
            {
                get => _sLayers;
                set
                {
                    if (value != null && (value < 1 || value > 8))
                    {
                        throw new ArgumentException(nameof(SLayers) + " has to be equel to or between 1 and 8");
                    }
                    _sLayers = value;
                }
            }
        }
    }
}
