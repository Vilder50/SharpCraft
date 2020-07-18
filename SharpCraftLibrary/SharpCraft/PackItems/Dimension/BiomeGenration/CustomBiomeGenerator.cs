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
        public CustomBiomeGenerator(Biome[] biomes) : base("minecraft:multi_noise")
        {
            Biomes = biomes;
        }

        /// <summary>
        /// Information about the biomes to generate
        /// </summary>
        [DataTag("biomes", JsonTag = true)]
        public Biome[] Biomes { get => biomes; set => biomes = Validators.ValidateNoneNullArray(value, nameof(Biomes), nameof(CustomBiomeGenerator)); }

        /// <summary>
        /// Class for biome generation information
        /// </summary>
        public class Biome : DataHolderBase
        {
            private float altitude;
            private float weirdness;
            private float offset;
            private float temperature;
            private float humidity;

            /// <summary>
            /// Intializes a new <see cref="Biome"/>
            /// </summary>
            /// <param name="biomeType">The biome to generate</param>
            public Biome(ID.Biome biomeType)
            {
                BiomeType = biomeType;
            }

            /// <summary>
            /// The biome to generate
            /// </summary>
            [DataTag("biome", JsonTag = true)]
            public ID.Biome BiomeType { get; set; }

            /// <summary>
            /// Used for placing similar biomes together (game uses perlin noice with values from -1 to 1)
            /// </summary>
            [DataTag("parameters.altitude", JsonTag = true)]
            public float Altitude { get => altitude; set => altitude = (float)Validators.ValidateRange(value, -2.0, 2.0, nameof(Altitude), nameof(CustomBiomeGenerator)); }

            /// <summary>
            /// Used for placing similar biomes together (game uses perlin noice with values from -1 to 1)
            /// </summary>
            [DataTag("parameters.weirdness", JsonTag = true)]
            public float Weirdness { get => weirdness; set => weirdness = (float)Validators.ValidateRange(value, -2.0, 2.0, nameof(Weirdness), nameof(CustomBiomeGenerator)); }

            /// <summary>
            /// Used for chosing how big a biome is compared to others. small values = big. High values = small.
            /// </summary>
            [DataTag("parameters.offset", JsonTag = true)]
            public float Offset { get => offset; set => offset = (float)Validators.ValidateRange(value, 0, 1.0, nameof(Offset), nameof(CustomBiomeGenerator)); }

            /// <summary>
            /// Used for placing similar biomes together (game uses perlin noice with values from -1 to 1)
            /// </summary>
            [DataTag("parameters.temperature", JsonTag = true)]
            public float Temperature { get => temperature; set => temperature = (float)Validators.ValidateRange(value, -2.0, 2.0, nameof(Temperature), nameof(CustomBiomeGenerator)); }

            /// <summary>
            /// Used for placing similar biomes together (game uses perlin noice with values from -1 to 1)
            /// </summary>
            [DataTag("parameters.humidity", JsonTag = true)]
            public float Humidity { get => humidity; set => humidity = (float)Validators.ValidateRange(value, -2.0, 2.0, nameof(Humidity), nameof(CustomBiomeGenerator)); }
        }
    }
}
