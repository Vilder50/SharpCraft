using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// The basic entity data for tameable
        /// </summary>
        public abstract class BaseTameable : BaseBreedable
        {
            /// <summary>
            /// Creates a new entity
            /// </summary>
            /// <param name="type">the type of entity</param>
            public BaseTameable(ID.Entity? type) : base(type) { }

            /// <summary>
            /// If the mob is sitting (wont follow / tp to its owner)
            /// </summary>
            [Data.DataTag]
            public bool? Sitting { get; set; }
            /// <summary>
            /// the <see cref="UUID"/> of the owner
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagString)]
            public UUID? OwnerUUID { get; set; }
        }

        /// <summary>
        /// Entity data for tameable mobs
        /// </summary>
        public class Tameable : BaseTameable
        {
            /// <summary>
            /// Creates a new tameable mob
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Tameable(ID.Entity? type) : base(type) { }
        }
    }
}
