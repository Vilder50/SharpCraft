using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes the loot count using the normal fortune ore drop method... whatever that is
    /// </summary>
    public class OreCountChange : BaseChange
    {
        /// <summary>
        /// Intializes a new <see cref="OreCountChange"/>
        /// </summary>
        /// <param name="enchant">The enchant to get the level from (An enchant on the item which activated this loot table)</param>
        public OreCountChange(ID.Enchant enchant) : base("apply_bonus")
        {
            Enchant = enchant;
        }

        /// <summary>
        /// The formula used
        /// </summary>
        [DataTag("formula", ForceType = ID.NBTTagType.TagNamespacedString, JsonTag = true)]
        public ID.LootBonusFormula Formula { get; private set; } = ID.LootBonusFormula.ore_drops;

        /// <summary>
        /// The enchant to get the level from (An enchant on the item which activated this loot table)
        /// </summary>
        [DataTag("enchant", ForceType = ID.NBTTagType.TagInt, JsonTag = true)]
        public ID.Enchant Enchant { get; set; }
    }
}
