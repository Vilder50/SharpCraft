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
        /// Creates a new enderman
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Enderman(ID.Entity? type = ID.Entity.endermite) : base(type) { }

        /// <summary>
        /// The block the enderman is holding.
        /// Note: block data is not supported
        /// </summary>
        [Data.DataTag("carriedBlockState", "Name", "Properties")]
        public Block? Holding { get; set; }
    }
}
