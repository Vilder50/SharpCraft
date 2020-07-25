using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// An object for end crystal entities
    /// </summary>
    public class EndCrystal : BasicEntity
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<EndCrystal> PathCreator => new Data.DataPathCreator<EndCrystal>();

        /// <summary>
        /// Creates a new end crystal
        /// </summary>
        /// <param name="type">the type of entity</param>
        public EndCrystal(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public EndCrystal() : base(SharpCraft.ID.Entity.end_crystal) { }

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
