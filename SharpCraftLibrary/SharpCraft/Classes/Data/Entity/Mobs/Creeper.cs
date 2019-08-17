using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for creepers
        /// </summary>
        public class Creeper : BaseMob
        {
            /// <summary>
            /// Creates a new creeper
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Creeper(ID.Entity? type = ID.Entity.creeper) : base(type) { }

            /// <summary>
            /// If the creeper is charged (was hit by lightning)
            /// </summary>
            [Data.DataTag("powered")]
            public bool? Charged { get; set; }

            /// <summary>
            /// The size of the explosion caused by the exploded creeper
            /// </summary>
            [Data.DataTag]
            public byte? ExplosionRadius { get; set; }

            /// <summary>
            /// The time till the creeper will explode when trying to explode
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagShort)]
            public Time Fuse { get; set; }

            /// <summary>
            /// If the creeper is ignited and is forced to blow up
            /// </summary>
            [Data.DataTag]
            public bool? Ignited { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = MobDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (Charged != null) { TempList.Add("powered:" + Charged); }
                    if (Ignited != null) { TempList.Add("ignited:" + Ignited); }
                    if (ExplosionRadius != null) { TempList.Add("ExplosionRadius:" + ExplosionRadius + "b"); }
                    if (Fuse != null) { TempList.Add("Fuse:" + Fuse.AsTicks(Time.TimerType.Short) + "s"); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
