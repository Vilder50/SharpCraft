using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for <see cref="ID.Entity.egg"/>s, <see cref="ID.Entity.ender_pearl"/>s, <see cref="ID.Entity.experience_bottle"/>s, <see cref="ID.Entity.potion"/>s and <see cref="ID.Entity.snowball"/>s
    /// </summary>
    public class Throwable : BaseProjectile
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Throwable> PathCreator => new Data.DataPathCreator<Throwable>();

        /// <summary>
        /// Creates a new <see cref="ID.Entity.egg"/>, <see cref="ID.Entity.ender_pearl"/>, <see cref="ID.Entity.experience_bottle"/>, <see cref="ID.Entity.potion"/> or <see cref="ID.Entity.snowball"/>
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Throwable(ID.Entity? type) : base(type) { }

        /// <summary>
        /// The entity shaking when hitting a block
        /// </summary>
        [Data.DataTag("shake")]
        public byte? Shake { get; set; }

        /// <summary>
        /// The owner of the projectile
        /// </summary>
        [Data.DataTag("Owner", ForceType = ID.NBTTagType.TagIntArray)]
        public UUID? OwnerUUID { get; set; }

        /// <summary>
        /// The type of thrown potion
        /// </summary>
        [Data.DataTag]
        public Item? Potion { get; set; }

        /// <summary>
        /// The item the entity is displayed as
        /// (Potions do not support use this)
        /// </summary>
        [Data.DataTag("Item")]
        public Item? DisplayItem { get; set; }
    }
}
