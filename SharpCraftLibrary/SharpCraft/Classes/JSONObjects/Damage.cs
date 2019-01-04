using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class JSONObjects
    {
        /// <summary>
        /// a <see cref="object"/> defining damage
        /// </summary>
        public class Damage
        {
            /// <summary>
            /// The amount of damage actually dealt
            /// </summary>
            public Range Dealt;

            /// <summary>
            /// The amount of damage taken
            /// </summary>
            public Range Taken;

            /// <summary>
            /// if the damage was blocked or not
            /// </summary>
            public bool? Blocked;

            /// <summary>
            /// the type of damage
            /// </summary>
            public DamageFlags Type;

            /// <summary>
            /// Outputs this <see cref="Damage"/> data in string format
            /// </summary>
            /// <returns>this <see cref="Damage"/>'s data</returns>
            public override string ToString()
            {
                List<string> TempList = new List<string>();

                if (Dealt != null) { TempList.Add(Dealt.JSONString("dealt")); }
                if (Taken != null) { TempList.Add(Taken.JSONString("taken")); }
                if (Blocked != null) { TempList.Add("\"blocked\":" + Blocked); }
                if (Type != null) { TempList.Add("\"type\":" + Type); }

                return "{" + string.Join(",", TempList) + "}";
            }
        }
    }
}
