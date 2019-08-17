using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.IBlock
{
    /// <summary>
    /// Defines a block state for the distance of the block
    /// </summary>
    public interface IDistance
    {
        /// <summary>
        /// The distance of the block.
        /// </summary>
        [BlockState("distance")]
        int? SDistance { get; set; }
    }
}