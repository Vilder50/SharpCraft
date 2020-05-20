using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player throws an eye of ender
    /// </summary>
    public class UsedEnderEyeTriggerTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="UsedEnderEyeTriggerTrigger"/>
        /// </summary>
        public UsedEnderEyeTriggerTrigger() : base("used_ender_eye") { }

        /// <summary>
        /// The distance to the closest stronghold
        /// </summary>
        [DataTag("conditions.distance", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
        public MCRange? Distance { get; set; }
    }
}
