using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for tripwire hook blocks
    /// </summary>
    public class TripwireHook : Block, Interfaces.IFacing, Interfaces.IPowered
    {
        /// <summary>
        /// Creates a tripwire hook block
        /// </summary>
        /// <param name="type">The type of block</param>
        public TripwireHook(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public TripwireHook() : base(SharpCraft.ID.Block.tripwire_hook) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.tripwire_hook;
        }

        /// <summary>
        /// If the tripwire hook is attached to a tripwire
        /// </summary>
        [BlockState("attached")]
        public bool? SAttached { get; set; }

        /// <summary>
        /// The direction the tripwire hook is facing into
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }

        /// <summary>
        /// If the tripwire hook's tripwire is being stepped on
        /// </summary>
        [BlockState("powered")]
        public bool? SPowered { get; set; }
    }
}
