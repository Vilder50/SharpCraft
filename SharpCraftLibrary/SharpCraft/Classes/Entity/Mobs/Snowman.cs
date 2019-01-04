using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for snowmen
        /// </summary>
        public class Snowman : BaseMob
        {
            /// <summary>
            /// Creates a new snowman
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Snowman(ID.Entity? type = ID.Entity.snow_golem) : base(type) { }

            /// <summary>
            /// True if the snowman has a pumpkin on
            /// </summary>
            [DataTag]
            public bool? Pumpkin { get; set; }

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
                    if (Pumpkin != null) { TempList.Add("Pumpkin:" + Pumpkin.ToMinecraftBool()); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
