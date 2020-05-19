using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks.Interfaces
{
    /// <summary>
    /// Defines a block state for what part this block is
    /// </summary>
    public interface IPart
    {
        /// <summary>
        /// The part this block is
        /// </summary>
        [BlockState("half")]
        ID.StatePart? SPart { get; set; }
    }
}