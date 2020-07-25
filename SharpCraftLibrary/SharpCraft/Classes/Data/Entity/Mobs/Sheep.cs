using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for sheeps
    /// </summary>
    public class Sheep : BreedableMob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Sheep> PathCreator => new Data.DataPathCreator<Sheep>();

        /// <summary>
        /// Creates a new sheep
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Sheep(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Sheep() : base(SharpCraft.ID.Entity.sheep) { }

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
