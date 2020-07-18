﻿using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for ender dragons
    /// </summary>
    public class Dragon : Mob
    {
        /// <summary>
        /// Creates a new ender dragon
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Dragon(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Dragon() : base(SharpCraft.ID.Entity.ender_dragon) { }

        /// <summary>
        /// The phase the ender dragon is in
        /// </summary>
        [Data.DataTag("DragonPhase", ForceType = ID.NBTTagType.TagInt)]
        public ID.DragonPhase? Phase { get; set; }
    }
}
