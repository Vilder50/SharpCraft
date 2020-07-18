using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft.DimensionObjects
{
    /// <summary>
    /// Generates a single biome
    /// </summary>
    public class SingleBiomeGenerator : BaseBiomeGenerator
    {
        /// <summary>
        /// Intializes a new <see cref="SingleBiomeGenerator"/>
        /// </summary>
        /// <param name="biome">The biome to generate</param>
        public SingleBiomeGenerator(ID.Biome biome) : base("minecraft:fixed")
        {
            Biome = biome;
        }

        /// <summary>
        /// The biome to generate
        /// </summary>
        [DataTag("biome", JsonTag = true)]
        public ID.Biome Biome { get; private set; }
    }
}