using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for end gateway blocks
        /// </summary>
        public class EndGateWay : Block
        {
            /// <summary>
            /// Creates a new end gateway block
            /// </summary>
            /// <param name="type">The type of block</param>
            public EndGateWay(ID.Block? type = SharpCraft.ID.Block.end_gateway) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public EndGateWay(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.end_gateway;
            }

            /// <summary>
            /// The location the entity gets teleported to when entering
            /// </summary>
            [Data.DataTag("ExitPortal","X","Y","Z", SharpCraft.ID.NBTTagType.TagInt)]
            public Coords DExit { get; set; }
            /// <summary>
            /// If the entity should be teleported to this exact location
            /// </summary>
            [Data.DataTag("ExactTeleport")]
            public bool? DExactTeleport { get; set; }

            /// <summary>
            /// The amount of time the portal has existed.
            /// x&lt;200 = magenta beam.
            /// </summary>
            [Data.DataTag("Age", ForceType = SharpCraft.ID.NBTTagType.TagLong)]
            public Time DAge { get; set; }
        }
    }
}
