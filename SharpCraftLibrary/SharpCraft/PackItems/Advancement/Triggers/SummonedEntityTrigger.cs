﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player summons a build-able entity
    /// </summary>
    public class SummonedEntityTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="SummonedEntityTrigger"/>
        /// </summary>
        public SummonedEntityTrigger() : base("summoned_entity") { }

        /// <summary>
        /// The summoned entity
        /// </summary>
        [DataTag("conditions.entity", JsonTag = true)]
        public JSONObjects.Entity Entity { get; set; }
    }
}
