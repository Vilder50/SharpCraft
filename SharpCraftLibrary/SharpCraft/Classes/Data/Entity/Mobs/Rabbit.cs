using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for rabbits
        /// </summary>
        public class Rabbit : BaseBreedable
        {
            /// <summary>
            /// Creates a new rabbit
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Rabbit(ID.Entity? type = ID.Entity.rabbit) : base(type) { }

            /// <summary>
            /// The type of rabbit
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
            public ID.Rabbit? RabbitType { get; set; }
            /// <summary>
            /// Set to 40 when the rabbit has eaten a carrot.
            /// Goes down by 0-2 every tick.
            /// </summary>
            [Data.DataTag]
            public int? MoreCarrotTicks { get; set; }
        }
    }
}
