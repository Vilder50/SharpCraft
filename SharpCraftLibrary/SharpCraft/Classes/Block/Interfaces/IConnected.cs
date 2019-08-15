﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.IBlock
{
    /// <summary>
    /// Defines the block states for the directions the block is connected to
    /// </summary>
    public interface IConnected: IConnectedCardinal
    {
        /// <summary>
        /// If the block is connected downwards
        /// </summary>
        [BlockData("down")]
        bool? SDown { get; set; }

        /// <summary>
        /// If the block is connected upwards
        /// </summary>
        [BlockData("up")]
        bool? SUp { get; set; }
    }
}