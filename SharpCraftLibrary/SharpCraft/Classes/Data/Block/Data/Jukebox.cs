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
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Jukebox> PathCreator => new Data.DataPathCreator<Jukebox>();

        /// <summary>
        /// Creates a jukebox block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Jukebox(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Jukebox() : base(SharpCraft.ID.Block.jukebox) { }

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
