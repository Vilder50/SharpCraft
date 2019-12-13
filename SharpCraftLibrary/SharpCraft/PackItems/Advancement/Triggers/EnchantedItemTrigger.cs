using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player enchants an item
    /// </summary>
    public class EnchantedItemTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="EnchantedItemTrigger"/>
        /// </summary>
        public EnchantedItemTrigger() : base("enchanted_item") { }

        /// <summary>
        /// The item which got enchanted
        /// </summary>
        [DataTag("conditions.item", JsonTag = true)]
        public JSONObjects.Item Item { get; set; }

        /// <summary>
        /// The amount of levels used for enchanting
        /// </summary>
        [DataTag("conditions.levels", "min", "max", ID.NBTTagType.TagInt, true, JsonTag = true)]
        public Range Levels { get; set; }
    }
}
