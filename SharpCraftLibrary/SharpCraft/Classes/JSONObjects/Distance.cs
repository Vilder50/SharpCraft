using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class JSONObjects
    {
        /// <summary>
        /// A <see cref="object"/> defining a distance
        /// </summary>
        public class Distance
        {
            /// <summary>
            /// The total distance between two things
            /// </summary>
            public Range Absolute;

            /// <summary>
            /// the total horizontal distance between two things
            /// </summary>
            public Range Horizontal;

            /// <summary>
            /// the x amount of distance between two things
            /// </summary>
            public Range X;

            /// <summary>
            /// the y amount of distance between two things
            /// </summary>
            public Range Y;

            /// <summary>
            /// the z amount of distance between two things
            /// </summary>
            public Range Z;

            /// <summary>
            /// Outputs this <see cref="Distance"/> data in string format
            /// </summary>
            /// <returns>this <see cref="Distance"/>'s data</returns>
            public override string ToString()
            {
                List<string> TempList = new List<string>();
                if (X != null) { TempList.Add(X.JSONString("x")); }
                if (Y != null) { TempList.Add(X.JSONString("y")); }
                if (Z != null) { TempList.Add(X.JSONString("z")); }
                if (Absolute != null) { TempList.Add(Absolute.JSONString("absolute")); }
                if (Horizontal != null) { TempList.Add(Horizontal.JSONString("horizontal")); }

                return "{" + string.Join(",", TempList) + "}";
            }
        }
    }
}
