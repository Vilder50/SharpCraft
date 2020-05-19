using System.Collections.Generic;

namespace SharpCraft.Items
{
    /// <summary>
    /// An object for colorable leather items
    /// </summary>
    public class Leather : Item
    {
        /// <summary>
        /// Creates an item without an id or anything but which can have data
        /// This is used to test for item with data
        /// </summary>
        public Leather() { }

        /// <summary>
        /// Creates a new item
        /// </summary>
        /// <param name="ItemID">The type of the item. If null the item has no type</param>
        /// <param name="Count">The amount of the item. If null the item has no amount</param>
        /// <param name="Slot">The slot the item is in. If null the item isn't in a slot</param>
        public Leather(ItemType? ItemID, sbyte? Count = null, sbyte? Slot = null) : base(ItemID, Count, Slot) { }

        /// <summary>
        /// The color of the leather armor
        /// </summary>
        [Data.DataTag("tag.display.color", ForceType = SharpCraft.ID.NBTTagType.TagInt)]
        public RGBColor? Color { get; set; }
    }
}
