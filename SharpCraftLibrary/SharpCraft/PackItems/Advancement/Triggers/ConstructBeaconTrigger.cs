using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player makes a beacon pyramide
    /// </summary>
    public class ConstructBeaconTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="ConstructBeaconTrigger"/>
        /// </summary>
        public ConstructBeaconTrigger() : base("construct_beacon") { }

        /// <summary>
        /// The amount of pyramide layers
        /// </summary>
        [DataTag("conditions.level", "min", "max", ID.NBTTagType.TagInt, true, JsonTag = true)]
        public MCRange? Layers { get; set; }
    }
}
