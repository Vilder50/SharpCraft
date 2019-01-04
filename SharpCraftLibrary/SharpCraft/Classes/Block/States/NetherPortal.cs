using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for nether portal blocks
        /// </summary>
        public class NetherPortal : Block
        {
            /// <summary>
            /// Creates a new nether portal block
            /// </summary>
            /// <param name="type">The type of block</param>
            public NetherPortal(ID.Block? type = SharpCraft.ID.Block.nether_portal) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public NetherPortal(Group group) : base(group) { }

            /// <summary>
            /// The axis the portal is parallel to
            /// </summary>
            [BlockData("east")]
            public ID.StatePortalAxis? SAxis { get; set; }
        }
    }
}
