﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for campfire blocks
    /// </summary>
    public class Campfire : BaseBlockEntity, Interfaces.IWaterLogged, Interfaces.ILit
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Campfire> PathCreator => new Data.DataPathCreator<Campfire>();

        private Item[]? _dItems;
        private Time<int>[]? _dCookingTimes;
        private Time<int>[]? _dTotalCookingTimes;

        /// <summary>
        /// Creates a campfire block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Campfire(IBlockType? type) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.campfire || block == SharpCraft.ID.Block.soul_campfire;
        }

        /// <summary>
        /// If the block is waterlogged
        /// </summary>
        [BlockState("waterlogged")]
        public bool? SWaterLogged { get; set; }

        /// <summary>
        /// If the fireplace's fire should be shown
        /// </summary>
        [BlockState("lit")]
        public bool? SLit { get; set; }

        /// <summary>
        /// If the smoke from the fireplace should go extra high up
        /// </summary>
        [BlockState("signal_fire")]
        public bool? SSignalFire { get; set; }

        /// <summary>
        /// The direction the block is facing (the way the bottom logs are facing)
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }

        /// <summary>
        /// The items being burned in the fireplace.
        /// Use the item's slot tag to chose the slot they are in.
        /// (The fire place has 4 slots (0-3))
        /// </summary>
        [Data.DataTag("Items")]
        public Item[]? DItems
        {
            get => _dItems;
            set
            {
                if (!(value is null) && value.Length > 4)
                {
                    throw new ArgumentException("Too many items have been specified. The fireplace only has 4 slots");
                }
                _dItems = value;
            }
        }

        /// <summary>
        /// How long the items have been burning.
        /// Each index are the time for a different slot. index 0 = slot 0. index 3 = slot 3.
        /// </summary>
        [Data.DataTag("CookingTimes")]
        public Time<int>[]? DCookingTimes
        {
            get => _dCookingTimes;
            set
            {
                if (!(value is null) && value.Length > 4)
                {
                    throw new ArgumentException("Too many times have been specified. The fireplace can only have 4 times");
                }
                _dCookingTimes = value;
            }
        }

        /// <summary>
        /// How long it takes for the items to burn.
        /// Each index are the time for a different slot. index 0 = slot 0. index 3 = slot 3.
        /// </summary>
        [Data.DataTag("CookingTotalTimes")]
        public Time<int>[]? DTotalCookingTimes
        {
            get => _dTotalCookingTimes;
            set
            {
                if (!(value is null) && value.Length > 4)
                {
                    throw new ArgumentException("Too many times have been specified. The fireplace can only have 4 times");
                }
                _dTotalCookingTimes = value;
            }
        }
    }
}
