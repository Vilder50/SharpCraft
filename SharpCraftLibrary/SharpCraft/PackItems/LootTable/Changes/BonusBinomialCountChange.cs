using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes the loot count using binomual distribution
    /// </summary>
    public class BinomialDistributionCountChange : BaseChange
    {
        /// <summary>
        /// Intializes a new <see cref="BinomialDistributionCountChange"/>
        /// </summary>
        /// <param name="enchant">The enchant to get the level from. The level is added to extra</param>
        /// <param name="maxItems">The amount of items to drop</param>
        /// <param name="probability">The probability for an item to drop</param>
        public BinomialDistributionCountChange(ID.Enchant enchant, int maxItems, double probability) : base("minecraft:apply_bonus")
        {
            Enchant = enchant;
            MaxItems = maxItems;
            Probability = probability;
        }

        /// <summary>
        /// The formula used
        /// </summary>
        [DataTag("formula", JsonTag = true)]
        public ID.LootBonusFormula Formula { get; private set; } = ID.LootBonusFormula.binomial_with_bonus_count;

        /// <summary>
        /// The enchant to get the level from. The level is added to extra
        /// </summary>
        [DataTag("enchant", ForceType = ID.NBTTagType.TagInt, JsonTag = true)]
        public ID.Enchant Enchant { get; set; }

        /// <summary>
        /// The amount of items to drop
        /// </summary>
        [DataTag("extra", JsonTag = true)]
        public int MaxItems { get; set; }

        /// <summary>
        /// The probability for an item to drop
        /// </summary>
        [DataTag("probability", JsonTag = true)]
        public double Probability { get; set; }
    }
}
