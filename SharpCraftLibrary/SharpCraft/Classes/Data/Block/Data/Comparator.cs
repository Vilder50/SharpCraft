﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for comparator blocks
    /// </summary>
    public class Comparator : BaseBlockEntity, Interfaces.IPowered, Interfaces.IFacing
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Comparator> PathCreator => new Data.DataPathCreator<Comparator>();

        /// <summary>
        /// Creates a comparator block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Comparator(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Comparator() : base(SharpCraft.ID.Block.comparator) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.comparator;
        }

        /// <summary>
        /// The direction the comparator is facing
        /// </summary>
        [BlockState("facing")]
        public ID.Facing? SFacing { get; set; }
        /// <summary>
        /// The comparator's compara mode
        /// </summary>
        [BlockState("mode")]
        public ID.StateCompareMode? SCompareMode { get; set; }
        /// <summary>
        /// If the comparator is powered
        /// </summary>
        [BlockState("powered")]
        public bool? SPowered { get; set; }

        /// <summary>
        /// The strenght of the comparators output
        /// </summary>
        [Data.DataTag("OutputSignal")]
        public int? DOutputSignal { get; set; }
    }
}
