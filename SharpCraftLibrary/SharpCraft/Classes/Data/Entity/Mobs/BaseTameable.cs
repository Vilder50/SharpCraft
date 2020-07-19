using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// The basic entity data for tameable
    /// </summary>
    public class TameableMob : BreedableMob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<TameableMob> PathCreator => new Data.DataPathCreator<TameableMob>();

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
