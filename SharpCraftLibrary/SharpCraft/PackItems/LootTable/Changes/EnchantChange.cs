using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes the enchants on the item to some of the given enchants
    /// </summary>
    public class EnchantChange : BaseChange
    {
        /// <summary>
        /// Intializes a new <see cref="EnchantChange"/>
        /// </summary>
        /// <param name="possibleEnchants">Enchants which the item randomly can get</param>
        public EnchantChange(ID.Enchant[] possibleEnchants) : base("minecraft:enchant_randomly")
        {
            PossibleEnchants = possibleEnchants;
        }

        /// <summary>
        /// Enchants which the item randomly can get
        /// </summary>
        [DataTag("enchantments", JsonTag = true)]
        public ID.Enchant[] PossibleEnchants { get; set; }
    }
}
