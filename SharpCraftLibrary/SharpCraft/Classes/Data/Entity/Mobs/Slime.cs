using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for slimes and magma cubes
    /// </summary>
    public class Slime : Mob
    {
        /// <summary>
        /// Creates a new slime or magma cube
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Slime(ID.Entity? type = ID.Entity.slime) : base(type) { }

        /// <summary>
        /// The size of the slime
        /// </summary>
        [Data.DataTag]
        public int? Size { get; set; }
        /// <summary>
        /// True if the slime touches the ground
        /// </summary>
        [Data.DataTag]
        public bool? WasOnGround { get; set; }
    }
}
