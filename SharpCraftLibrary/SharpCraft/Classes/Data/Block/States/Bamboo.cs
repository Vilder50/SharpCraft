using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.Blocks
{
    /// <summary>
    /// An object for bamboo blocks
    /// </summary>
    public class Bamboo : BambooSapling, Interfaces.IAge
    {
        private int? _sAge;

        /// <summary>
        /// Creates a bamboo block
        /// </summary>
        /// <param name="type">The type of block</param>
        public Bamboo(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public Bamboo() : base(SharpCraft.ID.Block.bamboo) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.bamboo;
        }

        /// <summary>
        /// The age of the bamboo.
        /// (0-1)
        /// 1 = Bamboo looks thicker.
        /// </summary>
        [BlockState("age")]
        [BlockIntStateRange(0, 1)]
        public int? SAge
        {
            get => _sAge;
            set
            {
                if (!(value == 0 || value == 1 || value == null))
                {
                    throw new ArgumentException(nameof(SAge) + "Only allows the numbers 0 and 1");
                }
                _sAge = value;
            }
        }

        /// <summary>
        /// The bamboo's leaves' size
        /// </summary>
        [BlockState("leaves")]
        public ID.StateBambooLeave? SLeaves { get; set; }
    }

    /// <summary>
    /// An object for bamboo sapling blocks
    /// </summary>
    public class BambooSapling : Block, Interfaces.IStage
    {
        private int? _sStage;

        /// <summary>
        /// Creates a bamboo sapling block
        /// </summary>
        /// <param name="type">The type of block</param>
        public BambooSapling(IBlockType? type) : base(type) { }

        /// <summary>
        /// Creates a new block
        /// </summary>
        public BambooSapling() : base(SharpCraft.ID.Block.bamboo_sapling) { }

        /// <summary>
        /// Tests if the given block type fits this type of block object
        /// </summary>
        /// <param name="block">The block to test</param>
        /// <returns>true if the block fits</returns>
        public new static bool FitsBlock(ID.Block block)
        {
            return block == SharpCraft.ID.Block.bamboo_sapling;
        }

        /// <summary>
        /// The stage of the bamboo
        /// (0-1)
        /// 1 = Bamboo will grow soon.
        /// </summary>
        [BlockState("stage")]
        public int? SStage
        {
            get => _sStage;
            set
            {
                if (!(value == 0 || value == 1 || value == null))
                {
                    throw new ArgumentException(nameof(SStage) + "Only allows the numbers 0 and 1");
                }
                _sStage = value;
            }
        }
    }
}
