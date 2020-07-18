using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for striders
    /// </summary>
    public class Strider : BreedableMob
    {
        /// <summary>
        /// Creates a new strider
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Strider(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Strider() : base(SharpCraft.ID.Entity.strider) { }

        /// <summary>
        /// If the strider has a saddle on
        /// </summary>
        [Data.DataTag]
        public bool? Saddle { get; set; }
    }
}
