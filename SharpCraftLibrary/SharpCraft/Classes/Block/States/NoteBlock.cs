using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for note block blocks
        /// </summary>
        public class NoteBlock : Block, IBlock.IPowered
        {
            /// <summary>
            /// Creates a new note block block
            /// </summary>
            /// <param name="type">The type of block</param>
            public NoteBlock(ID.Block? type = SharpCraft.ID.Block.note_block) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public NoteBlock(Group group) : base(group) { }

            /// <summary>
            /// The block's instrument
            /// </summary>
            [BlockData("instrument")]
            public ID.StateNoteInstrument? SInstrument { get; set; }

            /// <summary>
            /// The note to play
            /// </summary>
            [BlockData("note", true)]
            public ID.StateNote? SNote { get; set; }

            /// <summary>
            /// If the block is powered by redstone.
            /// </summary>
            [BlockData("powered")]
            public bool? SPowered { get; set; }
        }
    }
}
