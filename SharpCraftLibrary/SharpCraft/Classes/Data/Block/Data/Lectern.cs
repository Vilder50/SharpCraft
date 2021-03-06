﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for lectern blocks
    /// </summary>
    public class Lectern : BaseBlockEntity, Interfaces.IFacing, Interfaces.IPowered
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Lectern> PathCreator => new Data.DataPathCreator<Lectern>();

        /// <summary>
        /// Creates a lectern block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Lectern(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Lectern() : base(SharpCraft.ID.Block.lectern) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.lectern;
        }

        /// <summary>
        /// The way the lectern is facing
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }

        /// <summary>
        /// If the lectern should show a book
        /// </summary>
        [BlockState("has_book")]
        public bool? SHasBook { get; set; }

        /// <summary>
        /// If the lectern is outputting a redstone signal
        /// </summary>
        [BlockState("powered")]
        public bool? SPowered { get; set; }

        /// <summary>
        /// The book in the lectern
        /// </summary>
        [Data.DataTag("Book")]
        public Item? DBook { get; set; }

        /// <summary>
        /// The page the book in the lantern is on
        /// </summary>
        [Data.DataTag("Page")]
        public int? DPage { get; set; }
    }
}
