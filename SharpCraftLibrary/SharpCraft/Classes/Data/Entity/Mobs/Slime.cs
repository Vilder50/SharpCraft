﻿using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for slimes and magma cubes
    /// </summary>
    public class Slime : Mob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Slime> PathCreator => new Data.DataPathCreator<Slime>();

        /// <summary>
        /// Creates a new slime or magma cube
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Slime(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Slime() : base(SharpCraft.ID.Entity.slime) { }

        /// <summary>
        /// The size of the slime
        /// </summary>
        [Data.DataTag]
        public int? Size { get; set; }
        /// <summary>
        /// True if the slime touches the ground
        /// </summary>
        [Data.DataTag]
        public bool? WasOnGround { get; set; }
    }
}
