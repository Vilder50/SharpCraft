using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for hopper blocks
    /// </summary>
    public class Hopper : BaseContainer
    {
        private Item[]? _dItems;

        /// <summary>
        /// Creates a hopper block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Hopper(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Hopper() : base(SharpCraft.ID.Block.hopper) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.hopper;
        }

        /// <summary>
        /// If the hopper is allowed to transfer items
        /// </summary>
        [BlockState("enabled")]
        public bool? SEnabled { get; set; }
        /// <summary>
        /// The direction the hopper is facing
        /// </summary>
        [BlockState("facing")]
        public ID.StateHopperFacing? SFacing { get; set; }

        /// <summary>
        /// The item's inside the hopper.
        /// (0-4)
        /// </summary>
        [Data.DataTag("Items")]
        public override Item[]? DItems
        {
            get => _dItems;
            set
            {
                if (!(DItems is null) && DItems.Length > 5)
                {
                    throw new ArgumentException("Too many slots specified");
                }
                _dItems = value;
            }
        }

        /// <summary>
        /// The time till the hopper again will transfer an item.
        /// </summary>
        [Data.DataTag("TransferCooldown")]
        public Time<int>? DCooldown { get; set; }
    }
}
