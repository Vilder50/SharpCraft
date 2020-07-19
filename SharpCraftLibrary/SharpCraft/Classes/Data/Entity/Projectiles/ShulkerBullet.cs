using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// An object for shulker bullets
    /// </summary>
    public class ShulkerBullet : BasicEntity
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<ShulkerBullet> PathCreator => new Data.DataPathCreator<ShulkerBullet>();

        /// <summary>
        /// Creates a new shulker bullet
        /// </summary>
        /// <param name="type">the type of entity</param>
        public ShulkerBullet(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public ShulkerBullet() : base(SharpCraft.ID.Entity.shulker_bullet) { }

        /// <summary>
        /// The owner of the bullet
        /// </summary>
        [Data.DataTag("Owner", ForceType = ID.NBTTagType.TagIntArray)]
        public UUID? Owner { get; set; }
        /// <summary>
        /// The owner's location
        /// </summary>
        [Data.DataTag("Owner", "X", "Y", "Z")]
        public IntVector? OwnerCoords { get; set; }
        /// <summary>
        /// The bullet's target
        /// </summary>
        [Data.DataTag("Target", ForceType = ID.NBTTagType.TagIntArray)]
        public UUID? Target { get; set; }
        /// <summary>
        /// The target's location
        /// </summary>
        [Data.DataTag("Target", "X", "Y", "Z")]
        public IntVector? TargetCoords { get; set; }
        /// <summary>
        /// The amount of steps it takes to get to the target
        /// </summary>
        [Data.DataTag]
        public int? Steps { get; set; }
        /// <summary>
        /// The offset distance from the bullet to the target
        /// </summary>
        [Data.DataTag((object)"TXD", "TYD", "TZD", Merge = true)]
        public IntVector? OffsetTarget { get; set; }
    }
}
