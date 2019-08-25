using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    public partial class Block
    {
        /// <summary>
        /// An object for end spawner blocks
        /// </summary>
        public class Spawner : Block
        {
            /// <summary>
            /// Creates a new end spawner block
            /// </summary>
            /// <param name="type">The type of block</param>
            public Spawner(ID.Block? type = SharpCraft.ID.Block.spawner) : base(type) { }

            /// <summary>
            /// Converts a group of blocks into a block object
            /// </summary>
            /// <param name="group"></param>
            public Spawner(Group group) : base(group) { }

            /// <summary>
            /// Creates a spawner with the specified entity inside. The entity wont move and the spawner wont spawn the entity
            /// </summary>
            /// <param name="display">The entity to display</param>
            /// <param name="type">The type of block</param>
            public Spawner(SharpCraft.Entity.BaseEntity display, ID.Block? type = SharpCraft.ID.Block.spawner) : base(type)
            {
                DPotentials = new SpawnerPotential[]
                {
                    new SpawnerPotential(display,1)
                };
                DPlayerRange = 0;
                DMaxEntities = 0;
            }

            /// <summary>
            /// Tests if the given block type fits this type of block object
            /// </summary>
            /// <param name="block">The block to test</param>
            /// <returns>true if the block fits</returns>
            public new static bool FitsBlock(ID.Block block)
            {
                return block == SharpCraft.ID.Block.spawner;
            }

            /// <summary>
            /// The entities the spawner can spawn
            /// </summary>
            [Data.DataTag("SpawnerPotentials")]
            public SpawnerPotential[] DPotentials { get; set; }

            /// <summary>
            /// How many entities to try to spawn every time
            /// </summary>
            [Data.DataTag("SpawnCount")]
            public short? DSpawnCount { get; set; }

            /// <summary>
            /// The range to spawn the entities in
            /// </summary>
            [Data.DataTag("SpawnRange")]
            public short? DSpawnRange { get; set; }

            /// <summary>
            /// Time till the next spawn
            /// </summary>
            [Data.DataTag("Delay", ForceType = SharpCraft.ID.NBTTagType.TagShort)]
            public Time DDelay { get; set; }

            /// <summary>
            /// The amount of ticks to randomly add to the next spawn
            /// </summary>
            [Data.DataTag((object)"MinSpawnDelay", "MaxSpawnDelay", SharpCraft.ID.NBTTagType.TagShort, Merge = true)]
            public Range DRandomDelay { get; set; }

            /// <summary>
            /// The maximum amount of entities there can be around the spawner for it to spawn
            /// </summary>
            [Data.DataTag("MaxNearbyEntities")]
            public short? DMaxEntities { get; set; }

            /// <summary>
            /// The range the player has to be in for the spawner to start spawning
            /// (<see cref="DMaxEntities"/> has to be set for this to work)
            /// </summary>
            [Data.DataTag("RequiredPlayerRange")]
            public short? DPlayerRange { get; set; }
        }
    }
}
