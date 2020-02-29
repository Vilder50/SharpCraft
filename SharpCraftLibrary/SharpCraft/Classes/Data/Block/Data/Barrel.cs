using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for shulker box and barral blocks
    /// </summary>
    public class Barrel : BaseContainer, Interfaces.IOpen, Interfaces.IFacingFull
    {
        private Item?[]? _dItems;

        /// <summary>
        /// Creates a barrel block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Barrel(BlockType? type) : base(type) { }

        /// <summary>
        /// Creates a barrel block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Barrel(ID.Block type = SharpCraft.ID.Block.barrel) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.barrel;
        }

        /// <summary>
        /// The direction the barrel is facing (the way it opens out into )
        /// </summary>
        [BlockState("facing")]
        public ID.FacingFull? SFacing { get; set; }

        /// <summary>
        /// If the barrel is open or not
        /// </summary>
        [BlockState("open")]
        public bool? SOpen { get; set; }

        /// <summary>
        /// The item's inside the barrel.
        /// (0-26)
        /// </summary>
        [Data.DataTag("Items")]
        public override Item?[]? DItems
        {
            get => _dItems;
            set
            {
                if (DItems != null && DItems.Length > 27)
                {
                    throw new ArgumentException("Too many slots specified");
                }
                _dItems = value;
            }
        }
    }
}
