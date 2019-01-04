using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for furnace minecarts
        /// </summary>
        public class MinecartFurnace : BaseMinecart
        {
            /// <summary>
            /// Creates a new furnace minecart
            /// </summary>
            /// <param name="type">the type of entity</param>
            public MinecartFurnace(ID.Entity? type = ID.Entity.furnace_minecart) : base(type) { }

            /// <summary>
            /// Force along the x axis
            /// </summary>
            [DataTag]
            public int? PushX { get; set; }
            /// <summary>
            /// Force along the y axis
            /// </summary>
            [DataTag]
            public int? PushZ { get; set; }
            /// <summary>
            /// The time till the furnace's fuel runs out
            /// </summary>
            [DataTag]
            public Time Fuel { get; set; }

            /// <summary>
            /// Gets the raw data from this entity
            /// </summary>
            public override string DataString
            {
                get
                {
                    List<string> TempList = new List<string>();

                    string NormalData = MinecartDataString;
                    if (NormalData.Length != 0) { TempList.Add(NormalData); }
                    if (PushX != null) { TempList.Add("PushX:" + PushX + "b"); }
                    if (PushZ != null) { TempList.Add("PushZ:" + PushZ + "b"); }
                    if (Fuel != null) { TempList.Add("Fuel:" + Fuel.AsTicks(Time.TimerType.Short) + "s"); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
