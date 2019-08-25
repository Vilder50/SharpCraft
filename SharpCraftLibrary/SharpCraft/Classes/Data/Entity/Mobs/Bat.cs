using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for bats
        /// </summary>
        public class Bat : BaseMob
        {
            /// <summary>
            /// Creates a new bat
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Bat(ID.Entity? type = ID.Entity.bat) : base(type) { }
            /// <summary>
            /// True when flying. False when hanging
            /// </summary>
            [Data.DataTag]
            public bool? BatFlags { get; set; }
        }
    }
}
