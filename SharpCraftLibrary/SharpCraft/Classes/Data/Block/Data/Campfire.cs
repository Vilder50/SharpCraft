using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for campfire blocks
        /// </summary>
        public class Campfire : Block, IBlock.IWaterLogged, IBlock.ILit
        {
            private Item?[]? _dItems;
            private Time?[]? _dCookingTimes;
            private Time?[]? _dTotalCookingTimes;

            /// <summary>
            /// Creates a campfire block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Campfire(BlockType? type) : base(type) { }

            /// <summary>
            /// Creates a campfire block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Campfire(ID.Block type = SharpCraft.ID.Block.campfire) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.campfire;
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
            public Item?[]? DItems
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
            [Data.DataTag("CookingTimes", ForceType = SharpCraft.ID.NBTTagType.TagIntArray)]
            public Time?[]? DCookingTimes
            {
                get => _dCookingTimes;
                set
                {
                    if (!(value is null) && value.Length >4)
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
            [Data.DataTag("CookingTotalTimes", ForceType = SharpCraft.ID.NBTTagType.TagIntArray)]
            public Time?[]? DTotalCookingTimes
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
}
