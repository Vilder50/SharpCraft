using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for tripwire hook blocks
        /// </summary>
        public class TripwireHook : Block, IBlock.IFacing, IBlock.IPowered
        {
            /// <summary>
            /// Creates a tripwire hook block
            /// </summary>
            /// <param name="type">The type of block</param>
            public TripwireHook(BlockType type) : base(type) { }

            /// <summary>
            /// Creates a tripwire hook block
            /// </summary>
            /// <param name="type">The type of block</param>
            public TripwireHook(ID.Block type = SharpCraft.ID.Block.tripwire_hook) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.tripwire_hook;
            }

            /// <summary>
            /// If the tripwire hook is attached to a tripwire
            /// </summary>
            [BlockState("attached")]
            public bool? SAttached { get; set; }

            /// <summary>
            /// The direction the tripwire hook is facing into
            /// </summary>
            [BlockState("facing")]
            public ID.Facing? SFacing { get; set; }

            /// <summary>
            /// If the tripwire hook's tripwire is being stepped on
            /// </summary>
            [BlockState("powered")]
            public bool? SPowered { get; set; }
        }
    }
}
