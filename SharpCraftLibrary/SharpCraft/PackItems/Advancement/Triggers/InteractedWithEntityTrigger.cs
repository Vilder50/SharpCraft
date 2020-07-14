using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when 2 animals are bred together
    /// </summary>
    public class InteractedWithEntityTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="InteractedWithEntityTrigger"/>
        /// </summary>
        public InteractedWithEntityTrigger() : base("player_interacted_with_entity") { }

        /// <summary>
        /// The item which the player used for interacting with the entity
        /// </summary>
        [DataTag("conditions.item", JsonTag = true)]
        public JsonObjects.Item? Item { get; set; }

        /// <summary>
        /// The entity the player interacted with
        /// </summary>
        [DataTag("conditions.entity", JsonTag = true)]
        public Conditions.EntityCondition[]? Entity { get; set; }
    }
}
