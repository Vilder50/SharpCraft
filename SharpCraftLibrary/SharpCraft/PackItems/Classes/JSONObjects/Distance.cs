﻿using System.Collections.Generic;
using SharpCraft.Data;

namespace SharpCraft
{
    public static partial class JSONObjects
    {
        /// <summary>
        /// A <see cref="object"/> defining a distance
        /// </summary>
        public class Distance : DataHolderBase
        {
            /// <summary>
            /// The total distance between two things
            /// </summary>
            [DataTag("absolute", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
            public Range Absolute { get; set; }

            /// <summary>
            /// the total horizontal distance between two things
            /// </summary>
            [DataTag("horizontal", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
            public Range Horizontal { get; set; }

            /// <summary>
            /// the x amount of distance between two things
            /// </summary>
            [DataTag("x", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
            public Range X { get; set; }

            /// <summary>
            /// the y amount of distance between two things
            /// </summary>
            [DataTag("y", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
            public Range Y { get; set; }

            /// <summary>
            /// the z amount of distance between two things
            /// </summary>
            [DataTag("z", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
            public Range Z { get; set; }

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