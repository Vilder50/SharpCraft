namespace SharpCraft
{
    /// <summary>
    /// All the different ID's/Types/States things in the game can have
    /// </summary>
    public static partial class ID
    {
#pragma warning disable 1591
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
