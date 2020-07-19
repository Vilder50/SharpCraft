using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for jigsaw blocks
    /// </summary>
    public class Jigsaw : BaseBlockEntity
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Jigsaw> PathCreator => new Data.DataPathCreator<Jigsaw>();

        /// <summary>
        /// Creates a jigsaw block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Jigsaw(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Jigsaw() : base(SharpCraft.ID.Block.jigsaw) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.jigsaw;
        }

        /// <summary>
        /// The way the jigsaw is facing
        /// </summary>
        [BlockState("orientation")]
        public ID.JigsawOrientation? SOrientation { get; set; }

        /// <summary>
        /// The name of the structure pool to use
        /// </summary>
        [Data.DataTag("pool")]
        public string? DPool { get; set; }
        /// <summary>
        /// The block the jigsaw should transform into when done
        /// </summary>

        [Data.DataTag("final_state")]
        public ID.Block? DFinishedBlock { get; set; }

        /// <summary>
        /// The name of the jigsaw block.
        /// </summary>
        [Data.DataTag("name")]
        public string? DName { get; set; }

        /// <summary>
        /// The name of the jigsaw blocks this jigsaw connects to.
        /// </summary>
        [Data.DataTag("target")]
        public string? DTarget { get; set; }

        /// <summary>
        /// The type of connection the jigsaw makes
        /// </summary>
        [Data.DataTag("joint")]
        public ID.JigsawJoint? DJoint { get; set; }
    }
}
