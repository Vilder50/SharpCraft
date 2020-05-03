using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player trades with a villager
    /// </summary>
    public class TradedVillagerTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="TradedVillagerTrigger"/>
        /// </summary>
        public TradedVillagerTrigger() : base("villager_trade") { }

        /// <summary>
        /// The item the purchased
        /// </summary>
        [DataTag("conditions.item", JsonTag = true)]
        public JSONObjects.Item Item { get; set; }

        /// <summary>
        /// The villager the player traded with
        /// </summary>
        [DataTag("conditions.villager", JsonTag = true)]
        public Conditions.EntityCondition[] Villager { get; set; }
    }
}
