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
        public class Trapdoor : Block
        {
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
            /// The direction the trapdoor will be most open in
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? SFacing { get; set; }

            /// <summary>
            /// The type of trapdoor
            /// </summary>
            [BlockData("half")]
            public ID.StateSimplePlaced? SType { get; set; }

            /// <summary>
            /// If the trapdoor is open
            /// </summary>
            [BlockData("open")]
            public bool? SOpen { get; set; }

            /// <summary>
            /// If the trapdoor is powered by redstone
            /// </summary>
            [BlockData("powered")]
            public bool? SPowered { get; set; }

            /// <summary>
            /// If the trapdoor is water logged
            /// </summary>
            [BlockData("waterlogged")]
            public bool? SWaterLogged { get; set; }
        }
    }
}
