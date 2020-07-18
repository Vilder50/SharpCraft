using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for bats
    /// </summary>
    public class Bat : Mob
    {
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
