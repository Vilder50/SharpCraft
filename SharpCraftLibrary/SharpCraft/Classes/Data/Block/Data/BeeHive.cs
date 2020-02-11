using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for bee hive/nest blocks
        /// </summary>
        public class BeeHive : Block, IBlock.IFacing
        {
            private int? _sLevel;

            /// <summary>
            /// Creates a new bee hive/nest block
            /// </summary>
            /// <param name="type">The type of block</param>
            public BeeHive(BlockType type) : base(type) { }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.beehive || block == SharpCraft.ID.Block.bee_nest;
            }

            /// <summary>
            /// The direction the bee hive isfacing
            /// </summary>
            [BlockState("facing")]
            public ID.Facing? SFacing { get; set; }

            /// <summary>
            /// How much honey there is in the hive.
            /// (0-5. 0 = empty. 5 = full and harvest able)
            /// </summary>
            [BlockState("honey_level")]
            [BlockIntStateRange(0, 5)]
            public int? SLevel
            {
                get => _sLevel;
                set
                {
                    if (value != null && (value < 0 || value > 3))
                    {
                        throw new ArgumentException(nameof(SLevel) + " has to be equel to or between 0 and 3");
                    }
                    _sLevel = value;
                }
            }

            /// <summary>
            /// The position of a flower the bees should path find to
            /// </summary>
            [DataTag("FlowerPos", "X", "Y", "Z")]
            public IntVector DFlowerPosition { get; set; }

            /// <summary>
            /// The entities inside the bee hive
            /// </summary>
            [DataTag("Bees")]
            public InsideBeeHive[] DEntitiesInside { get; set; }

            /// <summary>
            /// An entity inside a bee hive
            /// </summary>
            public class InsideBeeHive : DataHolderBase
            {
                /// <summary>
                /// The entity inside the bee hive
                /// </summary>
                [DataTag("EntityData")]
                public Entity.BaseEntity Entity { get; set; }

                /// <summary>
                /// The amount of ticks the entity has been in the hive.
                /// </summary>
                [DataTag(ForceType = SharpCraft.ID.NBTTagType.TagInt)]
                public Time TicksInHive { get; set; }

                /// <summary>
                /// The minimum amount of ticks the entity has been in the hive.
                /// </summary>
                [DataTag("MinOccupationTicks", ForceType = SharpCraft.ID.NBTTagType.TagInt)]
                public Time MinimumTicksInHive { get; set; }
            }
        }
    }
}
