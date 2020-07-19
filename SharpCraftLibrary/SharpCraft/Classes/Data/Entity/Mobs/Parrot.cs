using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for parrots
    /// </summary>
    public class Parrot : TameableMob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Parrot> PathCreator => new Data.DataPathCreator<Parrot>();

        /// <summary>
        /// Creates a new parrot
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Parrot(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Parrot() : base(SharpCraft.ID.Entity.parrot) { }

        /// <summary>
        /// How the parrot looks
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
        public ID.Parrot? ParrotType { get; set; }
    }
}
