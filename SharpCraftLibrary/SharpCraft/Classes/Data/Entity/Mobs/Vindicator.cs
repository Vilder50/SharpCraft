using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for vindicators
        /// </summary>
        public class Vindicator : BaseIllager
        {
            /// <summary>
            /// Creates a new vindicator
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Vindicator(ID.Entity? type = ID.Entity.vindicator) : base(type) { }

            /// <summary>
            /// If the vindicator is a Johnny vindicator (attacks everything)
            /// </summary>
            [Data.DataTag]
            public bool? Johnny { get; set; }
        }
    }
}
