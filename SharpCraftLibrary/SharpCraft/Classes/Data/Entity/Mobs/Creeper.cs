using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for creepers
    /// </summary>
    public class Creeper : Mob
    {
        /// <summary>
        /// Creates a new creeper
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Creeper(ID.Entity? type = ID.Entity.creeper) : base(type) { }

        /// <summary>
        /// If the creeper is charged (was hit by lightning)
        /// </summary>
        [Data.DataTag("powered")]
        public bool? Charged { get; set; }

        /// <summary>
        /// The size of the explosion caused by the exploded creeper
        /// </summary>
        [Data.DataTag]
        public byte? ExplosionRadius { get; set; }

        /// <summary>
        /// The time till the creeper will explode when trying to explode
        /// </summary>
        [Data.DataTag]
        public Time<short>? Fuse { get; set; }

        /// <summary>
        /// If the creeper is ignited and is forced to blow up
        /// </summary>
        [Data.DataTag]
        public bool? Ignited { get; set; }
    }
}
