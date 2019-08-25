﻿using System.Collections.Generic;
using SharpCraft.Data;
using SharpCraft.IEntity;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for zombie villagers
        /// </summary>
        public class ZombieVillager : Zombie, IVillager
        {
            /// <summary>
            /// Creates a new zombie villager
            /// </summary>
            /// <param name="type">the type of entity</param>
            public ZombieVillager(ID.Entity? type = ID.Entity.zombie_villager) : base(type) { }

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
            public Gossip[] Gossips { get; set; }

            /// <summary>
            /// The villager's trades
            /// </summary>
            [DataTag("Offers.Recipes")]
            public Trade[] Trades { get; set; }

            /// <summary>
            /// The time till this zombie villager turns into a villager. -1 when not being converted
            /// </summary>
            [DataTag(ForceType = ID.NBTTagType.TagInt)]
            public Time ConversionTime { get; set; }

            /// <summary>
            /// The <see cref="UUID"/> of the player who is converting this zombie villager
            /// </summary>
            [DataTag((object)"ConversionPlayerMost", "ConversionPlayerLeast", Merge = true)]
            public UUID ConverterUUID { get; set; }
        }
    }
}