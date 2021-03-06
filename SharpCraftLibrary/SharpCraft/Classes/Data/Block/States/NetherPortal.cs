﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for nether portal blocks
    /// </summary>
    public class NetherPortal : Block
    {
        /// <summary>
        /// Creates a nether portal block
        /// </summary>
        /// <param name="type">The type of block</param>
        public NetherPortal(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public NetherPortal() : base(SharpCraft.ID.Block.nether_portal) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.nether_portal;
        }

        /// <summary>
        /// The axis the portal is parallel to
        /// </summary>
        [BlockState("east")]
        public ID.StatePortalAxis? SAxis { get; set; }
    }
}
