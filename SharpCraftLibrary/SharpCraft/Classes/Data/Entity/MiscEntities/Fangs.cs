using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// An object for evoker fangs entities
    /// </summary>
    public class EvokerFangs : BasicEntity
    {
        /// <summary>
        /// Creates a new evoker fangs
        /// </summary>
        /// <param name="type">the type of entity</param>
        public EvokerFangs(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public EvokerFangs() : base(SharpCraft.ID.Entity.evoker_fangs) { }

        /// <summary>
        /// The amount of time before the fangs appear
        /// </summary>
        [Data.DataTag]
        public Time<int>? Warmup { get; set; }

        /// <summary>
        /// The <see cref="UUID"/> of the entity who summoned the fangs
        /// </summary>
        [Data.DataTag("Owner", ForceType = ID.NBTTagType.TagIntArray)]
        public UUID? OwnerUUID { get; set; }

        /// <summary>
        /// Makes the fang into a marker entity
        /// (Sets <see cref="Warmup"/> to its max value which makes the entity invisible)
        /// </summary>
        public bool Marker
        {
            get
            {
                if (Warmup is null)
                {
                    return false;
                }
                return Warmup.GetAsTicks() == int.MaxValue;
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
