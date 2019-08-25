﻿using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for phantoms
        /// </summary>
        public class Phantom : BaseMob
        {
            /// <summary>
            /// Creates a new phantom
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Phantom(ID.Entity? type = ID.Entity.phantom) : base(type) { }

            /// <summary>
            /// The phantom will circle around this location when not attacking
            /// </summary>
            [Data.DataTag((object)"AX","AY","AZ", ID.NBTTagType.TagInt, Merge = true)]
            public Coords Area { get; set; }
            /// <summary>
            /// The size of the phantom.
            /// (0-64) Damage = 6+size
            /// </summary>
            [Data.DataTag]
            public int? Size { get; set; }
        }
    }
}
