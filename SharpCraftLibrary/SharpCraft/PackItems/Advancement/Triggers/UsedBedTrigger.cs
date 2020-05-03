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
    public class UsedBedTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="UsedBedTrigger"/>
        /// </summary>
        public UsedBedTrigger() : base("hero_of_the_village") { }
    }
}
