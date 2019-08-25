using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for withers
        /// </summary>
        public class Wither : BaseMob
        {
            /// <summary>
            /// Creates a new wither
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Wither(ID.Entity? type = ID.Entity.wither) : base(type) { }

            /// <summary>
            /// The time before the wither actually spawns / can be fought
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
            public Time Invul { get; set; }
        }
    }
}
