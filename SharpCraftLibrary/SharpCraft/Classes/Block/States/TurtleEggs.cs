using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for turtle egg blocks
        /// </summary>
        public class TurtleEggs : Block
        {
            private int? _sEggs;
            private int? _sHatching;

            /// <summary>
            /// Creates a turtle egg block
            /// </summary>
            /// <param name="type">The type of block</param>
            public TurtleEggs(ID.Block? type = SharpCraft.ID.Block.turtle_egg) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public TurtleEggs(Group group) : base(group) { }

            /// <summary>
            /// The amount of eggs in the block.
            /// (1-4)
            /// </summary>
            [BlockData("eggs")]
            public int? SEggs
            {
                get => _sEggs;
                set
                {
                    if (value != null && (value < 1 || value > 4))
                    {
                        throw new ArgumentException(nameof(SEggs) + " has to be equel to or between 1 and 4");
                    }
                    _sEggs = value;
                }
            }

            /// <summary>
            /// How far the eggs has hatched.
            /// (0-2. 2 == Will hatch soon)
            /// </summary>
            [BlockData("hatch")]
            public int? SHatching
            {
                get => _sHatching;
                set
                {
                    if (value != null && (value < 0 || value > 2))
                    {
                        throw new ArgumentException(nameof(SHatching) + " has to be equel to or between 0 and 2");
                    }
                    _sHatching = value;
                }
            }
        }
    }
}
