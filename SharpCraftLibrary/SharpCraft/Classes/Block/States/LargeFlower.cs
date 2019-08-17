using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for large flower blocks
        /// </summary>
        public class LargeFlower : Block, IBlock.IPart
        {
            /// <summary>
            /// Intilizes a new block object
            /// </summary>
            public LargeFlower()
            {
                ID = null;
            }

            /// <summary>
            /// Creates a new large flower block
            /// </summary>
            /// <param name="type">The type of block</param>
            public LargeFlower(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public LargeFlower(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.tall_grass ||
                    block == SharpCraft.ID.Block.tall_seagrass ||
                    block == SharpCraft.ID.Block.large_fern ||
                    block == SharpCraft.ID.Block.sunflower ||
                    block == SharpCraft.ID.Block.lilac ||
                    block == SharpCraft.ID.Block.rose_bush ||
                    block == SharpCraft.ID.Block.peony;
            }

            /// <summary>
            /// The part of the flower
            /// </summary>
            [BlockState("half")]
            public ID.StatePart? SPart { get; set; }
        }
    }
}
