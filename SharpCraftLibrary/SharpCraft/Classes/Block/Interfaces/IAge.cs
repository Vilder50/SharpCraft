﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.IBlock
{
    /// <summary>
    /// Defines a block state for the age of the block
    /// </summary>
    public interface IAge
    {
        /// <summary>
        /// The age of the block.
        /// </summary>
        [BlockData("age")]
        int? SAge { get; set; }
    }
}