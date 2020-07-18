using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// An object for llama spit entities
    /// </summary>
    public class LlamaSpit : BasicEntity
    {
        /// <summary>
        /// Creates new llama spit
        /// </summary>
        /// <param name="type">the type of entity</param>
        public LlamaSpit(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public LlamaSpit() : base(SharpCraft.ID.Entity.llama_spit) { }

        /// <summary>
        /// The owner of the spit
        /// </summary>
        [Data.DataTag("Owner", ForceType = ID.NBTTagType.TagIntArray)]
        public UUID? OwnerUUID { get; set; }
    }
}
