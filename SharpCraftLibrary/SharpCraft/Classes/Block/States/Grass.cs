﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for grass, mycelium and podzol blocks
        /// </summary>
        public class Grass : Block
        {
            /// <summary>
            /// Creates a new grass, mycelium or podzol block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Grass(ID.Block? type = SharpCraft.ID.Block.grass) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Grass(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.grass;
            }

            /// <summary>
            /// If there is snow ontop of the block
            /// </summary>
            [BlockData("snowy")]
            public bool? SSnowy { get; set; }
        }
    }
}
