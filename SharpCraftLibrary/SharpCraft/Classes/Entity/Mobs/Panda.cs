using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for pandas
        /// </summary>
        public class Panda : BaseTameable
        {
            /// <summary>
            /// Creates a new panda
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Panda(ID.Entity? type = ID.Entity.panda) : base(type) { }

            /// <summary>
            /// The type of the panda
            /// </summary>
            [Data.DataTag("MainGene", ForceType = ID.NBTTagType.TagString)]
            public ID.Panda? MainType { get; set; }

            /// <summary>
            /// The panda's hidden type which can be transfered to it's children
            /// </summary>
            [Data.DataTag("HiddenGene", ForceType = ID.NBTTagType.TagString)]
            public ID.Panda? HiddenType { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = TameDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (MainType != null) { TempList.Add("MainGene:" + MainType.ToString()); }
                    if (HiddenType != null) { TempList.Add("HiddenGene:" + HiddenType.ToString()); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
