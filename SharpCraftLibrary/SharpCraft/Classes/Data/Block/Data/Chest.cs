using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for chest blocks
    /// </summary>
    public class Chest : BaseContainer, Interfaces.IFacing, Interfaces.IWaterLogged
    {
        private Item[]? _dItems;

        /// <summary>
        /// Creates a chest block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Chest(BlockType? type) : base(type) { }

        /// <summary>
        /// Creates a chest block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Chest(ID.Block type = SharpCraft.ID.Block.chest) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.chest || block == SharpCraft.ID.Block.trapped_chest;
        }

        /// <summary>
        /// If the chest is water logged
        /// </summary>
        [BlockState("waterlogged")]
        public bool? SWaterLogged { get; set; }

        /// <summary>
        /// The direction the chest is facing
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }

        /// <summary>
        /// How the chest is connected to another chest
        /// </summary>
        [BlockState("type")]
        public ID.StateChestType? SConnectionType { get; set; }

        /// <summary>
        /// The item's inside the chest.
        /// (0-26)
        /// </summary>
        [Data.DataTag("Items")]
        public override Item[]? DItems
        {
            get => _dItems;
            set
            {
                if (!(DItems is null) && DItems.Length > 27)
                {
                    throw new ArgumentException("Too many slots specified");
                }
                _dItems = value;
            }
        }
    }
}
