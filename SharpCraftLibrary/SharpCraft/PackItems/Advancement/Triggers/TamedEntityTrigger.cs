﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player tames a mob
    /// </summary>
    public class TamedEntityTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="TamedEntityTrigger"/>
        /// </summary>
        public TamedEntityTrigger() : base("tame_animal") { }

        /// <summary>
        /// The tamed mob
        /// </summary>
        [DataTag("conditions.entity", JsonTag = true)]
        public Conditions.EntityCondition[]? Entity { get; set; }
    }
}
