﻿using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for vexes
    /// </summary>
    public class Vex : Mob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Vex> PathCreator => new Data.DataPathCreator<Vex>();

        /// <summary>
        /// Creates a new vex
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Vex(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Vex() : base(SharpCraft.ID.Entity.vex) { }

        /// <summary>
        /// The time till the vex dissapears
        /// </summary>
        [Data.DataTag]
        public Time<int>? LifeTicks { get; set; }
        /// <summary>
        /// The location the vex should fly around in
        /// (It flies to random location in a 15x11x15 around this spot)
        /// </summary>
        [Data.DataTag((object)"BoundX", "BoundY", "BoundZ", Merge = true)]
        public IntVector? Bound { get; set; }
    }
}
