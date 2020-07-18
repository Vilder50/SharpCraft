using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for withers
    /// </summary>
    public class Wither : Mob
    {
        /// <summary>
        /// Creates a new wither
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Wither(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Wither() : base(SharpCraft.ID.Entity.wither) { }

        /// <summary>
        /// The time before the wither actually spawns / can be fought
        /// </summary>
        [Data.DataTag]
        public Time<int>? Invul { get; set; }
    }
}
