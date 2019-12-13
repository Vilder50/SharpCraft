using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player catches something using a fishing rod
    /// </summary>
    public class CatchTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="CatchTrigger"/>
        /// </summary>
        public CatchTrigger() : base("fishing_rod_hooked") { }

        /// <summary>
        /// The entity caught with the rod
        /// </summary>
        [DataTag("conditions.entity", JsonTag = true)]
        public JSONObjects.Entity Entity { get; set; }

        /// <summary>
        /// The item caught with the rod
        /// </summary>
        [DataTag("conditions.item", JsonTag = true)]
        public JSONObjects.Item Item { get; set; }

        /// <summary>
        /// The fishing rod used
        /// </summary>
        [DataTag("conditions.rod", JsonTag = true)]
        public JSONObjects.Item FishingRod { get; set; }
    }
}
