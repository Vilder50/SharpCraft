using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft.DimensionObjects
{
    /// <summary>
    /// Base class for biome generators
    /// </summary>
    public abstract class BaseBiomeGenerator : DataHolderBase
    {
        /// <summary>
        /// Intializes a new biome generator
        /// </summary>
        /// <param name="generatorType">The type of generator</param>
        public BaseBiomeGenerator(string generatorType)
        {
            GeneratorType = generatorType;
        }

        /// <summary>
        /// The type of biome generator
        /// </summary>
        [DataTag("type", ForceType = ID.NBTTagType.TagNamespacedString, JsonTag = true)]
        public string GeneratorType { get; private set; }

        /// <summary>
        /// The seed used for generation the biomes
        /// </summary>
        [DataTag("seed", JsonTag = true)]
        public int Seed { get; set; }
    }
}
