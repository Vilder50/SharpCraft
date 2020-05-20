using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for note block blocks
    /// </summary>
    public class NoteBlock : Block, Interfaces.IPowered
    {
        /// <summary>
        /// Creates a note block
        /// </summary>
        /// <param name="type">The type of block</param>
        public NoteBlock(BlockType? type) : base(type) { }

        /// <summary>
        /// Creates a note block
        /// </summary>
        /// <param name="type">The type of block</param>
        public NoteBlock(ID.Block type = SharpCraft.ID.Block.note_block) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.note_block;
        }

        /// <summary>
        /// The block's instrument
        /// </summary>
        [BlockState("instrument")]
        public ID.StateNoteInstrument? SInstrument { get; set; }

        /// <summary>
        /// The note to play
        /// </summary>
        [BlockState("note", true)]
        public ID.StateNote? SNote { get; set; }

        /// <summary>
        /// If the block is powered by redstone.
        /// </summary>
        [BlockState("powered")]
        public bool? SPowered { get; set; }
    }
}
