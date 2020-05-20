using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks.Interfaces
{
    /// <summary>
    /// Defines a block state for if the block is powered
    /// </summary>
    public interface IPowered
    {
        /// <summary>
        /// If the block is powered.
        /// </summary>
        [BlockState("powered")]
        bool? SPowered { get; set; }
    }
}