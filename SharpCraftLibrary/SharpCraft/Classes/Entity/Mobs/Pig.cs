using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for pigs
        /// </summary>
        public class Pig : BaseBreedable
        {
            /// <summary>
            /// Creates a new pigs
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Pig(ID.Entity? type = ID.Entity.pig) : base(type) { }

            /// <summary>
            /// If the pig has a saddle on
            /// </summary>
            [DataTag]
            public bool? Saddle { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = BreedDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (Saddle != null) { TempList.Add("Saddle:" + Saddle.ToMinecraftBool()); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
