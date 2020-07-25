using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for button blocks.
    /// </summary>
    public class Button : Block, Interfaces.IFacing, Interfaces.IPlacedOn, Interfaces.IPowered
    {
        /// <summary>
        /// Creates a new button block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Button(IBlockType? type) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block.ToString().Contains("_button");
        }

        /// <summary>
        /// The way the button is placed
        /// </summary>
        [BlockState("face")]
        public ID.StatePlaced? SPlacedOn { get; set; }

        /// <summary>
        /// The way the button is facing
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }

        /// <summary>
        /// If the button is clicked
        /// </summary>
        [BlockState("powered")]
        public bool? SPowered { get; set; }
    }
}
