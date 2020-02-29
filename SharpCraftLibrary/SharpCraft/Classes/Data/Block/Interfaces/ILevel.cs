using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks.Interfaces
{
    /// <summary>
    /// Defines a block state for the level of the block
    /// </summary>
    public interface ILevel
    {
        /// <summary>
        /// The level of the block.
        /// </summary>
        [BlockState("level")]
        int? SLevel { get; set; }
    }
}