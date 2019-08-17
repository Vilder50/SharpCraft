using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
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
            public Arrow(ID.Entity? type = ID.Entity.arrow) : base(type) { }

            /// <summary>
            /// The arrow shaking when hitting a block
            /// </summary>
            [Data.DataTag("shake")]
            public byte? Shake { get; set; }
            /// <summary>
            /// Rules for picking up the arrow
            /// </summary>
            [Data.DataTag("pickup",ForceType = ID.NBTTagType.TagByte)]
            public ID.ArrowPickup? Pickupable { get; set; }
            /// <summary>
            /// If the arrow is shot by a player
            /// </summary>
            [Data.DataTag("player")]
            public bool? PlayerShot { get; set; }
            /// <summary>
            /// When it hits 1200 ticks while not moving the arrow despawns
            /// </summary>
            [Data.DataTag("life",ForceType = ID.NBTTagType.TagShort)]
            public Time Life { get; set; }
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
            public HexColor Color { get; set; }
            /// <summary>
            /// The effects given by the arrow
            /// </summary>
            [Data.DataTag]
            public Effect[] CustomPotionEffects { get; set; }
            /// <summary>
            /// The color of the arrow's particles
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
            public HexColor CustomPotionColor { get; set; }
            /// <summary>
            /// The amount of duration of the glowing effect given by the spectral arrow
            /// </summary>
            [Data.DataTag("Duration", ForceType = ID.NBTTagType.TagInt)]
            public Time SpectralDuration { get; set; }

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
                    if (Shake != null) { TempList.Add("shake:" + Shake + "b"); }
                    if (Pickupable != null) { TempList.Add("pickup:" + (int)Pickupable + "b"); }
                    if (PlayerShot != null) { TempList.Add("player:" + PlayerShot.ToMinecraftBool()); }
                    if (Life != null) { TempList.Add("life:" + Life.AsTicks(Time.TimerType.Short) + "s"); }
                    if (Damage != null) { TempList.Add("damage:" + Damage.ToMinecraftDouble()); }
                    if (InGround != null) { TempList.Add("inGround:" + InGround.ToMinecraftBool()); }
                    if (Crit != null) { TempList.Add("crit:" + Crit.ToMinecraftBool()); }
                    if (Color != null) { TempList.Add("Color:" + Color.ColorInt); }
                    if (CustomPotionColor != null) { TempList.Add("CustomPotionColor:" + CustomPotionColor); }
                    if (CustomPotionEffects != null)
                    {
                        string TempString = "CustomPotionEffects:[";
                        for (int a = 0; a < CustomPotionEffects.Length; a++)
                        {
                            if (a != 0) { TempString += ","; }
                            TempString += "{" + CustomPotionEffects[a] + "}";
                        }
                        TempString += "]";
                        TempList.Add(TempString);
                    }
                    if (SpectralDuration != null) { TempList.Add("Duration:" + SpectralDuration.AsTicks()); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
