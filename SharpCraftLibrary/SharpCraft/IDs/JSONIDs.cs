namespace SharpCraft
{
    /// <summary>
    /// All the different ID's/Types/States things in the game can have
    /// </summary>
    public static partial class ID
    {
#pragma warning disable 1591
        public enum LootTarget
        {
            /// <summary>
            /// Checks as the killed entity
            /// </summary>
            This,

            /// <summary>
            /// Checks as the killer
            /// </summary>
            Killer,

            /// <summary>
            /// Checks as the killer if the killer is a player
            /// </summary>
            Killer_player,

            /// <summary>
            /// The mined block
            /// </summary>
            block_entity,
        }

        public class LootBonusFormula: NamespacedEnumLike<string>
        {
            public LootBonusFormula(string value, BasePackNamespace? @namespace = null) : base(value, @namespace)
            {
            }

            public static readonly LootBonusFormula binomial_with_bonus_count = new LootBonusFormula("binomial_with_bonus_count");
            public static readonly LootBonusFormula uniform_bonus_count = new LootBonusFormula("uniform_bonus_count");
            public static readonly LootBonusFormula ore_drops = new LootBonusFormula("ore_drops");
        }

        public class LootEntryType : NamespacedEnumLike<string>
        {
            public LootEntryType(string value, BasePackNamespace? @namespace = null) : base(value, @namespace)
            {
            }

            public static readonly LootEntryType item = new LootEntryType("item");
            public static readonly LootEntryType tag = new LootEntryType("tag");
            public static readonly LootEntryType loot_table = new LootEntryType("loot_table");
            public static readonly LootEntryType group = new LootEntryType("group");
            public static readonly LootEntryType alternatives = new LootEntryType("alternatives");
            public static readonly LootEntryType sequence = new LootEntryType("sequence");
            public static readonly LootEntryType dynamic = new LootEntryType("dynamic");
            public static readonly LootEntryType empty = new LootEntryType("empty");
        }

        public enum AdvancementFrame
        {
            /// <summary>
            /// Normal basic advancement
            /// </summary>
            task,
            /// <summary>
            /// A star advancement which is supposed to be hard to get
            /// It makes a sounds and makes the name purple
            /// </summary>
            challenge,
            /// <summary>
            /// A advancement which is round instead of being square
            /// </summary>
            goal
        }

        /// <summary>
        /// Types of datatags
        /// </summary>
        public enum SimpleNBTTagType
        {
            /// <summary>
            /// Array tag
            /// </summary>
            Array,
            /// <summary>
            /// Compound tag
            /// </summary>
            Compound,
            /// <summary>
            /// Normal tag
            /// </summary>
            Tag,
            /// <summary>
            /// Unknown tag type
            /// </summary>
            Unknown,
        }

        public enum LootDataModifierType
        {
            replace,
            append,
            merge
        }

        public enum NBTTagType
        {
            TagByte,
            TagShort,
            TagInt,
            TagLong,
            TagFloat,
            TagDouble,
            TagString,

            TagCompound = 99,
            TagArrayArray = 100,

            TagByteArray = 101,
            TagShortArray = 102,
            TagIntArray = 103,
            TagLongArray = 104,
            TagFloatArray = 105,
            TagDoubleArray = 106,
            TagStringArray = 107,
            TagCompoundArray = 199,
        }
#pragma warning restore 1591
    }
}
