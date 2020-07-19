using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for pigs
    /// </summary>
    public class Pig : BreedableMob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Pig> PathCreator => new Data.DataPathCreator<Pig>();

        /// <summary>
        /// Creates a new pigs
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Pig(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Pig() : base(SharpCraft.ID.Entity.pig) { }

        /// <summary>
        /// If the pig has a saddle on
        /// </summary>
        [Data.DataTag]
        public bool? Saddle { get; set; }
    }
}
