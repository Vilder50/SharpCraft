using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for lantern blocks
    /// </summary>
    public class Lantern : Block
    {
        /// <summary>
        /// Creates a lantern block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Lantern(BlockType? type) : base(type) { }

        /// <summary>
        /// Creates a lantern block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Lantern(ID.Block type = SharpCraft.ID.Block.lantern) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.lantern || block == SharpCraft.ID.Block.soul_lantern;
        }

        /// <summary>
        /// If the lantern is hanging from the roof
        /// </summary>
        [BlockState("hanging")]
        public bool? SHanging { get; set; }
    }
}
