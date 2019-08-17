using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for spawner minecarts
        /// </summary>
        public class MinecartSpawner : BaseMinecart
        {
            /// <summary>
            /// Creates a new spawner minecart
            /// </summary>
            /// <param name="type">the type of entity</param>
            public MinecartSpawner(ID.Entity? type = ID.Entity.spawner_minecart) : base(type) { }

            /// <summary>
            /// The entities the spawner can spawn
            /// </summary>
            [Data.DataTag]
            public SpawnerPotential[] Potentials { get; set; }
            /// <summary>
            /// How many entities to try to spawn every time
            /// </summary>
            [Data.DataTag]
            public short? SpawnCount { get; set; }
            /// <summary>
            /// The range to spawn the entities in
            /// </summary>
            [Data.DataTag]
            public short? SpawnRange { get; set; }
            /// <summary>
            /// Time till the next spawn
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagShort)]
            public Time Delay { get; set; }
            /// <summary>
            /// The amount of ticks to randomly add to the next spawn
            /// </summary>
            [Data.CustomDataTag]
            public Range RandomDelay { get; set; }
            /// <summary>
            /// The maximum amount of entities there can be around the spawner for it to spawn
            /// </summary>
            [Data.DataTag("MaxNearbyEntities")]
            public short? MaxEntities { get; set; }
            /// <summary>
            /// The range the player has to be in for the spawner to start spawning
            /// (<see cref="MaxEntities"/> has to be set for this to work)
            /// </summary>
            [Data.DataTag("RequiredPlayerRange")]
            public short? PlayerRange { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = MinecartDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (Potentials != null)
                    {
                        List<string> TempPontentList = new List<string>();
                        for (int i = 0; i < Potentials.Length; i++)
                        {
                            TempPontentList.Add(Potentials[i].ToString());
                        }
                        TempList.Add("SpawnPotentials:[" + string.Join(",", TempPontentList) + "]");
                    }
                    if (SpawnCount != null) { TempList.Add("SpawnCount:" + SpawnCount + "s"); }
                    if (SpawnRange != null) { TempList.Add("SpawnRange:" + SpawnRange + "s"); }
                    if (Delay != null) { TempList.Add("Delay:" + Delay.AsTicks(Time.TimerType.Short) + "s"); }
                    if (MaxEntities != null) { TempList.Add("MaxNearbyEntities:" + MaxEntities + "s"); }
                    if (PlayerRange != null) { TempList.Add("RequiredPlayerRange:" + PlayerRange + "s"); }
                    if (RandomDelay != null) { TempList.Add("MinSpawnDelay:" + RandomDelay.Min + "s,MaxSpawnDelay:" + RandomDelay.Max + "s"); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
