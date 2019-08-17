using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for ghasts
        /// </summary>
        public class Ghast : BaseMob
        {
            /// <summary>
            /// Creates a new ghast
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Ghast(ID.Entity? type = ID.Entity.ghast) : base(type) { }

            /// <summary>
            /// The size of the explosion caused by the ghast's fireballs
            /// </summary>
            [Data.DataTag]
            public int? ExplosionPower { get; set; }

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
                    if (ExplosionPower != null) { TempList.Add("ExplosionPower:" + ExplosionPower); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
