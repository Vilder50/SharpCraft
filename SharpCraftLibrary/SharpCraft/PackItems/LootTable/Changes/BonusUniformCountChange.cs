using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes the loot count using uniform distribution. Drops 0 to Enchant.Level * Multiplier
    /// </summary>
    public class UniformDistributionCountChange : BaseChange
    {
        /// <summary>
        /// Intializes a new <see cref="UniformDistributionCountChange"/>
        /// </summary>
        /// <param name="enchant">The enchant to get the level from.</param>
        /// <param name="multiplier">The number to multiple the enchant level with.</param>
        public UniformDistributionCountChange(ID.Enchant enchant, double multiplier) : base("apply_bonus")
        {
            Enchant = enchant;
            Multiplier = multiplier;
        }

        /// <summary>
        /// The formula used
        /// </summary>
        [DataTag("formula", ForceType = ID.NBTTagType.TagNamespacedString, JsonTag = true)]
        public ID.LootBonusFormula Formula { get; private set; } = ID.LootBonusFormula.uniform_bonus_count;

        /// <summary>
        /// The enchant to get the level from (An enchant on the item which activated this loot table)
        /// </summary>
        [DataTag("enchant", ForceType = ID.NBTTagType.TagInt, JsonTag = true)]
        public ID.Enchant Enchant { get; set; }

        /// <summary>
        /// The number to multiple the enchant level with.
        /// </summary>
        [DataTag("bonusMultiplier", JsonTag = true)]
        public double Multiplier { get; set; }
    }
}
