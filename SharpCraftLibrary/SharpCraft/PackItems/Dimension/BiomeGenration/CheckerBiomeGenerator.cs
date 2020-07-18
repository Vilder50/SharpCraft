using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft.DimensionObjects
{
    /// <summary>
    /// Generates biomes in a checker pattern
    /// </summary>
    public class CheckerBiomeGenerator : BaseBiomeGenerator
    {
        /// <summary>
        /// Intializes a new <see cref="CheckerBiomeGenerator"/>
        /// </summary>
        /// <param name="biomes">The biomes to generate</param>
        /// <param name="scale">The scale of the biome squares</param>
        public CheckerBiomeGenerator(ID.Biome[] biomes, int scale) : base("minecraft:fixed")
        {
            Biomes = biomes;
            Scale = scale;
        }

        /// <summary>
        /// The biomes to generate
        /// </summary>
        [DataTag("biome", JsonTag = true)]
        public ID.Biome[] Biomes { get; private set; }

        /// <summary>
        /// The scale of the biome squares
        /// </summary>
        [DataTag("scale", JsonTag = true)]
        public int Scale { get; private set; }
    }
}
