using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for dispensers / dropper blocks
    /// </summary>
    public class DropperDispenser : BaseContainer, Interfaces.IPowered, Interfaces.IFacingFull
    {
        private Item?[]? _dItems;

        /// <summary>
        /// Creates a dispenser/dropper block
        /// </summary>
        /// <param name="type">The type of block</param>
        public DropperDispenser(BlockType? type) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.dropper || block == SharpCraft.ID.Block.dispenser;
        }

        /// <summary>
        /// The direction the dispenser / dropper is facing
        /// </summary>
        [BlockState("facing")]
        public ID.FacingFull? SFacing { get; set; }
        /// <summary>
        /// If the dispenser / dropper is powered right now
        /// </summary>
        [BlockState("triggered")]
        public bool? SPowered { get; set; }

        /// <summary>
        /// The item's inside the dispenser / dropper.
        /// (0-8)
        /// </summary>
        public override Item?[]? DItems
        {
            get => _dItems;
            set
            {
                if (DItems != null && DItems.Length > 9)
                {
                    throw new ArgumentException("Too many slots specified");
                }
                _dItems = value;
            }
        }
    }
}
