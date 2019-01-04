using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// An object for primed tnt entities
        /// </summary>
        public class TNT : EntityBasic
        {
            /// <summary>
            /// Creates new primed tnt
            /// </summary>
            /// <param name="type">the type of entity</param>
            public TNT(ID.Entity? type = ID.Entity.tnt) : base(type) { }

            /// <summary>
            /// The time in ticks before the tnt blows up
            /// </summary>
            [DataTag]
            public int? Fuse { get; set; }

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
                    if (Fuse != null) { TempList.Add("Fuse:" + Fuse + "s"); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
