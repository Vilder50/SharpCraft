using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for tnt minecarts
    /// </summary>
    public class MinecartTNT : Minecart
    {
        /// <summary>
        /// Creates a new tnt minecart
        /// </summary>
        /// <param name="type">the type of entity</param>
        public MinecartTNT(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public MinecartTNT() : base(SharpCraft.ID.Entity.tnt_minecart) { }

        /// <summary>
        /// Time till it explodes
        /// (-1 ticks = not exploding)
        /// </summary>
        [Data.DataTag]
        public Time<int>? TNTFuse { get; set; }
    }
}
