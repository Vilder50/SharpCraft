using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class Entity
    {
        /// <summary>
        /// Entity data for vexes
        /// </summary>
        public class Vex : BaseMob
        {
            /// <summary>
            /// Creates a new vex
            /// </summary>
            /// <param name="type">the type of entity</param>
            public Vex(ID.Entity? type = ID.Entity.vex) : base(type) { }

            /// <summary>
            /// The time till the vex dissapears
            /// </summary>
            [DataTag]
            public Time LifeTicks { get; set; }
            /// <summary>
            /// The location the vex should fly around in
            /// (It flies to random location in a 15x11x15 around this spot)
            /// </summary>
            [DataTag]
            public Coords Bound { get; set; }

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
                    if (LifeTicks != null) { TempList.Add("LifeTicks:" + LifeTicks.AsTicks()); }
                    if (Bound != null) { TempList.Add("BoundX:" + (int)Bound.X + ",BoundY:" + (int)Bound.Y + ",BoundZ:" + (int)Bound.Z); }

                    return string.Join(",", TempList);
                }
            }
        }
    }
}
