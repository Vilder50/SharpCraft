using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for turtle egg blocks
    /// </summary>
    public class TurtleEgg : Block
    {
        private int? _sEggs;
        private int? _sHatching;

        /// <summary>
        /// Creates a turtle egg block
        /// </summary>
        /// <param name="type">The type of block</param>
        public TurtleEgg(BlockType? type) : base(type) { }

        /// <summary>
        /// Creates a turtle egg block
        /// </summary>
        /// <param name="type">The type of block</param>
        public TurtleEgg(ID.Block type = SharpCraft.ID.Block.turtle_egg) : base(type) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.turtle_egg;
        }

        /// <summary>
        /// The amount of eggs in the block.
        /// (1-4)
        /// </summary>
        [BlockState("eggs")]
        [BlockIntStateRange(1, 4)]
        public int? SEggs
        {
            get => _sEggs;
            set
            {
                if (value != null && (value < 1 || value > 4))
                {
                    throw new ArgumentException(nameof(SEggs) + " has to be equel to or between 1 and 4");
                }
                _sEggs = value;
            }
        }

        /// <summary>
        /// How far the eggs has hatched.
        /// (0-2. 2 == Will hatch soon)
        /// </summary>
        [BlockState("hatch")]
        public int? SHatching
        {
            get => _sHatching;
            set
            {
                if (value != null && (value < 0 || value > 2))
                {
                    throw new ArgumentException(nameof(SHatching) + " has to be equel to or between 0 and 2");
                }
                _sHatching = value;
            }
        }
    }
}
