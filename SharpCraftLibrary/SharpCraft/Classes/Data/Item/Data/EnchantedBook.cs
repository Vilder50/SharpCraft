using System.Collections.Generic;

namespace SharpCraft.Items
{
    /// <summary>
    /// An object for enchanted book items
    /// </summary>
    public class EnchantedBook : Item
    {
        /// <summary>
        /// Creates an item without an id or anything but which can have data
        /// This is used to test for item with data
        /// </summary>
        public EnchantedBook() { }

        /// <summary>
        /// Creates a new item
        /// </summary>
        /// <param name="ItemID">The type of the item. If null the item has no type</param>
        /// <param name="Count">The amount of the item. If null the item has no amount</param>
        /// <param name="Slot">The slot the item is in. If null the item isn't in a slot</param>
        public EnchantedBook(IItemType? ItemID, sbyte? Count = null, sbyte? Slot = null) : base(ItemID, Count, Slot) { }

        /// <summary>
        /// The stored enchantments the item has. These enchants does not effect anything. but can be extracted from the book with an anvil.
        /// </summary>
        [Data.DataTag("tag.StoredEnchantments")]
        public Enchantment[]? StoredEnchants { get; set; }
    }
}
