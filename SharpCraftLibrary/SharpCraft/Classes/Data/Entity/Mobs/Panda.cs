using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for pandas
    /// </summary>
    public class Panda : TameableMob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Panda> PathCreator => new Data.DataPathCreator<Panda>();

        /// <summary>
        /// Creates a new panda
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Panda(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Panda() : base(SharpCraft.ID.Entity.panda) { }

        /// <summary>
        /// The type of the panda
        /// </summary>
        [Data.DataTag("MainGene", ForceType = ID.NBTTagType.TagString)]
        public ID.Panda? MainType { get; set; }

        /// <summary>
        /// The panda's hidden type which can be transfered to it's children
        /// </summary>
        [Data.DataTag("HiddenGene", ForceType = ID.NBTTagType.TagString)]
        public ID.Panda? HiddenType { get; set; }
    }
}
