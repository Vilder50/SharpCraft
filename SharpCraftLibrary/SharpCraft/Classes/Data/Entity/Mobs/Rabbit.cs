using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for rabbits
    /// </summary>
    public class Rabbit : BreedableMob
    {
        /// <summary>
        /// Creates a new rabbit
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Rabbit(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Rabbit() : base(SharpCraft.ID.Entity.rabbit) { }

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
