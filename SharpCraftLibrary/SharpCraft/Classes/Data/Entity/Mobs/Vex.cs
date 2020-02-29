using System.Collections.Generic;

namespace SharpCraft.Entities
{
    /// <summary>
    /// Entity data for vexes
    /// </summary>
    public class Vex : Mob
    {
        /// <summary>
        /// Creates a new vex
        /// </summary>
        /// <param name="type">the type of entity</param>
        public Vex(ID.Entity? type = ID.Entity.vex) : base(type) { }

        /// <summary>
        /// The time till the vex dissapears
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
        public Time? LifeTicks { get; set; }
        /// <summary>
        /// The location the vex should fly around in
        /// (It flies to random location in a 15x11x15 around this spot)
        /// </summary>
        [Data.DataTag((object)"BoundX", "BoundY", "BoundZ", Merge = true)]
        public IntVector? Bound { get; set; }
    }
}
