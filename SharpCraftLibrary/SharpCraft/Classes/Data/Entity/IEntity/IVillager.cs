using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Entities.Interfaces
{
    /// <summary>
    /// Interface for villager like mobs
    /// </summary>
    public interface IVillager
    {
        /// <summary>
        /// The villager's level (~Amount of trades)
        /// </summary>
        [DataTag("VillagerData.level")]
        int? VillagerLevel { get; set; }

        /// <summary>
        /// The villagers proffesion
        /// </summary>
        [DataTag("VillagerData.profession")]
        ID.VillagerProffession? VillagerProfession { get; set; }

        /// <summary>
        /// The type of villager
        /// </summary>
        [DataTag("VillagerData.type")]
        ID.VillagerType? VillagerType { get; set; }

        /// <summary>
        /// The gossips the villager has
        /// </summary>
        [DataTag]
        Gossip[]? Gossips { get; set; }

        /// <summary>
        /// The villager's trades
        /// </summary>
        [DataTag("Offers.Recipes")]
        Trade[]? Trades { get; set; }
    }

    /// <summary>
    /// Gossip which villagerscan exchange
    /// </summary>
    public class Gossip : DataHolderBase
    {
        /// <summary>
        /// The type of gossip
        /// </summary>
        [DataTag(ForceType = ID.NBTTagType.TagString)]
        public ID.GossipType? Type { get; set; }

        /// <summary>
        /// The gossip's strength 
        /// </summary>
        [DataTag("Value")]
        public int? Strength { get; set; }

        /// <summary>
        /// The UUID of the player who made this gossip.
        /// (If its a <see cref="ID.GossipType.golem"/> then its the villager who started the gossip)
        /// </summary>
        [Data.DataTag("Target",ForceType = ID.NBTTagType.TagIntArray)]
        public UUID? TartgetUUID { get; set; }
    }

    /// <summary>
    /// An object used to define trades
    /// </summary>
    public class Trade : DataHolderBase
    {
        /// <summary>
        /// True if the villager gives xp when traded with
        /// </summary>
        [DataTag("rewardExp")]
        public bool? RewardExp { get; set; }

        /// <summary>
        /// The maximum number of times the trade can be traded before closing
        /// </summary>
        [DataTag("maxUses")]
        public int? MaxUses { get; set; }

        /// <summary>
        /// The amount of times the trade has been used
        /// </summary>
        [DataTag("uses")]
        public int? Uses { get; set; }

        /// <summary>
        /// The first item the villager is buying in this trade
        /// </summary>
        [DataTag("buy")]
        public Item? BuyItem1 { get; set; }

        /// <summary>
        /// The second item the villager is buying in this trade
        /// </summary>
        [DataTag("buyB")]
        public Item? BuyItem2 { get; set; }

        /// <summary>
        /// The item the villager is selling in this trade
        /// </summary>
        [DataTag("sell")]
        public Item? SellItem { get; set; }
    }
}
