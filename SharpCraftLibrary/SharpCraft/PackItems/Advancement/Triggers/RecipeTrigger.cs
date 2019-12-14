using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player unlocks a new recipe
    /// </summary>
    public class RecipeTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="RecipeTrigger"/>
        /// </summary>
        public RecipeTrigger() : base("recipe_unlocked") { }

        /// <summary>
        /// The new recipe
        /// </summary>
        [DataTag("conditions.recipe", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
        public IRecipe Recipe { get; set; }
    }
}
