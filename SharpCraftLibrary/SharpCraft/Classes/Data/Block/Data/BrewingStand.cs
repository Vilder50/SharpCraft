﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for brewing stand blocks.
    /// (Blocks like bone blocks and purpur pillars)
    /// </summary>
    public class BrewingStand : BaseInventory
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<BrewingStand> PathCreator => new Data.DataPathCreator<BrewingStand>();

        private Item[]? _dItems;

        /// <summary>
        /// Creates a brewingstand block
        /// </summary>
        /// <param name="type">The type of block</param>
        public BrewingStand(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public BrewingStand() : base(SharpCraft.ID.Block.brewing_stand) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.brewing_stand;
        }

        /// <summary>
        /// If the brewing stand block should display a bottle in slot 0
        /// </summary>
        [BlockState("has_bottle_0")]
        public bool? SHasBottle0 { get; set; }

        /// <summary>
        /// If the brewing stand block should display a bottle in slot 1
        /// </summary>
        [BlockState("has_bottle_1")]
        public bool? SHasBottle1 { get; set; }

        /// <summary>
        /// If the brewing stand block should display a bottle in slot 2
        /// </summary>
        [BlockState("has_bottle_2")]
        public bool? SHasBottle2 { get; set; }

        /// <summary>
        /// The item's inside the brewing stand.
        /// 0-2 = potion slots (left to right). 3 = ingredient slot. 4 = fuel slot.
        /// </summary>
        [Data.DataTag("Items")]
        public override Item[]? DItems
        {
            get => _dItems;
            set
            {
                if (DItems != null && DItems.Length > 5)
                {
                    throw new ArgumentException("Too many slots specified");
                }
                _dItems = value;
            }
        }

        /// <summary>
        /// The amount of time the potion has brewed.
        /// Done when hitting 20 seconds.
        /// </summary>
        [Data.DataTag("BrewTime")]
        public Time<int>? DBrewTime { get; set; }

        /// <summary>
        /// The amount of fule in the brewing stand.
        /// (Fuel is used up everytime the brewing stand brews)
        /// </summary>
        [Data.DataTag("Fuel", ForceType = SharpCraft.ID.NBTTagType.TagByte)]
        public int? DFule { get; set; }
    }
}
