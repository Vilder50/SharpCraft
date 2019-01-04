using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for wolves
        /// </summary>
        public class Wolf : BaseTameable
        {
            /// <summary>
            /// Creates a new wolf
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Wolf(ID.Entity? type = ID.Entity.wolf) : base(type) { }

            /// <summary>
            /// The color of the wolf's collar
            /// </summary>
            [DataTag]
            public ID.Color? Color { get; set; }
            /// <summary>
            /// If the wolf is angry
            /// </summary>
            [DataTag]
            public bool? Angry { get; set; }

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
                    if (Color != null) { TempList.Add("CollarColor:" + (int)Color + "b"); }
                    if (Angry != null) { TempList.Add("Angry:" + Angry.ToMinecraftBool()); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
