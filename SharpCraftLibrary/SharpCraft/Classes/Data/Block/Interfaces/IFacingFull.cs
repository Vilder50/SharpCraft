using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks.Interfaces
{
    /// <summary>
    /// Defines a block state for the direction a block is facing
    /// </summary>
    public interface IFacingFull
    {
        /// <summary>
        /// The direction the block is facing
        /// </summary>
        [BlockState("facing")]
        ID.FacingFull? SFacing { get; set; }
    }
}