﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for pillar blocks.
    /// (Blocks like bone blocks and purpur pillars)
    /// </summary>
    public class Pillar : Block
    {
        /// <summary>
        /// Creates a new pillar block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Pillar(IBlockType? type) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            string blockName = block.ToString();
            return block == SharpCraft.ID.Block.bone_block || 
                block == SharpCraft.ID.Block.hay_block || 
                block == SharpCraft.ID.Block.purpur_pillar || 
                block == SharpCraft.ID.Block.quartz_block ||
                blockName.Contains("log") ||
                blockName.Contains("wood") ||
                blockName.Contains("hyphae") ||
                block == SharpCraft.ID.Block.basalt ||
                block == SharpCraft.ID.Block.crimson_stem ||
                block == SharpCraft.ID.Block.stripped_crimson_stem ||
                block == SharpCraft.ID.Block.stripped_warped_stem ||
                block == SharpCraft.ID.Block.warped_stem;
        }

        /// <summary>
        /// The axis the pillar is parallel to
        /// </summary>
        [BlockState("axis")]
        public ID.Axis? SAxis { get; set; }
    }
}
