using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for ladder blocks
        /// </summary>
        public class Ladder : Block
        {
            /// <summary>
            /// Creates a new ladder block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Ladder(ID.Block? type = SharpCraft.ID.Block.ladder) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Ladder(Group group) : base(group) { }

            /// <summary>
            /// The direction of the block the ladder is on
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? SSupportBlockDirection { get; set; }

            /// <summary>
            /// If the ladder is water logged
            /// </summary>
            [BlockData("waterlogged")]
            public bool? SWaterLogged { get; set; }
        }
    }
}
