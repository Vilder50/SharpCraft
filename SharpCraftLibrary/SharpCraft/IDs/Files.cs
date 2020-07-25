using SharpCraft.Data;
#pragma warning disable 1591

namespace SharpCraft
{
    public partial class ID
    {
        /// <summary>
        /// File names used by Minecraft
        /// </summary>
        public static partial class Files
        {
            /// <summary>
            /// Loot table file names used by Minecraft which then can be used in / as a <see cref="LootTable"/>
            /// </summary>
            public static class LootTables
            {
                /// <summary>
                /// Outputs the file name for the given block's loot table
                /// </summary>
                /// <param name="block">The block to get the file name of</param>
                /// <returns>A loot table file name</returns>
                public static string Block(ID.Block block)
                {
                    return "blocks/" + block.Value;
                }

                /// <summary>
                /// Outputs the file name for the given entity's loot table
                /// </summary>
                /// <param name="entity">The entity to get the file name of</param>
                /// <returns>A loot table file name</returns>
                public static string Entity(ID.Entity entity)
                {
                    return "entities/" + entity.Value;
                }

                /// <summary>
                /// Outputs the file name for the given sheep color's loot table
                /// </summary>
                /// <param name="color">The sheep color to get the file name of</param>
                /// <returns>A loot table file name</returns>
                public static string EntitySheep(Color color)
                {
                    return "entities/sheep/" + color;
                }

                /// <summary>
                /// Loot tables Minecraft uses for fishing
                /// </summary>
                public static class Fishing
                {
                    /// <summary>
                    /// The loot table called when the player cathes a fish
                    /// </summary>
                    public static string BaseFishing
                    {
                        get
                        {
                            return "gameplay/fishing";
                        }
                    }

                    /// <summary>
                    /// A loot table containing fishs
                    /// (this loot table is normally called by the <see cref="BaseFishing"/> loot table and the normal guardian loot table)
                    /// </summary>
                    public static string Fish
                    {
                        get
                        {
                            return "gameplay/fishing/fish";
                        }
                    }

                    /// <summary>
                    /// A loot table containing junk
                    /// (this loot table is normally called by the <see cref="BaseFishing"/> loot table)
                    /// </summary>
                    public static string Junk
                    {
                        get
                        {
                            return "gameplay/fishing/junk";
                        }
                    }

                    /// <summary>
                    /// A loot table containing treasure
                    /// (this loot table is normally called by the <see cref="BaseFishing"/> loot table)
                    /// </summary>
                    public static string Treasure
                    {
                        get
                        {
                            return "gameplay/fishing/treasure";
                        }
                    }
                }

                /// <summary>
                /// Gifts given by villagers from hero of the village
                /// </summary>
                public static class VillagerGifts
                {
                    public static string ArmorerGift { get { return "gameplay/hero_of_the_village/armorer_gift"; } }
                    public static string ButcherGift { get { return "gameplay/hero_of_the_village/butcher_gift"; } }
                    public static string CartographerGift { get { return "gameplay/hero_of_the_village/cartographer_gift"; } }
                    public static string ClericGift { get { return "gameplay/hero_of_the_village/cleric_gift"; } }
                    public static string FarmerGift { get { return "gameplay/hero_of_the_village/farmer_gift"; } }
                    public static string FishermanGift { get { return "gameplay/hero_of_the_village/fisherman_gift"; } }
                    public static string FletcherGift { get { return "gameplay/hero_of_the_village/fletcher_gift"; } }
                    public static string LeatherWorkerGift { get { return "gameplay/hero_of_the_village/leatherworker_gift"; } }
                    public static string LibrarianGift { get { return "gameplay/hero_of_the_village/librarian_gift"; } }
                    public static string MasonGift { get { return "gameplay/hero_of_the_village/mason_gift"; } }
                    public static string ShepherdGift { get { return "gameplay/hero_of_the_village/shepherd_gift"; } }
                    public static string ToolSmithGift { get { return "gameplay/hero_of_the_village/toolsmith_gift"; } }
                    public static string WeaponsmithGift { get { return "gameplay/hero_of_the_village/weaponsmith_gift"; } }
                }

                /// <summary>
                /// A random item dropped by cats at the morning
                /// </summary>
                public static string CatMorningGift { get => "gameplay/cat_morning_gift"; }

                /// <summary>
                /// Loot table for when bartering with piglins
                /// </summary>
                public static string PiglinBartering { get => "gameplay/piglin_bartering"; }

                /// <summary>
                /// Loot tables Minecraft uses for chest loot
                /// </summary>
                public static class Chest
                {
                    #pragma warning disable 1591
                    public static string AbandonedMineshaft { get => "chests/abandoned_mineshaft"; }
                    public static string BuriedTreasure { get => "chests/buried_treasure"; }
                    public static string DesertPyramid { get => "chests/desert_pyramid"; }
                    public static string EndCityTreasure { get => "chests/end_city_treasure"; }
                    public static string IglooChest { get => "chests/igloo_chest"; }
                    public static string JungleTemple { get => "chests/jungle_temple"; }
                    public static string JungleTempleDispenser { get => "chests/jungle_temple_dispenser"; }
                    public static string NetherBridge { get => "chests/nether_bridge"; }
                    public static string ShipwreckMap { get => "chests/shipwreck_map"; }
                    public static string ShipwreckSupply { get => "chests/shipwreck_supply"; }
                    public static string ShipwreckTreasure { get => "chests/shipwreck_treasure"; }
                    public static string SimpleDungeon { get => "chests/simple_dungeon"; }
                    public static string SpawnBonusChest { get => "chests/spawn_bonus_chest"; }
                    public static string StrongholdCorridor { get => "chests/stronghold_corridor"; }
                    public static string StrongholdCrossing { get => "chests/stronghold_crossing"; }
                    public static string StrongholdLibrary { get => "chests/stronghold_library"; }
                    public static string UnderwaterRuinBig { get => "chests/underwater_ruin_big"; }
                    public static string UnderwaterRuinSmall { get => "chests/underwater_ruin_small"; }
                    public static string WoodlandMansion { get => "chests/woodland_mansion"; }

                    /// <summary>
                    /// Loot tables for chests in villages
                    /// </summary>
                    public static class Village 
                    {
                        public static string Armorer { get => "chests/village/village_armorer"; }
                        public static string Butcher { get => "chests/village/village_butcher"; }
                        public static string Cartographer { get => "chests/village/village_cartographer"; }
                        public static string DesertHouse { get => "chests/village/village_desert_house"; }
                        public static string Fisher { get => "chests/village/village_fisher"; }
                        public static string Fletcher { get => "chests/village/village_fletcher"; }
                        public static string Mason { get => "chests/village/village_mason"; }
                        public static string PlainsHouse { get => "chests/village/village_plains_house"; }
                        public static string SavannaHouse { get => "chests/village/village_savanna_house"; }
                        public static string Shepherd { get => "chests/village/village_shepherd"; }
                        public static string SnowyHouse { get => "chests/village/village_snowy_house"; }
                        public static string TaigaHouse { get => "chests/village/village_taiga_house"; }
                        public static string Tannery { get => "chests/village/village_tannery"; }
                        public static string Temple { get => "chests/village/village_temple"; }
                        public static string Toolsmith { get => "chests/village/village_toolsmith"; }
                        public static string Weaponsmith { get => "chests/village/village_weaponsmith"; }
                    }

                    /// <summary>
                    /// Loot tables for chests in bastions
                    /// </summary>
                    public static class Bastion
                    {
                        public static string Bridge { get => "chests\\bastion_bridge"; }
                        public static string HoglinStable { get => "chests\\bastion_hoglin_stable"; }
                        public static string BastionOther { get => "chests\\bastion_other"; }
                        public static string BastionTreasure { get => "chests\\bastion_treasure"; }
                    }

                    #pragma warning restore 1591
                }
            }

            /// <summary>
            /// Names of the different types of groups in the game (Normally called "tags" in the game)
            /// </summary>
            public static partial class Groups
            {
                /// <summary>
                /// Function groups
                /// </summary>
                /// <summary>
				/// Item groups
				/// </summary>
				public class Function : NamespacedEnumLike<string>, IFunction
                {
                    #pragma warning disable 1591
                    public Function(string value, BasePackNamespace? @namespace = null) : base(value, @namespace)
                    {
                    }

                    /// <summary>
                    /// Converts this type into a <see cref="DataPartObject"/>
                    /// </summary>
                    /// <param name="conversionData">0: tag name if id. 1: tag name if group. 2: if json</param>
                    /// <returns></returns>
                    public DataPartObject GetAsDataObject(object?[] conversionData)
                    {
                        return (this as IGroupable).GetGroupData(conversionData);
                    }

                    /// <summary>
                    /// Returns a string that represents the current object
                    /// </summary>
                    /// <returns>A string that represents the current object</returns>
                    public override string ToString()
                    {
                        return "#" + base.ToString();
                    }

                    public string GetNamespacedName()
                    {
                        return ToString();
                    }

                    public string Name => ToString();

                    public bool IsAGroup => true;

                    public string FileId => Value;

                    public BasePackNamespace PackNamespace => PackNamespace ?? MockNamespace.GetMinecraftNamespace();

                    /// <summary>
                    /// Functions inside this group gets evoked everytime the world loads or someone uses /reload
                    /// (They are run when the datapack containing the group loads)
                    /// </summary>
                    public static readonly Function load = new Function("load");

                    /// <summary>
                    /// Functions inside this groups runs 20 times a second
                    /// </summary>
                    public static readonly Function tick = new Function("tick");
                }
            }
        }
    }
}
