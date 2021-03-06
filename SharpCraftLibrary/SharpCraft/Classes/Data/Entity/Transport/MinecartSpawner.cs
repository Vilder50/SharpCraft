﻿using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for spawner minecarts
    /// </summary>
    public class MinecartSpawner : Minecart
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<MinecartSpawner> PathCreator => new Data.DataPathCreator<MinecartSpawner>();

        /// <summary>
        /// Creates a new spawner minecart
        /// </summary>
        /// <param name="type">the type of entity</param>
        public MinecartSpawner(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public MinecartSpawner() : base(SharpCraft.ID.Entity.spawner_minecart) { }

        /// <summary>
        /// The entities the spawner can spawn
        /// </summary>
        [Data.DataTag]
        public SpawnerPotential[]? Potentials { get; set; }
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
        [Data.DataTag]
        public Time<short>? Delay { get; set; }
        /// <summary>
        /// The amount of ticks to randomly add to the next spawn
        /// </summary>
        [Data.DataTag((object)"MinSpawnDelay", "MaxSpawnDelay", ID.NBTTagType.TagShort, Merge = true)]
        public MCRange? RandomDelay { get; set; }
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
    }
}
