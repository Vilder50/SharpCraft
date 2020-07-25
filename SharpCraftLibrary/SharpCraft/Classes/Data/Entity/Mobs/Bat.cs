using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for bats
    /// </summary>
    public class Bat : Mob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Bat> PathCreator => new Data.DataPathCreator<Bat>();

        /// <summary>
        /// Creates a new bat
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Bat(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Bat() : base(SharpCraft.ID.Entity.bat) { }

        /// <summary>
        /// True when flying. False when hanging
        /// </summary>
        [Data.DataTag]
        public bool? BatFlags { get; set; }
    }
}
