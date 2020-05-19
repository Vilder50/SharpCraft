using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for boats
    /// </summary>
    public class Boat : BasicEntity
    {
        /// <summary>
        /// Creates a new boat
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Boat(ID.Entity? type = ID.Entity.boat) : base(type) { }

        /// <summary>
        /// The type of boat
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagString)]
        public ID.Boat? Type { get; set; }
    }
}
