using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for piglins
        /// </summary>
        public class Piglin : BaseMob
        {
            /// <summary>
            /// Creates a new piglin
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Piglin(ID.Entity? type = ID.Entity.piglin) : base(type) { }

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
            public Item[] Inventory { get; set; }
        }
    }
}
