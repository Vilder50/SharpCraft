using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.IBlock
{
    /// <summary>
    /// Defines a block state for the direction a block is facing
    /// </summary>
    public interface IFacingFull
    {
        /// <summary>
        /// The direction the block is facing
        /// </summary>
        [BlockData("facing")]
        ID.FacingFull? SFacing { get; set; }
    }
}