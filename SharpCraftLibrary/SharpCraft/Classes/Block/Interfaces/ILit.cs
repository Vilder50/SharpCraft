using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.IBlock
{
    /// <summary>
    /// Defines a block state for if the block is lit
    /// </summary>
    public interface ILit
    {
        /// <summary>
        /// If the block is lit.
        /// </summary>
        [BlockData("lit")]
        bool? SLit { get; set; }
    }
}