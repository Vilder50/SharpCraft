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
        public LlamaSpit(ID.Entity? type = ID.Entity.llama_spit) : base(type) { }

        /// <summary>
        /// The owner of the spit
        /// </summary>
        [Data.DataTag("Owner", "OwnerUUIDMost", "OwnerUUIDLeast")]
        public UUID? OwnerUUID { get; set; }
    }
}
