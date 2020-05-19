using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Reward for advancements
    /// </summary>
    public class Reward : DataHolderBase
    {
        /// <summary>
        /// The recipes to reward
        /// </summary>
        [DataTag("recipes", ForceType = ID.NBTTagType.TagStringArray, JsonTag = true)]
        public IRecipe[]? Recipes { get; set; }

        /// <summary>
        /// The function to run as a reward
        /// </summary>
        [DataTag("function", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
        public Function? Function { get; set; }

        /// <summary>
        /// The loot to reward
        /// </summary>
        [DataTag("loot", ForceType = ID.NBTTagType.TagStringArray, JsonTag = true)]
        public LootTable[]? LootTables { get; set; }

        /// <summary>
        /// The amount of experience to reward
        /// </summary>
        [DataTag("experience", JsonTag = true)]
        public int? Experience { get; set; }
    }
}
