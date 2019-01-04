using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// An object for effects
    /// </summary>
    public class Effect
    {
        /// <summary>
        /// The duration of the effect (in ticks)
        /// </summary>
        public int? Duration;

        /// <summary>
        /// The amplifier of the effect
        /// (0 = level 1)
        /// </summary>
        public int? Amplifier;

        /// <summary>
        /// The type of effect
        /// </summary>
        public ID.Effect? Type;

        /// <summary>
        /// If the effect should show particles or not
        /// </summary>
        public bool? ShowParticles;

        /// <summary>
        /// If the effect is an ambiant effect
        /// (comes from a beacon / conduit)
        /// </summary>
        public bool? Ambiant;

        /// <summary>
        /// Creates an empty effect
        /// </summary>
        public Effect() { }

        /// <summary>
        /// Creates an effect with the specified parameters
        /// </summary>
        /// <param name="EffectType">the type of effect</param>
        /// <param name="EffectDuration">the duration of the effect (in ticks)</param>
        /// <param name="EffectAmplifier">the amplifier of the effect (0 = level 1)</param>
        /// <param name="ShowParticles">if the effect should show particles or not</param>
        public Effect(ID.Effect EffectType, int EffectDuration, int EffectAmplifier, bool? ShowParticles = null)
        {
            Duration = EffectDuration;
            Amplifier = EffectAmplifier;
            Type = EffectType;
            this.ShowParticles = ShowParticles;
        }

        /// <summary>
        /// Gets the raw effect data
        /// </summary>
        /// <returns>the raw effect data used by the game</returns>
        public override string ToString()
        {
            List<string> TempList = new List<string>();

            if (Type != null) { TempList.Add("Id:" + ((int)Type + 1) + "b"); }
            if (Duration != null) { TempList.Add("Duration:" + Duration); }
            if (Amplifier != null) { TempList.Add("Amplifier:" + Amplifier + "b"); }
            if (Ambiant != null) { TempList.Add("Ambient:" + Ambiant); }
            if (ShowParticles != null) { TempList.Add("ShowParticles:" + ShowParticles); }

            return string.Join(",", TempList);

        }
    }
}
