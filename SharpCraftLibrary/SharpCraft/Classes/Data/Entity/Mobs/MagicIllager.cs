using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// List of all entities
    /// </summary>
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for evokers and illusioners
        /// </summary>
        public class MagicIllager : BaseIllager
        {
            /// <summary>
            /// Creates a new evoker or illusioner
            /// </summary>
            /// <param name="type">the type of entity</param>
            public MagicIllager(ID.Entity? type = ID.Entity.evoker) : base(type) { }

            /// <summary>
            /// The time till the next spell is casted
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
            public Time SpellTicks { get; set; }
        }
    }
}
