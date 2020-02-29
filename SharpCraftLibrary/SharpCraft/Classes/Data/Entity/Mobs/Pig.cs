﻿using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for pigs
    /// </summary>
    public class Pig : BreedableMob
    {
        /// <summary>
        /// Creates a new pigs
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Pig(ID.Entity? type = ID.Entity.pig) : base(type) { }

        /// <summary>
        /// If the pig has a saddle on
        /// </summary>
        [Data.DataTag]
        public bool? Saddle { get; set; }
    }
}
