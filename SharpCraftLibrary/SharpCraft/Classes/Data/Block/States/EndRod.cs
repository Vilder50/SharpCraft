using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for end rod blocks
        /// </summary>
        public class EndRod : Block, IBlock.IFacingFull
        {
            /// <summary>
            /// Intilizes a new block object
            /// </summary>
            public EndRod()
            {
                ID = null;
            }

            /// <summary>
            /// Creates a new end rod block
            /// </summary>
            /// <param name="type">The type of block</param>
            public EndRod(ID.Block? type = SharpCraft.ID.Block.end_rod) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public EndRod(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.end_rod;
            }

            /// <summary>
            /// The direction of the block its attached to.
            /// </summary>
            [BlockState("facing")]
            public ID.FacingFull? SFacing { get; set; }
        }
    }
}
