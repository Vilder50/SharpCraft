using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for parrots
        /// </summary>
        public class Parrot : BaseTameable
        {
            /// <summary>
            /// Creates a new parrot
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Parrot(ID.Entity? type = ID.Entity.parrot) : base(type) { }

            /// <summary>
            /// How the parrot looks
            /// </summary>
            [DataTag]
            public ID.Parrot? ParrotType { get; set; }

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
                    if (ParrotType != null) { TempList.Add("Variant:" + (int)ParrotType); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
