using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// An object for firework parts
    /// </summary>
    public class Firework : Data.DataHolderBase
    {
        /// <summary>
        /// If the firework should flicker
        /// </summary>
        [Data.DataTag]
        public bool? Flicker { get; set; }

        /// <summary>
        /// if the firework explosion should have trails
        /// </summary>
        [Data.DataTag]
        public bool? Trail { get; set; }

        /// <summary>
        /// the type of the firework explosion
        /// </summary>
        [Data.DataTag(ForceType = ID.NBTTagType.TagByte)]
        public ID.Firework? Type { get; set; }

        /// <summary>
        /// the colors of the explosion
        /// </summary>
        [Data.DataTag]
        public RGBColor[] Colors { get; set; }

        /// <summary>
        /// the colors the explosion fades into
        /// </summary>
        [Data.DataTag]
        public RGBColor[] FadeColors { get; set; }
    }
}
