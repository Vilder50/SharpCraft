using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player consumes an item
    /// </summary>
    public class ConsumeItemTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="ConsumeItemTrigger"/>
        /// </summary>
        public ConsumeItemTrigger() : base("consume_item") { }

        /// <summary>
        /// The item the player consumed
        /// </summary>
        [DataTag("conditions.item", JsonTag = true)]
        public JSONObjects.Item Item { get; set; }
    }
}
