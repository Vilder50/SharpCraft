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
            public FenceGate(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public FenceGate(Group group) : base(group) { }

            /// <summary>
            /// The direction the gate's door will swing into when open
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? SFacing { get; set; }

            /// <summary>
            /// Will lower the gate to be in the same height as a wall
            /// </summary>
            [BlockData("in_wall")]
            public bool? SPartOfWall { get; set; }

            /// <summary>
            /// If the gate is open
            /// </summary>
            [BlockData("open")]
            public bool? SOpen { get; set; }

            /// <summary>
            /// If the gate is powered by redstone
            /// </summary>
            [BlockData("powered")]
            public bool? SPowered { get; set; }
        }
    }
}
