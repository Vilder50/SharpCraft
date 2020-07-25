using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for redstone torches
    /// </summary>
    public class RedstoneWallTorch : Block, Interfaces.IFacing, Interfaces.ILit
    {
        /// <summary>
        /// Creates a redstone torch block
        /// </summary>
        /// <param name="type">The type of block</param>
        public RedstoneWallTorch(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public RedstoneWallTorch() : base(SharpCraft.ID.Block.redstone_wall_torch) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.redstone_wall_torch;
        }

        /// <summary>
        /// The way the torch is facing. (The way it points)
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }

        /// <summary>
        /// If the block is lit up
        /// </summary>
        [BlockState("lit")]
        public bool? SLit { get; set; }
    }
}