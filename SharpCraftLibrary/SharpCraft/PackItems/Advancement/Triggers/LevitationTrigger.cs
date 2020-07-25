using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player has the levitation effect
    /// </summary>
    public class LevitationTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="LevitationTrigger"/>
        /// </summary>
        public LevitationTrigger() : base("levitation") { }

        /// <summary>
        /// The distance the player has moved by the effect
        /// </summary>
        [DataTag("conditions.distance", JsonTag = true)]
        public JsonObjects.Distance? Distance { get; set; }

        /// <summary>
        /// The duration of the effect
        /// </summary>
        [DataTag("conditions.duration", "min", "max", ID.NBTTagType.TagInt, true, JsonTag = true)]
        public MCRange? Duration { get; set; }
    }
}
