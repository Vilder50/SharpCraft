using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// List of all entities
    /// </summary>
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for evokers and illusioners
        /// </summary>
        public class MagicIllager : BaseIllager
        {
            /// <summary>
            /// Creates a new evoker or illusioner
            /// </summary>
            /// <param name="type">the type of entity</param>
            public MagicIllager(ID.Entity? type = ID.Entity.evoker) : base(type) { }

            /// <summary>
            /// The time till the next spell is casted
            /// </summary>
            [DataTag]
            public Time SpellTicks { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = IllagerDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (SpellTicks != null) { TempList.Add("SpellTicks:" + SpellTicks.AsTicks()); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
