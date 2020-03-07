using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// An object for item frames and painting entities
        /// </summary>
        public class Painting : EntityBasic
        {
            /// <summary>
            /// Creates a new item frame or painting entity
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Painting(ID.Entity? type = ID.Entity.painting) : base(type) { }

            /// <summary>
            /// The block the entity is inside
            /// </summary>
            [Data.DataTag((object)"TileX","TileY","TileZ", Merge = true)]
            public IntVector InTile { get; set; }
            /// <summary>
            /// The direction the entity is facing
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagByte)]
            public ID.FacingFull? Facing { get; set; }

            /// <summary>
            /// The type of painting
            /// </summary>
            [Data.DataTag("Motive",ForceType = ID.NBTTagType.TagString)]
            public ID.Painting? Picture { get; set; }
        }
    }
}
