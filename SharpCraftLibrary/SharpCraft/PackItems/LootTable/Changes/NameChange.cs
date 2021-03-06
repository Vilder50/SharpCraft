﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes the loot name
    /// </summary>
    public class NameChange : BaseChange
    {
        private BaseJsonText name = null!;

        /// <summary>
        /// Intializes a new <see cref="NameChange"/>
        /// </summary>
        public NameChange(BaseJsonText name, ID.LootTarget? target = null) : base("minecraft:set_name")
        {
            Name = name;
            Target = target;
        }

        /// <summary>
        /// The entity @s referes to in the name
        /// </summary>
        [DataTag("entity", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
        public ID.LootTarget? Target { get; set; }

        /// <summary>
        /// The name
        /// </summary>
        [DataTag("name", ForceType = ID.NBTTagType.TagCompound, JsonTag = true)]
        public BaseJsonText Name { get => name; set => name = value ?? throw new ArgumentNullException(nameof(Name), "Name may not be null"); }
    }
}
