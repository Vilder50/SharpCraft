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

        public enum LootBonusFormula
        {
            binomial_with_bonus_count,

            uniform_bonus_count,

            ore_drops
        }

        public enum LootEntryType
        {
            item,
            tag,
            loot_table,
            group,
            alternatives,
            sequence,
            dynamic,
            empty
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
            TagNamespacedString,

            TagCompound = 99,
            TagArrayArray = 100,

            TagByteArray = 101,
            TagShortArray = 102,
            TagIntArray = 103,
            TagLongArray = 104,
            TagFloatArray = 105,
            TagDoubleArray = 106,
            TagStringArray = 107,
            TagNamespacedStringArray = 108,
            TagCompoundArray = 199,
        }
#pragma warning restore 1591
    }
}
