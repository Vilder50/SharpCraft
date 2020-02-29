using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks.Interfaces
{
    /// <summary>
    /// Defines a block state for the stage of the block
    /// </summary>
    public interface IStage
    {
        /// <summary>
        /// The stage of the block.
        /// </summary>
        [BlockState("stage")]
        int? SStage { get; set; }
    }
}