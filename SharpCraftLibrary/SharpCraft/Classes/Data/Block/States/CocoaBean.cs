using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for cocoa bean blocks
        /// </summary>
        public class CocoaBean : Block, IBlock.IAge, IBlock.IFacing
        {
            private int? _sAge;

            /// <summary>
            /// Creates a cocoa bean block
            /// </summary>
            /// <param name="type">The type of block</param>
            public CocoaBean(BlockType type) : base(type) { }

            /// <summary>
            /// Creates a cocoa bean block
            /// </summary>
            /// <param name="type">The type of block</param>
            public CocoaBean(ID.Block type = SharpCraft.ID.Block.cocoa) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.cocoa;
            }

            /// <summary>
            /// How much the cocoa bean has grown
            /// (0-2. 2 == fully grown)
            /// </summary>
            [BlockState("age")]
            [BlockIntStateRange(0, 2)]
            public int? SAge
            {
                get => _sAge;
                set
                {
                    if (value != null && (value < 0 || value > 2))
                    {
                        throw new ArgumentException(nameof(SAge) + " has to be equel to or between 0 and 2");
                    }
                    _sAge = value;
                }
            }

            /// <summary>
            /// The direction of the log the bean is placed on.
            /// </summary>
            [BlockState("facing")]
            public ID.Facing? SFacing { get; set; }
        }
    }
}
