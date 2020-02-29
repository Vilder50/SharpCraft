using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Condition which returns true if the item has the given enchants
    /// </summary>
    public class EnchantmentCondition : BaseCondition
    {
        private JSONObjects.Item.Enchantment[] enchantments = null!;

        /// <summary>
        /// Intializes a new <see cref="EnchantmentCondition"/>
        /// </summary>
        public EnchantmentCondition(JSONObjects.Item.Enchantment[] enchantments) : base("minecraft:tool_enchantment")
        {
            Enchantments = enchantments;
        }

        /// <summary>
        /// the enchants to test for
        /// </summary>
        [DataTag("enchantments", JsonTag = true)]
        public JSONObjects.Item.Enchantment[] Enchantments { get => enchantments; set => enchantments = value ?? throw new ArgumentNullException(nameof(Enchantments), "Enchantments may not be null"); }
    }
}