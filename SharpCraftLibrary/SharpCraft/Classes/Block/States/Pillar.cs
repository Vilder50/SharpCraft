using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for pillar blocks.
        /// (Blocks like bone blocks and purpur pillars)
        /// </summary>
        public class Pillar : Block
        {
            /// <summary>
            /// Intilizes a new block object
            /// </summary>
            public Pillar()
            {
                ID = null;
            }

            /// <summary>
            /// Creates a new pillar block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Pillar(ID.Block? type) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Pillar(Group group) : base(group) { }

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
                    blockName.Contains("wood");
            }

            /// <summary>
            /// The axis the pillar is parallel to
            /// </summary>
            [BlockState("axis")]
            public ID.Axis? SAxis { get; set; }
        }
    }
}
