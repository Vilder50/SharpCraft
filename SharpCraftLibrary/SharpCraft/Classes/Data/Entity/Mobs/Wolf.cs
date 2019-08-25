using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for wolves
        /// </summary>
        public class Wolf : BaseTameable
        {
            /// <summary>
            /// Creates a new wolf
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Wolf(ID.Entity? type = ID.Entity.wolf) : base(type) { }

            /// <summary>
            /// The color of the wolf's collar
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagByte)]
            public ID.Color? Color { get; set; }
            /// <summary>
            /// If the wolf is angry
            /// </summary>
            [Data.DataTag]
            public bool? Angry { get; set; }
        }
    }
}
