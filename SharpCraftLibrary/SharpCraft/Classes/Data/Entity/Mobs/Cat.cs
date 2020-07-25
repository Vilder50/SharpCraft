using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for cats
    /// </summary>
    public class Cat : TameableMob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Cat> PathCreator => new Data.DataPathCreator<Cat>();

        /// <summary>
        /// Creates a new cat
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Cat(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Cat() : base(SharpCraft.ID.Entity.cat) { }

        /// <summary>
        /// The cat's skin
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
        public ID.Cat? CatType { get; set; }
        /// <summary>
        /// The color of the cat's collar
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagByte)]
        public ID.Color? CollarColor { get; set; }
    }
}
