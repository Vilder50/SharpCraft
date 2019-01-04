using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for lectern blocks
        /// </summary>
        public class Lectern : CloneBlock<Lectern>
        {
            /// <summary>
            /// Creates a new lectern block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Lectern(ID.Block? type = SharpCraft.ID.Block.lectern) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Lectern(Group group) : base(group) { }

            /// <summary>
            /// The way the lectern is facing
            /// </summary>
            [BlockData("has_record")]
            public ID.Facing SFacing{get; set;}

            /// <summary>
            /// Gets the raw data for the data the block contains
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string GetDataString()
            {
                base.GetDataString();

                return "";
            }
        }
    }
}
