using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for fence gate blocks
        /// </summary>
        public class FenceGate : Block, IBlock.IPowered, IBlock.IOpen, IBlock.IFacing
        {
            /// <summary>
            /// Creates a new fence gate block
            /// </summary>
            /// <param name="type">The type of block</param>
            public FenceGate(BlockType? type) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                string blockName = block.ToString();
                return (blockName.Contains("fence") && blockName.Contains("gate"));
            }

            /// <summary>
            /// The direction the gate's door will swing into when open
            /// </summary>
            [BlockState("facing")]
            public ID.Facing? SFacing { get; set; }

            /// <summary>
            /// Will lower the gate to be in the same height as a wall
            /// </summary>
            [BlockState("in_wall")]
            public bool? SPartOfWall { get; set; }

            /// <summary>
            /// If the gate is open
            /// </summary>
            [BlockState("open")]
            public bool? SOpen { get; set; }

            /// <summary>
            /// If the gate is powered by redstone
            /// </summary>
            [BlockState("powered")]
            public bool? SPowered { get; set; }
        }
    }
}
