using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for wall blocks
        /// </summary>
        public class Wall : Block, IBlock.IConnectedCardinal, IBlock.IWaterLogged
        {
            /// <summary>
            /// Creates a new wall block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Wall(BlockType type) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                string blockName = block.ToString();
                return (blockName.Contains("wall") && !blockName.Contains("banner") && !blockName.Contains("sign") && !blockName.Contains("fan") && !blockName.Contains("head") && !blockName.Contains("torch"));
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

            /// <summary>
            /// If the block is connected in this direction
            /// </summary>
            [BlockState("up")]
            public bool? SUp { get; set; }
        }
    }
}
