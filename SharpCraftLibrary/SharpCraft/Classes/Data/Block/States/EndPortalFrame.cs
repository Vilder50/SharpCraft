﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for end portal frame blocks
    /// </summary>
    public class EndPortalFrame : Block, Interfaces.IFacing
    {
        /// <summary>
        /// Creates an end portal frame block
        /// </summary>
        /// <param name="type">The type of block</param>
        public EndPortalFrame(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public EndPortalFrame() : base(SharpCraft.ID.Block.end_portal_frame) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.end_portal_frame;
        }

        /// <summary>
        /// If there is an eye in the portal
        /// </summary>
        [BlockState("eye")]
        public bool? SEye { get; set; }

        /// <summary>
        /// The direction the frame is pointing.
        /// The last frame to get an eye has to face inward the portal.
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }
    }
}
