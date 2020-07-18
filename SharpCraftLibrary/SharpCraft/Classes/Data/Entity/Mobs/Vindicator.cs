using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for vindicators
    /// </summary>
    public class Vindicator : BaseIllager
    {
        /// <summary>
        /// Creates a new vindicator
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Vindicator(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Vindicator() : base(SharpCraft.ID.Entity.vindicator) { }

        /// <summary>
        /// If the vindicator is a Johnny vindicator (attacks everything)
        /// </summary>
        [Data.DataTag]
        public bool? Johnny { get; set; }
    }
}
