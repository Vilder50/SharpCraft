using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for bamboo (sapling) blocks
        /// </summary>
        public class Bamboo : Block, IBlock.IAge, IBlock.IStage
        {
            private int? _sAge;
            private int? _sStage;

            /// <summary>
            /// Creates a new bamboo (sapling) block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Bamboo(ID.Block? type = SharpCraft.ID.Block.bamboo) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Bamboo(Group group) : base(group) { }

            /// <summary>
            /// The age of the bamboo.
            /// (0-1)
            /// 1 = Bamboo looks thicker.
            /// (<see cref="ID.Block.bamboo_sapling"/> does not support this)
            /// </summary>
            [BlockData("age")]
            public int? SAge
            {
                get => _sAge;
                set
                {
                    if (!(value == 0 || value == 1 || value == null))
                    {
                        throw new ArgumentException(nameof(SAge) + "Only allows the numbers 0 and 1");
                    }
                    _sAge = value;
                } 
            }

            /// <summary>
            /// The stage of the bamboo
            /// (0-1)
            /// 1 = Bamboo will grow soon.
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

            /// <summary>
            /// The bamboo's leaves' size
            /// (<see cref="ID.Block.bamboo_sapling"/> does not support this)
            /// </summary>
            [BlockData("leaves")]
            public ID.StateBambooLeave? SLeaves { get; set; }
        }
    }
}
