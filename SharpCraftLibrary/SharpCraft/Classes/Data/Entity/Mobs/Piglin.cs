using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for piglins
    /// </summary>
    public class Piglin : Mob
    {
        /// <summary>
        /// Creates a new piglin
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Piglin(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Piglin() : base(SharpCraft.ID.Entity.piglin) { }

        /// <summary>
        /// If its a baby
        /// </summary>
        [Data.DataTag]
        public bool? IsBaby { get; set; }

        /// <summary>
        /// True if it shouldn't turn into a <see cref="ID.Entity.zombified_piglin"/> when in the overworld
        /// </summary>
        [Data.DataTag]
        public bool? IsImmuneToZombification { get; set; }

        /// <summary>
        /// The items in the piglins inventory
        /// </summary>
        [Data.DataTag]
        public Item[]? Inventory { get; set; }

        /// <summary>
        /// True if it shouldn't hunt hoglins
        /// </summary>
        [Data.DataTag]
        public bool? CannotHunt { get; set; }

        /// <summary>
        /// Time the piglin has been in the overworld. It transforms after 15 seconds.
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
        public Time<int>? TimeInOverworld { get; set; }
    }
}
