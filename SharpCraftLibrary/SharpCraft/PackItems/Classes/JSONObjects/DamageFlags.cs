using System.Collections.Generic;
using SharpCraft.Data;

namespace SharpCraft
{
    public static partial class JSONObjects
    {
        /// <summary>
        /// a <see cref="object"/> defining damage flags
        /// </summary>
        public class DamageFlags : DataHolderBase
        {
            /// <summary>
            /// if the damage by-passes armor or not
            /// </summary>
            [DataTag("bypasses_armor", JsonTag = true)]
            public bool? ByPassArmor { get; set; }

            /// <summary>
            /// if the damage can be blocked or not
            /// </summary>
            [DataTag("bypasses_invulnerability", JsonTag = true)]
            public bool? Unblockable { get; set; }

            /// <summary>
            /// if the damage is starvation damage or not
            /// </summary>
            [DataTag("bypasses_magic", JsonTag = true)]
            public bool? Starvation { get; set; }

            /// <summary>
            /// if the damage is explosion damage or not
            /// </summary>
            [DataTag("is_explosion", JsonTag = true)]
            public bool? Explosion { get; set; }

            /// <summary>
            /// if the damage if fire damage or not
            /// </summary>
            [DataTag("is_fire", JsonTag = true)]
            public bool? Fire { get; set; }

            /// <summary>
            /// if the damage is magic damage or not
            /// </summary>
            [DataTag("is_magic", JsonTag = true)]
            public bool? Magic { get; set; }

            /// <summary>
            /// if its a projectile or not
            /// </summary>
            [DataTag("is_projectile", JsonTag = true)]
            public bool? Projectile { get; set; }

            /// <summary>
            /// if its lightning or not
            /// </summary>
            [DataTag("is_lightning", JsonTag = true)]
            public bool? Lightning { get; set; }

            /// <summary>
            /// the <see cref="Entity"/> doing damage
            /// </summary>
            [DataTag("direct_entity", JsonTag = true)]
            public Entity DamagingEntity { get; set; }

            /// <summary>
            /// the <see cref="Entity"/> which made the damage happen
            /// </summary>
            [DataTag("source_entity", JsonTag = true)]
            public Entity SourceEntity { get; set; }

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
