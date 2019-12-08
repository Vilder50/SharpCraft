using System.Collections.Generic;
using SharpCraft.Data;

namespace SharpCraft
{
    public static partial class JSONObjects
    {
        /// <summary>
        /// A <see cref="object"/> defining a location
        /// </summary>
        public class Location : DataHolderBase
        {
            /// <summary>
            /// The biome to detect
            /// </summary>
            [DataTag("biome", JsonTag = true, ForceType = ID.NBTTagType.TagNamespacedString)]
            public ID.Biome? Biome { get; set; }

            /// <summary>
            /// the dimension to detect
            /// </summary>
            [DataTag("dimension", JsonTag = true, ForceType = ID.NBTTagType.TagNamespacedString)]
            public ID.Dimension? Dimension { get; set; }

            /// <summary>
            /// the structure to detect
            /// </summary>
            [DataTag("structure", JsonTag = true, ForceType = ID.NBTTagType.TagNamespacedString)]
            public ID.Structure? Structure { get; set; }

            /// <summary>
            /// the x coordinate to detect
            /// </summary>
            [DataTag("position.x","min","max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
            public Range X { get; set; }

            /// <summary>
            /// the y coordinate to detect
            /// </summary>
            [DataTag("position.y", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
            public Range Y { get; set; }

            /// <summary>
            /// the z coordinate to detect
            /// </summary>
            [DataTag("position.z", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
            public Range Z { get; set; }

            /// <summary>
            /// Outputs this <see cref="Location"/> data in string format
            /// </summary>
            /// <returns>this <see cref="Location"/>'s data</returns>
            public override string ToString()
            {
                List<string> TempList = new List<string>();
                if (Biome != null) { TempList.Add("\"biome\": \"" + Biome + "\""); }
                if (Structure != null) { TempList.Add("\"feature\": \"" + Structure + "\""); }
                if (Dimension != null) { TempList.Add("\"dimension\": \"" + Dimension + "\""); }
                if (Y != null || X != null || Z != null)
                {
                    List<string> TempPositionList = new List<string>();
                    if (X != null) { TempPositionList.Add(Y.JSONString("x")); }
                    if (Y != null) { TempPositionList.Add(Y.JSONString("y")); }
                    if (Z != null) { TempPositionList.Add(Y.JSONString("z")); }
                    TempList.Add("\"position\": {" + string.Join(",", TempPositionList) + "}");
                }
                return string.Join(",", TempList);
            }
            
            /// <summary>
            /// Converts a <see cref="ID.Biome"/> into a <see cref="Location"/>
            /// </summary>
            /// <param name="biome">The <see cref="ID.Biome"/> to convert</param>
            public static implicit operator Location(ID.Biome biome)
            {
                return new Location() { Biome = biome };
            }

            /// <summary>
            /// Converts a <see cref="ID.Structure"/> into a <see cref="Location"/>
            /// </summary>
            /// <param name="structure">The <see cref="ID.Structure"/> to convert</param>
            public static implicit operator Location(ID.Structure structure)
            {
                return new Location() { Structure = structure };
            }

            /// <summary>
            /// Converts a <see cref="ID.Dimension"/> into a <see cref="Location"/>
            /// </summary>
            /// <param name="dimension">The <see cref="ID.Dimension"/> to convert</param>
            public static implicit operator Location(ID.Dimension dimension)
            {
                return new Location() { Dimension = dimension };
            }
        }
    }
}
