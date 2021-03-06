﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks.Interfaces
{
    /// <summary>
    /// Defines a block state for if the block is water logged
    /// </summary>
    public interface IWaterLogged
    {
        /// <summary>
        /// If the block is water logged.
        /// </summary>
        [BlockState("waterlogged")]
        bool? SWaterLogged { get; set; }
    }
}