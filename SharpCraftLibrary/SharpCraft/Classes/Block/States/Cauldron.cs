using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for cauldron blocks
        /// </summary>
        public class Cauldron : Block, IBlock.ILevel
        {
            private int? _sLevel;

            /// <summary>
            /// Creates a new cauldron block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Cauldron(ID.Block? type = SharpCraft.ID.Block.cauldron) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Cauldron(Group group) : base(group) { }

            /// <summary>
            /// How much water there is in the cauldron
            /// (0-3. 0 = no water. 3 = full)
            /// </summary>
            [BlockData("level")]
            public int? SLevel
            {
                get => _sLevel;
                set
                {
                    if (value != null && (value < 0 || value > 3))
                    {
                        throw new ArgumentException(nameof(SLevel) + " has to be equel to or between 0 and 3");
                    }
                    _sLevel = value;
                }
            }
        }
    }
}
