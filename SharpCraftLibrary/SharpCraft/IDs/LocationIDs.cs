namespace SharpCraft
{
    /// <summary>
    /// All the different ID's/Types/States things in the game can have
    /// </summary>
    public static partial class ID
    {
#pragma warning disable 1591
        public enum Biome
        {
            basalt_deltas,
            crimson_forest,
            warped_forest,
            soul_sand_valley,
            nether_wastes,
            ocean,
            deep_ocean,
            frozen_ocean,
            deep_frozen_ocean,
            cold_ocean,
            deep_cold_ocean,
            lukewarm_ocean,
            deep_lukewarm_ocean,
            warm_ocean,
            deep_warm_ocean,
            river,
            frozen_river,
            beach,
            stone_shore,
            snowy_beach,
            forest,
            wooded_hills,
            flower_forest,
            birch_forest,
            birch_forest_hills,
            tall_birch_forest,
            tall_birch_hills,
            dark_forest,
            dark_forest_hills,
            jungle,
            jungle_hills,
            modified_jungle,
            jungle_edge,
            modified_jungle_edge,
            bamboo_jungle,
            bamboo_jungle_hills,
            taiga,
            taiga_hills,
            taiga_mountains,
            snowy_taiga,
            snowy_taiga_hills,
            snowy_taiga_mountains,
            giant_tree_taiga,
            giant_tree_taiga_hills,
            giant_spruce_taiga,
            giant_spruce_taiga_hills,
            mushroom_fields,
            mushroom_field_shore,
            swamp,
            swamp_hills,
            savanna,
            savanna_plateau,
            shattered_savanna,
            shattered_savanna_plateau,
            plains,
            sunflower_plains,
            desert,
            desert_hills,
            desert_lakes,
            snowy_tundra,
            snowy_mountains,
            ice_spikes,
            mountains,
            wooded_mountains,
            gravelly_mountains,
            modified_gravelly_mountains,
            mountain_edge,
            badlands,
            badlands_plateau,
            modified_badlands_plateau,
            wooded_badlands_plateau,
            modified_wooded_badlands_plateau,
            eroded_badlands,
            the_end,
            small_end_islands,
            end_midlands,
            end_highlands,
            end_barrens,
            the_void,
            BiomeEnumEnd
        }
        public enum Structure
        {
            Desert_pyramid,
            EndCity,
            Fortress,
            Igloo,
            Jungle_Pyramid,
            Mansion,
            Mineshaft,
            Monument,
            Stronghold,
            Swamp_Hut,
            Village,
            Buried_Treasure,
            Ocean_Ruin,
            Shipwreck,
            Nether_Fossil,
            Bastion_Remnant,
            Ruined_Portal
        }
#pragma warning restore 1591

        /// <summary>
        /// Options for <see cref="DimensionObjects.VanillaNoiseGenerator"/>
        /// </summary>
        public enum NoiseGeneratorSetting
        {
            /// <summary>
            /// Normal overworld generation
            /// </summary>
            overworld,
            /// <summary>
            /// Amplified generation
            /// </summary>
            amplified,
            /// <summary>
            /// Normal nether generation
            /// </summary>
            nether,
            /// <summary>
            /// Nether generation but with overworld features
            /// </summary>
            caves,
            /// <summary>
            /// Generation used for generating the end island
            /// </summary>
            end,
            /// <summary>
            /// End floating island generation
            /// </summary>
            floating_islands
        }
    }
}
