using System.Collections.Generic;
using System;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for endermen
    /// </summary>
    public class Enderman : Mob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Enderman> PathCreator => new Data.DataPathCreator<Enderman>();

        /// <summary>
        /// Creates a new enderman
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Enderman(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Enderman() : base(SharpCraft.ID.Entity.enderman) { }

        /// <summary>
        /// The block the enderman is holding.
        /// Note: block data is not supported
        /// </summary>
        [Data.DataTag("carriedBlockState", "Name", "Properties")]
        public Block? Holding { get; set; }
    }
}
