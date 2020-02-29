using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Triggers when a player travels to the nether and back to the overworld
    /// </summary>
    public class NetherTravelTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="NetherTravelTrigger"/>
        /// </summary>
        public NetherTravelTrigger() : base("nether_travel") { }

        /// <summary>
        /// The distance the player has moved in the overworld using the nether
        /// </summary>
        [DataTag("conditions.distance", JsonTag = true)]
        public JSONObjects.Distance? Distance { get; set; }
    }
}
