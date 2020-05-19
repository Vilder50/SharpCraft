using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for snowmen
    /// </summary>
    public class Snowman : Mob
    {
        /// <summary>
        /// Creates a new snowman
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Snowman(ID.Entity? type = ID.Entity.snow_golem) : base(type) { }

        /// <summary>
        /// True if the snowman has a pumpkin on
        /// </summary>
        [Data.DataTag]
        public bool? Pumpkin { get; set; }
    }
}
