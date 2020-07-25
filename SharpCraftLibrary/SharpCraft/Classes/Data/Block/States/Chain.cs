using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
     /// <summary>
     /// An object for chain blocks
     /// </summary>
     public class Chain : Block, Interfaces.IWaterLogged
     {
         /// <summary>
         /// Creates a chain block
         /// </summary>
         /// <param name="type">The type of block</param>
         public Chain(IBlockType type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Chain() : base(SharpCraft.ID.Block.chain) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
         {
             return block == SharpCraft.ID.Block.chain;
         }

         /// <summary>
         /// If the chain is water logged
         /// </summary>
         [BlockState("waterlogged")]
         public bool? SWaterLogged { get; set; }
     }
}
