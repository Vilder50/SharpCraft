using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for <see cref="ID.Entity.dragon_fireball"/>s, <see cref="ID.Entity.fireball"/>s, <see cref="ID.Entity.small_fireball"/> and <see cref="ID.Entity.wither_skull"/>s
    /// </summary>
    public class MobProjectile : BaseProjectile
    {
        /// <summary>
        /// Creates a new <see cref="ID.Entity.dragon_fireball"/>, <see cref="ID.Entity.fireball"/>, <see cref="ID.Entity.small_fireball"/> or <see cref="ID.Entity.wither_skull"/>
        /// </summary>
        /// <param name="type">the type of entity</param>
        public MobProjectile(ID.Entity? type) : base(type) { }

        /// <summary>
        /// The direction the projectile flies in
        /// </summary>
        [Data.DataTag("direction", ForceType = ID.NBTTagType.TagDoubleArray)]
        public Vector? Direction { get; set; }
        /// <summary>
        /// The amount of time the projectile hasnt been moving
        /// </summary>
        [Data.DataTag("life")]
        public Time<int>? Life { get; set; }
        /// <summary>
        /// The direction the projectile flies in nonestop
        /// </summary>
        [Data.DataTag("power", ForceType = ID.NBTTagType.TagDoubleArray)]
        public Vector? Power { get; set; }
        /// <summary>
        /// The power of the explosion caused by the ghast ball
        /// </summary>
        [Data.DataTag("ExplosionPower")]
        public int? GhastExplosionPower { get; set; }
    }
}
