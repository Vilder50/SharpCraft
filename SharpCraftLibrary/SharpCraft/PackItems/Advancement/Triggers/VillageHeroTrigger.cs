using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player defeats a raid
    /// </summary>
    public class VillageHeroTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="VillageHeroTrigger"/>
        /// </summary>
        public VillageHeroTrigger() : base("hero_of_the_village") { }

        /// <summary>
        /// The location the player defeated the raid at
        /// </summary>
        [DataTag("conditions", JsonTag = true)]
        public JsonObjects.Location? Location { get; set; }
    }
}
