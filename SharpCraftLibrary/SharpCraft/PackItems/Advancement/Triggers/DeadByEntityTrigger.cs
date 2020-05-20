using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player gets killed by an entity
    /// </summary>
    public class DeadByEntityTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="DeadByEntityTrigger"/>
        /// </summary>
        public DeadByEntityTrigger() : base("entity_killed_player") { }

        /// <summary>
        /// The type of hit the last hit was
        /// </summary>
        [DataTag("conditions.killing_blow", JsonTag = true)]
        public JsonObjects.Damage? KillingBlow { get; set; }

        /// <summary>
        /// The entity who did the last hit
        /// </summary>
        [DataTag("conditions.entity", JsonTag = true)]
        public Conditions.EntityCondition[]? Entity { get; set; }
    }
}
