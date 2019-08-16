using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for bamboo blocks
        /// </summary>
        public class Bamboo : BambooSapling, IBlock.IAge
        {
            private int? _sAge;

            /// <summary>
            /// Creates a new bamboo block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Bamboo(ID.Block? type = SharpCraft.ID.Block.bamboo) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Bamboo(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.bamboo;
            }

            /// <summary>
            /// The age of the bamboo.
            /// (0-1)
            /// 1 = Bamboo looks thicker.
            /// </summary>
            [BlockData("age")]
            [BlockIntStateRange(0, 1)]
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
            /// The bamboo's leaves' size
            /// </summary>
            [BlockData("leaves")]
            public ID.StateBambooLeave? SLeaves { get; set; }
        }

        /// <summary>
        /// An object for bamboo sapling blocks
        /// </summary>
        public class BambooSapling : Block, IBlock.IStage
        {
            private int? _sStage;

            /// <summary>
            /// Creates a new bamboo sapling block
            /// </summary>
            /// <param name="type">The type of block</param>
            public BambooSapling(ID.Block? type = SharpCraft.ID.Block.bamboo_sapling) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public BambooSapling(Group group) : base(group) { }

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
        }
    }
}
