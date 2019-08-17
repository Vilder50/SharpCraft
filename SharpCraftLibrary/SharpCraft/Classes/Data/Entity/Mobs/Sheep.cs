using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for sheeps
        /// </summary>
        public class Sheep : BaseBreedable
        {
            /// <summary>
            /// Creates a new sheep
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Sheep(ID.Entity? type = ID.Entity.sheep) : base(type) { }

            /// <summary>
            /// The sheep's color
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagByte)]
            public ID.Color? Color { get; set; }
            /// <summary>
            /// If the sheep is sheared
            /// </summary>
            [Data.DataTag]
            public bool? Sheared { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = BreedDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (Color != null) { TempList.Add("Color:" + (int)Color + "b"); }
                    if (Sheared != null) { TempList.Add("Sheared:" + Sheared.ToMinecraftBool()); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
