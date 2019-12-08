﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes the amount of items there can be in each stack
    /// </summary>
    public class LimitCountChange : BaseChange
    {
        private Range limit;

        /// <summary>
        /// Intializes a new <see cref="LimitCountChange"/>
        /// </summary>
        /// <param name="limit">The amount of items in each stack</param>
        public LimitCountChange(Range limit) : base("limit_count")
        {
            Limit = limit;
        }

        /// <summary>
        /// The amount of items in each stack
        /// </summary>
        [DataTag("limit", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
        public Range Limit { get => limit; set => limit = value ?? throw new ArgumentNullException(nameof(Limit), "Limit may not be null"); }
    }
}
