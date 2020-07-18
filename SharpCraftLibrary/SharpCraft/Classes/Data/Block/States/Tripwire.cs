using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for tripwire blocks
    /// </summary>
    public class Tripwire : Block, Interfaces.IConnectedCardinal, Interfaces.IPowered
    {
        /// <summary>
        /// Creates a tripwire block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Tripwire(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Tripwire() : base(SharpCraft.ID.Block.tripwire) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.tripwire;
        }

        /// <summary>
        /// If the block is connected in this direction
        /// </summary>
        [BlockState("east")]
        public bool? SEast { get; set; }

        /// <summary>
        /// If the block is connected in this direction
        /// </summary>
        [BlockState("north")]
        public bool? SNorth { get; set; }

        /// <summary>
        /// If the block is connected in this direction
        /// </summary>
        [BlockState("south")]
        public bool? SSouth { get; set; }

        /// <summary>
        /// If the block is connected in this direction
        /// </summary>
        [BlockState("west")]
        public bool? SWest { get; set; }

        /// <summary>
        /// If the tripwire is attached to tripwire hook
        /// </summary>
        [BlockState("attached")]
        public bool? SAttached { get; set; }

        /// <summary>
        /// If the tripwire has been disarmed
        /// </summary>
        [BlockState("disarmed")]
        public bool? SDisarmed { get; set; }

        /// <summary>
        /// If the tripwire is being stepped on
        /// </summary>
        [BlockState("powered")]
        public bool? SPowered { get; set; }
    }
}
