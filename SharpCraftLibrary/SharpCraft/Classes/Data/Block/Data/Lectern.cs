using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for lectern blocks
        /// </summary>
        public class Lectern : Block, IBlock.IFacing, IBlock.IPowered
        {
            /// <summary>
            /// Creates a new lectern block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Lectern(ID.Block? type = SharpCraft.ID.Block.lectern) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Lectern(Group group) : base(group) { }

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
            public ID.Facing? SFacing{get; set;}

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
            public Item DBook { get; set; }

            /// <summary>
            /// The page the book in the lantern is on
            /// </summary>
            [Data.DataTag("Page")]
            public int DPage { get; set; }

            /// <summary>
            /// Gets the raw data for the data the block contains
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string GetDataString()
            {
                base.GetDataString();

                if (DBook != null) { return "RecordItem:{" + DBook.DataString + "}"; }
                return "";
            }
        }
    }
}
