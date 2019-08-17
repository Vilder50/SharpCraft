using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
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
            public Coords Direction { get; set; }
            /// <summary>
            /// The amount of time the projectile hasnt been moving
            /// </summary>
            [Data.DataTag("life",ForceType = ID.NBTTagType.TagInt)]
            public Time Life { get; set; }
            /// <summary>
            /// The direction the projectile flies in nonestop
            /// </summary>
            [Data.DataTag("power",ForceType = ID.NBTTagType.TagDoubleArray)]
            public Coords Power { get; set; }
            /// <summary>
            /// The power of the explosion caused by the ghast ball
            /// </summary>
            [Data.DataTag("ExplosionPower")]
            public int? GhastExplosionPower { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = ProjectileDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (Life != null) { TempList.Add("Life:" + Life.AsTicks()); }
                    if (GhastExplosionPower != null) { TempList.Add("ExplosionPower:" + GhastExplosionPower); }
                    if (Direction != null) { TempList.Add("direction:[" + Direction.X + "f," + Direction.Y + "f," + Direction.Z + "f]"); }
                    if (Power != null) { TempList.Add("power:[" + Power.X + "f," + Power.Y + "f," + Power.Z + "f]"); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
