using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player damages an item
    /// </summary>
    public class DurabilityChangedTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="DurabilityChangedTrigger"/>
        /// </summary>
        public DurabilityChangedTrigger() : base("item_durability_changed") { }

        /// <summary>
        /// The item which was changed
        /// </summary>
        [DataTag("conditions.item", JsonTag = true)]
        public JsonObjects.Item? Item { get; set; }

        /// <summary>
        /// The amount of durability used
        /// </summary>
        [DataTag("conditions.delta","min","max",ID.NBTTagType.TagInt,true, JsonTag = true)]
        public MCRange? DurabilityUsed { get; set; }

        /// <summary>
        /// The amount of durability left on the item
        /// </summary>
        [DataTag("conditions.durability", "min", "max", ID.NBTTagType.TagInt, true, JsonTag = true)]
        public MCRange? DurabilityLeft { get; set; }
    }
}
