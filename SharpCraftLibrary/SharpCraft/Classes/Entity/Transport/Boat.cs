using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for boats
        /// </summary>
        public class Boat : EntityBasic
        {
            /// <summary>
            /// Creates a new boat
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Boat(ID.Entity? type = ID.Entity.boat) : base(type) { }

            /// <summary>
            /// The type of boat
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagString)]
            public ID.Boat? Type { get; set; }

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
                    if (Type != null) { TempList.Add("Type:" + Type); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
