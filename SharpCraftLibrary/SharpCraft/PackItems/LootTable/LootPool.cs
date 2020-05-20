using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;
using SharpCraft.Conditions;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// A loot pool in a loot table
    /// </summary>
    public class LootPool : DataHolderBase
    {
        private BaseEntry[] entries = null!;
        private MCRange rolls = null!;

        /// <summary>
        /// Intializes a new <see cref="LootPool"/>
        /// </summary>
        /// <param name="rolls">The amount of rolls to make (The amount of entries to use)</param>
        /// <param name="luckyRolls">The amount of extra rolls to take based on luck</param>
        /// <param name="conditions">All conditions have to be true for this pool to be used</param>
        /// <param name="entries">Entries in this pool</param>
        public LootPool(BaseEntry[] entries, MCRange rolls, BaseCondition[]? conditions = null, MCRange? luckyRolls = null)
        {
            Rolls = rolls;
            LuckyRolls = luckyRolls;
            Conditions = conditions;
            Entries = entries;
        }

        /// <summary>
        /// The amount of rolls to make (The amount of entries to use)
        /// </summary>
        [DataTag("rolls", "min", "max", ID.NBTTagType.TagInt, true, JsonTag = true)]
        public MCRange Rolls { get => rolls; set => rolls = value ?? throw new ArgumentNullException(nameof(Rolls), "Rolls may not be null"); }

        /// <summary>
        /// The amount of extra rolls to take based on luck
        /// </summary>
        [DataTag("bonus_rolls", "min", "max", ID.NBTTagType.TagFloat, true, JsonTag = true)]
        public MCRange? LuckyRolls { get; set; }

        /// <summary>
        /// All conditions have to be true for this pool to be used
        /// </summary>
        [DataTag("conditions", JsonTag = true)]
        public BaseCondition[]? Conditions { get; set; }

        /// <summary>
        /// Entries in this pool
        /// </summary>
        [DataTag("entries", JsonTag = true)]
        public BaseEntry[] Entries { get => entries; set => entries = value ?? throw new ArgumentNullException(nameof(Entries), "Entries may not be null"); }

        /// <summary>
        /// Converts a single pool into an array
        /// </summary>
        /// <param name="pool">The pool to convert</param>
        public static implicit operator LootPool[] (LootPool pool)
        {
            return new LootPool[] { pool };
        }
    }
}
