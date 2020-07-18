using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes the content stored in the loot
    /// </summary>
    public class ContentChange : BaseChange
    {
        private BaseEntry[] entries = null!;

        /// <summary>
        /// Intializes a new <see cref="ContentChange"/>
        /// </summary>
        /// <param name="entries">The loot to put into the item</param>
        public ContentChange(BaseEntry[] entries) : base("minecraft:set_contents")
        {
            Entries = entries;
        }

        /// <summary>
        /// The loot to put into the item
        /// </summary>
        [DataTag("entries", JsonTag = true)]
        public BaseEntry[] Entries { get => entries; set => entries = value ?? throw new ArgumentNullException(nameof(Entries), "Entries may not be null"); }
    }
}
