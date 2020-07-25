using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for evokers and illusioners
    /// </summary>
    public class MagicIllager : BaseIllager
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<MagicIllager> PathCreator => new Data.DataPathCreator<MagicIllager>();

        /// <summary>
        /// Creates a new evoker or illusioner
        /// </summary>
        /// <param name="type">the type of entity</param>
        public MagicIllager(ID.Entity? type) : base(type) { }

        /// <summary>
        /// The time till the next spell is casted
        /// </summary>
        [Data.DataTag]
        public Time<int>? SpellTicks { get; set; }
    }
}
