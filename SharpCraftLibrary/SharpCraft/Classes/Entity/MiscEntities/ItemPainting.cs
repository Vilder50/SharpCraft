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
            [DataTag]
            public Coords InTile { get; set; }
            /// <summary>
            /// The direction the entity is facing
            /// </summary>
            [DataTag]
            public ID.Facing? Facing { get; set; }
            /// <summary>
            /// The type of painting
            /// </summary>
            [DataTag]
            public ID.Painting? Painting { get; set; }
            /// <summary>
            /// The item in the item frame
            /// </summary>
            [DataTag]
            public Item FrameItem { get; set; }
            /// <summary>
            /// The chance of the frame dropping its item
            /// (0-1)
            /// </summary>
            [DataTag]
            public double? FrameDropChance { get; set; }
            /// <summary>
            /// The rotation of the item in the item frame.
            /// Rotation = <see cref="FrameRotation"/> * 45 degrees clockwise
            /// </summary>
            [DataTag]
            public int? FrameRotation { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            /// <returns>raw data Minecraft uses</returns>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = BasicDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (InTile != null) { TempList.Add("TileX:" + InTile.X + ",TileY:" + InTile.Y + ",TileZ:" + InTile.Z); }
                    if (Facing != null) { TempList.Add("Facing:" + Facing + "b"); }
                    if (Painting != null) { TempList.Add("Motive:\"" + Painting + "\""); }
                    if (FrameItem != null) { TempList.Add("Item:{" + FrameItem.DataString + "}"); }
                    if (FrameDropChance != null) { TempList.Add("ItemDropChance:" + FrameDropChance.ToString().Replace(",", ".") + "f"); }
                    if (FrameRotation != null) { TempList.Add("ItemRotation:" + FrameRotation + "b"); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
