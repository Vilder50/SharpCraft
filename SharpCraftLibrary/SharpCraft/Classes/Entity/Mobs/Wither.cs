using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for withers
        /// </summary>
        public class Wither : BaseMob
        {
            /// <summary>
            /// Creates a new wither
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Wither(ID.Entity? type = ID.Entity.wither) : base(type) { }

            /// <summary>
            /// The time before the wither actually spawns / can be fought
            /// </summary>
            [DataTag]
            public Time Invul { get; set; }

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
                    if (Invul != null) { TempList.Add("Invul:" + Invul.AsTicks()); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
