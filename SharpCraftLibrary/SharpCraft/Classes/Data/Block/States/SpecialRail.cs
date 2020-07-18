using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for special rail blocks
    /// </summary>
    public class SpecialRail : Block, Interfaces.IPowered
    {
        /// <summary>
        /// Creates a new special rail block
        /// </summary>
        /// <param name="type">The type of block</param>
        public SpecialRail(IBlockType? type) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.activator_rail || block == SharpCraft.ID.Block.detector_rail || block == SharpCraft.ID.Block.powered_rail;
        }

        /// <summary>
        /// The direction the rail is going in.
        /// </summary>
        [BlockState("direction")]
        public ID.StateSpecailRailShape? SDirection { get; set; }

        /// <summary>
        /// If the rail is activated
        /// </summary>
        [BlockState("powered")]
        public bool? SPowered { get; set; }
    }
}
