using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks.Interfaces
{
    /// <summary>
    /// Defines a block state for if the block is lit
    /// </summary>
    public interface ILit
    {
        /// <summary>
        /// If the block is lit.
        /// </summary>
        [BlockState("lit")]
        bool? SLit { get; set; }
    }
}