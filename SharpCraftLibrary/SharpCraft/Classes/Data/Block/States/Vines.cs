using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for vine blocks
        /// </summary>
        public class Vines : Block, IBlock.IConnected
        {

            /// <summary>
            /// Creates a new vine block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Vines(ID.Block? type = SharpCraft.ID.Block.vine) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Vines(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.vine;
            }

            /// <summary>
            /// If the vine should be displayed in this direction
            /// </summary>
            [BlockState("down")]
            public bool? SDown { get; set; }

            /// <summary>
            /// If the vine should be displayed in this direction
            /// </summary>
            [BlockState("up")]
            public bool? SUp { get; set; }

            /// <summary>
            /// If the vine should be displayed in this direction
            /// </summary>
            [BlockState("east")]
            public bool? SEast { get; set; }

            /// <summary>
            /// If the vine should be displayed in this direction
            /// </summary>
            [BlockState("north")]
            public bool? SNorth { get; set; }

            /// <summary>
            /// If the vine should be displayed in this direction
            /// </summary>
            [BlockState("south")]
            public bool? SSouth { get; set; }

            /// <summary>
            /// If the vine should be displayed in this direction
            /// </summary>
            [BlockState("west")]
            public bool? SWest { get; set; }
        }
    }
}
