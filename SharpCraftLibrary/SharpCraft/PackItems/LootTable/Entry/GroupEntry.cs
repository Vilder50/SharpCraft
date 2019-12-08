using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Entry for dropping a group of entries
    /// </summary>
    public class GroupEntry : BaseEntry
    {
        private BaseEntry[] entries;

        /// <summary>
        /// Intializes a new <see cref="GroupEntry"/>
        /// </summary>
        /// <param name="entries">The entries to group together</param>
        public GroupEntry(BaseEntry[] entries) : base(ID.LootEntryType.group)
        {
            Entries = entries;
        }

        /// <summary>
        /// The entries to group together
        /// </summary>
        [DataTag("children", JsonTag = true)]
        public BaseEntry[] Entries { get => entries; set => entries = value ?? throw new ArgumentNullException(nameof(Entries), "Entries may not be null"); }
    }
}

