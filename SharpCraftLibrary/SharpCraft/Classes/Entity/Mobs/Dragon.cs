using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for ender dragons
        /// </summary>
        public class Dragon : BaseMob
        {
            /// <summary>
            /// Creates a new ender dragon
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Dragon(ID.Entity? type = ID.Entity.ender_dragon) : base(type) { }

            /// <summary>
            /// The phase the ender dragon is in
            /// </summary>
            [DataTag]
            public ID.DragonPhase? Phase { get; set; }
            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = MobDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (Phase != null) { TempList.Add("Phase:" + (int)Phase); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
