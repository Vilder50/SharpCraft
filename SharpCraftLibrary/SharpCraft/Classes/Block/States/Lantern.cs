using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for lantern blocks
        /// </summary>
        public class Lantern : Block
        {
            /// <summary>
            /// Creates a new lantern block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Lantern(ID.Block? type = SharpCraft.ID.Block.lantern) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Lantern(Group group) : base(group) { }

            /// <summary>
            /// If the lantern is hanging from the roof
            /// </summary>
            [BlockData("hanging")]
            public bool? SHanging { get; set; }
        }
    }
}
