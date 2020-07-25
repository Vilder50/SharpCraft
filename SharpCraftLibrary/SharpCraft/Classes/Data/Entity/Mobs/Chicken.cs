using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for chickens
    /// </summary>
    public class Chicken : BreedableMob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Chicken> PathCreator => new Data.DataPathCreator<Chicken>();

        /// <summary>
        /// Creates a new chicken
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Chicken(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Chicken() : base(SharpCraft.ID.Entity.chicken) { }

        /// <summary>
        /// Makes the chicken despawnable and drop 10 xp
        /// </summary>
        [Data.DataTag]
        public bool? IsChickenJockey { get; set; }
        /// <summary>
        /// The time till the chicken lays another egg
        /// </summary>
        [Data.DataTag]
        public Time<int>? EggLayTime { get; set; }
    }
}
