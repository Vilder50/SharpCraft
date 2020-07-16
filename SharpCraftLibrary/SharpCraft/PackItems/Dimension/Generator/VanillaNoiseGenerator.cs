using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft.DimensionObjects
{
    /// <summary>
    /// Generator which generates the a world with noise
    /// </summary>
    public class VanillaNoiseGenerator : BaseGenerator
    {
        private BaseBiomeGenerator biomeGenerator = null!;

        /// <summary>
        /// Intializes a new <see cref="VanillaNoiseGenerator"/>
        /// </summary>
        /// <param name="generator">The generator to use</param>
        /// <param name="seed">The seed used for generation</param>
        /// <param name="biomeGenerator">The generator used for generating biomes</param>
        public VanillaNoiseGenerator(ID.NoiseGeneratorSetting generator, int seed, BaseBiomeGenerator biomeGenerator) : base("noise")
        {
            Generator = generator;
            Seed = seed;
            BiomeGenerator = biomeGenerator;
        }

        /// <summary>
        /// The seed used for generation
        /// </summary>
        [DataTag("seed", JsonTag = true)]
        public int Seed { get; set; }

        /// <summary>
        /// The generator to use
        /// </summary>
        [DataTag("settings", ForceType = ID.NBTTagType.TagNamespacedString, JsonTag = true)]
        public ID.NoiseGeneratorSetting Generator { get; set; }

        /// <summary>
        /// The generator used for generating biomes
        /// </summary>
        [DataTag("biome_source", JsonTag = true)]
        public BaseBiomeGenerator BiomeGenerator { get => biomeGenerator; set => biomeGenerator = value ?? throw new ArgumentNullException(nameof(BiomeGenerator), "BiomeGenerator may not be null"); }
    }
}
