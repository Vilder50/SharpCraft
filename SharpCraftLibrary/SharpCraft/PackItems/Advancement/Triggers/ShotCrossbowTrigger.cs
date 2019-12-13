using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player shoots with a crossbow
    /// </summary>
    public class ShotCrossbowTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="ShotCrossbowTrigger"/>
        /// </summary>
        public ShotCrossbowTrigger() : base("shot_crossbow") { }

        /// <summary>
        /// The crossbow
        /// </summary>
        [DataTag("conditions.item", JsonTag = true)]
        public JSONObjects.Item Crossbow { get; set; }
    }
}
