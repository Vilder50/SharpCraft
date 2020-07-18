using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for hoglins
    /// </summary>
    public class Hoglin : Entities.BreedableMob
    {
        /// <summary>
        /// Creates a new hoglin
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Hoglin(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Hoglin() : base(SharpCraft.ID.Entity.hoglin) { }

        /// <summary>
        /// True if it shouldn't turn into a <see cref="ID.Entity.zoglin"/> when in the overworld
        /// </summary>
        [Data.DataTag]
        public bool? IsImmuneToZombification { get; set; }

        /// <summary>
        /// True if it shouldn't be hunted by piglins
        /// </summary>
        [Data.DataTag]
        public bool? CannotBeHunted { get; set; }

        /// <summary>
        /// Time the hoglin has been in the overworld. It transforms after 15 seconds.
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
        public Time<int>? TimeInOverworld { get; set; }
    }
}
