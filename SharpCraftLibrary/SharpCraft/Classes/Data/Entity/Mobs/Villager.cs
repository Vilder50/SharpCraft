using System.Collections.Generic;
using SharpCraft.Data;
using SharpCraft.Entities.Interfaces;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for villagers
    /// </summary>
    public class Villager : BreedableMob, IVillager
    {
        Item[]? inventory;

        /// <summary>
        /// Creates a new villager
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Villager(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Villager() : base(SharpCraft.ID.Entity.villager) { }

        /// <summary>
        /// The villager's level (~Amount of trades)
        /// </summary>
        [DataTag("VillagerData.level")]
        public int? VillagerLevel { get; set; }

        /// <summary>
        /// The villagers proffesion
        /// </summary>
        [DataTag("VillagerData.profession", ForceType = ID.NBTTagType.TagString)]
        public ID.VillagerProffession? VillagerProfession { get; set; }

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
        /// If the villager is willing to mate.
        /// </summary>
        [DataTag]
        public bool? Willing { get; set; }

        /// <summary>
        /// Time since the villager last restocked
        /// </summary>
        [DataTag]
        public Time<long>? LastRestock { get; set; }

        /// <summary>
        /// Time since the villager's gossips escreased in strength
        /// </summary>
        [DataTag]
        public Time<long>? LastGossipDecay { get; set; }

        /// <summary>
        /// The amount of times the villager has restocked since the <see cref="LastRestock"/> time.
        /// </summary>
        [DataTag]
        public int? RestocksToday { get; set; }

        /// <summary>
        /// The amount of XP the villager has
        /// </summary>
        [DataTag]
        public int? Xp { get; set; }

        /// <summary>
        /// The items in the villagers inventory (up to 8 slots)
        /// </summary>
        [DataTag]
        public Item[]? Inventory
        {
            get
            {
                return inventory;
            }

            set
            {
                if (!(value is null) && value.Length > 8)
                {
                    throw new System.ArgumentException("Too many items specified for the villager inventory", nameof(Inventory));
                }

                inventory = value;
            }
        }
    }
}
