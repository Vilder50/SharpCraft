﻿using System.Collections.Generic;

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
            [Data.DataTag(ForceType = ID.NBTTagType.TagString)]
            public ID.Fox? FoxType { get; set; }

            /// <summary>
            /// If the fox is sleeping
            /// </summary>
            [Data.DataTag]
            public bool? Sleeping { get; set; }

            /// <summary>
            /// If the fox is crouching
            /// </summary>
            [Data.DataTag]
            public bool? Crouching { get; set; }
        }
    }
}
