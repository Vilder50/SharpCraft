using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft.DimensionObjects
{
    /// <summary>
    /// Generates nether biomes
    /// </summary>
    public class NetherBiomeGenerator : BaseBiomeGenerator
    {
        /// <summary>
        /// Intializes a new <see cref="NetherBiomeGenerator"/>
        /// </summary>
        public NetherBiomeGenerator() : base("multi_noise")
        {
            Preset = "minecraft:nether";
        }

        /// <summary>
        /// Used for chosing nether generation
        /// </summary>
        [DataTag("preset", JsonTag = true)]
        public string Preset { get; private set; }
    }
}
