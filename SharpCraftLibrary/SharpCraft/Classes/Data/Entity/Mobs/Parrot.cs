﻿using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for parrots
    /// </summary>
    public class Parrot : TameableMob
    {
        /// <summary>
        /// Creates a new parrot
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Parrot(ID.Entity? type = ID.Entity.parrot) : base(type) { }

        /// <summary>
        /// How the parrot looks
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
        public ID.Parrot? ParrotType { get; set; }
    }
}
