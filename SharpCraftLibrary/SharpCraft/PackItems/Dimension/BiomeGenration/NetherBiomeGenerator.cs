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
            /// <summary>
            /// The biome to generate
            /// </summary>
            [DataTag("biome", ForceType = ID.NBTTagType.TagNamespacedString, JsonTag = true)]
            public ID.Biome BiomeType { get; set; }

            [DataTag("altitude", JsonTag = true)]
            public float Altitude { get; set; }

            [DataTag("weirdness", JsonTag = true)]
            public float Weirdness { get; set; }

            [DataTag("offset", JsonTag = true)]
            public float Offset { get; set; }

            /// <summary>
            /// Affects snow and rain. 0.15 = snow. 0.15 - 0.95 for rain. Temperature goes down 0.0016 per block above sea level.
            /// </summary>
            [DataTag("temperature", JsonTag = true)]
            public float Temperature { get; set; }

            [DataTag("humidity", JsonTag = true)]
            public float Humidity { get; set; }
        }
    }
}
