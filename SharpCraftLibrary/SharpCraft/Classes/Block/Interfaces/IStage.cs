﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.IBlock
{
    /// <summary>
    /// Defines a block state for the stage of the block
    /// </summary>
    public interface IStage
    {
        /// <summary>
        /// The stage of the block.
        /// </summary>
        [BlockData("stage")]
        int? SStage { get; set; }
    }
}