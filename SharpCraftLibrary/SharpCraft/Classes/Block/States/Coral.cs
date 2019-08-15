using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for coral plants (not blocks)
        /// </summary>
        public class Coral : Block, IBlock.IWaterLogged
        {
            /// <summary>
            /// Creates a new coral plant (not block)
            /// </summary>
            /// <param name="type">The type of plant</param>
            public Coral(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Coral(Group group) : base(group) { }

            /// <summary>
            /// If the coral is water logged
            /// </summary>
            [BlockData("waterlogged")]
            public bool? SWaterLogged { get; set; }
        }

        /// <summary>
        /// An object for coral plants fans
        /// </summary>
        public class CoralFan : Coral, IBlock.IFacing
        {
            /// <summary>
            /// Creates a new coral plant (not block)
            /// </summary>
            /// <param name="type">The type of plant</param>
            public CoralFan(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public CoralFan(Group group) : base(group) { }

            /// <summary>
            /// The way the coral is facing
            /// Note: this is only for coral wall fans
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? SFacing { get; set; }
        }
    }
}
