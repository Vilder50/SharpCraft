using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for redstone repeater blocks
    /// </summary>
    public class RedstoneRepeater : Block, Interfaces.IPower, Interfaces.IFacing, Interfaces.IPowered
    {
        private int? _sDelay;

        /// <summary>
        /// Creates a repeater block
        /// </summary>
        /// <param name="type">The type of block</param>
        public RedstoneRepeater(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public RedstoneRepeater() : base(SharpCraft.ID.Block.repeater) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.repeater;
        }

        /// <summary>
        /// How many ticks delay the repeater gives.
        /// (1-4 ticks)
        /// </summary>
        [BlockState("power")]
        [BlockIntStateRange(1, 4)]
        public int? SPower
        {
            get => _sDelay;
            set
            {
                if (value != null && (value < 1 || value > 4))
                {
                    throw new ArgumentException(nameof(SPower) + " has to be equel to or between 1 and 4");
                }
                _sDelay = value;
            }
        }

        /// <summary>
        /// The direction the output will be send in
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }

        /// <summary>
        /// If the repeater is locked
        /// </summary>
        [BlockState("locked")]
        public bool? SLocked { get; set; }

        /// <summary>
        /// If the repeater is powered
        /// </summary>
        [BlockState("powered")]
        public bool? SPowered { get; set; }
    }
}
