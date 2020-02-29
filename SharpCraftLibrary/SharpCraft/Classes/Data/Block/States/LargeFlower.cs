using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for large flower blocks
    /// </summary>
    public class LargeFlower : Block, Interfaces.IPart
    {
        /// <summary>
        /// Creates a new large flower block
        /// </summary>
        /// <param name="type">The type of block</param>
        public LargeFlower(BlockType? type) : base(type) { }

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
