using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for end gateway blocks
    /// </summary>
    public class EndGateWay : BaseBlockEntity
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<EndGateWay> PathCreator => new Data.DataPathCreator<EndGateWay>();

        /// <summary>
        /// Creates an end gateway block
        /// </summary>
        /// <param name="type">The type of block</param>
        public EndGateWay(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public EndGateWay() : base(SharpCraft.ID.Block.end_gateway) { }

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
        [Data.DataTag("ExitPortal", "X", "Y", "Z")]
        public IntVector? DExit { get; set; }
        /// <summary>
        /// If the entity should be teleported to this exact location
        /// </summary>
        [Data.DataTag("ExactTeleport")]
        public bool? DExactTeleport { get; set; }

        /// <summary>
        /// The amount of time the portal has existed.
        /// x&lt;200 = magenta beam.
        /// </summary>
        [Data.DataTag("Age")]
        public Time<long>? DAge { get; set; }
    }
}
