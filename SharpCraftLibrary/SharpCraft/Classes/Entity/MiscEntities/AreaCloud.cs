using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// An object for area effect cloud entities
        /// </summary>
        public class AreaCloud : EntityBasic
        {
            /// <summary>
            /// Creates a new area effect cloud
            /// </summary>
            /// <param name="type">the type of entity</param>
            public AreaCloud(ID.Entity? type = ID.Entity.area_effect_cloud) : base(type) { }

            /// <summary>
            /// The amount of time before the cloud disapears after the <see cref="WaitTime"/> is over
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
            public Time Duration { get; set; }

            /// <summary>
            /// The color of the particles it displays
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
            public HexColor Color { get; set; }

            /// <summary>
            /// The amount of time the cloud has existed.
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
            public Time Age { get; set; }

            /// <summary>
            /// The time before the cloud will show up.
            /// (Time before the <see cref="Radius"/> will be used)
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
            public Time WaitTime { get; set; }

            /// <summary>
            /// The time before the cloud's effect will be given out to the entities inside again.
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
            public Time ReapplicationDealy { get; set; }

            /// <summary>
            /// The UUID of the entity who made the cloud
            /// </summary>
            [Data.CustomDataTag]
            public UUID OwnerUUID { get; set; }

            /// <summary>
            /// The amount of time to remove from the duration every time the cloud gives out its effect
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
            public Time DurationOnUse { get; set; }

            /// <summary>
            /// The radius of the cloud
            /// </summary>
            [Data.DataTag]
            public float? Radius { get; set; }

            /// <summary>
            /// The amount of radius to remove every time the cloud gives out its effect
            /// </summary>
            [Data.DataTag]
            public float? RadiusOnUse { get; set; }

            /// <summary>
            /// The amount of radius to remove each tick.
            /// </summary>
            [Data.DataTag]
            public float? RadiusPerTick { get; set; }

            /// <summary>
            /// The particle type the cloud displays
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagString)]
            public ID.Particle? Particle { get; set; }

            /// <summary>
            /// The effect the cloud gives
            /// </summary>
            [Data.DataTag]
            public Effect[] Effects { get; set; }

            /// <summary>
            /// If the cloud shouldn't despawn
            /// (Sets <see cref="Duration"/> to its max value)
            /// </summary>
            public bool Unspawnable
            {
                get
                {
                    return Duration.AsTicks() == int.MaxValue;
                }
                set
                {
                    if (value)
                    {
                        Duration = int.MaxValue;
                    }
                    else
                    {
                        Duration = null;
                    }
                }
            }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = BasicDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (Duration != null) { TempList.Add("Duration:" + Duration.AsTicks()); }
                    if (Effects != null)
                    {
                        string TempString = "Effects:[";
                        for (int a = 0; a < Effects.Length; a++)
                        {
                            if (a != 0) { TempString += ","; }
                            TempString += "{" + Effects[a] + "}";
                        }
                        TempString += "]";
                        TempList.Add(TempString);
                    }
                    if (Color != null) { TempList.Add("Color:" + Color.ColorInt); }
                    if (Age != null) { TempList.Add("Age:" + Age.AsTicks()); }
                    if (WaitTime != null) { TempList.Add("WaitTime:" + WaitTime.AsTicks()); }
                    if (ReapplicationDealy != null) { TempList.Add("ReapplicationDealy:" + ReapplicationDealy.AsTicks()); }
                    if (OwnerUUID != null) { TempList.Add("OwnerUUIDLeast:" + OwnerUUID.Least + ",OwnerUUIDMost:" + OwnerUUID.Most); }
                    if (DurationOnUse != null) { TempList.Add("DurationOnUse:" + DurationOnUse.AsTicks()); }
                    if (Radius != null) { TempList.Add("Radius:" + Radius.ToString().Replace(",", ".") + "f"); }
                    if (RadiusOnUse != null) { TempList.Add("RadiusOnUse:" + RadiusOnUse.ToString().Replace(",", ".") + "f"); }
                    if (RadiusPerTick != null) { TempList.Add("RadiusPerTick:" + RadiusPerTick.ToString().Replace(",", ".") + "f"); }
                    if (Particle != null) { TempList.Add("Particle:" + Particle); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
