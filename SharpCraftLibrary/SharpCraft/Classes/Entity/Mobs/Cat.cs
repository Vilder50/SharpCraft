using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for cats
        /// </summary>
        public class Cat : BaseTameable
        {
            /// <summary>
            /// Creates a new cat
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Cat(ID.Entity? type = ID.Entity.cat) : base(type) { }

            /// <summary>
            /// The cat's skin
            /// </summary>
            [DataTag]
            public ID.Cat? CatType { get; set; }
            /// <summary>
            /// The color of the cat's collar
            /// </summary>
            [DataTag]
            public ID.Color? CollarColor { get; set; }

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
                    if (CatType != null) { TempList.Add("CatType:" + (int)CatType); }
                    if (CollarColor != null) { TempList.Add("CollarColor:" + (int)CollarColor); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
