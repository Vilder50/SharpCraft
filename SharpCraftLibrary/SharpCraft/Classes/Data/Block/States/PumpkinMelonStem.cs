using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for stem blocks
        /// </summary>
        public class PumpkinMelonStem : Block, IBlock.IAge
        {
            private int? _sAge;

            /// <summary>
            /// Creates a new stem block
            /// </summary>
            /// <param name="type">The type of block</param>
            public PumpkinMelonStem(BlockType? type) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.melon_stem || block == SharpCraft.ID.Block.pumpkin_stem;
            }

            /// <summary>
            /// The age of the stem.
            /// (0-7. 7 == Fully grown)
            /// </summary>
            [BlockState("age")]
            [BlockIntStateRange(0, 7)]
            public int? SAge
            {
                get => _sAge;
                set
                {
                    if (value != null && (value < 0 || value > 7))
                    {
                        throw new ArgumentException(nameof(SAge) + " has to be equel to or between 0 and 7");
                    }
                    _sAge = value;
                }
            }
        }

        /// <summary>
        /// An object for attached stem blocks
        /// </summary>
        public class AttachedPumpkinMelonStem : PumpkinMelonStem, IBlock.IFacing
        {
            /// <summary>
            /// Creates a new stem block
            /// </summary>
            /// <param name="type">The type of block</param>
            public AttachedPumpkinMelonStem(BlockType type) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.attached_melon_stem || block == SharpCraft.ID.Block.attached_pumpkin_stem;
            }

            /// <summary>
            /// The direction of the block the stem is growing into
            /// </summary>
            [BlockState("facing")]
            public ID.Facing? SFacing { get; set; }
        }
    }
}
