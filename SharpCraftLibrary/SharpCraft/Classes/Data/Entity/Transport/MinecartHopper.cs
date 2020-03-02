using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for hopper minecarts
    /// </summary>
    public class MinecartHopper : Minecart
    {
        /// <summary>
        /// Creates a new hopper minecart
        /// </summary>
        /// <param name="type">the type of entity</param>
        public MinecartHopper(ID.Entity? type = ID.Entity.hopper_minecart) : base(type) { }

        /// <summary>
        /// The hopper's loottable
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagString)]
        public LootTable? LootTable { get; set; }
        /// <summary>
        /// The seed used to generate the loot
        /// </summary>
        [Data.DataTag]
        public long? LootTableSeed { get; set; }
        /// <summary>
        /// The items in the hopper
        /// </summary>
        [Data.DataTag]
        public SharpCraft.Item[]? Items { get; set; }
        /// <summary>
        /// If the hopper is enabled
        /// </summary>
        [Data.DataTag]
        public bool? Enabled { get; set; }
        /// <summary>
        /// Time until it transfer another item
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
        public Time? TransferCooldown { get; set; }
    }
}
