using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for pane blocks
    /// </summary>
    public class Pane : Block, Interfaces.IConnectedCardinal, Interfaces.IWaterLogged
    {
        /// <summary>
        /// Creates a new pane block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Pane(BlockType? type) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            string blockName = block.ToString();
            return (blockName.Contains("pane") || blockName.Contains("iron_bars"));
        }

        /// <summary>
        /// If the block is connected in this direction
        /// </summary>
        [BlockState("east")]
        public bool? SEast { get; set; }

        /// <summary>
        /// If the block is connected in this direction
        /// </summary>
        [BlockState("north")]
        public bool? SNorth { get; set; }

        /// <summary>
        /// If the block is connected in this direction
        /// </summary>
        [BlockState("south")]
        public bool? SSouth { get; set; }

        /// <summary>
        /// If the block is connected in this direction
        /// </summary>
        [BlockState("west")]
        public bool? SWest { get; set; }

        /// <summary>
        /// If the block is water logged
        /// </summary>
        [BlockState("waterlogged")]
        public bool? SWaterLogged { get; set; }
    }
}
