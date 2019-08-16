using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for slab blocks
        /// </summary>
        public class Slab : Block, IBlock.IWaterLogged
        {
            /// <summary>
            /// Creates a new slab block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Slab(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Slab(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                string blockName = block.ToString();
                return (blockName.Contains("slab"));
            }

            /// <summary>
            /// If the slab is water logged
            /// </summary>
            [BlockData("waterlogged")]
            public bool? SWaterLogged { get; set; }

            /// <summary>
            /// The type of slab
            /// </summary>
            [BlockData("type")]
            public ID.StateSlabPart? SPart { get; set; }
        }
    }
}
