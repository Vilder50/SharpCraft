using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// An object for primed tnt entities
    /// </summary>
    public class TNT : BasicEntity
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<TNT> PathCreator => new Data.DataPathCreator<TNT>();

        /// <summary>
        /// Creates new primed tnt
        /// </summary>
        /// <param name="type">the type of entity</param>
        public TNT(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public TNT() : base(SharpCraft.ID.Entity.tnt) { }

        /// <summary>
        /// The time in ticks before the tnt blows up
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagShort)]
        public int? Fuse { get; set; }
    }
}
