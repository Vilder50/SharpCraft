using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for zombie pigmen
    /// </summary>
    public class ZombifiedPiglin : Zombie
    {
        /// <summary>
        /// Creates a new combified piglin
        /// </summary>
        /// <param name="type">the type of entity</param>
        public ZombifiedPiglin(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public ZombifiedPiglin() : base(SharpCraft.ID.Entity.zombified_piglin) { }

        /// <summary>
        /// The time till the zombie pigman stops being angry (0 or smaller if not angry)
        /// </summary>
        [Data.DataTag]
        public Time<short>? Anger { get; set; }

        /// <summary>
        /// The <see cref="UUID"/> of the entity this pigman is angry on
        /// </summary>
        [Data.DataTag("HurtBy", ForceType = ID.NBTTagType.TagIntArray)]
        public UUID? AngryOn { get; set; }
    }
}
