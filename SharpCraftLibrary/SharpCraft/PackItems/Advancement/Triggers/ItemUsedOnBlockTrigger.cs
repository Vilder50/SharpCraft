using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player uses an item on a block
    /// </summary>
    public class ItemUsedOnBlockTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="ItemUsedOnBlockTrigger"/>
        /// </summary>
        public ItemUsedOnBlockTrigger() : base("item_used_on_block") { }

        /// <summary>
        /// The item the player used on the block
        /// </summary>
        [DataTag("conditions.item", JsonTag = true)]
        public JsonObjects.Item? Item { get; set; }

        /// <summary>
        /// The location of the block the item was used on
        /// </summary>
        [DataTag("conditions.location", JsonTag = true)]
        public JsonObjects.Location? BlockLocation { get; set; }
    }
}
