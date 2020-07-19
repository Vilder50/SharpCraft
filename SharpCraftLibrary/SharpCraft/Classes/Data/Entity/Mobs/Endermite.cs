using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for endermites
    /// </summary>
    public class Endermite : Mob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Endermite> PathCreator => new Data.DataPathCreator<Endermite>();

        /// <summary>
        /// Creates a new endermite
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Endermite(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Endermite() : base(SharpCraft.ID.Entity.endermite) { }

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
