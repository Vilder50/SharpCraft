using System.Collections.Generic;

namespace SharpCraft
{
    public static partial class JSONObjects
    {
        /// <summary>
        /// a <see cref="object"/> defining an effect
        /// </summary>
        public class Effect
        {
            /// <summary>
            /// creates a new <see cref="Effect"/>
            /// </summary>
            /// <param name="Effect">the effect to look for</param>
            public Effect(ID.Effect Effect)
            {
                EffectName = Effect;
            }

            private readonly ID.Effect? EffectName;

            /// <summary>
            /// the <see cref="Effect"/>'s amplifer
            /// </summary>
            public Range Amplifier;

            /// <summary>
            /// the <see cref="Effect"/>'s duration
            /// </summary>
            public Range Duration;

            /// <summary>
            /// if <see cref="Effect"/> is ambient or not
            /// </summary>
            public bool? Ambient;

            /// <summary>
            /// if <see cref="Effect"/>'s particles are visible or not
            /// </summary>
            public bool? Visible;

            /// <summary>
            /// Outputs this <see cref="Effect"/> data in string format
            /// </summary>
            /// <returns>this <see cref="Effect"/>'s data</returns>
            public override string ToString()
            {
                List<string> TempList = new List<string>();

                if (Amplifier != null) { TempList.Add(Amplifier.JSONString("amplifier")); }
                if (Duration != null) { TempList.Add(Duration.JSONString("duration")); }
                if (Ambient != null) { TempList.Add("ambient:" + Ambient); }
                if (Visible != null) { TempList.Add("visible:" + Visible); }

                return "\"" + EffectName + "\":{" + string.Join(",", TempList) + "}";
            }
        }
    }
}
