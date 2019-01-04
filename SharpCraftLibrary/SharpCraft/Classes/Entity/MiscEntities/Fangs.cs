using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// An object for evoker fangs entities
        /// </summary>
        public class EvokerFangs : EntityBasic
        {
            /// <summary>
            /// Creates a new evoker fangs
            /// </summary>
            /// <param name="type">the type of entity</param>
            public EvokerFangs(ID.Entity? type = ID.Entity.evoker_fangs) : base(type) { }

            /// <summary>
            /// The amount of time before the fangs appear
            /// </summary>
            [DataTag]
            public Time Warmup { get; set; }
            /// <summary>
            /// The <see cref="UUID"/> of the entity who summoned the fangs
            /// </summary>
            [DataTag]
            public UUID OwnerUUID { get; set; }

            /// <summary>
            /// Makes the fang into a marker entity
            /// (Sets <see cref="Warmup"/> to its max value which makes the entity invisible)
            /// </summary>
            public bool Marker
            {
                get
                {
                    return Warmup.AsTicks() == int.MaxValue;
                }
                set
                {
                    if (value)
                    {
                        Warmup = int.MaxValue;
                    }
                    else
                    {
                        Warmup = null;
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
                    if (Warmup != null) { TempList.Add("Warmup:" + Warmup.AsTicks()); }
                    if (OwnerUUID != null) { TempList.Add("Owner:{OwnerUUIDLeast:" + OwnerUUID.Least + "L,OwnerUUIDMost:" + OwnerUUID.Most + "L}"); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
