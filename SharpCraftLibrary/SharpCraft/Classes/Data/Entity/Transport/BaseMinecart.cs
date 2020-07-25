using System.Collections.Generic;
using System;

namespace SharpCraft.Entities
{
    /// <summary>
    /// The basic entity data for minecarts
    /// </summary>
    public abstract class Minecart : BasicEntity
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Minecart> PathCreator => new Data.DataPathCreator<Minecart>();

        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Minecart(ID.Entity? type) : base(type) { }

        /// <summary>
        /// If <see cref="DisplayBlock"/> should be displayed
        /// </summary>
        [Data.DataTag]
        public bool? CustomDisplayTile { get; set; }
        /// <summary>
        /// The block to display
        /// Note: block data is not supported
        /// </summary>
        [Data.DataTag("DisplayState", "Name", "Properties")]
        public Block? DisplayBlock { get; set; }
        /// <summary>
        /// The y-offset the block is displayed with
        /// </summary>
        [Data.DataTag]
        public int? DisplayOffset { get; set; }
    }
}
