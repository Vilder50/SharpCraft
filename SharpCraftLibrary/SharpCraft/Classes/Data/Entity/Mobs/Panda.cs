using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for pandas
    /// </summary>
    public class Panda : TameableMob
    {
        /// <summary>
        /// Creates a new panda
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Panda(ID.Entity? type = ID.Entity.panda) : base(type) { }

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
