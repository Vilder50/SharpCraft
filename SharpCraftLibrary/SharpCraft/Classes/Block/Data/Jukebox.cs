using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for jukebox blocks
        /// </summary>
        public class Jukebox : Block
        {
            /// <summary>
            /// Creates a new jukebox block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Jukebox(ID.Block? type = SharpCraft.ID.Block.jukebox) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Jukebox(Group group) : base(group) { }

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
            [BlockData("has_record")]
            public bool? SHasRecord{get; set;}

            /// <summary>
            /// The item in the jukebox
            /// </summary>
            [BlockData]
            public Item DRecordItem { get; set; }

            /// <summary>
            /// Gets the raw data for the data the block contains
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string GetDataString()
            {
                base.GetDataString();

                if (DRecordItem != null) { return "RecordItem:{" + DRecordItem.DataString + "}"; }
                return "";
            }
        }
    }
}
