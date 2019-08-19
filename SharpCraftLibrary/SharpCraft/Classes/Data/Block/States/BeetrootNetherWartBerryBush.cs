using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for beetroot, berry bush and nether wart blocks
        /// </summary>
        public class BeetrootNetherWartBerryBush : Block, IBlock.IAge
        {
            private int? _sAge;

            /// <summary>
            /// Creates a new beetroot, berry bush or nether wart block
            /// </summary>
            /// <param name="type">The type of block</param>
            public BeetrootNetherWartBerryBush(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public BeetrootNetherWartBerryBush(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.beetroots || block == SharpCraft.ID.Block.nether_wart || block == SharpCraft.ID.Block.sweet_berry_bush;
            }

            /// <summary>
            /// How far the beetroot / berry bush / nether wart has grown
            /// (0-3. 3 == fully grown)
            /// </summary>
            [BlockState("age")]
            [BlockIntStateRange(0, 3)]
            public int? SAge
            {
                get => _sAge;
                set
                {
                    if (value != null && (value < 0 || value > 3))
                    {
                        throw new ArgumentException(nameof(SAge) + " has to be equel to or between 0 and 3");
                    }
                    _sAge = value;
                }
            }
        }
    }
}
