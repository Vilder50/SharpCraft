using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks.Interfaces
{
    /// <summary>
    /// Defines a block state for the age of the block
    /// </summary>
    public interface IAge
    {
        /// <summary>
        /// The age of the block.
        /// </summary>
        [BlockState("age")]
        int? SAge { get; set; }
    }
}