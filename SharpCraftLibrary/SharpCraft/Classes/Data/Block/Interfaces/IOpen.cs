using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks.Interfaces
{
    /// <summary>
    /// Defines a block state for if the block is open
    /// </summary>
    public interface IOpen
    {
        /// <summary>
        /// If the block is open.
        /// </summary>
        [BlockState("open")]
        bool? SOpen { get; set; }
    }
}