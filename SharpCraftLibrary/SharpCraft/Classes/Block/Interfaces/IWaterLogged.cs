using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.IBlock
{
    /// <summary>
    /// Defines a block state for if the block is water logged
    /// </summary>
    public interface IWaterLogged
    {
        /// <summary>
        /// If the block is water logged.
        /// </summary>
        [BlockData("waterlogged")]
        bool? SWaterLogged { get; set; }
    }
}