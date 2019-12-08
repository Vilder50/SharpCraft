using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Condition which returns true randomly based on looting level. Checks if a random number from 0-1 is less than the number
    /// </summary>
    public class RandomLootCondition : BaseCondition
    {
        /// <summary>
        /// Intializes a new <see cref="RandomLootCondition"/>
        /// </summary>
        /// <param name="baseChance">The base chance</param>
        /// <param name="lootingChance">Extra chance per looting level</param>
        public RandomLootCondition(double baseChance, double lootingChance) : base("minecraft:random_chance_with_looting")
        {
            BaseChance = baseChance;
            LootingChance = lootingChance;
        }

        /// <summary>
        /// The base chance
        /// </summary>
        [DataTag("chance", JsonTag = true)]
        public double BaseChance { get; set; }

        /// <summary>
        /// Extra chance per looting level
        /// </summary>
        [DataTag("looting_multiplier", JsonTag = true)]
        public double LootingChance { get; set; }
    }
}
