﻿using System.Collections.Generic;

namespace SharpCraft.Items
{
    /// <summary>
    /// The data for potions
    /// </summary>
    public class Potion : Item
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Potion> PathCreator => new Data.DataPathCreator<Potion>();

        /// <summary>
        /// Creates an item without an id or anything but which can have data
        /// This is used to test for item with data
        /// </summary>
        public Potion() { }

        /// <summary>
        /// Creates a new item
        /// </summary>
        /// <param name="ItemID">The type of the item. If null the item has no type</param>
        /// <param name="Count">The amount of the item. If null the item has no amount</param>
        /// <param name="Slot">The slot the item is in. If null the item isn't in a slot</param>
        public Potion(IItemType ItemID, sbyte? Count = null, sbyte? Slot = null) : base(ItemID, Count, Slot) { }

        /// <summary>
        /// The effects given by the potion
        /// </summary>
        [Data.DataTag("tag.CustomPotionEffects")]
        public Effect[]? PotionEffects { get; set; }
        /// <summary>
        /// The color of the potion
        /// </summary>
        [Data.DataTag("tag.CustomPotionColor", ForceType = SharpCraft.ID.NBTTagType.TagInt)]
        public RGBColor? PotionColor { get; set; }
        /// <summary>
        /// The effect given by the potion using minecraft values.
        /// </summary>
        [Data.DataTag("tag.Potion", ForceType = SharpCraft.ID.NBTTagType.TagString)]
        public ID.Potion? PotionType { get; set; }
    }
}
