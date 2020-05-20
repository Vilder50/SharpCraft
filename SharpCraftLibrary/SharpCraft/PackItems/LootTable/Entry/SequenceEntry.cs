using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Entry where only all entries are used untill an entry's conditions returns false
    /// </summary>
    public class SequenceEntry : BaseEntry
    {
        private BaseEntry[] entries = null!;

        /// <summary>
        /// Intializes a new <see cref="SequenceEntry"/>
        /// </summary>
        /// <param name="entries">The entries to sequence over</param>
        public SequenceEntry(BaseEntry[] entries) : base(ID.LootEntryType.sequence)
        {
            Entries = entries;
        }

        /// <summary>
        /// The entries to sequence over
        /// </summary>
        [DataTag("children", JsonTag = true)]
        public BaseEntry[] Entries { get => entries; set => entries = value ?? throw new ArgumentNullException(nameof(Entries), "Entries may not be null"); }
    }
}

