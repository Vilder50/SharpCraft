﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for structure blocks
        /// </summary>
        public class StructureBlock : Block
        {
            /// <summary>
            /// Creates a structure block
            /// </summary>
            /// <param name="type">The type of block</param>
            public StructureBlock(BlockType type) : base(type) { }

            /// <summary>
            /// Creates a structure block
            /// </summary>
            /// <param name="type">The type of block</param>
            public StructureBlock(ID.Block type = SharpCraft.ID.Block.barrel) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.structure_block;
            }

            /// <summary>
            /// The way the block should display
            /// </summary>
            [BlockState("mode")]
            public ID.StructureMode? SMode { get; set; }


            /// <summary>
            /// The name of the structure
            /// </summary>
            [Data.DataTag("name")]
            public string DName { get; set; }
            /// <summary>
            /// The name of the structure's creator
            /// </summary>

            [Data.DataTag("metadata")]
            public string DMetadata { get; set; }

            /// <summary>
            /// The location relative to the structure block to load/save the structure
            /// </summary>
            [Data.DataTag((object)"posX","posY","posZ",SharpCraft.ID.NBTTagType.TagInt, Merge = true)]
            public Coords DCoords { get; set; }

            /// <summary>
            /// The size of the structure to save
            /// </summary>
            [Data.DataTag((object)"sizeX", "sizeY", "sizeZ", SharpCraft.ID.NBTTagType.TagInt, Merge = true)]
            public Coords DSize { get; set; }

            /// <summary>
            /// The way the structure is rotated when loaded
            /// </summary>
            [Data.DataTag("rotation", ForceType = SharpCraft.ID.NBTTagType.TagString)]
            public ID.StructureRotation? DRotation { get; set; }

            /// <summary>
            /// The way the structure is mirrored when loaded
            /// </summary>
            [Data.DataTag("mirror", ForceType = SharpCraft.ID.NBTTagType.TagString)]
            public ID.StructureMirror? DMirror { get; set; }

            /// <summary>
            /// The mode the structure block is in
            /// </summary>
            [Data.DataTag("mode", ForceType = SharpCraft.ID.NBTTagType.TagString)]
            public ID.StructureMode? DMode { get; set; }

            /// <summary>
            /// If the structure block should ignore entities
            /// </summary>
            [Data.DataTag("ignoreEntities")]
            public bool? DIgnoreEntities { get; set; }

            /// <summary>
            /// The percent amount of random air blocks in the structure.
            /// (0 = none. 1 = all)
            /// </summary>
            [Data.DataTag("integrity")]
            public double? DIntegrity { get; set; }

            /// <summary>
            /// The seed to use when placing the random air blocks with <see cref="DIntegrity"/>
            /// </summary>
            [Data.DataTag("seed")]
            public long? DSeed { get; set; }
        }
    }
}
