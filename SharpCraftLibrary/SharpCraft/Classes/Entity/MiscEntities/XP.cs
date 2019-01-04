using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// An object for xp orb entities
        /// </summary>
        public class XP : EntityBasic
        {
            /// <summary>
            /// Creates a new xp orb
            /// </summary>
            /// <param name="type">the type of entity</param>
            public XP(ID.Entity? type = ID.Entity.experience_orb) : base(type) { }

            /// <summary>
            /// The age of the item in ticks. When it hits 6000 it despawns
            /// </summary>
            [DataTag]
            public int? Age { get; set; }
            /// <summary>
            /// The health of the item. despawns when at 0
            /// </summary>
            [DataTag]
            public int? Health { get; set; }
            /// <summary>
            /// The amount of xp in the orb
            /// </summary>
            [DataTag]
            public int? Value { get; set; }
            /// <summary>
            /// Makes the orb not despawn
            /// (This overwrites <see cref="Age"/>)
            /// </summary>
            [DataTag]
            public bool IgnoreAge
            {
                get
                {
                    if (Age != -32768)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                set
                {
                    if (value)
                    {
                        Age = -32768;
                    }
                    else
                    {
                        Age = null;
                    }
                }
            }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            /// <returns>raw data Minecraft uses</returns>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = BasicDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (Age != null) { TempList.Add("Age:" + Age + "s"); }
                    if (Health != null) { TempList.Add("Health:" + Health + "b"); }
                    if (Value != null) { TempList.Add("Value:" + Value + "s"); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
