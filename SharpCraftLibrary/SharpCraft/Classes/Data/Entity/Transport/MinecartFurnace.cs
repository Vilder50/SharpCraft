using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for furnace minecarts
        /// </summary>
        public class MinecartFurnace : BaseMinecart
        {
            /// <summary>
            /// Creates a new furnace minecart
            /// </summary>
            /// <param name="type">the type of entity</param>
            public MinecartFurnace(ID.Entity? type = ID.Entity.furnace_minecart) : base(type) { }

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
            [Data.DataTag(ForceType = ID.NBTTagType.TagShort)]
            public Time? Fuel { get; set; }
        }
    }
}
