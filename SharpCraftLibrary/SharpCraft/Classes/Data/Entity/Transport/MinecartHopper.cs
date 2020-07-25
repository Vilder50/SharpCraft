using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for hopper minecarts
    /// </summary>
    public class MinecartHopper : Minecart
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<MinecartHopper> PathCreator => new Data.DataPathCreator<MinecartHopper>();

        /// <summary>
        /// Creates a new hopper minecart
        /// </summary>
        /// <param name="type">the type of entity</param>
        public MinecartHopper(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public MinecartHopper() : base(SharpCraft.ID.Entity.hopper_minecart) { }

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
        [Data.DataTag]
        public Time<int>? TransferCooldown { get; set; }
    }
}
