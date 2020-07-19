using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for turtles
    /// </summary>
    public class Turtle : BreedableMob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Turtle> PathCreator => new Data.DataPathCreator<Turtle>();

        /// <summary>
        /// Creates a new turtle
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Turtle(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Turtle() : base(SharpCraft.ID.Entity.turtle) { }

        /// <summary>
        /// The location of the turtles home
        /// </summary>
        [Data.DataTag((object)"HomePosX", "HomePosY", "HomePosZ", Merge = true)]
        public IntVector? HomePos { get; set; }
        /// <summary>
        /// The location the turtle is traveling to
        /// </summary>
        [Data.DataTag((object)"TravelPosX", "TravelPosY", "TravelPosZ", Merge = true)]
        public IntVector? TravelPos { get; set; }
        /// <summary>
        /// True if the turtle has eggs
        /// </summary>
        [Data.DataTag]
        public bool? HasEgg { get; set; }
    }
}
