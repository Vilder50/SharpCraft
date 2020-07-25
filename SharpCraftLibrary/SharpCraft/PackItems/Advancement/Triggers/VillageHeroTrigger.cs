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
    public class VillageHeroTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="VillageHeroTrigger"/>
        /// </summary>
        public VillageHeroTrigger() : base("hero_of_the_village") { }
    }
}
