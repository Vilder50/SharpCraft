using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class JSONObjects
    {
        /// <summary>
        /// a <see cref="object"/> defining damage flags
        /// </summary>
        public class DamageFlags
        {
            /// <summary>
            /// if the damage by-passes armor or not
            /// </summary>
            public bool? ByPassArmor;

            /// <summary>
            /// if the damage can be blocked or not
            /// </summary>
            public bool? Unblockable;

            /// <summary>
            /// if the damage is starvation damage or not
            /// </summary>
            public bool? Starvation;

            /// <summary>
            /// if the damage is explosion damage or not
            /// </summary>
            public bool? Explosion;

            /// <summary>
            /// if the damage if fire damage or not
            /// </summary>
            public bool? Fire;

            /// <summary>
            /// if the damage is magic damage or not
            /// </summary>
            public bool? Magic;

            /// <summary>
            /// if its a projectile or not
            /// </summary>
            public bool? Projectile;

            /// <summary>
            /// if its lightning or not
            /// </summary>
            public bool? Lightning;

            /// <summary>
            /// the <see cref="Entity"/> doing damage
            /// </summary>
            public Entity DamagingEntity;

            /// <summary>
            /// the <see cref="Entity"/> which made the damage happen
            /// </summary>
            public Entity SourceEntity;

            /// <summary>
            /// Outputs this <see cref="DamageFlags"/> data in string format
            /// </summary>
            /// <returns>this <see cref="DamageFlags"/>'s data</returns>
            public override string ToString()
            {
                List<string> TempList = new List<string>();

                if (ByPassArmor != null) { TempList.Add("\"bypasses_armor\":" + ByPassArmor.ToMinecraftBool()); }
                if (Unblockable != null) { TempList.Add("\"bypasses_invulnerability\":" + Unblockable.ToMinecraftBool()); }
                if (Starvation != null) { TempList.Add("\"bypasses_magic\":" + Starvation.ToMinecraftBool()); }
                if (Explosion != null) { TempList.Add("\"is_explosion\":" + Explosion.ToMinecraftBool()); }
                if (Fire != null) { TempList.Add("\"is_fire\":" + Fire.ToMinecraftBool()); }
                if (Magic != null) { TempList.Add("\"is_magic\":" + Magic.ToMinecraftBool()); }
                if (Projectile != null) { TempList.Add("\"is_projectile\":" + Projectile.ToMinecraftBool()); }
                if (Lightning != null) { TempList.Add("\"is_lightning\":" + Lightning.ToMinecraftBool()); }
                if (DamagingEntity != null) { TempList.Add("\"direct_entity\":" + DamagingEntity); }
                if (SourceEntity != null) { TempList.Add("\"source_entity\":" + SourceEntity); }

                return "{" + string.Join(",", TempList) + "}";
            }
        }
    }
}
