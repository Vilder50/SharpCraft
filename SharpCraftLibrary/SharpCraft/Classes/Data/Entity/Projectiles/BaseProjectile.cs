using System.Collections.Generic;
using System;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// The base of all projectile entities
        /// </summary>
        public abstract class BaseProjectile : EntityBasic
        {
            /// <summary>
            /// Creates a new entity
            /// </summary>
            /// <param name="type">the type of entity</param>
            public BaseProjectile(ID.Entity? type) : base(type) { }

            /// <summary>
            /// The coords of the block
            /// </summary>
            [Data.DataTag((object)"xTile", "yTile", "zTile", ID.NBTTagType.TagInt, Merge = true)]
            public Coords TileCoords { get; set; }

            /// <summary>
            /// The block this projectile is in
            /// </summary>
            [Data.DataTag("inBlockState","Name","Properties")]
            public Block InBlock { get; set; }
        }
    }
}
