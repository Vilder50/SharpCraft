using System;
using System.Collections.Generic;
using System.Text;
using SharpCraft.Data;

namespace SharpCraft.DimensionObjects
{
    /// <summary>
    /// Generator which generates the a world with noise
    /// </summary>
    public class CustomNoiseGenerator : BaseGenerator
    {
        private BaseBiomeGenerator biomeGenerator = null!;
        private NoiseCurveSetting topNoise = null!;
        private NoiseCurveSetting bottomNoise = null!;
        private StructureList structures = null!;
        private NoiseSamplingSetting sampling = null!;
        private Block defaultFluid = null!;
        private Block defaultBlock = null!;
        private int sizeVertical;
        private int sizeHorizontal;
        private double densityOffset;

        /// <summary>
        /// Intializes a new <see cref="CustomNoiseGenerator"/>
        /// </summary>
        public CustomNoiseGenerator() : base("noise")
        {

        }

        /// <summary>
        /// The seed used for generation
        /// </summary>
        [DataTag("seed", JsonTag = true)]
        public int Seed { get; set; }

        /// <summary>
        /// The position of the bedrock roof (0-255). 0 = layer 255. A number outside the range results in no roof.
        /// </summary>
        [DataTag("settings.bedrock_roof_position", JsonTag = true)]
        public int BedrockRoofPosition { get; set; }

        /// <summary>
        /// The position of the bedrock floor (0-255). 0 = layer 0. A number outside the range results in no floor
        /// </summary>
        [DataTag("settings.bedrock_floor_position", JsonTag = true)]
        public int BedrockFloorPosition { get; set; }

        /// <summary>
        /// Stops mobs from spawning when the world is being generated
        /// </summary>
        [DataTag("settings.disable_mob_generation", JsonTag = true)]
        public bool DisableMobGeneration { get; set; }

        /// <summary>
        /// The level of the sea (0-255).
        /// </summary>
        [DataTag("settings.sea_level", JsonTag = true)]
        public int SeaLevel { get; set; }

        /// <summary>
        /// The structures which can generate
        /// </summary>
        [DataTag("settings.structures.structures", JsonTag = true, ForceWriteEmptyCompoundTag = true)]
        public StructureList Structures { get => structures; set => structures = value ?? throw new ArgumentNullException(nameof(Structures), "Structures may not be null"); }

        /// <summary>
        /// Sets the noise used for the top part of the world
        /// </summary>
        [DataTag("settings.noise.top_slide", JsonTag = true)]
        public NoiseCurveSetting TopNoise { get => topNoise; set => topNoise = value ?? throw new ArgumentNullException(nameof(TopNoise), "TopNoise may not be null"); }

        /// <summary>
        /// Sets the noise used for the bottom part of the world
        /// </summary>
        [DataTag("settings.noise.bottom_slide", JsonTag = true)]
        public NoiseCurveSetting BottomNoise { get => bottomNoise; set => bottomNoise = value ?? throw new ArgumentNullException(nameof(BottomNoise), "BottomNoise may not be null"); }

        /// <summary>
        /// Changes the noise
        /// </summary>
        [DataTag("settings.noise.sampling", JsonTag = true)]
        public NoiseSamplingSetting Sampling { get => sampling; set => sampling = value ?? throw new ArgumentNullException(nameof(Sampling), "Sampling may not be null"); }

        /// <summary>
        /// Changes the x/z scale of the landmass
        /// </summary>
        [DataTag("settings.noise.size_horizontal", JsonTag = true)]
        public int SizeHorizontal { get => sizeHorizontal; set => sizeHorizontal = Validators.ValidateRange(value, 1, 4, nameof(sizeHorizontal), nameof(CustomNoiseGenerator)); }

        /// <summary>
        /// The y scale of the landmass.
        /// </summary>
        [DataTag("settings.noise.size_vertical", JsonTag = true)]
        public int SizeVertical { get => sizeVertical; set => sizeVertical = Validators.ValidateRange(value, 1, 4, nameof(SizeVertical), nameof(CustomNoiseGenerator)); }

        /// <summary>
        /// Squashes the world so it fits a specific height. (eg a world which normally is 64 blocks high with a SquashHeight of 128 makes the world 32 blocks high)
        /// </summary>
        [DataTag("settings.noise.height", JsonTag = true)]
        public int SquashHeight { get; set; }

        [DataTag("settings.noise.density_factor", JsonTag = true)]
        public double DensityFactor { get; set; }

        /// <summary>
        /// Changes the average of land level. (-1 to 1)
        /// </summary>
        [DataTag("settings.noise.density_offset", JsonTag = true)]
        public double DensityOffset { get => densityOffset; set => densityOffset = Validators.ValidateRange(value, -1, 1, nameof(DensityFactor), nameof(CustomNoiseGenerator)); }

        [DataTag("settings.noise.random_density_offset", JsonTag = true)]
        public bool? RandomDensityOffset { get; set; }

        [DataTag("settings.noise.simplex_surface_noise", JsonTag = true)]
        public bool SimplexSurfaceNoise { get; set; }

        /// <summary>
        /// Causes the world to generate like the end
        /// </summary>
        [DataTag("settings.noise.island_noise_override", JsonTag = true)]
        public bool? IslandNoiseOverride { get; set; }

        /// <summary>
        /// Generates amplified terrain
        /// </summary>
        [DataTag("settings.noise.amplified", JsonTag = true)]
        public bool? Amplified { get; set; }

        /// <summary>
        /// The default block the world is made out of
        /// </summary>
        [DataTag("settings.default_block", "Name", "Properties", true, JsonTag = true)]
        public Block DefaultBlock { get => defaultBlock; set => defaultBlock = value ?? throw new ArgumentNullException(nameof(DefaultBlock), "DefaultBlock may not be null"); }

        /// <summary>
        /// The default block the default fluid is made out of
        /// </summary>
        [DataTag("settings.default_fluid", "Name", "Properties", true, JsonTag = true)]
        public Block DefaultFluid { get => defaultFluid; set => defaultFluid = value ?? throw new ArgumentNullException(nameof(DefaultFluid), "DefaultFluid may not be null"); }

        /// <summary>
        /// The generator used for generating biomes
        /// </summary>
        [DataTag("biome_source", JsonTag = true)]
        public BaseBiomeGenerator BiomeGenerator { get => biomeGenerator; set => biomeGenerator = value ?? throw new ArgumentNullException(nameof(BiomeGenerator), "BiomeGenerator may not be null"); }

        /// <summary>
        /// The distance between the strongholds (This either effects how many strongholds there is in each circle or how much space there is between the circles)
        /// </summary>
        [DataTag("structures.stronghold.distance", JsonTag = true)]
        public int StrongholdDistance { get; set; }

        /// <summary>
        /// The amount of strongholds
        /// </summary>
        [DataTag("structures.stronghold.distance", JsonTag = true)]
        public int StrongholdCount { get; set; }

        /// <summary>
        /// More distance between the strongholds (This either effects how many strongholds there is in each circle or how much space there is between the circles)
        /// </summary>
        [DataTag("structures.stronghold.distance", JsonTag = true)]
        public int StrongholdSpread { get; set; }
    }
}
