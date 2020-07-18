using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for redstone wire blocks
    /// </summary>
    public class RedstoneDust : Block, Interfaces.IPower
    {
        private int? _sPower;

        /// <summary>
        /// Creates a redstone dust block
        /// </summary>
        /// <param name="type">The type of block</param>
        public RedstoneDust(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public RedstoneDust() : base(SharpCraft.ID.Block.redstone_wire) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.redstone_wire;
        }

        /// <summary>
        /// How much power the dust has
        /// (0-15)
        /// </summary>
        [BlockState("power")]
        [BlockIntStateRange(0, 15)]
        public int? SPower
        {
            get => _sPower;
            set
            {
                if (value != null && (value < 0 || value > 15))
                {
                    throw new ArgumentException(nameof(SPower) + " has to be equel to or between 0 and 15");
                }
                _sPower = value;
            }
        }

        /// <summary>
        /// The way the redstone is connected in the direction
        /// </summary>
        [BlockState("east")]
        public ID.StateRedstoneConnection? SEast { get; set; }

        /// <summary>
        /// The way the redstone is connected in the direction
        /// </summary>
        [BlockState("north")]
        public ID.StateRedstoneConnection? SNorth { get; set; }

        /// <summary>
        /// The way the redstone is connected in the direction
        /// </summary>
        [BlockState("south")]
        public ID.StateRedstoneConnection? SSouth { get; set; }

        /// <summary>
        /// The way the redstone is connected in the direction
        /// </summary>
        [BlockState("west")]
        public ID.StateRedstoneConnection? SWest { get; set; }
    }
}
