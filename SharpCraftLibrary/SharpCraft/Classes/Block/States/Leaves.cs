using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for leave blocks
        /// </summary>
        public class Leaves : Block, IBlock.IDistance
        {
            private int? _sDistance;

            /// <summary>
            /// Creates a new leave block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Leaves(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Leaves(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                string blockName = block.ToString();
                return (blockName.Contains("leaves"));
            }

            /// <summary>
            /// The distance to the nearest wooden block.
            /// (1-7. 7 == Can despawn)
            /// </summary>
            [BlockData("distance")]
            public int? SDistance
            {
                get => _sDistance;
                set
                {
                    if (value != null && (value < 1 || value > 7))
                    {
                        throw new ArgumentException(nameof(SDistance) + " has to be equel to or between 1 and 7");
                    }
                    _sDistance = value;
                }
            }

            /// <summary>
            /// If the leave shouldn't decay
            /// </summary>
            [BlockData("persistent")]
            public bool? SPersistant { get; set; }
        }
    }
}
