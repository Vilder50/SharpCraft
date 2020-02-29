using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for observer blocks
        /// </summary>
        public class Observer : Block, IBlock.IFacingFull, IBlock.IPowered
        {
            /// <summary>
            /// Creates a observer block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Observer(BlockType? type) : base(type) { }

            /// <summary>
            /// Creates a observer block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Observer(ID.Block type = SharpCraft.ID.Block.observer) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.observer;
            }

            /// <summary>
            /// The direction the observer is observing.
            /// </summary>
            [BlockState("facing")]
            public ID.FacingFull? SFacing { get; set; }

            /// <summary>
            /// If the observer has observed a change
            /// </summary>
            [BlockState("powered")]
            public bool? SPowered { get; set; }
        }
    }
}
