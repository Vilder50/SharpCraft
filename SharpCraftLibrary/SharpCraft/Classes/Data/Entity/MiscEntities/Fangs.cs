using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// An object for evoker fangs entities
        /// </summary>
        public class EvokerFangs : EntityBasic
        {
            /// <summary>
            /// Creates a new evoker fangs
            /// </summary>
            /// <param name="type">the type of entity</param>
            public EvokerFangs(ID.Entity? type = ID.Entity.evoker_fangs) : base(type) { }

            /// <summary>
            /// The amount of time before the fangs appear
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
            public Time Warmup { get; set; }

            /// <summary>
            /// The <see cref="UUID"/> of the entity who summoned the fangs
            /// </summary>
            [Data.DataTag("Owner","OwnerUUIDMost","OwnerUUIDLeast")]
            public UUID OwnerUUID { get; set; }

            /// <summary>
            /// Makes the fang into a marker entity
            /// (Sets <see cref="Warmup"/> to its max value which makes the entity invisible)
            /// </summary>
            public bool Marker
            {
                get
                {
                    return Warmup.AsTicks() == int.MaxValue;
                }
                set
                {
                    if (value)
                    {
                        Warmup = int.MaxValue;
                    }
                    else
                    {
                        Warmup = null;
                    }
                }
            }
        }
    }
}
