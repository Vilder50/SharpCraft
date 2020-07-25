using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for boats
    /// </summary>
    public class Boat : BasicEntity
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Boat> PathCreator => new Data.DataPathCreator<Boat>();

        /// <summary>
        /// Creates a new boat
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Boat(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Boat() : base(SharpCraft.ID.Entity.boat) { }

        /// <summary>
        /// The type of boat
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagString)]
        public ID.Boat? Type { get; set; }
    }
}
