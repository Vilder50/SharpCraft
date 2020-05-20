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
        public MinecartTNT(ID.Entity? type = ID.Entity.tnt_minecart) : base(type) { }

        /// <summary>
        /// Time till it explodes
        /// (-1 ticks = not exploding)
        /// </summary>
        [Data.DataTag]
        public Time<int>? TNTFuse { get; set; }
    }
}
