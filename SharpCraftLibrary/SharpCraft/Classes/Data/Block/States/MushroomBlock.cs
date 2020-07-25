using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for mushroom blocks
    /// </summary>
    public class MushroomBlock : Block, Interfaces.IConnected
    {
        /// <summary>
        /// Creates a new mushroom block
        /// </summary>
        /// <param name="type">The type of block</param>
        public MushroomBlock(IBlockType? type) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.mushroom_stem || block == SharpCraft.ID.Block.brown_mushroom_block || block == SharpCraft.ID.Block.red_mushroom_block;
        }

        /// <summary>
        /// If the texture should be shown downwards.
        /// False will show pores texture
        /// </summary>
        [BlockState("down")]
        public bool? SDown { get; set; }

        /// <summary>
        /// If the texture should be shown upwards.
        /// False will show pores texture
        /// </summary>
        [BlockState("up")]
        public bool? SUp { get; set; }

        /// <summary>
        /// If the texture should be in east.
        /// False will show pores texture
        /// </summary>
        [BlockState("east")]
        public bool? SEast { get; set; }

        /// <summary>
        /// If the texture should be in north.
        /// False will show pores texture
        /// </summary>
        [BlockState("north")]
        public bool? SNorth { get; set; }

        /// <summary>
        /// If the texture should be in south.
        /// False will show pores texture
        /// </summary>
        [BlockState("south")]
        public bool? SSouth { get; set; }

        /// <summary>
        /// If the texture should be in west.
        /// False will show pores texture
        /// </summary>
        [BlockState("west")]
        public bool? SWest { get; set; }
    }
}
