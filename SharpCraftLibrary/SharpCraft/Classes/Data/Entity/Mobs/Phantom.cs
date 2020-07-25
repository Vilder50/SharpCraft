using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for phantoms
    /// </summary>
    public class Phantom : Mob
    {
        /// <summary>
        /// Returns a object which can be used for creating data paths
        /// </summary>
        /// <returns>Object used for making data paths</returns>
        public new static Data.DataPathCreator<Phantom> PathCreator => new Data.DataPathCreator<Phantom>();

        /// <summary>
        /// Creates a new phantom
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Phantom(ID.Entity? type) : base(type) { }

        /// <summary>
        /// Creates a new entity
        /// </summary>
        public Phantom() : base(SharpCraft.ID.Entity.phantom) { }

        /// <summary>
        /// The phantom will circle around this location when not attacking
        /// </summary>
        [Data.DataTag((object)"AX", "AY", "AZ", Merge = true)]
        public IntVector? Area { get; set; }
        /// <summary>
        /// The size of the phantom.
        /// (0-64) Damage = 6+size
        /// </summary>
        [Data.DataTag]
        public int? Size { get; set; }
    }
}
