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
        public class Conduit : Block
        {
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
            /// If the conduit is water logged
            /// </summary>
            [BlockData("waterlogged")]
            public bool? SWaterLogged { get; set; }
        }
    }
}
