using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// An object for llama spit entities
        /// </summary>
        public class LlamaSpit : EntityBasic
        {
            /// <summary>
            /// Creates new llama spit
            /// </summary>
            /// <param name="type">the type of entity</param>
            public LlamaSpit(ID.Entity? type = ID.Entity.llama_spit) : base(type) { }

            /// <summary>
            /// The owner of the spit
            /// </summary>
            [Data.DataTag("Owner", ForceType = ID.NBTTagType.TagIntArray)]
            public UUID OwnerUUID { get; set; }
        }
    }
}
