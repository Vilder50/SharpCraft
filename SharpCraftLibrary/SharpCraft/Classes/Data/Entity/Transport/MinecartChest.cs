using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for chest minecarts
    /// </summary>
    public class MinecartChest : Minecart
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<MinecartChest> PathCreator => new Data.DataPathCreator<MinecartChest>();

        /// <summary>
        /// Creates a new chest minecart
        /// </summary>
        /// <param name="type">the type of entity</param>
        public MinecartChest(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public MinecartChest() : base(SharpCraft.ID.Entity.chest_minecart) { }

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
        public SharpCraft.Item[]? Items { get; set; }
    }
}
