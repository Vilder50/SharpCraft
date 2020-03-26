using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for endermites
    /// </summary>
    public class Endermite : Mob
    {
        /// <summary>
        /// Creates a new endermite
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Endermite(ID.Entity? type = ID.Entity.endermite) : base(type) { }

        /// <summary>
        /// The time the endermite has existed. When hitting 24000 it despawns
        /// </summary>
        [Data.DataTag]
        public Time<int>? LifeTime { get; set; }
        /// <summary>
        /// If the endermite was spawned by a player. If true endermen will attack it.
        /// </summary>
        [Data.DataTag]
        public bool? PlayerSpawned { get; set; }
    }
}
