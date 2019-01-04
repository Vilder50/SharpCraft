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
        public class Spawner : CloneBlock<Spawner>
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
            /// The entities the spawner can spawn
            /// </summary>
            [BlockData]
            public SpawnerPotential[] DPotentials { get; set; }

            /// <summary>
            /// How many entities to try to spawn every time
            /// </summary>
            [BlockData]
            public int? DSpawnCount { get; set; }

            /// <summary>
            /// The range to spawn the entities in
            /// </summary>
            [BlockData]
            public int? DSpawnRange { get; set; }

            /// <summary>
            /// Time till the next spawn
            /// </summary>
            [BlockData]
            public Time DDelay { get; set; }

            /// <summary>
            /// The amount of ticks to randomly add to the next spawn
            /// </summary>
            [BlockData]
            public Range DRandomDelay { get; set; }

            /// <summary>
            /// The maximum amount of entities there can be around the spawner for it to spawn
            /// </summary>
            [BlockData]
            public int? DMaxEntities { get; set; }

            /// <summary>
            /// The range the player has to be in for the spawner to start spawning
            /// (<see cref="DMaxEntities"/> has to be set for this to work)
            /// </summary>
            [BlockData]
            public int? DPlayerRange { get; set; }

            /// <summary>
            /// Gets the raw data for the data the block contains
            /// </summary>
            /// <returns>Raw data used by Minecraft</returns>
            public override string GetDataString()
            {
                base.GetDataString();

                List<string> TempList = new List<string>();

                if (DPotentials != null)
                {
                    List<string> TempPontentList = new List<string>();
                    for (int i = 0; i < DPotentials.Length; i++)
                    {
                        TempPontentList.Add(DPotentials[i].ToString());
                    }
                    TempList.Add("SpawnPotentials:[" + string.Join(",", TempPontentList) + "]");
                }
                if (DSpawnCount != null) { TempList.Add("SpawnCount:" + DSpawnCount + "s"); }
                if (DSpawnRange != null) { TempList.Add("SpawnRange:" + DSpawnRange + "s"); }
                if (DDelay != null) { TempList.Add("Delay:" + DDelay.AsTicks(Time.TimerType.Short) + "s"); }
                if (DMaxEntities != null) { TempList.Add("MaxNearbyEntities:" + DMaxEntities + "s"); }
                if (DPlayerRange != null) { TempList.Add("RequiredPlayerRange:" + DPlayerRange + "s"); }
                if (DRandomDelay != null) { TempList.Add("MinSpawnDelay:" + DRandomDelay.Min + "s,MaxSpawnDelay:" + DRandomDelay.Max + "s"); }

                return string.Join(",", TempList);
            }
        }
    }
}
