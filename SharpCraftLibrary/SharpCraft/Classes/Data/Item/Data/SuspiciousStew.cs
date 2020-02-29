using System.Collections.Generic;

namespace SharpCraft.Items
{
    /// <summary>
    /// An object for suspicious stew
    /// </summary>
    public class SuspiciousStew : Item
    {
        /// <summary>
        /// Creates an item without an id or anything but which can have data
        /// This is used to test for item with data
        /// </summary>
        public SuspiciousStew() { }

        /// <summary>
        /// Creates a new item
        /// </summary>
        /// <param name="ItemID">The type of the item. If null the item has no type</param>
        /// <param name="Count">The amount of the item. If null the item has no amount</param>
        /// <param name="Slot">The slot the item is in. If null the item isn't in a slot</param>
        public SuspiciousStew(ItemType? ItemID, sbyte? Count = null, sbyte? Slot = null) : base(ItemID, Count, Slot) { }

        /// <summary>
        /// The effect the stew gives
        /// </summary>
        [Data.DataTag("tag.EffectId")]
        public ID.Effect? Effect { get; set; }

        /// <summary>
        /// The duration of the effect.
        /// </summary>
        [Data.DataTag("tag.EffectDuration", ForceType = SharpCraft.ID.NBTTagType.TagInt)]
        public Time? Duration { get; set; }
    }
}
