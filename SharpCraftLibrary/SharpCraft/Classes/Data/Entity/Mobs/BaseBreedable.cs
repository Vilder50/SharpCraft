using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// The basic entity data for breedable mobs
    /// </summary>
    public class BreedableMob : Mob
    {
        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="type">the type of entity</param>
        public BreedableMob(ID.Entity? type) : base(type) { }

        /// <summary>
        /// The amount of time the mob will be in love
        /// </summary>
        [Data.DataTag]
        public Time<int>? InLove { get; set; }

        /// <summary>
        /// When negative it's the time till the mob turns into an adult
        /// When positive it's the time till the mob can breed again
        /// </summary>
        [Data.DataTag]
        public Time<int>? Age { get; set; }

        /// <summary>
        /// A age which will be given to the mob when it has grown up.
        /// </summary>
        [Data.DataTag]
        public Time<int>? ForcedAge { get; set; }

        /// <summary>
        /// The <see cref="UUID"/> of the entity who fed the mob
        /// </summary>
        [Data.DataTag("LoveCause", ForceType = ID.NBTTagType.TagIntArray)]
        public UUID? LoveCause { get; set; }

    }
}