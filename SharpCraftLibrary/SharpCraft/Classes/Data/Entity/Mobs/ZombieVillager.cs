using System.Collections.Generic;
using SharpCraft.Data;
using SharpCraft.Entities.Interfaces;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for zombie villagers
    /// </summary>
    public class ZombieVillager : Zombie, IVillager
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<ZombieVillager> PathCreator => new Data.DataPathCreator<ZombieVillager>();

        /// <summary>
        /// Creates a new zombie villager
        /// </summary>
        /// <param name="type">the type of entity</param>
        public ZombieVillager(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public ZombieVillager() : base(SharpCraft.ID.Entity.zombie_villager) { }

        /// <summary>
        /// The villager's level (~Amount of trades)
        /// </summary>
        [DataTag("VillagerData.level")]
        public int? VillagerLevel { get; set; }

        /// <summary>
        /// The villagers proffesion
        /// </summary>
        [DataTag("VillagerData.profession", ForceType = ID.NBTTagType.TagString)]
        public ID.VillagerProfession? VillagerProfession { get; set; }

        /// <summary>
        /// The type of villager
        /// </summary>
        [DataTag("VillagerData.type", ForceType = ID.NBTTagType.TagString)]
        public ID.VillagerType? VillagerType { get; set; }

        /// <summary>
        /// The gossips the villager has
        /// </summary>
        [DataTag]
        public Gossip[]? Gossips { get; set; }

        /// <summary>
        /// The villager's trades
        /// </summary>
        [DataTag("Offers.Recipes")]
        public Trade[]? Trades { get; set; }

        /// <summary>
        /// The time till this zombie villager turns into a villager. -1 when not being converted
        /// </summary>
        [DataTag]
        public Time<int>? ConversionTime { get; set; }

        /// <summary>
        /// The <see cref="UUID"/> of the player who is converting this zombie villager
        /// </summary>
        [Data.DataTag("ConverterUUID", ForceType = ID.NBTTagType.TagIntArray)]
        public UUID? ConverterUUID { get; set; }
    }
}
