using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for conduit blocks
    /// </summary>
    public class Conduit : Block, Interfaces.IWaterLogged
    {
        /// <summary>
        /// Creates a conduit block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Conduit(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Conduit() : base(SharpCraft.ID.Block.conduit) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.conduit;
        }

        /// <summary>
        /// If the conduit is water logged
        /// </summary>
        [BlockState("waterlogged")]
        public bool? SWaterLogged { get; set; }
    }
}
