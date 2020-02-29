using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// An object for item frames and painting entities
        /// </summary>
        public class ItemPainting : EntityBasic
        {
            /// <summary>
            /// Creates a new item frame or painting entity
            /// </summary>
            /// <param name="type">the type of entity</param>
            public ItemPainting(ID.Entity? type = ID.Entity.item_frame) : base(type) { }

            /// <summary>
            /// The block the entity is inside
            /// </summary>
            [Data.DataTag((object)"TileX","TileY","TileZ", Merge = true)]
            public IntVector? InTile { get; set; }
            /// <summary>
            /// The direction the entity is facing
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagByte)]
            public ID.FacingFull? Facing { get; set; }

            /// <summary>
            /// The type of painting
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagString)]
            public ID.Painting? Painting { get; set; }

            /// <summary>
            /// The item in the item frame
            /// </summary>
            [Data.DataTag("Item")]
            public Item? FrameItem { get; set; }

            /// <summary>
            /// The chance of the frame dropping its item
            /// (0-1)
            /// </summary>
            [Data.DataTag("ItemDropChance")]
            public float? FrameDropChance { get; set; }

            /// <summary>
            /// The rotation of the item in the item frame.
            /// Rotation = <see cref="FrameRotation"/> * 45 degrees clockwise
            /// </summary>
            [Data.DataTag("ItemRotation")]
            public sbyte? FrameRotation { get; set; }
        }
    }
}
