using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Condition which returns true randomly based on enchant level
    /// </summary>
    public class EnchantChanceCondition : BaseCondition
    {
        private double[] chances = null!;

        /// <summary>
        /// Intializes a new <see cref="EnchantChanceCondition"/>
        /// </summary>
        /// <param name="chances">The enchant to check for</param>
        /// <param name="enchant">The chance for each level of the enchantment</param>
        public EnchantChanceCondition(ID.Enchant enchant, double[] chances) : base("minecraft:table_bonus")
        {
            Enchant = enchant;
            Chances = chances;
        }

        /// <summary>
        /// The enchant to check for
        /// </summary>
        [DataTag("enchantment", JsonTag = true)]
        public ID.Enchant Enchant { get; set; }

        /// <summary>
        /// The chance for each level of the enchantment (number from 0 to 1 where 1 = drops 100%)
        /// </summary>
        [DataTag("chances", JsonTag = true)]
        public double[] Chances { get => chances; set => chances = value ?? throw new ArgumentNullException(nameof(Chances), "Chances may not be null"); }
    }
}