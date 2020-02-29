using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for grass blocks
    /// </summary>
    public class Grass : Block
    {
        /// <summary>
        /// Creates a grass block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Grass(BlockType? type) : base(type) { }

        /// <summary>
        /// Creates a grass block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Grass(ID.Block type = SharpCraft.ID.Block.grass_block) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.grass;
        }

        /// <summary>
        /// If there is snow ontop of the block
        /// </summary>
        [BlockState("snowy")]
        public bool? SSnowy { get; set; }
    }
}
