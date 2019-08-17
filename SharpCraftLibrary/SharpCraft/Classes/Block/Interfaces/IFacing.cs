using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.IBlock
{
    /// <summary>
    /// Defines a block state for the direction a block is facing
    /// </summary>
    public interface IFacing
    {
        /// <summary>
        /// The direction the block is facing
        /// </summary>
        [BlockState("facing")]
        ID.Facing? SFacing { get; set; }
    }
}