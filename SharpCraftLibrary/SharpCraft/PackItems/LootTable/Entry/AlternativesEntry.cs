using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Entry where only the first entry where the conditions are true is used
    /// </summary>
    public class AlternativesEntry : BaseEntry
    {
        private BaseEntry[] entries = null!;

        /// <summary>
        /// Intializes a new <see cref="GroupEntry"/>
        /// </summary>
        /// <param name="entries">The entries to alternative over</param>
        public AlternativesEntry(BaseEntry[] entries) : base(ID.LootEntryType.alternatives)
        {
            Entries = entries;
        }

        /// <summary>
        /// The entries to alternative over
        /// </summary>
        [DataTag("children", JsonTag = true)]
        public BaseEntry[] Entries { get => entries; set => entries = value ?? throw new ArgumentNullException(nameof(Entries), "Entries may not be null"); }
    }
}

