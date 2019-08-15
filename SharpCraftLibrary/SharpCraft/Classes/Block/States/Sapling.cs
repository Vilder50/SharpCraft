using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for sapling blocks
        /// </summary>
        public class Sapling : Block, IBlock.IStage
        {
            private int? _sStage;

            /// <summary>
            /// Creates a new sapling block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Sapling(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Sapling(Group group) : base(group) { }


            /// <summary>
            /// The stage of the sapling
            /// (0-1)
            /// 1 = sapling will grow soon.
            /// </summary>
            [BlockData("stage")]
            public int? SStage
            {
                get => _sStage;
                set
                {
                    if (!(value == 0 || value == 1 || value == null))
                    {
                        throw new ArgumentException(nameof(SStage) + "Only allows the numbers 0 and 1");
                    }
                    _sStage = value;
                }
            }
        }
    }
}
