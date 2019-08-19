﻿using System.Collections.Generic;

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
            [Data.DataTag(ForceType = ID.NBTTagType.TagShort)]
            public int? Age { get; set; }

            /// <summary>
            /// The health of the item. despawns when at 0
            /// </summary>
            [Data.DataTag]
            public byte? Health { get; set; }

            /// <summary>
            /// The amount of xp in the orb
            /// </summary>
            [Data.DataTag]
            public short? Value { get; set; }

            /// <summary>
            /// Makes the orb not despawn
            /// (This overwrites <see cref="Age"/>)
            /// </summary>
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