using System;
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
    public class LootingChange : BaseChange
    {
        private MCRange count = null!;

        /// <summary>
        /// Intializes a new <see cref="LootingChange"/>
        /// </summary>
        /// <param name="count">The amount of items in each stack</param>
        /// <param name="limit">The maximum amount of items this change can make. Setting to 0 = no limit.</param>
        public LootingChange(MCRange count, int limit = 0) : base("limit_count")
        {
            Count = count;
            Limit = limit;
        }

        /// <summary>
        /// The amount of extra items per looting level
        /// </summary>
        [DataTag("count", "min", "max", ID.NBTTagType.TagDouble, true, JsonTag = true)]
        public MCRange Count { get => count; set => count = value ?? throw new ArgumentNullException(nameof(Count), "Count may not be null"); }

        /// <summary>
        /// The maximum amount of items this change can make. Setting to 0 = no limit.
        /// </summary>
        [DataTag("limit", JsonTag = true)]
        public int Limit { get; set; }
    }
}
