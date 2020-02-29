using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks.Interfaces
{
    /// <summary>
    /// Defines a block state for the power of the block
    /// </summary>
    public interface IPower
    {
        /// <summary>
        /// The power of the block.
        /// </summary>
        [BlockState("power")]
        int? SPower { get; set; }
    }
}