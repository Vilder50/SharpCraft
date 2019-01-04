using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for ocelots
        /// </summary>
        public class Ocelot : BaseMob
        {
            /// <summary>
            /// Creates a new ocelot
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Ocelot(ID.Entity? type = ID.Entity.ocelot) : base(type) { }

            /// <summary>
            /// If the ocelot trusts the player
            /// </summary>
            [DataTag]
            public bool? Trust { get; set; }

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
                    if (Trust != null) { TempList.Add("Trusting:" + Trust.ToMinecraftBool()); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
