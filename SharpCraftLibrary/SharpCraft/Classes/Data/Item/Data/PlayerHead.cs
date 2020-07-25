using System.Collections.Generic;

namespace SharpCraft.Items
{
    /// <summary>
    /// An object for player heads
    /// </summary>
    public class PlayerHead : Item
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<PlayerHead> PathCreator => new Data.DataPathCreator<PlayerHead>();

        /// <summary>
        /// Creates an item without an id or anything but which can have data
        /// This is used to test for item with data
        /// </summary>
        public PlayerHead() { }

        /// <summary>
        /// Creates a new item
        /// </summary>
        /// <param name="ItemID">The type of the item. If null the item has no type</param>
        /// <param name="Count">The amount of the item. If null the item has no amount</param>
        /// <param name="Slot">The slot the item is in. If null the item isn't in a slot</param>
        public PlayerHead(IItemType? ItemID, sbyte? Count = null, sbyte? Slot = null) : base(ItemID, Count, Slot) { }

        /// <summary>
        /// The name of the player whose skin to display. Tag doesn't work with other head tags.
        /// </summary>
        [Data.DataTag("tag.SkullOwner")]
        public string? PlayerName { get; set; }

        /// <summary>
        /// The UUID of the player to show the skin for.
        /// </summary>
        [Data.DataTag("tag.SkullOwner.Id","Id", ForceType = SharpCraft.ID.NBTTagType.TagString)]
        public UUID? UUID { get; set; }

        /// <summary>
        /// The name of the player whose skin to display.
        /// </summary>
        [Data.DataTag("tag.SkullOwner.Name")]
        public string? HeadName { get; set; }

        /// <summary>
        /// Textures saved in the skull.
        /// </summary>
        [Data.DataTag("tag.SkullOwner.Properties.textures")]
        public HeadTexture[]? Textures { get; set; }
    }
}
