using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for zombies and drowned
    /// </summary>
    public class Zombie : Mob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Zombie> PathCreator => new Data.DataPathCreator<Zombie>();

        /// <summary>
        /// Creates a new zombie or drowned
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Zombie(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Zombie() : base(SharpCraft.ID.Entity.zombie) { }

        /// <summary>
        /// If its a baby
        /// </summary>
        [Data.DataTag]
        public bool? IsBaby { get; set; }

        /// <summary>
        /// If its allowed to break doors
        /// </summary>
        [Data.DataTag]
        public bool? CanBreakDoors { get; set; }

        /// <summary>
        /// Time till it converts into its next drowning type
        /// </summary>
        [Data.DataTag]
        public Time<int>? DrownedConversionTime { get; set; }

        /// <summary>
        /// The amount of time the zombie has been in water
        /// </summary>
        [Data.DataTag]
        public Time<int>? InWaterTime { get; set; }
    }
}
