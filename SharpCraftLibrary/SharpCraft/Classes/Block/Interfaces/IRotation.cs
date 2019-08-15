using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.IBlock
{
    /// <summary>
    /// Defines a block state for the rotation of the block
    /// </summary>
    public interface IRotation
    {
        /// <summary>
        /// The rotation of the block.
        /// </summary>
        [BlockData("rotation")]
        int? SRotation { get; set; }
    }
}