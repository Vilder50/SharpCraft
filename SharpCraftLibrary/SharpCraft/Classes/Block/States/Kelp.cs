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
            /// Creates a new krlp block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Kelp(ID.Block? type = SharpCraft.ID.Block.kelp) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Kelp(Group group) : base(group) { }

            /// <summary>
            /// The age of the kelp.
            /// (0-25. 1-24 == will try to grow. 25 = won't try to grow)
            /// </summary>
            [BlockData("age")]
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
