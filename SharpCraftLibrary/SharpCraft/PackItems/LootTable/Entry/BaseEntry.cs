using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Base class for loot entries in a loot pools in a loot tables
    /// </summary>
    public abstract class BaseEntry : DataHolderBase
    {
        /// <summary>
        /// Intializes a new <see cref="BaseEntry"/>
        /// </summary>
        /// <param name="entryType">The entry type</param>
        /// <param name="weight">The weight of this entry</param>
        protected BaseEntry(ID.LootEntryType entryType, int weight = 1)
        {
            EntryType = entryType;
            Weight = weight;
        }

        /// <summary>
        /// The type of loot entry
        /// </summary>
        [DataTag("type", ForceType = ID.NBTTagType.TagNamespacedString, JsonTag = true)]
        public ID.LootEntryType EntryType { get; private set; }

        /// <summary>
        /// All conditions have to be true for this entry to be used
        /// </summary>
        [DataTag("conditions", JsonTag = true)]
        public Conditions.BaseCondition[]? Conditions { get; set; }

        /// <summary>
        /// The weight of this entry
        /// </summary>
        [DataTag("weight", JsonTag = true)]
        public int Weight { get; set; }

        /// <summary>
        /// Extra weight for the entry based on the amount of luck the player has
        /// </summary>
        [DataTag("quality", JsonTag = true)]
        public int? LuckyWeight { get; set; }

        /// <summary>
        /// Converts a single entry into an array of entries
        /// </summary>
        /// <param name="entry">The entry to convert</param>
        public static implicit operator BaseEntry[](BaseEntry entry)
        {
            return new BaseEntry[] { entry };
        }
    }
}
