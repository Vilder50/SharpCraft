using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft.DimensionObjects
{
    /// <summary>
    /// Generates vanilla biomes
    /// </summary>
    public class VanillaBiomeGenerator : BaseBiomeGenerator
    {
        /// <summary>
        /// Intializes a new <see cref="VanillaBiomeGenerator"/>
        /// </summary>
        public VanillaBiomeGenerator() : base("minecraft:vanilla_layered")
        {

        }

        /// <summary>
        /// If the biomes should be large
        /// </summary>
        [DataTag("large_biomes", JsonTag = true)]
        public bool LargeBiomes { get; set; }

        [DataTag("legacy_biome_init_layer", JsonTag = true)]
        public bool LegacyBiomes { get; set; }
    }
}
