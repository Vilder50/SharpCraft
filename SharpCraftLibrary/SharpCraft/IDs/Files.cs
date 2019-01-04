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
            /// Loot table file names used by Minecraft which then can be used in / as a <see cref="Loottable"/>
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
                    public static string VillageBlacksmith { get => "chests\\village_blacksmith"; }
                    public static string WoodlandMansion { get => "chests\\woodland_mansion"; }
                    #pragma warning restore 1591
                }
            }

            /// <summary>
            /// Group file names used by Minecraft which then can be used in / as a <see cref="Group"/>
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
                        jungle_logs,
                        leaves,
                        oak_logs,
                        planks,
                        rails,
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
                        /// Blocks in this groups does not allow water to drip through them
                        /// </summary>
                        impermeable,

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
                        bamboo_plantable
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
                        small_flowers,
                        acacia_logs,
                        anvil,
                        banners,
                        birch_logs,
                        boats,
                        buttons,
                        carpets,
                        dark_oak_logs,
                        doors,
                        jungle_logs,
                        leaves,
                        logs,
                        oak_logs,
                        rails,
                        sand,
                        saplings,
                        slabs,
                        spruce_logs,
                        stairs,
                        stone_bricks,
                        wooden_buttons,
                        wooden_doors,
                        wooden_pressure_plates,
                        wooden_slabs,
                        wooden_stairs,
                        wool
                        #pragma warning restore 1591
                    }

                    /// <summary>
                    /// special item groups which has special properties
                    /// </summary>
                    public enum Special
                    {
                        /// <summary>
                        /// Items in this group can be used to repeair wooden tools and shields
                        /// </summary>
                        planks,
                        /// <summary>
                        /// Dolphins swims to players with this item
                        /// Can be feet to dolphins
                        /// </summary>
                        fishes
                    }
                }

                /// <summary>
                /// entity groups
                /// </summary>
                public enum Entities
                {
                    #pragma warning disable 1591
                    skeletons
                    #pragma warning restore 1591
                }
            }
        }
    }
}
