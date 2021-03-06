﻿using System.Collections.Generic;

namespace SharpCraft.Items
{
    /// <summary>
    /// An object for compass items
    /// </summary>
    public class Compass : Item
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Compass> PathCreator => new Data.DataPathCreator<Compass>();

        /// <summary>
        /// Creates an item without an id or anything but which can have data
        /// This is used to test for item with data
        /// </summary>
        public Compass() { }

        /// <summary>
        /// Creates a new item
        /// </summary>
        /// <param name="ItemID">The type of the item. If null the item has no type</param>
        /// <param name="Count">The amount of the item. If null the item has no amount</param>
        /// <param name="Slot">The slot the item is in. If null the item isn't in a slot</param>
        public Compass(IItemType ItemID, sbyte? Count = null, sbyte? Slot = null) : base(ItemID, Count, Slot) { }

        /// <summary>
        /// The dimension of the lodestone the compass is tracking
        /// </summary>
        [Data.DataTag("tag.LodestoneDimension")]
        public DimensionObjects.IDimension? LodestoneDimension { get; set; }

        /// <summary>
        /// If the compass is tracking a lodestone
        /// </summary>
        [Data.DataTag("tag.LodestoneTracked")]
        public bool? TrackingLodestone { get; set; }

        /// <summary>
        /// The location of the lodestone the compass is tracking
        /// </summary>
        [Data.DataTag("tag.LodestonePos", "X", "Y", "Z")]
        public IntVector? LodestonePosition { get; set; }
    }
}
