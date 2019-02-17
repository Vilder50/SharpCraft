using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for foxes
        /// </summary>
        public class Fox : BaseTameable
        {
            /// <summary>
            /// Creates a new fox
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Fox(ID.Entity? type = ID.Entity.fox) : base(type) { }

            /// <summary>
            /// The fox' skin
            /// </summary>
            [DataTag]
            public ID.Fox? FoxType { get; set; }

            /// <summary>
            /// If the fox is sleeping
            /// </summary>
            [DataTag]
            public bool? Sleeping { get; set; }

            /// <summary>
            /// If the fox is crouching
            /// </summary>
            [DataTag]
            public bool? Crouching { get; set; }
            

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = TameDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (FoxType != null) { TempList.Add("Type:\"" + FoxType + "\""); }
                    if (Sleeping != null) { TempList.Add("Sleeping:" + Sleeping.ToMinecraftBool() + "b"); }
                    if (Crouching != null) { TempList.Add("Crouching:" + Crouching.ToMinecraftBool() + "b"); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
