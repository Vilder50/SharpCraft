using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for jukebox blocks
    /// </summary>
    public class Jukebox : BaseBlockEntity
    {
        /// <summary>
        /// Creates a jukebox block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Jukebox(BlockType? type) : base(type) { }

        /// <summary>
        /// Creates a jukebox block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Jukebox(ID.Block type = SharpCraft.ID.Block.jukebox) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.jukebox;
        }

        /// <summary>
        /// If the jukebox block should display it has an item inside
        /// </summary>
        [BlockState("has_record")]
        public bool? SHasRecord { get; set; }

        /// <summary>
        /// The item in the jukebox
        /// </summary>
        [Data.DataTag("RecordItem")]
        public Item? DRecordItem { get; set; }
    }
}
