using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player kills another entity
    /// </summary>
    public class KilledEntityTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="KilledEntityTrigger"/>
        /// </summary>
        public KilledEntityTrigger() : base("player_killed_entity") { }

        /// <summary>
        /// The type of hit the last hit was
        /// </summary>
        [DataTag("conditions.killing_blow", JsonTag = true)]
        public JsonObjects.Damage? KillingBlow { get; set; }

        /// <summary>
        /// The entity which died
        /// </summary>
        [DataTag("conditions.entity", JsonTag = true)]
        public Conditions.EntityCondition[]? Entity { get; set; }
    }
}
