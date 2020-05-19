using System.Collections.Generic;

namespace SharpCraft.Entities
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
        [Data.DataTag]
        public Time<int>? SpellTicks { get; set; }
    }
}
