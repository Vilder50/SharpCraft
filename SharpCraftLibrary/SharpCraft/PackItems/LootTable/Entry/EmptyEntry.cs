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
    public class EmptyEntry : BaseEntry
    {
        /// <summary>
        /// Intializes a new <see cref="EmptyEntry"/>
        /// </summary>
        public EmptyEntry() : base(ID.LootEntryType.empty)
        {

        }
    }
}

