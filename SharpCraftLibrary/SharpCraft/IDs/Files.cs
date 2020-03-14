using System;
using System.Collections.Generic;
using System.Text;
namespace SharpCraft
{
    public partial class ID
    {
        /// <summary>
        /// File names used by Minecraft
        /// </summary>
        public static class Files
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
                public static string Block(Block block)
                {
                    return "blocks\\" + block;
                }

                /// <summary>
                /// Outputs the file name for the given entity's loot table
                /// </summary>
                /// <param name="entity">The entity to get the file name of</param>
                /// <returns>A loot table file name</returns>
                public static string Entity(Entity entity)
                {
                    return "entities\\" + entity;
                }

                /// <summary>
                /// Outputs the file name for the given sheep color's loot table
                /// </summary>
                /// <param name="color">The sheep color to get the file name of</param>
                /// <returns>A loot table file name</returns>
                public static string EntitySheep(Color color)
                {
                    return "entities\\sheep\\" + color;
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
                            return "gameplay\\fishing";
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
                            return "gameplay\\fishing\\fish";
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
                            return "gameplay\\fishing\\junk";
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
                            return "gameplay\\fishing\\treasure";
                        }
                    }
                }

                /// <summary>
                /// Gifts given by villagers from hero of the village
                /// </summary>
                public static class VillagerGifts
                {
#pragma warning disable 1591
                    public static string ArmorerGift { get { return "gameplay\\hero_of_the_village\\armorer_gift"; } }
                    public static string ButcherGift { get { return "gameplay\\hero_of_the_village\\butcher_gift"; } }
                    public static string CartographerGift { get { return "gameplay\\hero_of_the_village\\cartographer_gift"; } }
                    public static string ClericGift { get { return "gameplay\\hero_of_the_village\\cleric_gift"; } }
                    public static string FarmerGift { get { return "gameplay\\hero_of_the_village\\farmer_gift"; } }
                    public static string FishermanGift { get { return "gameplay\\hero_of_the_village\\fisherman_gift"; } }
                    public static string FletcherGift { get { return "gameplay\\hero_of_the_village\\fletcher_gift"; } }
                    public static string LeatherWorkerGift { get { return "gameplay\\hero_of_the_village\\leatherworker_gift"; } }
                    public static string LibrarianGift { get { return "gameplay\\hero_of_the_village\\librarian_gift"; } }
                    public static string MasonGift { get { return "gameplay\\hero_of_the_village\\mason_gift"; } }
                    public static string ShepherdGift { get { return "gameplay\\hero_of_the_village\\shepherd_gift"; } }
                    public static string ToolSmithGift { get { return "gameplay\\hero_of_the_village\\toolsmith_gift"; } }
                    public static string WeaponsmithGift { get { return "gameplay\\hero_of_the_village\\weaponsmith_gift"; } }
#pragma warning restore 1591
                }

                /// <summary>
                /// A random item dropped by cats at the morning
                /// </summary>
                public static string CatMorningGift { get => "gameplay\\cat_morning_gift"; }

                /// <summary>
                /// Loot table for when bartering with piglins
                /// </summary>
                public static string PiglinBartering { get => "gameplay\\piglin_bartering"; }

                /// <summary>
                /// Loot tables Minecraft uses for chest loot
                /// </summary>
                public static class Chest
                {
                    #pragma warning disable 1591
                    public static string AbandonedMineshaft { get => "chests\\abandoned_mineshaft"; }
                    public static string BuriedTreasure { get => "chests\\buried_treasure"; }
                    public static string DesertPyramid { get => "chests\\desert_pyramid"; }
                    public static string EndCityTreasure { get => "chests\\end_city_treasure"; }
                    public static string IglooChest { get => "chests\\igloo_chest"; }
                    public static string JungleTemple { get => "chests\\jungle_temple"; }
                    public static string JungleTempleDispenser { get => "chests\\jungle_temple_dispenser"; }
                    public static string NetherBridge { get => "chests\\nether_bridge"; }
                    public static string ShipwreckMap { get => "chests\\shipwreck_map"; }
                    public static string ShipwreckSupply { get => "chests\\shipwreck_supply"; }
                    public static string ShipwreckTreasure { get => "chests\\shipwreck_treasure"; }
                    public static string SimpleDungeon { get => "chests\\simple_dungeon"; }
                    public static string SpawnBonusChest { get => "chests\\spawn_bonus_chest"; }
                    public static string StrongholdCorridor { get => "chests\\stronghold_corridor"; }
                    public static string StrongholdCrossing { get => "chests\\stronghold_crossing"; }
                    public static string StrongholdLibrary { get => "chests\\stronghold_library"; }
                    public static string UnderwaterRuinBig { get => "chests\\underwater_ruin_big"; }
                    public static string UnderwaterRuinSmall { get => "chests\\underwater_ruin_small"; }
                    public static string WoodlandMansion { get => "chests\\woodland_mansion"; }

                    /// <summary>
                    /// Loot tables for chests in villagers
                    /// </summary>
                    public static class Village 
                    {
                        public static string Armorer { get => "chests\\village\\village_armorer"; }
                        public static string Butcher { get => "chests\\village\\village_butcher"; }
                        public static string Cartographer { get => "chests\\village\\village_cartographer"; }
                        public static string DesertHouse { get => "chests\\village\\village_desert_house"; }
                        public static string Fisher { get => "chests\\village\\village_fisher"; }
                        public static string Fletcher { get => "chests\\village\\village_fletcher"; }
                        public static string Mason { get => "chests\\village\\village_mason"; }
                        public static string PlainsHouse { get => "chests\\village\\village_plains_house"; }
                        public static string SavannaHouse { get => "chests\\village\\village_savanna_house"; }
                        public static string Shepherd { get => "chests\\village\\village_shepherd"; }
                        public static string SnowyHouse { get => "chests\\village\\village_snowy_house"; }
                        public static string TaigaHouse { get => "chests\\village\\village_taiga_house"; }
                        public static string Tannery { get => "chests\\village\\village_tannery"; }
                        public static string Temple { get => "chests\\village\\village_temple"; }
                        public static string Toolsmith { get => "chests\\village\\village_toolsmith"; }
                        public static string Weaponsmith { get => "chests\\village\\village_weaponsmith"; }
                    }

                    #pragma warning restore 1591
                }
            }

            /// <summary>
            /// These groups are called tags in Minecraft. But let's face it. It's a bad name!
            /// </summary>
            public static class Groups
            {
                /// <summary>
                /// Function groups
                /// </summary>
                public enum Function
                {
                    /// <summary>
                    /// Functions inside this group gets evoked everytime the world loads or someone uses /reload
                    /// (They are run when the datapack containing the group loads)
                    /// </summary>
                    load,

                    /// <summary>
                    /// Functions inside this groups runs 20 times a second
                    /// </summary>
                    tick
                }

                /// <summary>
                /// Block groups
                /// </summary>
                public static class Blocks
                {
                    /// <summary>
                    /// Normal block groups made by the game. These groups has no real uses other than grouping blocks
                    /// </summary>
                    public enum Normal
                    {
                        #pragma warning disable 1591
                        acacia_logs,
                        birch_logs,
                        buttons,
                        carpets,
                        coral_blocks,
                        corals,
                        dark_oak_logs,
                        doors,
                        flower_pots,
                        ice,
                        leaves,
                        oak_logs,
                        planks,
                        sand,
                        saplings,
                        slabs,
                        spruce_logs,
                        stairs,
                        stone_bricks,
                        wall_corals,
                        wooden_buttons,
                        wooden_doors,
                        wooden_pressure_plates,
                        wooden_slabs,
                        wooden_stairs,
                        small_flowers,
                        crops,
                        tall_flowers,
                        standing_signs,
                        wall_signs,
                        walls,
                        warped_stems,
                        crimson_stems,
                        wart_blocks,
                        #pragma warning restore 1591
                    }

                    /// <summary>
                    /// Special block groups which has special properties
                    /// </summary>
                    public enum Special
                    {
                        /// <summary>
                        /// Endermen can only pick up blocks in this group
                        /// </summary>
                        enderman_holdable,

                        /// <summary>
                        /// This group makes anvils in it show their gui when clicked (Only works for anvil blocks)
                        /// Changes the death message caused by the block as a falling block landing on and killing a player
                        /// </summary>
                        anvil,

                        /// <summary>
                        /// Right clicking these blocks with a map marks it on the map
                        /// </summary>
                        banners,

                        /// <summary>
                        /// Blocks cats can sit on.
                        /// Blocks which can be slept in
                        /// </summary>
                        beds,

                        /// <summary>
                        /// Blocks in this groups does not allow water and honey to drip through them
                        /// </summary>
                        impermeable,

                        /// <summary>
                        /// Blocks leads can attach to
                        /// Blocks which mobs see as fences while pathfinding
                        /// </summary>
                        fences,

                        /// <summary>
                        /// Leaves wont decay around these blocks
                        /// Trees can grow into blocks with this tag
                        /// </summary>
                        logs,

                        /// <summary>
                        /// When one of these blocks are bonemealed under water in a warm water biome the block duplicates
                        /// </summary>
                        underwater_bonemeals,

                        /// <summary>
                        /// Blocks in this group allows players to spawn on them
                        /// </summary>
                        valid_spawn,

                        /// <summary>
                        /// Blocks in this group can be broken using shears
                        /// If a block in this group is under a note block it will sound like a guitar
                        /// </summary>
                        wool,

                        /// <summary>
                        /// Blocks in this group allows bamboo to be planted ontop
                        /// </summary>
                        bamboo_plantable,

                        /// <summary>
                        /// Blocks the wither wont have easy to break
                        /// </summary>
                        wither_immune,

                        /// <summary>
                        /// Blocks the enderdragon wont destroy
                        /// </summary>
                        dragon_immune,

                        /// <summary>
                        /// Blocks bees can make grow
                        /// </summary>
                        bee_growables,

                        /// <summary>
                        /// Blocks bees can fill with pollen and blocks dispensers can use a shear or a glass bottle on
                        /// </summary>
                        beehives,

                        /// <summary>
                        /// Blocks bees can pollinate and remember
                        /// </summary>
                        flowers,

                        /// <summary>
                        /// Blocks fences doesn't connect to
                        /// </summary>
                        shulker_boxes,

                        /// <summary>
                        /// Blocks cocoa beans can be placed on
                        /// </summary>
                        jungle_logs,

                        /// <summary>
                        /// Blocks mobs can't spawn on.
                        /// Blocks minecarts can be dispenced on to.
                        /// Blocks tnt minecarts doesn't destroy.
                        /// </summary>
                        rails,

                        /// <summary>
                        /// Water doesn't break these blocks
                        /// </summary>
                        signs,

                        /// <summary>
                        /// Blocks for building a beacon pyramid
                        /// </summary>
                        beacon_base_blocks,

                        /// <summary>
                        /// Make blocks climbable
                        /// </summary>
                        climbable,

                        /// <summary>
                        /// Blocks which can be broken with water bottle water. Falling blocks will try to fall through these blocks. 
                        /// </summary>
                        fire,

                        /// <summary>
                        /// Blocks nether fungus, roots and sprouts can be placed on. Netherrack can be bonemealed if around of these blocks (netherrack only converts into nylium).
                        /// </summary>
                        nylium,

                        /// <summary>
                        /// none solid blocks which still makes walls connect upwards.
                        /// </summary>
                        wall_post_override,

                        /// <summary>
                        /// Blocks used for making the T part of the wither summon build.
                        /// </summary>
                        wither_summon_base_blocks,

                        /// <summary>
                        /// Blocks hoglins tries to stay away from
                        /// </summary>
                        hoglin_repellents,

                        /// <summary>
                        /// Blocks piglins tries to stay away from
                        /// </summary>
                        piglin_repellents,

                        /// <summary>
                        /// Iron pickaxes or better will make blocks with this tag drop their loot
                        /// </summary>
                        gold_ores,

                        /// <summary>
                        /// Blocks the <see cref="ID.Enchant.soul_speed"/> enchant works on
                        /// </summary>
                        soul_speed_blocks,
                    }
                }

                /// <summary>
                /// fluid groups used by Minecraft
                /// </summary>
                public enum Fluid
                {
                    /// <summary>
                    /// Cactus breaks beside these.
                    /// Used to make a fluid look like lava.
                    /// Used to make smoke particles when rain hits these.
                    /// Items and experience orbs burns inside these.
                    /// used when creating cobblestone/stone/obsidian.
                    /// </summary>
                    lava,

                    /// <summary>
                    /// Corals must be beside one of these.
                    /// Farmland stays hydrated around these.
                    /// Sugar canes can stay around these.
                    /// Sponges absorb these.
                    /// some particles can only survive in these.
                    /// Entities in these moves like in water.
                    /// concrete gets solid in these.
                    /// items float in these.
                    /// glass bottles can be filled with these.
                    /// </summary>
                    water
                }

                /// <summary>
                /// item groups
                /// </summary>
                public static class Items
                {
                    /// <summary>
                    /// Normal item groups made by the game. These groups has no real uses other than grouping items
                    /// </summary>
                    public enum Normal
                    {
                        #pragma warning disable 1591
                        acacia_logs,
                        gold_ores
                        anvil,
                        birch_logs,
                        buttons,
                        dark_oak_logs,
                        doors,
                        jungle_logs,
                        leaves,
                        oak_logs,
                        rails,
                        sand,
                        slabs,
                        spruce_logs,
                        stairs,
                        stone_bricks,
                        tall_flowers,
                        warped_stems,
                        crimson_stems,
                        #pragma warning restore 1591
                    }

                    /// <summary>
                    /// special item groups which has special properties
                    /// </summary>
                    public enum Special
                    {
                        /// <summary>
                        /// Items in this group can be used to repeair wooden tools and shields.
                        /// Items which burns in a furnace for 300 ticks
                        /// </summary>
                        planks,
                        /// <summary>
                        /// Dolphins swims to players with this item
                        /// Can be feet to dolphins
                        /// </summary>
                        fishes,
                        /// <summary>
                        /// Items which can be used to breed bees
                        /// </summary>
                        flowers,
                        /// <summary>
                        /// Items which can be placed on a lectern (only accepts <see cref="ID.Item.writable_book"/> and <see cref="ID.Item.written_book"/>)
                        /// </summary>
                        lectern_books,
                        /// <summary>
                        /// Items which can be shot from a bow/crossbow
                        /// </summary>
                        arrows,
                        /// <summary>
                        /// Items which burns in a furnace for 300 ticks
                        /// </summary>
                        banners,
                        /// <summary>
                        /// Items which burns in a furnace for 1200 ticks
                        /// </summary>
                        boats,
                        /// <summary>
                        /// Items which can be placed on llamas.
                        /// Items which burns in a furnace for 67 ticks
                        /// </summary>
                        carpets,
                        /// <summary>
                        /// Items which burns in a furnace for 300 ticks
                        /// </summary>
                        logs,
                        /// <summary>
                        /// Items which burns in a furnace for 100 ticks
                        /// </summary>
                        saplings,
                        /// <summary>
                        /// Items which burns in a furnace for 200 ticks
                        /// </summary>
                        signs,
                        /// <summary>
                        /// Items used for crafting suspicious stew.
                        /// Items which can be fed to brown mushrooms.
                        /// Items which bees follows
                        /// </summary>
                        small_flowers,
                        /// <summary>
                        /// Items which burns in a furnace for 100 ticks
                        /// </summary>
                        wooden_buttons,
                        /// <summary>
                        /// Items which burns in a furnace for 200 ticks
                        /// </summary>
                        wooden_doors,
                        /// <summary>
                        /// Items which burns in a furnace for 300 ticks
                        /// </summary>
                        wooden_pressure_plates,
                        /// <summary>
                        /// Items which burns in a furnace for 150 ticks
                        /// </summary>
                        wooden_slabs,
                        /// <summary>
                        /// Items which burns in a furnace for 300 ticks
                        /// </summary>
                        wooden_stairs,
                        /// <summary>
                        /// Items which burns in a furnace for 300 ticks
                        /// </summary>
                        wooden_trapdoors,
                        /// <summary>
                        /// Items which burns in a furnace for 100 ticks
                        /// </summary>
                        wool,
                        /// <summary>
                        /// Items which can be used for activating beacons.
                        /// </summary>
                        beacon_payment_items,
                        /// <summary>
                        /// Items piglins wont pickup
                        /// </summary>
                        piglin_repellents,
                    }
                }

                /// <summary>
                /// entity groups
                /// </summary>
                public static class Entities
                {
                    /// <summary>
                    /// Normal entity groups made by the game. These groups has no real uses other than grouping entities for advancements
                    /// </summary>
                    public enum Normal
                    {
#pragma warning disable 1591
                        skeletons,
                        arrows
#pragma warning restore 1591
                    }

                    /// <summary>
                    /// special entity groups which has special properties
                    /// </summary>
                    public enum Special
                    {
                        /// <summary>
                        /// entities which can be in beehives
                        /// </summary>
                        beehive_inhabitors,

                        /// <summary>
                        /// Entities which glows when the bell rings.
                        /// Entities which don't override ravager AI when riding on one
                        /// </summary>
                        raiders,

                        /// <summary>
                        /// Entities which can break chorus fruit.
                        /// </summary>
                        impact_projectiles,
                    }
                }
            }
        }
    }
}
