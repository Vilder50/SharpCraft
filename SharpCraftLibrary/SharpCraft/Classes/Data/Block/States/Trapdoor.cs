using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for trapdoor blocks
        /// </summary>
        public class Trapdoor : Block, IBlock.IFacing, IBlock.IOpen, IBlock.IPowered, IBlock.IWaterLogged
        {
            /// <summary>
            /// Intilizes a new block object
            /// </summary>
            public Trapdoor()
            {
                ID = null;
            }

            /// <summary>
            /// Creates a trapdoor block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Trapdoor(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Trapdoor(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                string blockName = block.ToString();
                return (blockName.Contains("trapdoor"));
            }

            /// <summary>
            /// The direction the trapdoor will be most open in
            /// </summary>
            [BlockState("facing")]
            public ID.Facing? SFacing { get; set; }

            /// <summary>
            /// The type of trapdoor
            /// </summary>
            [BlockState("half")]
            public ID.StateSimplePlaced? SPlaced { get; set; }

            /// <summary>
            /// If the trapdoor is open
            /// </summary>
            [BlockState("open")]
            public bool? SOpen { get; set; }

            /// <summary>
            /// If the trapdoor is powered by redstone
            /// </summary>
            [BlockState("powered")]
            public bool? SPowered { get; set; }

            /// <summary>
            /// If the trapdoor is water logged
            /// </summary>
            [BlockState("waterlogged")]
            public bool? SWaterLogged { get; set; }
        }
    }
}
