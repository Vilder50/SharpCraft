using System.Collections.Generic;
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
            public MCRange? Absolute { get; set; }

            /// <summary>
            /// the total horizontal distance between two things
            /// </summary>
            [DataTag("horizontal", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
            public MCRange? Horizontal { get; set; }

            /// <summary>
            /// the x amount of distance between two things
            /// </summary>
            [DataTag("x", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
            public MCRange? X { get; set; }

            /// <summary>
            /// the y amount of distance between two things
            /// </summary>
            [DataTag("y", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
            public MCRange? Y { get; set; }

            /// <summary>
            /// the z amount of distance between two things
            /// </summary>
            [DataTag("z", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
            public MCRange? Z { get; set; }
        }
    }
}
