using System.Collections.Generic;

namespace SharpCraft.Items
{
    /// <summary>
    /// An object for crossbow
    /// </summary>
    public class Crossbow : Item
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Crossbow> PathCreator => new Data.DataPathCreator<Crossbow>();

        /// <summary>
        /// Creates an item without an id or anything but which can have data
        /// This is used to test for item with data
        /// </summary>
        public Crossbow() { }

        /// <summary>
        /// Creates a new item
        /// </summary>
        /// <param name="ItemID">The type of the item. If null the item has no type</param>
        /// <param name="Count">The amount of the item. If null the item has no amount</param>
        /// <param name="Slot">The slot the item is in. If null the item isn't in a slot</param>
        public Crossbow(IItemType? ItemID, sbyte? Count = null, sbyte? Slot = null) : base(ItemID, Count, Slot) { }

        /// <summary>
        /// The item there is in the crossbow
        /// </summary>
        [Data.DataTag("tag.ChargedProjectiles")]
        public Item? Projectile { get; set; }

        /// <summary>
        /// If the crossbow is charged
        /// </summary>
        [Data.DataTag]
        public bool? Charged { get; set; }
    }
}
