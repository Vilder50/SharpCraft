﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks.Interfaces
{
    /// <summary>
    /// Defines a block state for the surface the block is placed on
    /// </summary>
    public interface IPlaced
    {
        /// <summary>
        /// The surface the block is placed on
        /// </summary>
        [BlockState("half")]
        ID.StateSimplePlaced? SPlaced { get; set; }
    }
}