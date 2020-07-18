using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for furnace minecarts
    /// </summary>
    public class MinecartFurnace : Minecart
    {
        /// <summary>
        /// Creates a new furnace minecart
        /// </summary>
        /// <param name="type">the type of entity</param>
        public MinecartFurnace(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public MinecartFurnace() : base(SharpCraft.ID.Entity.furnace_minecart) { }

        /// <summary>
        /// Force along the x axis
        /// </summary>
        [Data.DataTag]
        public double? PushX { get; set; }
        /// <summary>
        /// Force along the y axis
        /// </summary>
        [Data.DataTag]
        public double? PushZ { get; set; }
        /// <summary>
        /// The time till the furnace's fuel runs out
        /// </summary>
        [Data.DataTag]
        public Time<short>? Fuel { get; set; }
    }
}
