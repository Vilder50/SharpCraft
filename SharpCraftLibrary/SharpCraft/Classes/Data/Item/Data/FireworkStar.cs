using System.Collections.Generic;

namespace SharpCraft.Items
{
    /// <summary>
    /// An object for firework star items
    /// </summary>
    public class FireworkStar : Item
    {
        /// <summary>
        /// Creates an item without an id or anything but which can have data
        /// This is used to test for item with data
        /// </summary>
        public FireworkStar() { }

        /// <summary>
        /// Creates a new item
        /// </summary>
        /// <param name="ItemID">The type of the item. If null the item has no type</param>
        /// <param name="Count">The amount of the item. If null the item has no amount</param>
        /// <param name="Slot">The slot the item is in. If null the item isn't in a slot</param>
        public FireworkStar(IItemType? ItemID, sbyte? Count = null, sbyte? Slot = null) : base(ItemID, Count, Slot) { }

        /// <summary>
        /// The data for a single firework star item
        /// </summary>
        [Data.DataTag("tag.Explosion")]
        public SharpCraft.Firework? Explosion { get; set; }
    }
}
