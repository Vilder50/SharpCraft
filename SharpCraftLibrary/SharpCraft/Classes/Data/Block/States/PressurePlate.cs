using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for pressure plate blocks
        /// </summary>
        public class PressurePlate : Block, IBlock.IPowered, IBlock.IPower
        {
            private int? _sPower;

            /// <summary>
            /// Creates a new pressure plate block
            /// </summary>
            /// <param name="type">The type of block</param>
            public PressurePlate(BlockType? type) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                string blockName = block.ToString();
                return (blockName.Contains("pressure"));
            }

            /// <summary>
            /// If the plate is pushed down
            /// </summary>
            [BlockState("powered")]
            public bool? SPowered { get; set; }

            /// <summary>
            /// How much redstone power the plate outputs
            /// (0-15)
            /// Note: only works for metal plates
            /// </summary>
            [BlockState("power")]
            public int? SPower
            {
                get => _sPower;
                set
                {
                    if (value != null && (value < 0 || value > 15))
                    {
                        throw new ArgumentException(nameof(SPower) + " has to be equel to or between 0 and 15");
                    }
                    _sPower = value;
                }
            }
        }
    }
}
