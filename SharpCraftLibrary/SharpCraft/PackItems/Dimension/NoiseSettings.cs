using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft.DimensionObjects
{
    /// <summary>
    /// Setting used for setting a part of a noise curve... or something...
    /// </summary>
    public class NoiseCurveSetting : DataHolderBase
    {
        /// <summary>
        /// Intializes a new <see cref="NoiseCurveSetting"/>
        /// </summary>
        /// <param name="target">How "smooth" it should be. Negative numbers = perfect for the bottom of islands and mountain tops. Positive numbers = perfect for ground and cave roofs.</param>
        /// <param name="size">The size of the effected area.</param>
        /// <param name="offset">Offsets the effected area. positive numbers when used for bottom part moves the thing up while positive numbers in top part moves down.</param>
        public NoiseCurveSetting(int target, int size, int offset)
        {
            Target = target;
            Size = size;
            Offset = offset;
        }

        /// <summary>
        /// How "smooth" it should be. Negative numbers = perfect for the bottom of islands and mountain tops. Positive numbers = perfect for ground and cave roofs.
        /// </summary>
        [DataTag("target", JsonTag = true)]
        public int Target { get; set; }

        /// <summary>
        /// The size of the effected area.
        /// </summary>
        [DataTag("size", JsonTag = true)]
        public int Size { get; set; }

        /// <summary>
        /// Offsets the effected area. positive numbers when used for bottom part moves the thing up while positive numbers in top part moves down.
        /// </summary>
        [DataTag("offset", JsonTag = true)]
        public int Offset { get; set; }
    }

    /// <summary>
    /// Settings used for scaling noise
    /// </summary>
    public class NoiseSamplingSetting : DataHolderBase
    {
        private double yScale;
        private double yFactor;
        private double xZFactor;
        private double xZScale;

        /// <summary>
        /// Scales the noise on the horizontal axis
        /// </summary>
        [DataTag("xz_scale", JsonTag = true)]
        public double XZScale { get => xZScale; set => xZScale = Utils.ValidateRange(value, 0.001, 1000, nameof(XZScale), nameof(NoiseSamplingSetting)); }

        /// <summary>
        /// Smoothes the noise on the horizontal axis
        /// </summary>
        [DataTag("xz_factor", JsonTag = true)]
        public double XZFactor { get => xZFactor; set => xZFactor = Utils.ValidateRange(value, 0.001, 1000, nameof(XZFactor), nameof(NoiseSamplingSetting)); }

        /// <summary>
        /// Scales the noise on the vertical axis
        /// </summary>
        [DataTag("y_scale", JsonTag = true)]
        public double YScale { get => yScale; set => yScale = Utils.ValidateRange(value, 0.001, 1000, nameof(YScale), nameof(NoiseSamplingSetting)); }

        /// <summary>
        /// Smoothes the noise on the vertical axis
        /// </summary>
        [DataTag("y_factor", JsonTag = true)]
        public double YFactor { get => yFactor; set => yFactor = Utils.ValidateRange(value, 0.001, 1000, nameof(YFactor), nameof(NoiseSamplingSetting)); }
    }
}
