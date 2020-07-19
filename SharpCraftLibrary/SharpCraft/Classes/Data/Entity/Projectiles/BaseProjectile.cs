using System.Collections.Generic;
using System;

namespace SharpCraft.Entities
{
    /// <summary>
    /// The base of all projectile entities
    /// </summary>
    public abstract class BaseProjectile : BasicEntity
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<BaseProjectile> PathCreator => new Data.DataPathCreator<BaseProjectile>();

        /// <summary>
        /// Creates a new entity
        /// </summary>
        /// <param name="type">the type of entity</param>
        public BaseProjectile(ID.Entity? type) : base(type) { }

        /// <summary>
        /// The coords of the block
        /// </summary>
        [Data.DataTag((object)"xTile", "yTile", "zTile", Merge = true)]
        public IntVector? TileCoords { get; set; }

        /// <summary>
        /// The block this projectile is in
        /// </summary>
        [Data.DataTag("inBlockState", "Name", "Properties")]
        public Block? InBlock { get; set; }
    }
}
