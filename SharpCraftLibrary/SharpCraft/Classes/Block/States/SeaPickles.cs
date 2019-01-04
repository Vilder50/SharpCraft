using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for sea picle blocks
        /// </summary>
        public class SeaPicles : Block
        {
            private int? _sPicles;

            /// <summary>
            /// Creates a new sea picle block
            /// </summary>
            /// <param name="type">The type of block</param>
            public SeaPicles(ID.Block? type = SharpCraft.ID.Block.sea_pickle) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public SeaPicles(Group group) : base(group) { }

            /// <summary>
            /// The amount of picles in the block
            /// (1-4)
            /// </summary>
            [BlockData("pickles")]
            public int? SPicles
            {
                get => _sPicles;
                set
                {
                    if (value != null && (value < 1 || value > 4))
                    {
                        throw new ArgumentException(nameof(SPicles) + " has to be equel to or between 1 and 4");
                    }
                    _sPicles = value;
                }
            }

            /// <summary>
            /// If the picles are water logged
            /// </summary>
            [BlockData("waterlogged")]
            public bool? SWaterLogged { get; set; }
        }
    }
}
