using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player safely harvest honey from a nest
    /// </summary>
    public class SafeHoneyHarvestTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="SafeHoneyHarvestTrigger"/>
        /// </summary>
        public SafeHoneyHarvestTrigger() : base("safely_harvest_honey") { }

        /// <summary>
        /// The item the player used for harvisting
        /// </summary>
        [DataTag("conditions.item", JsonTag = true)]
        public JsonObjects.Item? Item { get; set; }

        /// <summary>
        /// The block the honey was harvested from
        /// </summary>
        [DataTag("conditions.block", "block", "tag", "nbt", "state", true, JsonTag = true)]
        public Block? Block { get; set; }
    }
}
