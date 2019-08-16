using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for comparator blocks
        /// </summary>
        public class Comparator : Block, IBlock.IPowered, IBlock.IFacing
        {
            /// <summary>
            /// Creates a new comparator block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Comparator(ID.Block? type = SharpCraft.ID.Block.comparator) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Comparator(Group group) : base(group) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.comparator;
            }

            /// <summary>
            /// The direction the comparator is facing
            /// </summary>
            [BlockData("facing")]
            public ID.Facing? SFacing { get; set; }
            /// <summary>
            /// The comparator's compara mode
            /// </summary>
            [BlockData("mode")]
            public ID.StateCompareMode? SCompareMode { get; set; }
            /// <summary>
            /// If the comparator is powered
            /// </summary>
            [BlockData("powered")]
            public bool? SPowered { get; set; }

            /// <summary>
            /// The strenght of the comparators output
            /// </summary>
            [BlockData]
            public int? DOutputSignal { get; set; }

            /// <summary>
            /// Gets the raw data for the data the block contains
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string GetDataString()
            {
                base.GetDataString();

                if (DOutputSignal != null)
                {
                    return "OutputSignal:" + DOutputSignal;
                }

                return "";
            }
        }
    }
}
