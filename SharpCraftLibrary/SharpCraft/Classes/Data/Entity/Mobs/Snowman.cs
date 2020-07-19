using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for snowmen
    /// </summary>
    public class Snowman : Mob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Snowman> PathCreator => new Data.DataPathCreator<Snowman>();

        /// <summary>
        /// Creates a new snowman
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Snowman(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Snowman() : base(SharpCraft.ID.Entity.snow_golem) { }

        /// <summary>
        /// True if the snowman has a pumpkin on
        /// </summary>
        [Data.DataTag]
        public bool? Pumpkin { get; set; }
    }
}
