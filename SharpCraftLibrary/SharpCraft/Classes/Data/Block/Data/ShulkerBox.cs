using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for shulker box and barral blocks
    /// </summary>
    public class ShulkerBox : BaseContainer, Interfaces.IFacingFull
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<ShulkerBox> PathCreator => new Data.DataPathCreator<ShulkerBox>();

        private Item[]? _dItems;

        /// <summary>
        /// Creates a new shulker box
        /// </summary>
        /// <param name="type">The type of block</param>
        public ShulkerBox(IBlockType? type) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            string blockName = block.ToString();
            return (blockName.Contains("shulker"));
        }

        /// <summary>
        /// The direction the shulker box is facing (the way it opens out into )
        /// </summary>
        [BlockState("facing")]
        public ID.FacingFull? SFacing { get; set; }

        /// <summary>
        /// The item's inside the shulker box.
        /// (0-26)
        /// </summary>
        [Data.DataTag("Items")]
        public override Item[]? DItems
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
