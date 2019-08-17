using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// An object for llama spit entities
        /// </summary>
        public class LlamaSpit : EntityBasic
        {
            /// <summary>
            /// Creates new llama spit
            /// </summary>
            /// <param name="type">the type of entity</param>
            public LlamaSpit(ID.Entity? type = ID.Entity.llama_spit) : base(type) { }

            /// <summary>
            /// The owner of the spit
            /// </summary>
            [Data.CustomDataTag]
            public UUID OwnerUUID { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = BasicDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (OwnerUUID != null) { TempList.Add("Owner:{OwnerUUIDLeast:" + OwnerUUID.Least + "L,OwnerUUIDMost:" + OwnerUUID.Most + "L}"); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
