//
//This file was generated by SharpCraft.Generator.
//Do not make changes directly to this file. Change the template file instead.
//

namespace SharpCraft
{
    /// <summary>
    /// All the different ID's/Types/States things in the game can have
    /// </summary>
    public static partial class ID
    {
		#pragma warning disable 1591
        public class VillagerType: NamespacedEnumLike<string>
        {
            public VillagerType(string value, BasePackNamespace? @namespace = null) : base(value, @namespace)
            {
            }

            public static readonly VillagerType desert = new VillagerType("desert");
            public static readonly VillagerType jungle = new VillagerType("jungle");
            public static readonly VillagerType plains = new VillagerType("plains");
            public static readonly VillagerType savanna = new VillagerType("savanna");
            public static readonly VillagerType snow = new VillagerType("snow");
            public static readonly VillagerType swamp = new VillagerType("swamp");
            public static readonly VillagerType taiga = new VillagerType("taiga");

        }
    }
}