using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player throws an item which gets picked up by another entity
    /// </summary>
    public class ThrownItemPickedUp : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="ThrownItemPickedUp"/>
        /// </summary>
        public ThrownItemPickedUp() : base("thrown_item_picked_up_by_entity") { }

        /// <summary>
        /// The thrown item
        /// </summary>
        [DataTag("conditions.item", JsonTag = true)]
        public Conditions.EntityCondition[]? Item { get; set; }

        /// <summary>
        /// The entity which picked up the item
        /// </summary>
        [DataTag("conditions.entity", JsonTag = true)]
        public Conditions.EntityCondition[]? PickedUpBy { get; set; }
    }
}
