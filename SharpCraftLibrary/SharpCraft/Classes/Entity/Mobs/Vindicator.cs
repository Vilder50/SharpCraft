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
                    if (Johnny != null) { TempList.Add("Johnny:" + Johnny.ToMinecraftBool()); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
