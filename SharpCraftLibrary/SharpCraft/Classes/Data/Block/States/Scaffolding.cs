using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for scaffolding blocks
    /// </summary>
    public class Scaffolding : Block, Interfaces.IDistance, Interfaces.IWaterLogged
    {
        private int? _sDistance;

        /// <summary>
        /// Creates a scaffolding block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Scaffolding(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Scaffolding() : base(SharpCraft.ID.Block.scaffolding) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.scaffolding;
        }

        /// <summary>
        /// How far out the scaffolding has gone
        /// (0-7. 7 == will start to fall)
        /// </summary>
        [BlockState("distance")]
        [BlockIntStateRange(0, 7)]
        public int? SDistance
        {
            get => _sDistance;
            set
            {
                if (value != null && (value < 0 || value > 7))
                {
                    throw new ArgumentException(nameof(SDistance) + " has to be equel to or between 0 and 6");
                }
                _sDistance = value;
            }
        }

        /// <summary>
        /// If the scaffoling is water logged
        /// </summary>
        [BlockState("waterlogged")]
        public bool? SWaterLogged { get; set; }

        /// <summary>
        /// If there isn't a block under the scaffolding
        /// </summary>
        [BlockState("bottom")]
        public bool? SNoBlockUnder { get; set; }
    }
}
