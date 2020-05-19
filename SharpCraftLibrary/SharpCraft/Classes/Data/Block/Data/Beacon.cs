using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for beacon blocks
    /// </summary>
    public class Beacon : BaseBlockEntity
    {
        private int? _dLevels;

        /// <summary>
        /// Creates a beacon block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Beacon(BlockType? type) : base(type) { }

        /// <summary>
        /// Creates a beacon block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Beacon(ID.Block type = SharpCraft.ID.Block.beacon) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.beacon;
        }

        /// <summary>
        /// The inventory's lock.
        /// (Players have to use an item with the given name to open the block)
        /// </summary>
        [Data.DataTag("Lock")]
        public string? DLock { get; set; }

        /// <summary>
        /// The number of layers the beacon pyramide has
        /// </summary>
        [Data.DataTag("Levels")]
        public int? DLevels
        {
            get => _dLevels;
            set
            {
                if (value != null && (value < 0 || value > 3))
                {
                    throw new ArgumentException(nameof(DLevels) + " has to be equel to or between 0 and 3");
                }
                _dLevels = value;
            }
        }
        /// <summary>
        /// The primary effect chosen in the beacon
        /// </summary>
        [Data.DataTag("Primary", ForceType = SharpCraft.ID.NBTTagType.TagInt)]
        public ID.Effect? DPrimary { get; set; }

        /// <summary>
        /// The secondary effect chosen in the beacon
        /// </summary>
        [Data.DataTag("Secondary", ForceType = SharpCraft.ID.NBTTagType.TagInt)]
        public ID.Effect? DSecondary { get; set; }
    }
}
