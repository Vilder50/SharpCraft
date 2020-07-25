using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for ender dragons
    /// </summary>
    public class Dragon : Mob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Dragon> PathCreator => new Data.DataPathCreator<Dragon>();

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
