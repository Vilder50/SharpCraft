using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for iron golems
    /// </summary>
    public class IronGolem : Mob
    {
        /// <summary>
        /// Creates a new iron golem
        /// </summary>
        /// <param name="type">the type of entity</param>
        public IronGolem(ID.Entity? type = ID.Entity.iron_golem) : base(type) { }

        /// <summary>
        /// If true the golem wont attack players
        /// </summary>
        [Data.DataTag]
        public bool? PlayerCreated { get; set; }
    }
}
