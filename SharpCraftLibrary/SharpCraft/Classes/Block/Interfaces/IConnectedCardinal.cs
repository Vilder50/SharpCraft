using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.IBlock
{
    /// <summary>
    /// Defines the block states for the cardinal directions the block is connected to
    /// </summary>
    public interface IConnectedCardinal
    {
        /// <summary>
        /// If the block is connected in east
        /// </summary>
        [BlockState("east")]
        bool? SEast { get; set; }

        /// <summary>
        /// If the block is connected in north
        /// </summary>
        [BlockState("north")]
        bool? SNorth { get; set; }

        /// <summary>
        /// If the block is connected in south
        /// </summary>
        [BlockState("south")]
        bool? SSouth { get; set; }

        /// <summary>
        /// If the block is connected in west
        /// </summary>
        [BlockState("west")]
        bool? SWest { get; set; }
    }
}