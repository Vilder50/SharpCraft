using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Triggers once every second
    /// </summary>
    public class LocationTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="LocationTrigger"/>
        /// </summary>
        public LocationTrigger() : base("location") { }

        /// <summary>
        /// The location of the player
        /// </summary>
        [DataTag("conditions", JsonTag = true)]
        public JSONObjects.Location? Location { get; set; }
    }
}
