using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for stem blocks
        /// </summary>
        public class PumpkinMelonStem : Block
        {
            private int? _sAge;

            /// <summary>
            /// Creates a new stem block
            /// </summary>
            /// <param name="type">The type of block</param>
            public PumpkinMelonStem(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public PumpkinMelonStem(Group group) : base(group) { }

            /// <summary>
            /// The age of the stem.
            /// (0-7. 7 == Fully grown)
            /// </summary>
            [BlockData("age")]
            public int? SAge
            {
                get => _sAge;
                set
                {
                    if (value != null && (value < 0 || value > 7))
                    {
                        throw new ArgumentException(nameof(SAge) + " has to be equel to or between 0 and 7");
                    }
                    _sAge = value;
                }
            }

            /// <summary>
            /// The direction of the block the stem is growing into
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? SGrowInto { get; set; }
        }
    }
}
