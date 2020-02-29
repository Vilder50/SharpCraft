using System.Collections.Generic;

namespace SharpCraft.Items
{
    /// <summary>
    /// An object for firework rocket items
    /// </summary>
    public class FireworkRocket : Item
    {
        /// <summary>
        /// Creates an item without an id or anything but which can have data
        /// This is used to test for item with data
        /// </summary>
        public FireworkRocket() { }

        /// <summary>
        /// Creates a new item
        /// </summary>
        /// <param name="ItemID">The type of the item. If null the item has no type</param>
        /// <param name="Count">The amount of the item. If null the item has no amount</param>
        /// <param name="Slot">The slot the item is in. If null the item isn't in a slot</param>
        public FireworkRocket(ItemType? ItemID, sbyte? Count = null, sbyte? Slot = null) : base(ItemID, Count, Slot) { }

        /// <summary>
        /// The data for a firework rocket
        /// </summary>
        [Data.DataTag("tag.Fireworks.Explosions")]
        public Firework?[]? Explosion { get; set; }
        /// <summary>
        /// How many seconds the rocket will fly for
        /// </summary>
        [Data.DataTag("tag.Fireworks.Flight")]
        public int? RocketFlight { get; set; }
    }
}
