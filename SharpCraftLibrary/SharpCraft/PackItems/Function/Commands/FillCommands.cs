﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which fills a square with a block
    /// </summary>
    public class FillCommand : BaseCommand
    {
        private Vector corner1 = null!;
        private Vector corner2 = null!;
        private Block block = null!;

        /// <summary>
        /// Intializes a new <see cref="FillCommand"/>
        /// </summary>
        /// <param name="corner1">One of the corners of the square to fill</param>
        /// <param name="corner2">The oppesite corner of the square to fill</param>
        /// <param name="block">The block to fill with</param>
        /// <param name="fillMode">The way to fill</param>
        public FillCommand(Vector corner1, Vector corner2, Block block, ID.BlockFill fillMode)
        {
            Corner1 = corner1;
            Corner2 = corner2;
            Block = block;
            FillMode = fillMode;
        }

        /// <summary>
        /// One of the corners of the square to fill
        /// </summary>
        public Vector Corner1
        {
            get => corner1;
            set
            {
                corner1 = value ?? throw new ArgumentNullException(nameof(Corner1), "Corner1 may not be null.");
            }
        }

        /// <summary>
        /// The oppesite corner of the square to fill
        /// </summary>
        public Vector Corner2
        {
            get => corner2;
            set
            {
                corner2 = value ?? throw new ArgumentNullException(nameof(Corner2), "Corner2 may not be null.");
            }
        }

        /// <summary>
        /// The block to fill with
        /// </summary>
        public Block Block
        {
            get => block;
            set
            {
                block = value ?? throw new ArgumentNullException(nameof(Block), "Block may not be null.");
            }
        }

        /// <summary>
        /// The way to fill
        /// </summary>
        public ID.BlockFill FillMode { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>fill [Corner1] [Corner2] [Block] [FillMode]</returns>
        public override string GetCommandString()
        {
            if (FillMode == ID.BlockFill.replace)
            {
                return $"fill {Corner1.GetVectorString()} {Corner2.GetVectorString()} {Block.GetBlockPlacementString()}";
            }
            else
            {
                return $"fill {Corner1.GetVectorString()} {Corner2.GetVectorString()} {Block.GetBlockPlacementString()} {FillMode}";
            }
        }
    }

    /// <summary>
    /// Command which replaces all blocks in a square with another block
    /// </summary>
    public class FillReplaceCommand : BaseCommand
    {
        private Vector corner1 = null!;
        private Vector corner2 = null!;
        private Block block = null!;
        private Block replaceBlock = null!;

        /// <summary>
        /// Intializes a new <see cref="FillReplaceCommand"/>
        /// </summary>
        /// <param name="corner1">One of the corners of the square to fill</param>
        /// <param name="corner2">The oppesite corner of the square to fill</param>
        /// <param name="block">The block to fill with</param>
        /// <param name="replaceBlock">The block to replace</param>
        public FillReplaceCommand(Vector corner1, Vector corner2, Block block, Block replaceBlock)
        {
            Corner1 = corner1;
            Corner2 = corner2;
            Block = block;
            ReplaceBlock = replaceBlock;
        }

        /// <summary>
        /// One of the corners of the square to fill
        /// </summary>
        public Vector Corner1
        {
            get => corner1;
            set
            {
                corner1 = value ?? throw new ArgumentNullException(nameof(Corner1), "Corner1 may not be null.");
            }
        }

        /// <summary>
        /// The oppesite corner of the square to fill
        /// </summary>
        public Vector Corner2
        {
            get => corner2;
            set
            {
                corner2 = value ?? throw new ArgumentNullException(nameof(Corner2), "Corner2 may not be null.");
            }
        }

        /// <summary>
        /// The block to fill with
        /// </summary>
        public Block Block
        {
            get => block;
            set
            {
                block = value ?? throw new ArgumentNullException(nameof(Block), "Block may not be null.");
            }
        }

        /// <summary>
        /// The block to replace
        /// </summary>
        public Block ReplaceBlock
        {
            get => replaceBlock;
            set
            {
                replaceBlock = value ?? throw new ArgumentNullException(nameof(ReplaceBlock), "ReplaceBlock may not be null.");
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>fill [Corner1] [Corner2] [Block] replace [ReplaceBlock]</returns>
        public override string GetCommandString()
        {
            return $"fill {Corner1.GetVectorString()} {Corner2.GetVectorString()} {Block.GetBlockPlacementString()} replace {ReplaceBlock.GetBlockPlacementString()}";
        }
    }
}
