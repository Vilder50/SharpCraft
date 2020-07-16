using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft.DimensionObjects
{
    /// <summary>
    /// Generates biomes in a custom way
    /// </summary>
    public class CustomBiomeGenerator : BaseBiomeGenerator
    {
        private Biome[] biomes = null!;

        /// <summary>
        /// Intializes a new <see cref="CustomBiomeGenerator"/>
        /// </summary>
        /// <param name="biomes">Information about the biomes to generate</param>
        public CustomBiomeGenerator(Biome[] biomes) : base("multi_noise")
        {
            Biomes = biomes;
        }

        /// <summary>
        /// Information about the biomes to generate
        /// </summary>
        public Biome[] Biomes { get => biomes; set => biomes = Utils.ValidateNoneNullArray(value, nameof(Biomes), nameof(CustomBiomeGenerator)); }

        /// <summary>
        /// Class for biome generation information
        /// </summary>
        public class Biome : DataHolderBase
        {
            private float altitude;
            private float weirdness;
            private float offset;

            /// <summary>
            /// The biome to generate
            /// </summary>
            [DataTag("biome", ForceType = ID.NBTTagType.TagNamespacedString, JsonTag = true)]
            public ID.Biome BiomeType { get; set; }

            /// <summary>
            /// Used for placing similar biomes together
            /// </summary>
            [DataTag("parameters.altitude", JsonTag = true)]
            public float Altitude { get => altitude; set => altitude = (float)Utils.ValidateRange(value, -2.0, 2.0, nameof(Altitude), nameof(CustomBiomeGenerator)); }

            /// <summary>
            /// Used for placing similar biomes together
            /// </summary>
            [DataTag("parameters.weirdness", JsonTag = true)]
            public float Weirdness { get => weirdness; set => weirdness = (float)Utils.ValidateRange(value, -2.0, 2.0, nameof(Weirdness), nameof(CustomBiomeGenerator)); }

            /// <summary>
            /// Used for placing similar biomes together
            /// </summary>
            [DataTag("parameters.offset", JsonTag = true)]
            public float Offset { get => offset; set => offset = (float)Utils.ValidateRange(value, 0, 1.0, nameof(Offset), nameof(CustomBiomeGenerator)); }

            /// <summary>
            /// Used for placing similar biomes together
            /// </summary>
            [DataTag("parameters.temperature", JsonTag = true)]
            public float Temperature { get; set; }

            /// <summary>
            /// Used for placing similar biomes together
            /// </summary>
            [DataTag("parameters.humidity", JsonTag = true)]
            public float Humidity { get; set; }
        }
    }
}
