using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player gets hurt
    /// </summary>
    public class HurtTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="HurtTrigger"/>
        /// </summary>
        public HurtTrigger() : base("entity_hurt_player") { }

        /// <summary>
        /// The type of damage
        /// </summary>
        [DataTag("conditions.damage", JsonTag = true)]
        public JsonObjects.Damage? Damage { get; set; }
    }
}
