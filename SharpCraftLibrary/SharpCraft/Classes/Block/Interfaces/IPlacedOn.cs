using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.IBlock
{
    /// <summary>
    /// Defines a block state for the surface the block is placed on
    /// </summary>
    public interface IPlacedOn
    {
        /// <summary>
        /// The surface the block is placed on
        /// </summary>
        [BlockData("facing")]
        ID.StatePlaced? SPlacedOn { get; set; }
    }
}