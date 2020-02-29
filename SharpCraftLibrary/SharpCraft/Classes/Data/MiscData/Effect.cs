using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// An object for effects
    /// </summary>
    public class Effect : Data.DataHolderBase
    {
        /// <summary>
        /// Creates an empty effect
        /// </summary>
        public Effect() { }

        /// <summary>
        /// Creates an effect with the specified parameters
        /// </summary>
        /// <param name="type">the type of effect</param>
        /// <param name="duration">the duration of the effect (in ticks)</param>
        /// <param name="amplifier">the amplifier of the effect (0 = level 1)</param>
        /// <param name="showParticles">if the effect should show particles or not</param>
        public Effect(ID.Effect type, Time? duration, sbyte? amplifier, bool? showParticles = null)
        {
            Duration = duration;
            Amplifier = amplifier;
            Type = type;
            ShowParticles = showParticles;
        }

        /// <summary>
        /// The duration of the effect (in ticks)
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagInt)]
        public Time? Duration { get; set; }

        /// <summary>
        /// The amplifier of the effect
        /// (0 = level 1)
        /// </summary>
        [Data.DataTag]
        public sbyte? Amplifier { get; set; }

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
        public bool? Ambient { get; set; }

        /// <summary>
        /// Replaces the other effect when it runs out. (Duration also decreases for this effect)
        /// </summary>
        [Data.DataTag]
        public Effect? HiddenEffect { get; set; }
    }
}
