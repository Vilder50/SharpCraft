using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for striders
        /// </summary>
        public class Strider : BaseBreedable
        {
            /// <summary>
            /// Creates a new strider
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Strider(ID.Entity? type = ID.Entity.strider) : base(type) { }

            /// <summary>
            /// If the strider has a saddle on
            /// </summary>
            [Data.DataTag]
            public bool? Saddle { get; set; }
        }
    }
}
