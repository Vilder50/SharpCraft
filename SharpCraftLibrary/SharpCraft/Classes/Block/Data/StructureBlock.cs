using System;
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
            /// Creates a new structure block
            /// </summary>
            /// <param name="type">The type of block</param>
            public StructureBlock(ID.Block? type = SharpCraft.ID.Block.structure_block) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public StructureBlock(Group group) : base(group) { }

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
            [BlockData("mode")]
            public ID.StructureMode? SMode { get; set; }
            

            /// <summary>
            /// The name of the structure
            /// </summary>
            [BlockData]
            public string DName { get; set; }
            /// <summary>
            /// The name of the structure's creator
            /// </summary>
            [BlockData]
            public string DAuthor { get; set; }
            /// <summary>
            /// Custom data for the structure
            /// </summary>
            [BlockData]
            public string DMetadata { get; set; }
            /// <summary>
            /// The location relative to the structure block to load/save the structure
            /// </summary>
            [BlockData]
            public Coords DCoords { get; set; }
            /// <summary>
            /// The size of the structure to save
            /// </summary>
            [BlockData]
            public Coords DSize { get; set; }
            /// <summary>
            /// The way the structure is rotated when loaded
            /// </summary>
            [BlockData]
            public ID.StructureRotation? DRotation { get; set; }
            /// <summary>
            /// The way the structure is mirrored when loaded
            /// </summary>
            [BlockData]
            public ID.StructureMirror? DMirror { get; set; }
            /// <summary>
            /// The mode the structure block is in
            /// </summary>
            [BlockData]
            public ID.StructureMode? DMode { get; set; }
            /// <summary>
            /// If the structure block should ignore entities
            /// </summary>
            [BlockData]
            public bool? DIgnoreEntities { get; set; }
            /// <summary>
            /// The percent amount of random air blocks in the structure.
            /// (0 = none. 1 = all)
            /// </summary>
            [BlockData]
            public double? DIntegrity { get; set; }
            /// <summary>
            /// The seed to use when placing the random air blocks with <see cref="DIntegrity"/>
            /// </summary>
            [BlockData]
            public long? DSeed { get; set; }

            /// <summary>
            /// Gets the raw data for the data the block contains
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string GetDataString()
            {
                base.GetDataString();

                List<string> TempList = new List<string>();


                if (DName != null) { TempList.Add("name:\"" + DName.Escape() + "\""); }
                if (DAuthor != null) { TempList.Add("author:\"" + DAuthor.Escape() + "\""); }
                if (DMetadata != null) { TempList.Add("metadata:\"" + DMetadata.Escape() + "\""); }
                if (DRotation != null) { TempList.Add("rotation:\"" + DRotation + "\""); }
                if (DMirror != null) { TempList.Add("mirror:\"" + DMirror + "\""); }
                if (DMode != null) { TempList.Add("mode:\"" + DMode.ToString().ToUpper() + "\""); }
                if (DIgnoreEntities != null) { TempList.Add("ignoreEntities:" + DIgnoreEntities); }
                if (DCoords != null) { TempList.Add("posX:" + (int)DCoords.X + ",posY:" + (int)DCoords.Y + ",posZ:" + (int)DCoords.Z); }
                if (DSize != null) { TempList.Add("sizeX:" + (int)DSize.X + ",sizeY:" + (int)DSize.Y + ",sizeZ:" + (int)DSize.Z); }
                if (DSeed != null) { TempList.Add("seed:" + DSeed + "L"); }
                if (DIntegrity != null) { TempList.Add("integrity:" + DIntegrity); }

                return string.Join(",", TempList);
            }
        }
    }
}
