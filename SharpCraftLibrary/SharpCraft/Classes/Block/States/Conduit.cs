using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for conduit blocks
        /// </summary>
        public class Conduit : Block, IBlock.IWaterLogged
        {
            /// <summary>
            /// Intilizes a new block object
            /// </summary>
            public Conduit()
            {
                ID = null;
            }

            /// <summary>
            /// Creates a new conduit block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Conduit(ID.Block? type = SharpCraft.ID.Block.conduit) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Conduit(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.conduit;
            }

            /// <summary>
            /// If the conduit is water logged
            /// </summary>
            [BlockState("waterlogged")]
            public bool? SWaterLogged { get; set; }
        }
    }
}
