using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for wolves
    /// </summary>
    public class Wolf : TameableMob
    {
        /// <summary>
        /// Creates a new wolf
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Wolf(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Wolf() : base(SharpCraft.ID.Entity.wolf) { }

        /// <summary>
        /// The color of the wolf's collar
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagByte)]
        public ID.Color? Color { get; set; }
        /// <summary>
        /// If the wolf is angry
        /// </summary>
        [Data.DataTag]
        public bool? Angry { get; set; }
    }
}