using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player enters a bed
    /// </summary>
    public class UsedBedTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="UsedBedTrigger"/>
        /// </summary>
        public UsedBedTrigger() : base("hero_of_the_village") { }

        /// <summary>
        /// The location the player entered the bed
        /// </summary>
        [DataTag("conditions.location", JsonTag = true)]
        public JsonObjects.Location? Location { get; set; }
    }
}
