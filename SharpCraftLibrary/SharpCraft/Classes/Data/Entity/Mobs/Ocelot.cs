using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for ocelots
        /// </summary>
        public class Ocelot : BaseMob
        {
            /// <summary>
            /// Creates a new ocelot
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Ocelot(ID.Entity? type = ID.Entity.ocelot) : base(type) { }

            /// <summary>
            /// If the ocelot trusts the player
            /// </summary>
            [Data.DataTag("Trusting")]
            public bool? Trust { get; set; }
        }
    }
}
