using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks.Interfaces
{
    /// <summary>
    /// Defines the block states for the directions the block is connected to
    /// </summary>
    public interface IConnected: IConnectedCardinal
    {
        /// <summary>
        /// If the block is connected downwards
        /// </summary>
        [BlockState("down")]
        bool? SDown { get; set; }

        /// <summary>
        /// If the block is connected upwards
        /// </summary>
        [BlockState("up")]
        bool? SUp { get; set; }
    }
}