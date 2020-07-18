﻿using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for turtles
    /// </summary>
    public class Turtle : BreedableMob
    {
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
