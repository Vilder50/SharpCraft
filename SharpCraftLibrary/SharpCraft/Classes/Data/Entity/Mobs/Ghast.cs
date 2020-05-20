using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for ghasts
    /// </summary>
    public class Ghast : Mob
    {
        /// <summary>
        /// Creates a new ghast
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Ghast(ID.Entity? type = ID.Entity.ghast) : base(type) { }

        /// <summary>
        /// The size of the explosion caused by the ghast's fireballs
        /// </summary>
        [Data.DataTag]
        public int? ExplosionPower { get; set; }
    }
}
