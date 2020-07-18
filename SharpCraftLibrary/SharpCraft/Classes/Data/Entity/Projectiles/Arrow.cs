using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for arrows
    /// </summary>
    public class Arrow : BaseProjectile
    {
        /// <summary>
        /// Creates a new arrow
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Arrow(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Arrow() : base(SharpCraft.ID.Entity.arrow) { }

        /// <summary>
        /// The arrow shaking when hitting a block
        /// </summary>
        [Data.DataTag("shake")]
        public byte? Shake { get; set; }
        /// <summary>
        /// Rules for picking up the arrow
        /// </summary>
        [Data.DataTag("pickup", ForceType = ID.NBTTagType.TagByte)]
        public ID.ArrowPickup? Pickupable { get; set; }
        /// <summary>
        /// If the arrow is shot by a player
        /// </summary>
        [Data.DataTag("player")]
        public bool? PlayerShot { get; set; }
        /// <summary>
        /// When it hits 1200 ticks while not moving the arrow despawns
        /// </summary>
        [Data.DataTag("life")]
        public Time<short>? Life { get; set; }
        /// <summary>
        /// The amount of damage dealt by the arrow
        /// </summary>
        [Data.DataTag("damage")]
        public double? Damage { get; set; }
        /// <summary>
        /// If the arrow is in the ground
        /// </summary>
        [Data.DataTag("inGround")]
        public bool? InGround { get; set; }
        /// <summary>
        /// If the deals critical damage
        /// </summary>
        [Data.DataTag("crit")]
        public bool? Crit { get; set; }
        /// <summary>
        /// The color of the arrow
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
        public RGBColor? Color { get; set; }
        /// <summary>
        /// The effects given by the arrow
        /// </summary>
        [Data.DataTag]
        public Effect[]? CustomPotionEffects { get; set; }
        /// <summary>
        /// The color of the arrow's particles
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
        public RGBColor? CustomPotionColor { get; set; }
        /// <summary>
        /// The amount of duration of the glowing effect given by the spectral arrow
        /// </summary>
        [Data.DataTag("Duration")]
        public Time<int>? SpectralDuration { get; set; }
    }
}
