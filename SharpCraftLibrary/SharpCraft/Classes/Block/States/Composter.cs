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
        public class Composter : Block
        {
            private int? _sLevel;

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
            /// How much compost there is in the composter
            /// (0-8. 0 = compost. 8 = full)
            /// </summary>
            [BlockData("level")]
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
