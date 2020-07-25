using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when a potion is taken out of a brewing stand
    /// </summary>
    public class BrewedPotionTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="BrewedPotionTrigger"/>
        /// </summary>
        public BrewedPotionTrigger() : base("brewed_potion") { }

        /// <summary>
        /// The brewed potion
        /// </summary>
        [DataTag("conditions.potion", JsonTag = true)]
        public ID.Potion? Potion { get; set; }
    }
}
