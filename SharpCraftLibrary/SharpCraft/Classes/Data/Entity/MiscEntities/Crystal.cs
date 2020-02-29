using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// An object for end crystal entities
    /// </summary>
    public class EndCrystal : BasicEntity
    {
        /// <summary>
        /// Creates a new end crystal
        /// </summary>
        /// <param name="type">the type of entity</param>
        public EndCrystal(ID.Entity? type = ID.Entity.end_crystal) : base(type) { }

        /// <summary>
        /// If the bedrock should be shown under the crystal
        /// </summary>
        [Data.DataTag]
        public bool? ShowBottom { get; set; }
        /// <summary>
        /// The coords the crystal's beam should point to
        /// </summary>
        [Data.DataTag((object)"X", "Y", "Z")]
        public IntVector? BeamTarget { get; set; }
    }
}
