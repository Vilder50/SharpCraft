using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for sheeps
    /// </summary>
    public class Sheep : BreedableMob
    {
        /// <summary>
        /// Creates a new sheep
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Sheep(ID.Entity? type = ID.Entity.sheep) : base(type) { }

        /// <summary>
        /// The sheep's color
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagByte)]
        public ID.Color? Color { get; set; }
        /// <summary>
        /// If the sheep is sheared
        /// </summary>
        [Data.DataTag]
        public bool? Sheared { get; set; }
    }
}
