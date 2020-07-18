using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft.DimensionObjects
{
    /// <summary>
    /// Generator which generates the a world with noise
    /// </summary>
    public class SuperflatGenerator : BaseGenerator
    {
        private Layer[] layers = null!;
        private StructureList structures = null!;

        /// <summary>
        /// Intializes a new <see cref="SuperflatGenerator"/>
        /// </summary>
        /// <param name="biome">The biome to use</param>
        /// <param name="layers">The layers to generate</param>
        /// <param name="structures">The structures which can spawn</param>
        public SuperflatGenerator(Layer[] layers, ID.Biome biome, StructureList structures) : base("flat")
        {
            Layers = layers;
            Biome = biome;
            Structures = structures;
        }

        /// <summary>
        /// The layers to generate
        /// </summary>
        [DataTag("settings.layers", JsonTag = true)]
        public Layer[] Layers { get => layers; set => layers = Validators.ValidateNoneNullArray(value, nameof(Layers), nameof(SuperflatGenerator)); }

        /// <summary>
        /// The biome to use
        /// </summary>
        [DataTag("settings.biome", JsonTag = true)]
        public ID.Biome Biome { get; set; }

        /// <summary>
        /// The structures which can spawn
        /// </summary>
        [DataTag("settings.structures", JsonTag = true, ForceWriteEmptyCompoundTag = true)]
        public StructureList Structures { get => structures; set => structures = value ?? throw new ArgumentNullException(nameof(Structures), "Structures may not be null"); }

        /// <summary>
        /// Class for superflat layers
        /// </summary>
        public class Layer : DataHolderBase
        {
            /// <summary>
            /// Intializes a new <see cref="Layer"/>
            /// </summary>
            /// <param name="height">The height of the layer</param>
            /// <param name="block">The block the layer is made out of</param>
            public Layer(int height, ID.Block block) : base()
            {
                Height = height;
                Block = block;
            }

            /// <summary>
            /// The height of the layer
            /// </summary>
            [DataTag("height", JsonTag = true)]
            public int Height { get; set; }

            /// <summary>
            /// The block the layer is made out of
            /// </summary>
            [DataTag("block", ForceType = ID.NBTTagType.TagString,  JsonTag = true)]
            public ID.Block Block { get; set; }
        }
    }
}
