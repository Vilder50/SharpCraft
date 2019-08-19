using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// An object for effects
    /// </summary>
    public class Effect : Data.DataHolderBase
    {
        /// <summary>
        /// The duration of the effect (in ticks)
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
        public Time Duration { get; set; }

        /// <summary>
        /// The amplifier of the effect
        /// (0 = level 1)
        /// </summary>
        public byte? Amplifier { get; set; }

        /// <summary>
        /// The type of effect
        /// </summary>
        [Data.DataTag("Id", ForceType = ID.NBTTagType.TagByte)]
        public ID.Effect? Type { get; set; }

        /// <summary>
        /// If the effect should show particles or not
        /// </summary>
        [Data.DataTag]
        public bool? ShowParticles { get; set; }

        /// <summary>
        /// If the effect is an ambiant effect
        /// (comes from a beacon / conduit)
        /// </summary>
        [Data.DataTag]
        public bool? Ambiant { get; set; }

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
        public Effect(ID.Effect EffectType, int EffectDuration, byte EffectAmplifier, bool? ShowParticles = null)
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
            if (Duration != null) { TempList.Add("Duration:" + Duration.AsTicks()); }
            if (Amplifier != null) { TempList.Add("Amplifier:" + Amplifier + "b"); }
            if (Ambiant != null) { TempList.Add("Ambient:" + Ambiant); }
            if (ShowParticles != null) { TempList.Add("ShowParticles:" + ShowParticles); }

            return string.Join(",", TempList);

        }
    }
}
