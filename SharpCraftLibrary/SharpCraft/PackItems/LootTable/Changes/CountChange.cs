using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes the amount of items
    /// </summary>
    public class CountChange : BaseChange
    {
        private MCRange count = null!;

        /// <summary>
        /// Intializes a new <see cref="CountChange"/>
        /// </summary>
        /// <param name="count">The amount of items</param>
        public CountChange(MCRange count) : base("minecraft:set_count")
        {
            Count = count;
        }

        /// <summary>
        /// The amount of items in each stack
        /// </summary>
        [DataTag("count", "min", "max", ID.NBTTagType.TagInt, true, JsonTag = true)]
        public MCRange Count { get => count; set => count = value ?? throw new ArgumentNullException(nameof(count), "Count may not be null"); }
    }
}
