using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Entry which drops dynamic loot
    /// </summary>
    public class DynamicEntry : BaseEntry
    {
        /// <summary>
        /// Intializes a new <see cref="DynamicEntry"/>
        /// </summary>
        /// <param name="type">The type of loot to get</param>
        public DynamicEntry(DynamicType type) : base(ID.LootEntryType.dynamic)
        {
            Type = type;
        }

        /// <summary>
        /// The type of loot to get
        /// </summary>
        [DataTag("name", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
        public DynamicType Type { get; set; }

        /// <summary>
        /// the place to get the items from
        /// </summary>
        public enum DynamicType
        {
            /// <summary>
            /// The content of the block
            /// </summary>
            contents,

            /// <summary>
            /// The block
            /// </summary>
            self
        }
    }
}

