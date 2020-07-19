using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for iron golems
    /// </summary>
    public class IronGolem : Mob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<IronGolem> PathCreator => new Data.DataPathCreator<IronGolem>();

        /// <summary>
        /// Creates a new iron golem
        /// </summary>
        /// <param name="type">the type of entity</param>
        public IronGolem(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public IronGolem() : base(SharpCraft.ID.Entity.iron_golem) { }

        /// <summary>
        /// If true the golem wont attack players
        /// </summary>
        [Data.DataTag]
        public bool? PlayerCreated { get; set; }
    }
}
