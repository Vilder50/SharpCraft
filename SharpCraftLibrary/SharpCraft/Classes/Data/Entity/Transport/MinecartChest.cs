using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for chest minecarts
        /// </summary>
        public class MinecartChest : BaseMinecart
        {
            /// <summary>
            /// Creates a new chest minecart
            /// </summary>
            /// <param name="type">the type of entity</param>
            public MinecartChest(ID.Entity? type = ID.Entity.chest_minecart) : base(type) { }

            /// <summary>
            /// The chest's loottable
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagString)]
            public LootTable? LootTable { get; set; }
            /// <summary>
            /// The seed used to generate the loot
            /// </summary>
            [Data.DataTag]
            public long? LootTableSeed { get; set; }
            /// <summary>
            /// The items in the chest
            /// </summary>
            [Data.DataTag]
            public SharpCraft.Item?[]? Items { get; set; }
        }
    }
}
