using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// Base object for block entities
    /// </summary>
    public abstract class BaseBlockEntity : Block
    {
        /// <summary>
        /// Creates a block entity
        /// </summary>
        /// <param name="type">The type of block</param>
        public BaseBlockEntity(IBlockType? type) : base(type) { }

        /// <summary>
        /// The location the block entity is at
        /// </summary>
        [Data.DataTag((object)"x", "y", "z", Merge = true)]
        public IntVector? DBlockEntityCoords { get; set; }

        /// <summary>
        /// The id of the block entity
        /// </summary>
        [Data.DataTag("id")]
        public string? DBlockEntityId { get; set; }
    }
}
