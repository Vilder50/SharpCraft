using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for tnt blocks
        /// </summary>
        public class TNT : Block
        {
            /// <summary>
            /// Creates a tnt block
            /// </summary>
            /// <param name="type">The type of block</param>
            public TNT(ID.Block? type = SharpCraft.ID.Block.tnt) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public TNT(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.tnt;
            }

            /// <summary>
            /// If the tnt gets ignited when broken
            /// </summary>
            [BlockData("unstable")]
            public bool? SUnstable { get; set; }
        }
    }
}
