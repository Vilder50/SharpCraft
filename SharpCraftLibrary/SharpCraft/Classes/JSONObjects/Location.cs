using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class JSONObjects
    {
        /// <summary>
        /// A <see cref="object"/> defining a location
        /// </summary>
        public class Location
        {
            /// <summary>
            /// The biome to detect
            /// </summary>
            public ID.Biome? Biome;

            /// <summary>
            /// the dimension to detect
            /// </summary>
            public ID.Dimension? Dimension;

            /// <summary>
            /// the structure to detect
            /// </summary>
            public ID.Structure? Structure;

            /// <summary>
            /// the x coordinate to detect
            /// </summary>
            public Range X;

            /// <summary>
            /// the y coordinate to detect
            /// </summary>
            public Range Y;

            /// <summary>
            /// the z coordinate to detect
            /// </summary>
            public Range Z;

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
        }
    }
}
