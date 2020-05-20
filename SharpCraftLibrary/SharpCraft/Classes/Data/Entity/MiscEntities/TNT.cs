using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// An object for primed tnt entities
    /// </summary>
    public class TNT : BasicEntity
    {
        /// <summary>
        /// Creates new primed tnt
        /// </summary>
        /// <param name="type">the type of entity</param>
        public TNT(ID.Entity? type = ID.Entity.tnt) : base(type) { }

        /// <summary>
        /// The time in ticks before the tnt blows up
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagShort)]
        public int? Fuse { get; set; }
    }
}
