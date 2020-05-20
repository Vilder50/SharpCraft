using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// The basic entity data for tameable
    /// </summary>
    public class TameableMob : BreedableMob
    {
        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="type">the type of entity</param>
        public TameableMob(ID.Entity? type) : base(type) { }

        /// <summary>
        /// If the mob is sitting (wont follow / tp to its owner)
        /// </summary>
        [Data.DataTag]
        public bool? Sitting { get; set; }
        /// <summary>
        /// the <see cref="UUID"/> of the owner
        /// </summary>
        [Data.DataTag("Owner", ForceType = ID.NBTTagType.TagIntArray)]
        public UUID? OwnerUUID { get; set; }
    }
}
