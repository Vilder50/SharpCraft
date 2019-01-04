using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for iron golems
        /// </summary>
        public class IronGolem : BaseMob
        {
            /// <summary>
            /// Creates a new iron golem
            /// </summary>
            /// <param name="type">the type of entity</param>
            public IronGolem(ID.Entity? type = ID.Entity.iron_golem) : base(type) { }

            /// <summary>
            /// If true the golem wont attack players
            /// </summary>
            [DataTag]
            public bool? PlayerCreated { get; set; }

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
                    if (PlayerCreated != null) { TempList.Add("PlayerCreated:" + PlayerCreated); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
