﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Condition which returns true if the damage is of a given type
    /// </summary>
    public class EntityCondition : BaseCondition
    {
        private JsonObjects.Entity data = null!;

        /// <summary>
        /// Intializes a new <see cref="EntityCondition"/>
        /// </summary>
        /// <param name="data">The entity data to check for</param>
        /// <param name="target">The entity to check for data on</param>
        public EntityCondition(ID.LootTarget target, JsonObjects.Entity data) : base("minecraft:entity_properties")
        {
            Data = data;
            Target = target;
        }

        /// <summary>
        /// The entity data to check for
        /// </summary>
        [DataTag("predicate", JsonTag = true)]
        public JsonObjects.Entity Data { get => data; set => data = value ?? throw new ArgumentNullException(nameof(Data), "Data may not be null"); }

        /// <summary>
        /// The entity to check for data on
        /// </summary>
        [DataTag("entity", JsonTag = true, ForceType = ID.NBTTagType.TagString)]
        public ID.LootTarget Target { get; set; }
    }
}
