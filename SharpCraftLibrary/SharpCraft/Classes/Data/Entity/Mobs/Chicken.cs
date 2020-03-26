using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for chickens
    /// </summary>
    public class Chicken : BreedableMob
    {
        /// <summary>
        /// Creates a new chicken
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Chicken(ID.Entity? type = ID.Entity.chicken) : base(type) { }

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
