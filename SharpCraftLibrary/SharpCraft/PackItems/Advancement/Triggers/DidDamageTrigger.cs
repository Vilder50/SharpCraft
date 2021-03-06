﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player damages another entity
    /// </summary>
    public class DidDamageTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="DidDamageTrigger"/>
        /// </summary>
        public DidDamageTrigger() : base("player_hurt_entity") { }

        /// <summary>
        /// The type of damage
        /// </summary>
        [DataTag("conditions.damage", JsonTag = true)]
        public JsonObjects.Damage? Damage { get; set; }

        /// <summary>
        /// The entity which got damaged
        /// </summary>
        [DataTag("conditions.entity", JsonTag = true)]
        public Conditions.EntityCondition[]? Entity { get; set; }
    }
}
