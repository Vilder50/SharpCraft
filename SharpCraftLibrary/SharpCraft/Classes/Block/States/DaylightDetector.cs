using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for daylight detector blocks
        /// </summary>
        public class DaylightDetector : Block
        {
            private int? _sPower;

            /// <summary>
            /// Creates a new daylight detector block
            /// </summary>
            /// <param name="type">The type of block</param>
            public DaylightDetector(ID.Block? type = SharpCraft.ID.Block.daylight_detector) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public DaylightDetector(Group group) : base(group) { }

            /// <summary>
            /// How much redstone power it outputs
            /// (0-15)
            /// </summary>
            [BlockData("power")]
            public int? SPower
            {
                get => _sPower;
                set
                {
                    if (value != null && (value < 0 || value > 15))
                    {
                        throw new ArgumentException(nameof(SPower) + " has to be equel to or between 0 and 15");
                    }
                    _sPower = value;
                }
            }
        }
    }
}
