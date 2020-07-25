using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for stair blocks
    /// </summary>
    public class Stair : Block, Interfaces.IFacing, Interfaces.IWaterLogged
    {
        /// <summary>
        /// Creates a stair block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Stair(IBlockType? type) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            string blockName = block.ToString();
            return (blockName.Contains("stair"));
        }

        /// <summary>
        /// If the stair is water logged
        /// </summary>
        [BlockState("waterlogged")]
        public bool? SWaterLogged { get; set; }

        /// <summary>
        /// The shape of the stair
        /// </summary>
        [BlockState("shape")]
        public ID.StateStairShape? SShape { get; set; }

        /// <summary>
        /// The block the stair is placed on
        /// </summary>
        [BlockState("half")]
        public ID.StateSimplePlaced? SPlaced { get; set; }

        /// <summary>
        /// The direction the full stair part is facing
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }
    }
}
