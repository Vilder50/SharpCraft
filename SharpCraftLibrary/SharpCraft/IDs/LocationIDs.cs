namespace SharpCraft
{
    /// <summary>
    /// All the different ID's/Types/States things in the game can have
    /// </summary>
    public static partial class ID
    {
#pragma warning disable 1591
        public class Biome: NamespacedEnumLike<string>
        {
            public Biome(string value, BasePackNamespace? @namespace = null) : base(value, @namespace)
            {
            }

            public static readonly Biome basalt_deltas = new Biome("basalt_deltas");
            public static readonly Biome crimson_forest = new Biome("crimson_forest");
            public static readonly Biome warped_forest = new Biome("warped_forest");
            public static readonly Biome soul_sand_valley = new Biome("soul_sand_valley");
            public static readonly Biome nether_wastes = new Biome("nether_wastes");
            public static readonly Biome ocean = new Biome("ocean");
            public static readonly Biome deep_ocean = new Biome("deep_ocean");
            public static readonly Biome frozen_ocean = new Biome("frozen_ocean");
            public static readonly Biome deep_frozen_ocean = new Biome("deep_frozen_ocean");
            public static readonly Biome cold_ocean = new Biome("cold_ocean");
            public static readonly Biome deep_cold_ocean = new Biome("deep_cold_ocean");
            public static readonly Biome lukewarm_ocean = new Biome("lukewarm_ocean");
            public static readonly Biome deep_lukewarm_ocean = new Biome("deep_lukewarm_ocean");
            public static readonly Biome warm_ocean = new Biome("warm_ocean");
            public static readonly Biome deep_warm_ocean = new Biome("deep_warm_ocean");
            public static readonly Biome river = new Biome("river");
            public static readonly Biome frozen_river = new Biome("frozen_river");
            public static readonly Biome beach = new Biome("beach");
            public static readonly Biome stone_shore = new Biome("stone_shore");
            public static readonly Biome snowy_beach = new Biome("snowy_beach");
            public static readonly Biome forest = new Biome("forest");
            public static readonly Biome wooded_hills = new Biome("wooded_hills");
            public static readonly Biome flower_forest = new Biome("flower_forest");
            public static readonly Biome birch_forest = new Biome("birch_forest");
            public static readonly Biome birch_forest_hills = new Biome("birch_forest_hills");
            public static readonly Biome tall_birch_forest = new Biome("tall_birch_forest");
            public static readonly Biome tall_birch_hills = new Biome("tall_birch_hills");
            public static readonly Biome dark_forest = new Biome("dark_forest");
            public static readonly Biome dark_forest_hills = new Biome("dark_forest_hills");
            public static readonly Biome jungle = new Biome("jungle");
            public static readonly Biome jungle_hills = new Biome("jungle_hills");
            public static readonly Biome modified_jungle = new Biome("modified_jungle");
            public static readonly Biome jungle_edge = new Biome("jungle_edge");
            public static readonly Biome modified_jungle_edge = new Biome("modified_jungle_edge");
            public static readonly Biome bamboo_jungle = new Biome("bamboo_jungle");
            public static readonly Biome bamboo_jungle_hills = new Biome("bamboo_jungle_hills");
            public static readonly Biome taiga = new Biome("taiga");
            public static readonly Biome taiga_hills = new Biome("taiga_hills");
            public static readonly Biome taiga_mountains = new Biome("taiga_mountains");
            public static readonly Biome snowy_taiga = new Biome("snowy_taiga");
            public static readonly Biome snowy_taiga_hills = new Biome("snowy_taiga_hills");
            public static readonly Biome snowy_taiga_mountains = new Biome("snowy_taiga_mountains");
            public static readonly Biome giant_tree_taiga = new Biome("giant_tree_taiga");
            public static readonly Biome giant_tree_taiga_hills = new Biome("giant_tree_taiga_hills");
            public static readonly Biome giant_spruce_taiga = new Biome("giant_spruce_taiga");
            public static readonly Biome giant_spruce_taiga_hills = new Biome("giant_spruce_taiga_hills");
            public static readonly Biome mushroom_fields = new Biome("mushroom_fields");
            public static readonly Biome mushroom_field_shore = new Biome("mushroom_field_shore");
            public static readonly Biome swamp = new Biome("swamp");
            public static readonly Biome swamp_hills = new Biome("swamp_hills");
            public static readonly Biome savanna = new Biome("savanna");
            public static readonly Biome savanna_plateau = new Biome("savanna_plateau");
            public static readonly Biome shattered_savanna = new Biome("shattered_savanna");
            public static readonly Biome shattered_savanna_plateau = new Biome("shattered_savanna_plateau");
            public static readonly Biome plains = new Biome("plains");
            public static readonly Biome sunflower_plains = new Biome("sunflower_plains");
            public static readonly Biome desert = new Biome("desert");
            public static readonly Biome desert_hills = new Biome("desert_hills");
            public static readonly Biome desert_lakes = new Biome("desert_lakes");
            public static readonly Biome snowy_tundra = new Biome("snowy_tundra");
            public static readonly Biome snowy_mountains = new Biome("snowy_mountains");
            public static readonly Biome ice_spikes = new Biome("ice_spikes");
            public static readonly Biome mountains = new Biome("mountains");
            public static readonly Biome wooded_mountains = new Biome("wooded_mountains");
            public static readonly Biome gravelly_mountains = new Biome("gravelly_mountains");
            public static readonly Biome modified_gravelly_mountains = new Biome("modified_gravelly_mountains");
            public static readonly Biome mountain_edge = new Biome("mountain_edge");
            public static readonly Biome badlands = new Biome("badlands");
            public static readonly Biome badlands_plateau = new Biome("badlands_plateau");
            public static readonly Biome modified_badlands_plateau = new Biome("modified_badlands_plateau");
            public static readonly Biome wooded_badlands_plateau = new Biome("wooded_badlands_plateau");
            public static readonly Biome modified_wooded_badlands_plateau = new Biome("modified_wooded_badlands_plateau");
            public static readonly Biome eroded_badlands = new Biome("eroded_badlands");
            public static readonly Biome the_end = new Biome("the_end");
            public static readonly Biome small_end_islands = new Biome("small_end_islands");
            public static readonly Biome end_midlands = new Biome("end_midlands");
            public static readonly Biome end_highlands = new Biome("end_highlands");
            public static readonly Biome end_barrens = new Biome("end_barrens");
            public static readonly Biome the_void = new Biome("the_void");
        }
        public class Structure: NamespacedEnumLike<string>
        {
            public Structure(string value, BasePackNamespace? @namespace = null) : base(value, @namespace)
            {
            }

            public static readonly Structure Desert_pyramid = new Structure("desert_pyramid");
            public static readonly Structure EndCity = new Structure("endcity");
            public static readonly Structure Fortress = new Structure("fortress");
            public static readonly Structure Igloo = new Structure("igloo");
            public static readonly Structure Jungle_Pyramid = new Structure("jungle_pyramid");
            public static readonly Structure Mansion = new Structure("mansion");
            public static readonly Structure Mineshaft = new Structure("mineshaft");
            public static readonly Structure Monument = new Structure("monument");
            public static readonly Structure Stronghold = new Structure("stronghold");
            public static readonly Structure Swamp_Hut = new Structure("swamp_Hut");
            public static readonly Structure Village = new Structure("village");
            public static readonly Structure Buried_Treasure = new Structure("buried_Treasure");
            public static readonly Structure Ocean_Ruin = new Structure("ocean_Ruin");
            public static readonly Structure Shipwreck = new Structure("shipwreck");
            public static readonly Structure Nether_Fossil = new Structure("nether_Fossil");
            public static readonly Structure Bastion_Remnant = new Structure("bastion_Remnant");
            public static readonly Structure Ruined_Portal = new Structure("ruined_Portal");
        }

        /// <summary>
        /// Options for <see cref="DimensionObjects.VanillaNoiseGenerator"/>
        /// </summary>
        public class NoiseGeneratorSetting : NamespacedEnumLike<string>
        {
            public NoiseGeneratorSetting(string value, BasePackNamespace? @namespace = null) : base(value, @namespace)
            {
            }

            /// <summary>
            /// Normal overworld generation
            /// </summary>
            public static readonly NoiseGeneratorSetting overworld = new NoiseGeneratorSetting("overworld");
            /// <summary>
            /// Amplified generation
            /// </summary>
            public static readonly NoiseGeneratorSetting amplified = new NoiseGeneratorSetting("amplified");
            /// <summary>
            /// Normal nether generation
            /// </summary>
            public static readonly NoiseGeneratorSetting nether = new NoiseGeneratorSetting("nether");
            /// <summary>
            /// Nether generation but with overworld features
            /// </summary>
            public static readonly NoiseGeneratorSetting caves = new NoiseGeneratorSetting("caves");
            /// <summary>
            /// Generation used for generating the end island
            /// </summary>
            public static readonly NoiseGeneratorSetting end = new NoiseGeneratorSetting("end");
            /// <summary>
            /// End floating island generation
            /// </summary>
            public static readonly NoiseGeneratorSetting floating_islands = new NoiseGeneratorSetting("floating_islands");
        }
#pragma warning restore 1591
    }
}
