using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for vine blocks
    /// </summary>
    public class Vine : Block, Interfaces.IConnected
    {
        /// <summary>
        /// Creates a vine block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Vine(BlockType? type) : base(type) { }

        /// <summary>
        /// Creates a vine block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Vine(ID.Block type = SharpCraft.ID.Block.vine) : base(type) { }

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
