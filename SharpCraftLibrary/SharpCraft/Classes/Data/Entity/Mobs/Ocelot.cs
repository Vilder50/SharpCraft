using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for ocelots
    /// </summary>
    public class Ocelot : Mob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Ocelot> PathCreator => new Data.DataPathCreator<Ocelot>();

        /// <summary>
        /// Creates a new ocelot
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Ocelot(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Ocelot() : base(SharpCraft.ID.Entity.ocelot) { }

        /// <summary>
        /// If the ocelot trusts the player
        /// </summary>
        [Data.DataTag("Trusting")]
        public bool? Trust { get; set; }
    }
}
