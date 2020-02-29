using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for zombie pigmen
        /// </summary>
        public class ZombiePigman : Zombie
        {
            /// <summary>
            /// Creates a new zombie pigman
            /// </summary>
            /// <param name="type">the type of entity</param>
            public ZombiePigman(ID.Entity? type = ID.Entity.zombie_pigman) : base(type) { }

            /// <summary>
            /// The time till the zombie pigman stops being angry (0 or smaller if not angry)
            /// </summary>
            [Data.DataTag(ForceType = ID.NBTTagType.TagShort)]
            public Time? Anger { get; set; }

            /// <summary>
            /// The <see cref="UUID"/> of the entity this pigman is angry on
            /// </summary>
            [Data.DataTag("HurtBy",ForceType = ID.NBTTagType.TagString)]
            public UUID? AngryOn { get; set; }
        }
    }
}
