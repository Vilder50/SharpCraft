using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for cake blocks
        /// </summary>
        public class Cake : Block
        {
            private int? _sBites;

            /// <summary>
            /// Intilizes a new block object
            /// </summary>
            public Cake()
            {
                ID = null;
            }

            /// <summary>
            /// Creates a new cake block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Cake(ID.Block? type = SharpCraft.ID.Block.cake) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Cake(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.cake;
            }

            /// <summary>
            /// How much cake there has been eaten
            /// (0-6. 0 = Full cake. 6 = One bite left)
            /// </summary>
            [BlockState("bites")]
            [BlockIntStateRange(0, 6)]
            public int? SBites
            {
                get => _sBites;
                set
                {
                    if (value != null && (value < 0 || value > 6))
                    {
                        throw new ArgumentException(nameof(SBites) + " has to be equel to or between 0 and 6");
                    }
                    _sBites = value;
                }
            }
        }
    }
}
