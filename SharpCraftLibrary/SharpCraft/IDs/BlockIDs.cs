﻿using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// All the different ID's/Types/States things in the game can have
    /// </summary>
    public static partial class ID
    {
#pragma warning disable 1591
        public class Block : NamespacedEnumLike<string>, IBlockType
        {
            public Block(string value, BasePackNamespace? @namespace = null) : base(value, @namespace)
            {
            }

            /// <summary>
            /// Converts this block into an item type. There is no check checking if the item is valid
            /// </summary>
            /// <returns>The item type</returns>
            public Item ConvertToItem()
            {
                return new Item(Value, Namespace);
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

            public string Name => ToString();

            public bool IsAGroup => false;

            public static readonly Block blackstone = new Block("blackstone");
            public static readonly Block blackstone_slab = new Block("blackstone_slab");
            public static readonly Block blackstone_stairs = new Block("blackstone_stairs");
            public static readonly Block blackstone_wall = new Block("blackstone_wall");
            public static readonly Block gilded_blackstone = new Block("gilded_blackstone");
            public static readonly Block polished_blackstone = new Block("polished_blackstone");
            public static readonly Block polished_blackstone_slab = new Block("polished_blackstone_slab");
            public static readonly Block polished_blackstone_stairs = new Block("polished_blackstone_stairs");
            public static readonly Block polished_blackstone_wall = new Block("polished_blackstone_wall");
            public static readonly Block chiseled_polished_blackstone = new Block("chiseled_polished_blackstone");
            public static readonly Block polished_blackstone_bricks = new Block("polished_blackstone_bricks");
            public static readonly Block polished_blackstone_brick_slab = new Block("polished_blackstone_brick_slab");
            public static readonly Block polished_blackstone_brick_stairs = new Block("polished_blackstone_brick_stairs");
            public static readonly Block polished_blackstone_brick_wall = new Block("polished_blackstone_brick_wall");
            public static readonly Block cracked_polished_blackstone_bricks = new Block("cracked_polished_blackstone_bricks");
            public static readonly Block chain = new Block("chain");
            public static readonly Block cracked_nether_bricks = new Block("cracked_nether_bricks");
            public static readonly Block chiseled_nether_bricks = new Block("chiseled_nether_bricks");
            public static readonly Block quartz_bricks = new Block("quartz_bricks");
            public static readonly Block soul_campfire = new Block("soul_campfire");
            public static readonly Block polished_basalt = new Block("polished_basalt");
            public static readonly Block respawn_anchor = new Block("respawn_anchor");
            public static readonly Block lodestone = new Block("lodestone");
            public static readonly Block nether_gold_ore = new Block("nether_gold_ore");
            public static readonly Block twisting_vines = new Block("twisting_vines");
            public static readonly Block twisting_vines_plant = new Block("twisting_vines_plant");
            public static readonly Block stripped_warped_hyphae = new Block("stripped_warped_hyphae");
            public static readonly Block stripped_crimson_hyphae = new Block("stripped_crimson_hyphae");
            public static readonly Block warped_hyphae = new Block("warped_hyphae");
            public static readonly Block crimson_hyphae = new Block("crimson_hyphae");
            public static readonly Block ancient_debris = new Block("ancient_debris");
            public static readonly Block basalt = new Block("basalt");
            public static readonly Block netherite_block = new Block("netherite_block");
            public static readonly Block crimson_fungus = new Block("crimson_fungus");
            public static readonly Block crimson_nylium = new Block("crimson_nylium");
            public static readonly Block crimson_sign = new Block("crimson_sign");
            public static readonly Block crimson_wall_sign = new Block("crimson_wall_sign");
            public static readonly Block stripped_crimson_stem = new Block("stripped_crimson_stem");
            public static readonly Block crimson_planks = new Block("crimson_planks");
            public static readonly Block crimson_stem = new Block("crimson_stem");
            public static readonly Block crimson_pressure_plate = new Block("crimson_pressure_plate");
            public static readonly Block crimson_trapdoor = new Block("crimson_trapdoor");
            public static readonly Block crimson_stairs = new Block("crimson_stairs");
            public static readonly Block crimson_button = new Block("crimson_button");
            public static readonly Block crimson_slab = new Block("crimson_slab");
            public static readonly Block crimson_fence_gate = new Block("crimson_fence_gate");
            public static readonly Block crimson_fence = new Block("crimson_fence");
            public static readonly Block crimson_door = new Block("crimson_door");
            public static readonly Block crimson_roots = new Block("crimson_roots");
            public static readonly Block potted_crimson_fungus = new Block("potted_crimson_fungus");
            public static readonly Block potted_crimson_roots = new Block("potted_crimson_roots");
            public static readonly Block warped_fungus = new Block("warped_fungus");
            public static readonly Block warped_nylium = new Block("warped_nylium");
            public static readonly Block warped_sign = new Block("warped_sign");
            public static readonly Block warped_wall_sign = new Block("warped_wall_sign");
            public static readonly Block stripped_warped_stem = new Block("stripped_warped_stem");
            public static readonly Block warped_planks = new Block("warped_planks");
            public static readonly Block warped_stem = new Block("warped_stem");
            public static readonly Block warped_pressure_plate = new Block("warped_pressure_plate");
            public static readonly Block warped_trapdoor = new Block("warped_trapdoor");
            public static readonly Block warped_stairs = new Block("warped_stairs");
            public static readonly Block warped_button = new Block("warped_button");
            public static readonly Block warped_slab = new Block("warped_slab");
            public static readonly Block warped_fence_gate = new Block("warped_fence_gate");
            public static readonly Block warped_fence = new Block("warped_fence");
            public static readonly Block warped_door = new Block("warped_door");
            public static readonly Block warped_roots = new Block("warped_roots");
            public static readonly Block warped_wart_block = new Block("warped_wart_block");
            public static readonly Block potted_warped_fungus = new Block("potted_warped_fungus");
            public static readonly Block potted_warped_roots = new Block("potted_warped_roots");
            public static readonly Block crying_obsidian = new Block("crying_obsidian");
            public static readonly Block nether_sprouts = new Block("nether_sprouts");
            public static readonly Block shroomlight = new Block("shroomlight");
            public static readonly Block soul_fire = new Block("soul_fire");
            public static readonly Block soul_lantern = new Block("soul_lantern");
            public static readonly Block soul_touch = new Block("soul_touch");
            public static readonly Block soul_wall_touch = new Block("soul_wall_touch");
            public static readonly Block target = new Block("target");
            public static readonly Block weeping_vines = new Block("weeping_vines");
            public static readonly Block weeping_vines_plant = new Block("weeping_vines_plant");
            public static readonly Block soul_soil = new Block("soul_soil");

            public static readonly Block beehive = new Block("beehive");
            public static readonly Block bee_nest = new Block("bee_nest");
            public static readonly Block honey_block = new Block("honey_block");
            public static readonly Block honeycomb_block = new Block("honeycomb_block");
            public static readonly Block composter = new Block("composter");
            public static readonly Block campfire = new Block("campfire");
            public static readonly Block sweet_berry_bush = new Block("sweet_berry_bush");
            public static readonly Block barrel = new Block("barrel");
            public static readonly Block smoker = new Block("smoker");
            public static readonly Block blast_furnace = new Block("blast_furnace");
            public static readonly Block cartography_table = new Block("cartography_table");
            public static readonly Block fletching_table = new Block("fletching_table");
            public static readonly Block grindstone = new Block("grindstone");
            public static readonly Block smithing_table = new Block("smithing_table");
            public static readonly Block stonecutter = new Block("stonecutter");
            public static readonly Block bell = new Block("bell");
            public static readonly Block lantern = new Block("lantern");
            public static readonly Block lectern = new Block("lectern");
            public static readonly Block jigsaw = new Block("jigsaw");
            public static readonly Block scaffolding = new Block("scaffolding");
            public static readonly Block polished_diorite_stairs = new Block("polished_diorite_stairs");
            public static readonly Block mossy_cobblestone_stairs = new Block("mossy_cobblestone_stairs");
            public static readonly Block end_stone_brick_stairs = new Block("end_stone_brick_stairs");
            public static readonly Block stone_stairs = new Block("stone_stairs");
            public static readonly Block smooth_sandstone_stairs = new Block("smooth_sandstone_stairs");
            public static readonly Block smooth_quartz_stairs = new Block("smooth_quartz_stairs");
            public static readonly Block granite_stairs = new Block("granite_stairs");
            public static readonly Block andesite_stairs = new Block("andesite_stairs");
            public static readonly Block red_nether_brick_stairs = new Block("red_nether_brick_stairs");
            public static readonly Block polished_andesite_stairs = new Block("polished_andesite_stairs");
            public static readonly Block diorite_stairs = new Block("diorite_stairs");
            public static readonly Block mossy_stone_brick_stairs = new Block("mossy_stone_brick_stairs");
            public static readonly Block smooth_red_sandstone_stairs = new Block("smooth_red_sandstone_stairs");
            public static readonly Block polished_granite_stairs = new Block("polished_granite_stairs");
            public static readonly Block polished_diorite_slab = new Block("polished_diorite_slab");
            public static readonly Block mossy_cobblestone_slab = new Block("mossy_cobblestone_slab");
            public static readonly Block end_stone_brick_slab = new Block("end_stone_brick_slab");
            public static readonly Block stone_slab = new Block("stone_slab");
            public static readonly Block smooth_sandstone_slab = new Block("smooth_sandstone_slab");
            public static readonly Block smooth_quartz_slab = new Block("smooth_quartz_slab");
            public static readonly Block granite_slab = new Block("granite_slab");
            public static readonly Block andesite_slab = new Block("andesite_slab");
            public static readonly Block red_nether_brick_slab = new Block("red_nether_brick_slab");
            public static readonly Block polished_andesite_slab = new Block("polished_andesite_slab");
            public static readonly Block diorite_slab = new Block("diorite_slab");
            public static readonly Block mossy_stone_brick_slab = new Block("mossy_stone_brick_slab");
            public static readonly Block smooth_red_sandstone_slab = new Block("smooth_red_sandstone_slab");
            public static readonly Block polished_granite_slab = new Block("polished_granite_slab");
            public static readonly Block smooth_stone_slab = new Block("smooth_stone_slab");
            public static readonly Block mossy_cobblestone_wall = new Block("mossy_cobblestone_wall");
            public static readonly Block end_stone_brick_wall = new Block("end_stone_brick_wall");
            public static readonly Block granite_wall = new Block("granite_wall");
            public static readonly Block andesite_wall = new Block("andesite_wall");
            public static readonly Block red_nether_brick_wall = new Block("red_nether_brick_wall");
            public static readonly Block diorite_wall = new Block("diorite_wall");
            public static readonly Block mossy_stone_brick_wall = new Block("mossy_stone_brick_wall");
            public static readonly Block prismarine_wall = new Block("prismarine_wall");
            public static readonly Block nether_brick_wall = new Block("nether_brick_wall");
            public static readonly Block oak_sign = new Block("oak_sign");
            public static readonly Block spruce_sign = new Block("spruce_sign");
            public static readonly Block birch_sign = new Block("birch_sign");
            public static readonly Block acacia_sign = new Block("acacia_sign");
            public static readonly Block jungle_sign = new Block("jungle_sign");
            public static readonly Block dark_oak_sign = new Block("dark_oak_sign");
            public static readonly Block bamboo = new Block("bamboo");
            public static readonly Block cornflower = new Block("cornflower");
            public static readonly Block lily_of_the_valley = new Block("lily_of_the_valley");
            public static readonly Block wither_rose = new Block("wither_rose");
            public static readonly Block loom = new Block("loom");
            public static readonly Block oak_wall_sign = new Block("oak_wall_sign");
            public static readonly Block spruce_wall_sign = new Block("spruce_wall_sign");
            public static readonly Block birch_wall_sign = new Block("birch_wall_sign");
            public static readonly Block acacia_wall_sign = new Block("acacia_wall_sign");
            public static readonly Block jungle_wall_sign = new Block("jungle_wall_sign");
            public static readonly Block dark_oak_wall_sign = new Block("dark_oak_wall_sign");
            public static readonly Block bamboo_sapling = new Block("bamboo_sapling");
            public static readonly Block dead_tube_coral = new Block("dead_tube_coral");
            public static readonly Block dead_brain_coral = new Block("dead_brain_coral");
            public static readonly Block dead_bubble_coral = new Block("dead_bubble_coral");
            public static readonly Block dead_fire_coral = new Block("dead_fire_coral");
            public static readonly Block dead_horn_coral = new Block("dead_horn_coral");
            public static readonly Block dead_tube_coral_fan = new Block("dead_tube_coral_fan");
            public static readonly Block dead_brain_coral_fan = new Block("dead_brain_coral_fan");
            public static readonly Block dead_bubble_coral_fan = new Block("dead_bubble_coral_fan");
            public static readonly Block dead_fire_coral_fan = new Block("dead_fire_coral_fan");
            public static readonly Block dead_horn_coral_fan = new Block("dead_horn_coral_fan");
            public static readonly Block dead_tube_coral_wall_fan = new Block("dead_tube_coral_wall_fan");
            public static readonly Block dead_brain_coral_wall_fan = new Block("dead_brain_coral_wall_fan");
            public static readonly Block dead_bubble_coral_wall_fan = new Block("dead_bubble_coral_wall_fan");
            public static readonly Block dead_fire_coral_wall_fan = new Block("dead_fire_coral_wall_fan");
            public static readonly Block dead_horn_coral_wall_fan = new Block("dead_horn_coral_wall_fan");
            public static readonly Block tube_coral_wall_fan = new Block("tube_coral_wall_fan");
            public static readonly Block brain_coral_wall_fan = new Block("brain_coral_wall_fan");
            public static readonly Block bubble_coral_wall_fan = new Block("bubble_coral_wall_fan");
            public static readonly Block fire_coral_wall_fan = new Block("fire_coral_wall_fan");
            public static readonly Block horn_coral_wall_fan = new Block("horn_coral_wall_fan");
            public static readonly Block stripped_oak_wood = new Block("stripped_oak_wood");
            public static readonly Block stripped_spruce_wood = new Block("stripped_spruce_wood");
            public static readonly Block stripped_birch_wood = new Block("stripped_birch_wood");
            public static readonly Block stripped_jungle_wood = new Block("stripped_jungle_wood");
            public static readonly Block stripped_acacia_wood = new Block("stripped_acacia_wood");
            public static readonly Block stripped_dark_oak_wood = new Block("stripped_dark_oak_wood");
            public static readonly Block tall_seagrass = new Block("tall_seagrass");
            public static readonly Block shulker_box = new Block("shulker_box");
            public static readonly Block sea_pickle = new Block("sea_pickle");
            public static readonly Block conduit = new Block("conduit");
            public static readonly Block dead_tube_coral_block = new Block("dead_tube_coral_block");
            public static readonly Block dead_brain_coral_block = new Block("dead_brain_coral_block");
            public static readonly Block dead_bubble_coral_block = new Block("dead_bubble_coral_block");
            public static readonly Block dead_fire_coral_block = new Block("dead_fire_coral_block");
            public static readonly Block dead_horn_coral_block = new Block("dead_horn_coral_block");
            public static readonly Block tube_coral_block = new Block("tube_coral_block");
            public static readonly Block brain_coral_block = new Block("brain_coral_block");
            public static readonly Block bubble_coral_block = new Block("bubble_coral_block");
            public static readonly Block fire_coral_block = new Block("fire_coral_block");
            public static readonly Block horn_coral_block = new Block("horn_coral_block");
            public static readonly Block tube_coral = new Block("tube_coral");
            public static readonly Block brain_coral = new Block("brain_coral");
            public static readonly Block bubble_coral = new Block("bubble_coral");
            public static readonly Block fire_coral = new Block("fire_coral");
            public static readonly Block horn_coral = new Block("horn_coral");
            public static readonly Block tube_coral_fan = new Block("tube_coral_fan");
            public static readonly Block brain_coral_fan = new Block("brain_coral_fan");
            public static readonly Block bubble_coral_fan = new Block("bubble_coral_fan");
            public static readonly Block fire_coral_fan = new Block("fire_coral_fan");
            public static readonly Block horn_coral_fan = new Block("horn_coral_fan");
            public static readonly Block blue_ice = new Block("blue_ice");
            public static readonly Block kelp = new Block("kelp");
            public static readonly Block kelp_plant = new Block("kelp_plant");
            public static readonly Block turtle_egg = new Block("turtle_egg");
            public static readonly Block seagrass = new Block("seagrass");
            public static readonly Block stripped_oak_log = new Block("stripped_oak_log");
            public static readonly Block stripped_spruce_log = new Block("stripped_spruce_log");
            public static readonly Block stripped_birch_log = new Block("stripped_birch_log");
            public static readonly Block stripped_jungle_log = new Block("stripped_jungle_log");
            public static readonly Block stripped_acacia_log = new Block("stripped_acacia_log");
            public static readonly Block stripped_dark_oak_log = new Block("stripped_dark_oak_log");
            public static readonly Block dried_kelp_block = new Block("dried_kelp_block");
            public static readonly Block cave_air = new Block("cave_air");
            public static readonly Block void_air = new Block("void_air");
            public static readonly Block air = new Block("air");
            public static readonly Block stone = new Block("stone");
            public static readonly Block granite = new Block("granite");
            public static readonly Block polished_granite = new Block("polished_granite");
            public static readonly Block diorite = new Block("diorite");
            public static readonly Block polished_diorite = new Block("polished_diorite");
            public static readonly Block andesite = new Block("andesite");
            public static readonly Block polished_andesite = new Block("polished_andesite");
            public static readonly Block grass_block = new Block("grass_block");
            public static readonly Block dirt = new Block("dirt");
            public static readonly Block coarse_dirt = new Block("coarse_dirt");
            public static readonly Block podzol = new Block("podzol");
            public static readonly Block cobblestone = new Block("cobblestone");
            public static readonly Block oak_planks = new Block("oak_planks");
            public static readonly Block spruce_planks = new Block("spruce_planks");
            public static readonly Block birch_planks = new Block("birch_planks");
            public static readonly Block jungle_planks = new Block("jungle_planks");
            public static readonly Block acacia_planks = new Block("acacia_planks");
            public static readonly Block dark_oak_planks = new Block("dark_oak_planks");
            public static readonly Block oak_sapling = new Block("oak_sapling");
            public static readonly Block spruce_sapling = new Block("spruce_sapling");
            public static readonly Block birch_sapling = new Block("birch_sapling");
            public static readonly Block jungle_sapling = new Block("jungle_sapling");
            public static readonly Block acacia_sapling = new Block("acacia_sapling");
            public static readonly Block dark_oak_sapling = new Block("dark_oak_sapling");
            public static readonly Block bedrock = new Block("bedrock");
            public static readonly Block water = new Block("water");
            public static readonly Block lava = new Block("lava");
            public static readonly Block sand = new Block("sand");
            public static readonly Block red_sand = new Block("red_sand");
            public static readonly Block gravel = new Block("gravel");
            public static readonly Block gold_ore = new Block("gold_ore");
            public static readonly Block iron_ore = new Block("iron_ore");
            public static readonly Block coal_ore = new Block("coal_ore");
            public static readonly Block oak_log = new Block("oak_log");
            public static readonly Block spruce_log = new Block("spruce_log");
            public static readonly Block birch_log = new Block("birch_log");
            public static readonly Block jungle_log = new Block("jungle_log");
            public static readonly Block acacia_log = new Block("acacia_log");
            public static readonly Block dark_oak_log = new Block("dark_oak_log");
            public static readonly Block oak_wood = new Block("oak_wood");
            public static readonly Block spruce_wood = new Block("spruce_wood");
            public static readonly Block birch_wood = new Block("birch_wood");
            public static readonly Block jungle_wood = new Block("jungle_wood");
            public static readonly Block acacia_wood = new Block("acacia_wood");
            public static readonly Block dark_oak_wood = new Block("dark_oak_wood");
            public static readonly Block oak_leaves = new Block("oak_leaves");
            public static readonly Block spruce_leaves = new Block("spruce_leaves");
            public static readonly Block birch_leaves = new Block("birch_leaves");
            public static readonly Block jungle_leaves = new Block("jungle_leaves");
            public static readonly Block acacia_leaves = new Block("acacia_leaves");
            public static readonly Block dark_oak_leaves = new Block("dark_oak_leaves");
            public static readonly Block sponge = new Block("sponge");
            public static readonly Block wet_sponge = new Block("wet_sponge");
            public static readonly Block glass = new Block("glass");
            public static readonly Block lapis_ore = new Block("lapis_ore");
            public static readonly Block lapis_block = new Block("lapis_block");
            public static readonly Block dispenser = new Block("dispenser");
            public static readonly Block sandstone = new Block("sandstone");
            public static readonly Block chiseled_sandstone = new Block("chiseled_sandstone");
            public static readonly Block cut_sandstone = new Block("cut_sandstone");
            public static readonly Block note_block = new Block("note_block");
            public static readonly Block white_bed = new Block("white_bed");
            public static readonly Block orange_bed = new Block("orange_bed");
            public static readonly Block magenta_bed = new Block("magenta_bed");
            public static readonly Block light_blue_bed = new Block("light_blue_bed");
            public static readonly Block yellow_bed = new Block("yellow_bed");
            public static readonly Block lime_bed = new Block("lime_bed");
            public static readonly Block pink_bed = new Block("pink_bed");
            public static readonly Block gray_bed = new Block("gray_bed");
            public static readonly Block light_gray_bed = new Block("light_gray_bed");
            public static readonly Block cyan_bed = new Block("cyan_bed");
            public static readonly Block purple_bed = new Block("purple_bed");
            public static readonly Block blue_bed = new Block("blue_bed");
            public static readonly Block brown_bed = new Block("brown_bed");
            public static readonly Block green_bed = new Block("green_bed");
            public static readonly Block red_bed = new Block("red_bed");
            public static readonly Block black_bed = new Block("black_bed");
            public static readonly Block powered_rail = new Block("powered_rail");
            public static readonly Block detector_rail = new Block("detector_rail");
            public static readonly Block sticky_piston = new Block("sticky_piston");
            public static readonly Block cobweb = new Block("cobweb");
            public static readonly Block grass = new Block("grass");
            public static readonly Block fern = new Block("fern");
            public static readonly Block dead_bush = new Block("dead_bush");
            public static readonly Block piston = new Block("piston");
            public static readonly Block piston_head = new Block("piston_head");
            public static readonly Block white_wool = new Block("white_wool");
            public static readonly Block orange_wool = new Block("orange_wool");
            public static readonly Block magenta_wool = new Block("magenta_wool");
            public static readonly Block light_blue_wool = new Block("light_blue_wool");
            public static readonly Block yellow_wool = new Block("yellow_wool");
            public static readonly Block lime_wool = new Block("lime_wool");
            public static readonly Block pink_wool = new Block("pink_wool");
            public static readonly Block gray_wool = new Block("gray_wool");
            public static readonly Block light_gray_wool = new Block("light_gray_wool");
            public static readonly Block cyan_wool = new Block("cyan_wool");
            public static readonly Block purple_wool = new Block("purple_wool");
            public static readonly Block blue_wool = new Block("blue_wool");
            public static readonly Block brown_wool = new Block("brown_wool");
            public static readonly Block green_wool = new Block("green_wool");
            public static readonly Block red_wool = new Block("red_wool");
            public static readonly Block black_wool = new Block("black_wool");
            public static readonly Block moving_piston = new Block("moving_piston");
            public static readonly Block dandelion = new Block("dandelion");
            public static readonly Block poppy = new Block("poppy");
            public static readonly Block blue_orchid = new Block("blue_orchid");
            public static readonly Block allium = new Block("allium");
            public static readonly Block azure_bluet = new Block("azure_bluet");
            public static readonly Block red_tulip = new Block("red_tulip");
            public static readonly Block orange_tulip = new Block("orange_tulip");
            public static readonly Block white_tulip = new Block("white_tulip");
            public static readonly Block pink_tulip = new Block("pink_tulip");
            public static readonly Block oxeye_daisy = new Block("oxeye_daisy");
            public static readonly Block brown_mushroom = new Block("brown_mushroom");
            public static readonly Block red_mushroom = new Block("red_mushroom");
            public static readonly Block gold_block = new Block("gold_block");
            public static readonly Block iron_block = new Block("iron_block");
            public static readonly Block bricks = new Block("bricks");
            public static readonly Block tnt = new Block("tnt");
            public static readonly Block bookshelf = new Block("bookshelf");
            public static readonly Block mossy_cobblestone = new Block("mossy_cobblestone");
            public static readonly Block obsidian = new Block("obsidian");
            public static readonly Block torch = new Block("torch");
            public static readonly Block wall_torch = new Block("wall_torch");
            public static readonly Block fire = new Block("fire");
            public static readonly Block spawner = new Block("spawner");
            public static readonly Block oak_stairs = new Block("oak_stairs");
            public static readonly Block chest = new Block("chest");
            public static readonly Block redstone_wire = new Block("redstone_wire");
            public static readonly Block diamond_ore = new Block("diamond_ore");
            public static readonly Block diamond_block = new Block("diamond_block");
            public static readonly Block crafting_table = new Block("crafting_table");
            public static readonly Block wheat = new Block("wheat");
            public static readonly Block farmland = new Block("farmland");
            public static readonly Block furnace = new Block("furnace");
            public static readonly Block oak_door = new Block("oak_door");
            public static readonly Block ladder = new Block("ladder");
            public static readonly Block rail = new Block("rail");
            public static readonly Block cobblestone_stairs = new Block("cobblestone_stairs");
            public static readonly Block lever = new Block("lever");
            public static readonly Block stone_pressure_plate = new Block("stone_pressure_plate");
            public static readonly Block iron_door = new Block("iron_door");
            public static readonly Block oak_pressure_plate = new Block("oak_pressure_plate");
            public static readonly Block spruce_pressure_plate = new Block("spruce_pressure_plate");
            public static readonly Block birch_pressure_plate = new Block("birch_pressure_plate");
            public static readonly Block jungle_pressure_plate = new Block("jungle_pressure_plate");
            public static readonly Block acacia_pressure_plate = new Block("acacia_pressure_plate");
            public static readonly Block dark_oak_pressure_plate = new Block("dark_oak_pressure_plate");
            public static readonly Block redstone_ore = new Block("redstone_ore");
            public static readonly Block redstone_torch = new Block("redstone_torch");
            public static readonly Block redstone_wall_torch = new Block("redstone_wall_torch");
            public static readonly Block stone_button = new Block("stone_button");
            public static readonly Block snow = new Block("snow");
            public static readonly Block ice = new Block("ice");
            public static readonly Block snow_block = new Block("snow_block");
            public static readonly Block cactus = new Block("cactus");
            public static readonly Block clay = new Block("clay");
            public static readonly Block sugar_cane = new Block("sugar_cane");
            public static readonly Block jukebox = new Block("jukebox");
            public static readonly Block oak_fence = new Block("oak_fence");
            public static readonly Block pumpkin = new Block("pumpkin");
            public static readonly Block netherrack = new Block("netherrack");
            public static readonly Block soul_sand = new Block("soul_sand");
            public static readonly Block glowstone = new Block("glowstone");
            public static readonly Block nether_portal = new Block("nether_portal");
            public static readonly Block carved_pumpkin = new Block("carved_pumpkin");
            public static readonly Block jack_o_lantern = new Block("jack_o_lantern");
            public static readonly Block cake = new Block("cake");
            public static readonly Block repeater = new Block("repeater");
            public static readonly Block white_stained_glass = new Block("white_stained_glass");
            public static readonly Block orange_stained_glass = new Block("orange_stained_glass");
            public static readonly Block magenta_stained_glass = new Block("magenta_stained_glass");
            public static readonly Block light_blue_stained_glass = new Block("light_blue_stained_glass");
            public static readonly Block yellow_stained_glass = new Block("yellow_stained_glass");
            public static readonly Block lime_stained_glass = new Block("lime_stained_glass");
            public static readonly Block pink_stained_glass = new Block("pink_stained_glass");
            public static readonly Block gray_stained_glass = new Block("gray_stained_glass");
            public static readonly Block light_gray_stained_glass = new Block("light_gray_stained_glass");
            public static readonly Block cyan_stained_glass = new Block("cyan_stained_glass");
            public static readonly Block purple_stained_glass = new Block("purple_stained_glass");
            public static readonly Block blue_stained_glass = new Block("blue_stained_glass");
            public static readonly Block brown_stained_glass = new Block("brown_stained_glass");
            public static readonly Block green_stained_glass = new Block("green_stained_glass");
            public static readonly Block red_stained_glass = new Block("red_stained_glass");
            public static readonly Block black_stained_glass = new Block("black_stained_glass");
            public static readonly Block oak_trapdoor = new Block("oak_trapdoor");
            public static readonly Block spruce_trapdoor = new Block("spruce_trapdoor");
            public static readonly Block birch_trapdoor = new Block("birch_trapdoor");
            public static readonly Block jungle_trapdoor = new Block("jungle_trapdoor");
            public static readonly Block acacia_trapdoor = new Block("acacia_trapdoor");
            public static readonly Block dark_oak_trapdoor = new Block("dark_oak_trapdoor");
            public static readonly Block infested_stone = new Block("infested_stone");
            public static readonly Block infested_cobblestone = new Block("infested_cobblestone");
            public static readonly Block infested_stone_bricks = new Block("infested_stone_bricks");
            public static readonly Block infested_mossy_stone_bricks = new Block("infested_mossy_stone_bricks");
            public static readonly Block infested_cracked_stone_bricks = new Block("infested_cracked_stone_bricks");
            public static readonly Block infested_chiseled_stone_bricks = new Block("infested_chiseled_stone_bricks");
            public static readonly Block stone_bricks = new Block("stone_bricks");
            public static readonly Block mossy_stone_bricks = new Block("mossy_stone_bricks");
            public static readonly Block cracked_stone_bricks = new Block("cracked_stone_bricks");
            public static readonly Block chiseled_stone_bricks = new Block("chiseled_stone_bricks");
            public static readonly Block brown_mushroom_block = new Block("brown_mushroom_block");
            public static readonly Block red_mushroom_block = new Block("red_mushroom_block");
            public static readonly Block mushroom_stem = new Block("mushroom_stem");
            public static readonly Block iron_bars = new Block("iron_bars");
            public static readonly Block glass_pane = new Block("glass_pane");
            public static readonly Block melon = new Block("melon");
            public static readonly Block attached_pumpkin_stem = new Block("attached_pumpkin_stem");
            public static readonly Block attached_melon_stem = new Block("attached_melon_stem");
            public static readonly Block pumpkin_stem = new Block("pumpkin_stem");
            public static readonly Block melon_stem = new Block("melon_stem");
            public static readonly Block vine = new Block("vine");
            public static readonly Block oak_fence_gate = new Block("oak_fence_gate");
            public static readonly Block brick_stairs = new Block("brick_stairs");
            public static readonly Block stone_brick_stairs = new Block("stone_brick_stairs");
            public static readonly Block mycelium = new Block("mycelium");
            public static readonly Block lily_pad = new Block("lily_pad");
            public static readonly Block nether_bricks = new Block("nether_bricks");
            public static readonly Block nether_brick_fence = new Block("nether_brick_fence");
            public static readonly Block nether_brick_stairs = new Block("nether_brick_stairs");
            public static readonly Block nether_wart = new Block("nether_wart");
            public static readonly Block enchanting_table = new Block("enchanting_table");
            public static readonly Block brewing_stand = new Block("brewing_stand");
            public static readonly Block cauldron = new Block("cauldron");
            public static readonly Block end_portal = new Block("end_portal");
            public static readonly Block end_portal_frame = new Block("end_portal_frame");
            public static readonly Block end_stone = new Block("end_stone");
            public static readonly Block dragon_egg = new Block("dragon_egg");
            public static readonly Block redstone_lamp = new Block("redstone_lamp");
            public static readonly Block cocoa = new Block("cocoa");
            public static readonly Block sandstone_stairs = new Block("sandstone_stairs");
            public static readonly Block emerald_ore = new Block("emerald_ore");
            public static readonly Block ender_chest = new Block("ender_chest");
            public static readonly Block tripwire_hook = new Block("tripwire_hook");
            public static readonly Block tripwire = new Block("tripwire");
            public static readonly Block emerald_block = new Block("emerald_block");
            public static readonly Block spruce_stairs = new Block("spruce_stairs");
            public static readonly Block birch_stairs = new Block("birch_stairs");
            public static readonly Block jungle_stairs = new Block("jungle_stairs");
            public static readonly Block command_block = new Block("command_block");
            public static readonly Block beacon = new Block("beacon");
            public static readonly Block cobblestone_wall = new Block("cobblestone_wall");
            public static readonly Block flower_pot = new Block("flower_pot");
            public static readonly Block potted_oak_sapling = new Block("potted_oak_sapling");
            public static readonly Block potted_spruce_sapling = new Block("potted_spruce_sapling");
            public static readonly Block potted_birch_sapling = new Block("potted_birch_sapling");
            public static readonly Block potted_jungle_sapling = new Block("potted_jungle_sapling");
            public static readonly Block potted_acacia_sapling = new Block("potted_acacia_sapling");
            public static readonly Block potted_dark_oak_sapling = new Block("potted_dark_oak_sapling");
            public static readonly Block potted_fern = new Block("potted_fern");
            public static readonly Block potted_dandelion = new Block("potted_dandelion");
            public static readonly Block potted_poppy = new Block("potted_poppy");
            public static readonly Block potted_blue_orchid = new Block("potted_blue_orchid");
            public static readonly Block potted_allium = new Block("potted_allium");
            public static readonly Block potted_azure_bluet = new Block("potted_azure_bluet");
            public static readonly Block potted_red_tulip = new Block("potted_red_tulip");
            public static readonly Block potted_orange_tulip = new Block("potted_orange_tulip");
            public static readonly Block potted_white_tulip = new Block("potted_white_tulip");
            public static readonly Block potted_pink_tulip = new Block("potted_pink_tulip");
            public static readonly Block potted_oxeye_daisy = new Block("potted_oxeye_daisy");
            public static readonly Block potted_red_mushroom = new Block("potted_red_mushroom");
            public static readonly Block potted_brown_mushroom = new Block("potted_brown_mushroom");
            public static readonly Block potted_dead_bush = new Block("potted_dead_bush");
            public static readonly Block potted_cactus = new Block("potted_cactus");
            public static readonly Block carrots = new Block("carrots");
            public static readonly Block potatoes = new Block("potatoes");
            public static readonly Block oak_button = new Block("oak_button");
            public static readonly Block spruce_button = new Block("spruce_button");
            public static readonly Block birch_button = new Block("birch_button");
            public static readonly Block jungle_button = new Block("jungle_button");
            public static readonly Block acacia_button = new Block("acacia_button");
            public static readonly Block dark_oak_button = new Block("dark_oak_button");
            public static readonly Block skeleton_wall_skull = new Block("skeleton_wall_skull");
            public static readonly Block skeleton_skull = new Block("skeleton_skull");
            public static readonly Block wither_skeleton_wall_skull = new Block("wither_skeleton_wall_skull");
            public static readonly Block wither_skeleton_skull = new Block("wither_skeleton_skull");
            public static readonly Block zombie_wall_head = new Block("zombie_wall_head");
            public static readonly Block zombie_head = new Block("zombie_head");
            public static readonly Block player_wall_head = new Block("player_wall_head");
            public static readonly Block player_head = new Block("player_head");
            public static readonly Block creeper_wall_head = new Block("creeper_wall_head");
            public static readonly Block creeper_head = new Block("creeper_head");
            public static readonly Block dragon_wall_head = new Block("dragon_wall_head");
            public static readonly Block dragon_head = new Block("dragon_head");
            public static readonly Block anvil = new Block("anvil");
            public static readonly Block chipped_anvil = new Block("chipped_anvil");
            public static readonly Block damaged_anvil = new Block("damaged_anvil");
            public static readonly Block trapped_chest = new Block("trapped_chest");
            public static readonly Block light_weighted_pressure_plate = new Block("light_weighted_pressure_plate");
            public static readonly Block heavy_weighted_pressure_plate = new Block("heavy_weighted_pressure_plate");
            public static readonly Block comparator = new Block("comparator");
            public static readonly Block daylight_detector = new Block("daylight_detector");
            public static readonly Block redstone_block = new Block("redstone_block");
            public static readonly Block nether_quartz_ore = new Block("nether_quartz_ore");
            public static readonly Block hopper = new Block("hopper");
            public static readonly Block quartz_block = new Block("quartz_block");
            public static readonly Block chiseled_quartz_block = new Block("chiseled_quartz_block");
            public static readonly Block quartz_pillar = new Block("quartz_pillar");
            public static readonly Block quartz_stairs = new Block("quartz_stairs");
            public static readonly Block activator_rail = new Block("activator_rail");
            public static readonly Block dropper = new Block("dropper");
            public static readonly Block white_terracotta = new Block("white_terracotta");
            public static readonly Block orange_terracotta = new Block("orange_terracotta");
            public static readonly Block magenta_terracotta = new Block("magenta_terracotta");
            public static readonly Block light_blue_terracotta = new Block("light_blue_terracotta");
            public static readonly Block yellow_terracotta = new Block("yellow_terracotta");
            public static readonly Block lime_terracotta = new Block("lime_terracotta");
            public static readonly Block pink_terracotta = new Block("pink_terracotta");
            public static readonly Block gray_terracotta = new Block("gray_terracotta");
            public static readonly Block light_gray_terracotta = new Block("light_gray_terracotta");
            public static readonly Block cyan_terracotta = new Block("cyan_terracotta");
            public static readonly Block purple_terracotta = new Block("purple_terracotta");
            public static readonly Block blue_terracotta = new Block("blue_terracotta");
            public static readonly Block brown_terracotta = new Block("brown_terracotta");
            public static readonly Block green_terracotta = new Block("green_terracotta");
            public static readonly Block red_terracotta = new Block("red_terracotta");
            public static readonly Block black_terracotta = new Block("black_terracotta");
            public static readonly Block white_stained_glass_pane = new Block("white_stained_glass_pane");
            public static readonly Block orange_stained_glass_pane = new Block("orange_stained_glass_pane");
            public static readonly Block magenta_stained_glass_pane = new Block("magenta_stained_glass_pane");
            public static readonly Block light_blue_stained_glass_pane = new Block("light_blue_stained_glass_pane");
            public static readonly Block yellow_stained_glass_pane = new Block("yellow_stained_glass_pane");
            public static readonly Block lime_stained_glass_pane = new Block("lime_stained_glass_pane");
            public static readonly Block pink_stained_glass_pane = new Block("pink_stained_glass_pane");
            public static readonly Block gray_stained_glass_pane = new Block("gray_stained_glass_pane");
            public static readonly Block light_gray_stained_glass_pane = new Block("light_gray_stained_glass_pane");
            public static readonly Block cyan_stained_glass_pane = new Block("cyan_stained_glass_pane");
            public static readonly Block purple_stained_glass_pane = new Block("purple_stained_glass_pane");
            public static readonly Block blue_stained_glass_pane = new Block("blue_stained_glass_pane");
            public static readonly Block brown_stained_glass_pane = new Block("brown_stained_glass_pane");
            public static readonly Block green_stained_glass_pane = new Block("green_stained_glass_pane");
            public static readonly Block red_stained_glass_pane = new Block("red_stained_glass_pane");
            public static readonly Block black_stained_glass_pane = new Block("black_stained_glass_pane");
            public static readonly Block acacia_stairs = new Block("acacia_stairs");
            public static readonly Block dark_oak_stairs = new Block("dark_oak_stairs");
            public static readonly Block slime_block = new Block("slime_block");
            public static readonly Block barrier = new Block("barrier");
            public static readonly Block iron_trapdoor = new Block("iron_trapdoor");
            public static readonly Block prismarine = new Block("prismarine");
            public static readonly Block prismarine_bricks = new Block("prismarine_bricks");
            public static readonly Block dark_prismarine = new Block("dark_prismarine");
            public static readonly Block sea_lantern = new Block("sea_lantern");
            public static readonly Block hay_block = new Block("hay_block");
            public static readonly Block white_carpet = new Block("white_carpet");
            public static readonly Block orange_carpet = new Block("orange_carpet");
            public static readonly Block magenta_carpet = new Block("magenta_carpet");
            public static readonly Block light_blue_carpet = new Block("light_blue_carpet");
            public static readonly Block yellow_carpet = new Block("yellow_carpet");
            public static readonly Block lime_carpet = new Block("lime_carpet");
            public static readonly Block pink_carpet = new Block("pink_carpet");
            public static readonly Block gray_carpet = new Block("gray_carpet");
            public static readonly Block light_gray_carpet = new Block("light_gray_carpet");
            public static readonly Block cyan_carpet = new Block("cyan_carpet");
            public static readonly Block purple_carpet = new Block("purple_carpet");
            public static readonly Block blue_carpet = new Block("blue_carpet");
            public static readonly Block brown_carpet = new Block("brown_carpet");
            public static readonly Block green_carpet = new Block("green_carpet");
            public static readonly Block red_carpet = new Block("red_carpet");
            public static readonly Block black_carpet = new Block("black_carpet");
            public static readonly Block terracotta = new Block("terracotta");
            public static readonly Block coal_block = new Block("coal_block");
            public static readonly Block packed_ice = new Block("packed_ice");
            public static readonly Block sunflower = new Block("sunflower");
            public static readonly Block lilac = new Block("lilac");
            public static readonly Block rose_bush = new Block("rose_bush");
            public static readonly Block peony = new Block("peony");
            public static readonly Block tall_grass = new Block("tall_grass");
            public static readonly Block large_fern = new Block("large_fern");
            public static readonly Block white_banner = new Block("white_banner");
            public static readonly Block orange_banner = new Block("orange_banner");
            public static readonly Block magenta_banner = new Block("magenta_banner");
            public static readonly Block light_blue_banner = new Block("light_blue_banner");
            public static readonly Block yellow_banner = new Block("yellow_banner");
            public static readonly Block lime_banner = new Block("lime_banner");
            public static readonly Block pink_banner = new Block("pink_banner");
            public static readonly Block gray_banner = new Block("gray_banner");
            public static readonly Block light_gray_banner = new Block("light_gray_banner");
            public static readonly Block cyan_banner = new Block("cyan_banner");
            public static readonly Block purple_banner = new Block("purple_banner");
            public static readonly Block blue_banner = new Block("blue_banner");
            public static readonly Block brown_banner = new Block("brown_banner");
            public static readonly Block green_banner = new Block("green_banner");
            public static readonly Block red_banner = new Block("red_banner");
            public static readonly Block black_banner = new Block("black_banner");
            public static readonly Block white_wall_banner = new Block("white_wall_banner");
            public static readonly Block orange_wall_banner = new Block("orange_wall_banner");
            public static readonly Block magenta_wall_banner = new Block("magenta_wall_banner");
            public static readonly Block light_blue_wall_banner = new Block("light_blue_wall_banner");
            public static readonly Block yellow_wall_banner = new Block("yellow_wall_banner");
            public static readonly Block lime_wall_banner = new Block("lime_wall_banner");
            public static readonly Block pink_wall_banner = new Block("pink_wall_banner");
            public static readonly Block gray_wall_banner = new Block("gray_wall_banner");
            public static readonly Block light_gray_wall_banner = new Block("light_gray_wall_banner");
            public static readonly Block cyan_wall_banner = new Block("cyan_wall_banner");
            public static readonly Block purple_wall_banner = new Block("purple_wall_banner");
            public static readonly Block blue_wall_banner = new Block("blue_wall_banner");
            public static readonly Block brown_wall_banner = new Block("brown_wall_banner");
            public static readonly Block green_wall_banner = new Block("green_wall_banner");
            public static readonly Block red_wall_banner = new Block("red_wall_banner");
            public static readonly Block black_wall_banner = new Block("black_wall_banner");
            public static readonly Block red_sandstone = new Block("red_sandstone");
            public static readonly Block chiseled_red_sandstone = new Block("chiseled_red_sandstone");
            public static readonly Block cut_red_sandstone = new Block("cut_red_sandstone");
            public static readonly Block red_sandstone_stairs = new Block("red_sandstone_stairs");
            public static readonly Block oak_slab = new Block("oak_slab");
            public static readonly Block spruce_slab = new Block("spruce_slab");
            public static readonly Block birch_slab = new Block("birch_slab");
            public static readonly Block jungle_slab = new Block("jungle_slab");
            public static readonly Block acacia_slab = new Block("acacia_slab");
            public static readonly Block dark_oak_slab = new Block("dark_oak_slab");
            public static readonly Block sandstone_slab = new Block("sandstone_slab");
            public static readonly Block petrified_oak_slab = new Block("petrified_oak_slab");
            public static readonly Block cobblestone_slab = new Block("cobblestone_slab");
            public static readonly Block brick_slab = new Block("brick_slab");
            public static readonly Block stone_brick_slab = new Block("stone_brick_slab");
            public static readonly Block nether_brick_slab = new Block("nether_brick_slab");
            public static readonly Block quartz_slab = new Block("quartz_slab");
            public static readonly Block red_sandstone_slab = new Block("red_sandstone_slab");
            public static readonly Block purpur_slab = new Block("purpur_slab");
            public static readonly Block smooth_stone = new Block("smooth_stone");
            public static readonly Block smooth_sandstone = new Block("smooth_sandstone");
            public static readonly Block smooth_quartz = new Block("smooth_quartz");
            public static readonly Block smooth_red_sandstone = new Block("smooth_red_sandstone");
            public static readonly Block spruce_fence_gate = new Block("spruce_fence_gate");
            public static readonly Block birch_fence_gate = new Block("birch_fence_gate");
            public static readonly Block jungle_fence_gate = new Block("jungle_fence_gate");
            public static readonly Block acacia_fence_gate = new Block("acacia_fence_gate");
            public static readonly Block dark_oak_fence_gate = new Block("dark_oak_fence_gate");
            public static readonly Block spruce_fence = new Block("spruce_fence");
            public static readonly Block birch_fence = new Block("birch_fence");
            public static readonly Block jungle_fence = new Block("jungle_fence");
            public static readonly Block acacia_fence = new Block("acacia_fence");
            public static readonly Block dark_oak_fence = new Block("dark_oak_fence");
            public static readonly Block spruce_door = new Block("spruce_door");
            public static readonly Block birch_door = new Block("birch_door");
            public static readonly Block jungle_door = new Block("jungle_door");
            public static readonly Block acacia_door = new Block("acacia_door");
            public static readonly Block dark_oak_door = new Block("dark_oak_door");
            public static readonly Block end_rod = new Block("end_rod");
            public static readonly Block chorus_plant = new Block("chorus_plant");
            public static readonly Block chorus_flower = new Block("chorus_flower");
            public static readonly Block purpur_block = new Block("purpur_block");
            public static readonly Block purpur_pillar = new Block("purpur_pillar");
            public static readonly Block purpur_stairs = new Block("purpur_stairs");
            public static readonly Block end_stone_bricks = new Block("end_stone_bricks");
            public static readonly Block beetroots = new Block("beetroots");
            public static readonly Block grass_path = new Block("grass_path");
            public static readonly Block end_gateway = new Block("end_gateway");
            public static readonly Block repeating_command_block = new Block("repeating_command_block");
            public static readonly Block chain_command_block = new Block("chain_command_block");
            public static readonly Block frosted_ice = new Block("frosted_ice");
            public static readonly Block magma_block = new Block("magma_block");
            public static readonly Block nether_wart_block = new Block("nether_wart_block");
            public static readonly Block red_nether_bricks = new Block("red_nether_bricks");
            public static readonly Block bone_block = new Block("bone_block");
            public static readonly Block structure_void = new Block("structure_void");
            public static readonly Block observer = new Block("observer");
            public static readonly Block white_shulker_box = new Block("white_shulker_box");
            public static readonly Block orange_shulker_box = new Block("orange_shulker_box");
            public static readonly Block magenta_shulker_box = new Block("magenta_shulker_box");
            public static readonly Block light_blue_shulker_box = new Block("light_blue_shulker_box");
            public static readonly Block yellow_shulker_box = new Block("yellow_shulker_box");
            public static readonly Block lime_shulker_box = new Block("lime_shulker_box");
            public static readonly Block pink_shulker_box = new Block("pink_shulker_box");
            public static readonly Block gray_shulker_box = new Block("gray_shulker_box");
            public static readonly Block light_gray_shulker_box = new Block("light_gray_shulker_box");
            public static readonly Block cyan_shulker_box = new Block("cyan_shulker_box");
            public static readonly Block purple_shulker_box = new Block("purple_shulker_box");
            public static readonly Block blue_shulker_box = new Block("blue_shulker_box");
            public static readonly Block brown_shulker_box = new Block("brown_shulker_box");
            public static readonly Block green_shulker_box = new Block("green_shulker_box");
            public static readonly Block red_shulker_box = new Block("red_shulker_box");
            public static readonly Block black_shulker_box = new Block("black_shulker_box");
            public static readonly Block white_glazed_terracotta = new Block("white_glazed_terracotta");
            public static readonly Block orange_glazed_terracotta = new Block("orange_glazed_terracotta");
            public static readonly Block magenta_glazed_terracotta = new Block("magenta_glazed_terracotta");
            public static readonly Block light_blue_glazed_terracotta = new Block("light_blue_glazed_terracotta");
            public static readonly Block yellow_glazed_terracotta = new Block("yellow_glazed_terracotta");
            public static readonly Block lime_glazed_terracotta = new Block("lime_glazed_terracotta");
            public static readonly Block pink_glazed_terracotta = new Block("pink_glazed_terracotta");
            public static readonly Block gray_glazed_terracotta = new Block("gray_glazed_terracotta");
            public static readonly Block light_gray_glazed_terracotta = new Block("light_gray_glazed_terracotta");
            public static readonly Block cyan_glazed_terracotta = new Block("cyan_glazed_terracotta");
            public static readonly Block purple_glazed_terracotta = new Block("purple_glazed_terracotta");
            public static readonly Block blue_glazed_terracotta = new Block("blue_glazed_terracotta");
            public static readonly Block brown_glazed_terracotta = new Block("brown_glazed_terracotta");
            public static readonly Block green_glazed_terracotta = new Block("green_glazed_terracotta");
            public static readonly Block red_glazed_terracotta = new Block("red_glazed_terracotta");
            public static readonly Block black_glazed_terracotta = new Block("black_glazed_terracotta");
            public static readonly Block white_concrete = new Block("white_concrete");
            public static readonly Block orange_concrete = new Block("orange_concrete");
            public static readonly Block magenta_concrete = new Block("magenta_concrete");
            public static readonly Block light_blue_concrete = new Block("light_blue_concrete");
            public static readonly Block yellow_concrete = new Block("yellow_concrete");
            public static readonly Block lime_concrete = new Block("lime_concrete");
            public static readonly Block pink_concrete = new Block("pink_concrete");
            public static readonly Block gray_concrete = new Block("gray_concrete");
            public static readonly Block light_gray_concrete = new Block("light_gray_concrete");
            public static readonly Block cyan_concrete = new Block("cyan_concrete");
            public static readonly Block purple_concrete = new Block("purple_concrete");
            public static readonly Block blue_concrete = new Block("blue_concrete");
            public static readonly Block brown_concrete = new Block("brown_concrete");
            public static readonly Block green_concrete = new Block("green_concrete");
            public static readonly Block red_concrete = new Block("red_concrete");
            public static readonly Block black_concrete = new Block("black_concrete");
            public static readonly Block white_concrete_powder = new Block("white_concrete_powder");
            public static readonly Block orange_concrete_powder = new Block("orange_concrete_powder");
            public static readonly Block magenta_concrete_powder = new Block("magenta_concrete_powder");
            public static readonly Block light_blue_concrete_powder = new Block("light_blue_concrete_powder");
            public static readonly Block yellow_concrete_powder = new Block("yellow_concrete_powder");
            public static readonly Block lime_concrete_powder = new Block("lime_concrete_powder");
            public static readonly Block pink_concrete_powder = new Block("pink_concrete_powder");
            public static readonly Block gray_concrete_powder = new Block("gray_concrete_powder");
            public static readonly Block light_gray_concrete_powder = new Block("light_gray_concrete_powder");
            public static readonly Block cyan_concrete_powder = new Block("cyan_concrete_powder");
            public static readonly Block purple_concrete_powder = new Block("purple_concrete_powder");
            public static readonly Block blue_concrete_powder = new Block("blue_concrete_powder");
            public static readonly Block brown_concrete_powder = new Block("brown_concrete_powder");
            public static readonly Block green_concrete_powder = new Block("green_concrete_powder");
            public static readonly Block red_concrete_powder = new Block("red_concrete_powder");
            public static readonly Block black_concrete_powder = new Block("black_concrete_powder");
            public static readonly Block structure_block = new Block("structure_block");
        }

        public class Liquid : NamespacedEnumLike<string>, ILiquidType
        {
            public Liquid(string value, BasePackNamespace? @namespace = null) : base(value, @namespace)
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

            public string Name => ToString();

            public bool IsAGroup => false;

            public static readonly Liquid emtpty = new Liquid("empty");
            public static readonly Liquid lava = new Liquid("lava");
            public static readonly Liquid flowing_lava = new Liquid("flowing_lava");
            public static readonly Liquid water = new Liquid("water");
            public static readonly Liquid flowing_water = new Liquid("flowing_water");
        }
        public enum StructureRotation { NONE, CLOCKWISE_90, CLOCKWISE_180, COUNTERCLOCKWISE_90 }
        public enum StructureMirror { NONE, LEFT_RIGHT, FRONT_BACK }
        public enum StructureDataMode { DATA, SAVE, LOAD, CORNER }
        public enum StructureMode { data, save, load, corner }
        public enum FacingFull { down, up, north, south, west, east }
        public enum PistonType { normal, sticky }

        public enum StateWallConnection
        {
            /// <summary>
            /// Wall isn't connected
            /// </summary>
            none,
            /// <summary>
            /// Wall is connected. But no connection upwards.
            /// </summary>
            low,
            /// <summary>
            /// Wall is connected upwards
            /// </summary>
            tall,
        }

        public enum StateCompareMode
        {
            /// <summary>
            /// Outputs the signal if it's the greatest signal
            /// </summary>
            compare,
            /// <summary>
            /// Subtracts the side signal from the input signal
            /// </summary>
            subtract
        }
        public enum StatePortalAxis { x, z }
        public enum StateNoteInstrument { basedrum, xylophone, chime, guitar, bell, flute, bass, hat, snare, harp }
        public enum StateSlabPart { bottom, top, both }
        public enum StateHopperFacing { north, south, east, west, down }
        public enum StateChestType
        {
            /// <summary>
            /// Its a single chest
            /// </summary>
            single,
            /// <summary>
            /// Connected with the chest to the left
            /// </summary>
            left,
            /// <summary>
            /// Connected with the chest to the right
            /// </summary>
            right
        }
        public enum StateDoorHinge { left, right }
        public enum StatePlaced { wall, ceiling, floor }
        public enum StateBambooLeave { none, small, large }
        public enum StateBedPart { foot, head }
        public enum StateBellAttachment { ceiling, double_wall, floor, single_wall }
        public enum StatePart { lower, upper }
        public enum StateNote
        {
            /// <summary>
            /// F♯/G♭
            /// </summary>
            FSharp1,
            G1,
            /// <summary>
            /// G♯/A♭
            /// </summary>
            GSharp1,
            A1,
            /// <summary>
            /// A♯/B♭
            /// </summary>
            ASharp1,
            B1,
            C1,
            /// <summary>
            /// C♯/D♭
            /// </summary>
            CSharp1,
            D1,
            /// <summary>
            /// D♯/E♭
            /// </summary>
            DSharp1,
            E1,
            F1,
            /// <summary>
            /// F♯/G♭
            /// </summary>
            FSharp2,
            G2,
            /// <summary>
            /// G♯/A♭
            /// </summary>
            GSharp2,
            A2,
            /// <summary>
            /// A♯/B♭
            /// </summary>
            ASharp2,
            B2,
            C2,
            /// <summary>
            /// C♯/D♭
            /// </summary>
            CSharp2,
            D2,
            /// <summary>
            /// D♯/E♭
            /// </summary>
            DSharp2,
            E2,
            F2,
            /// <summary>
            /// F♯/G♭
            /// </summary>
            FSharp3,
        }
        public enum StateRailShape { east_west, north_east, north_south, north_west, south_east, south_west, ascending_east, ascending_north, ascending_south, ascending_west }
        public enum StateSpecailRailShape { east_west, north_south, ascending_east, ascending_north, ascending_south, ascending_west }
        public enum StateRedstoneConnection { none, side, up }
        public enum StateStairShape
        {
            /// <summary>
            /// 7/8 of a block.
            /// </summary>
            inner_left,
            /// <summary>
            /// 7/8 of a block.
            /// </summary>
            inner_right,
            /// <summary>
            /// 5/8 of a block.
            /// </summary>
            outer_left,
            /// <summary>
            /// 5/8 of a block.
            /// </summary>
            outer_right,
            /// <summary>
            /// 6/8 of a block.
            /// </summary>
            straight
        }
        public enum StateSimplePlaced { bottom, top }
        public enum Axis { x, y, z }
        public enum Facing { north, south, east, west }
        public enum JigsawOrientation
        {
            down_east,
            down_south,
            down_north,
            down_west,
            east_up,
            north_up,
            south_up,
            up_east,
            up_north,
            up_south,
            up_west,
            west_up
        }
        public enum JigsawJoint
        {
            /// <summary>
            /// If it can be rotated
            /// </summary>
            rollable,
            /// <summary>
            /// If it can't be rotated
            /// </summary>
            aligned
        }
        public enum BannerPattern
        {
            /// <summary>
            /// Bottom Stripe
            /// </summary>
            bs,
            /// <summary>
            /// Top Stripe
            /// </summary>
            ts,
            /// <summary>
            /// Left Stripe
            /// </summary>
            ls,
            /// <summary>
            /// Right Stripe
            /// </summary>
            rs,
            /// <summary>
            /// Center Stripe
            /// (bottom to top)
            /// </summary>
            cs,
            /// <summary>
            /// Middle Stripe
            /// (left to right)
            /// </summary>
            ms,
            /// <summary>
            /// Down Right Stripe
            /// (Starts at top left ends at bottom right)
            /// </summary>
            drs,
            /// <summary>
            /// Down Left Stripe
            /// (Starts at top right ends at bottom left)
            /// </summary>
            dls,
            /// <summary>
            /// Small Stripes
            /// (multiple lines going from bottom to top)
            /// </summary>
            ss,
            /// <summary>
            /// Cross
            /// (X)
            /// </summary>
            cr,
            /// <summary>
            /// Cross
            /// (+)
            /// </summary>
            sc,
            /// <summary>
            /// Left Top Diagonal
            /// (Goes from bottom left to top right with the left side being colored)
            /// </summary>
            ld,
            /// <summary>
            /// Right Top Diagonal
            /// (Goes from top left to bottom right with the right side being colored)
            /// </summary>
            rud,
            /// <summary>
            /// Left Bottom Diagonal
            /// (Goes from top left to bottom right with the left side being colored)
            /// </summary>
            lud,
            /// <summary>
            /// Right Bottom Diagonal
            /// (Goes from bottom left to top right with the right side being colored)
            /// </summary>
            rd,
            /// <summary>
            /// Vertical Half Left
            /// </summary>
            vh,
            /// <summary>
            /// Vertical Half Right
            /// </summary>
            vhr,
            /// <summary>
            /// Horizontal Half Top
            /// </summary>
            hh,
            /// <summary>
            /// Horizontal Half Bottom
            /// </summary>
            hhb,
            /// <summary>
            /// Bottom Left Square
            /// </summary>
            bl,
            /// <summary>
            /// Bottom Right Square
            /// </summary>
            br,
            /// <summary>
            /// top Left Square
            /// </summary>
            tl,
            /// <summary>
            /// Top Right Square
            /// </summary>
            tr,
            /// <summary>
            /// Bottom Triangle
            /// </summary>
            bt,
            /// <summary>
            /// Top Triangle
            /// </summary>
            tt,
            /// <summary>
            /// Multiple Bottom Triangles
            /// </summary>
            bts,
            /// <summary>
            /// Multiple Top Triangles
            /// </summary>
            tts,
            /// <summary>
            /// Middle Circle
            /// </summary>
            mc,
            /// <summary>
            /// Middle Rhombus
            /// </summary>
            mr,
            /// <summary>
            /// Border
            /// </summary>
            bo,
            /// <summary>
            /// Weird waving border
            /// </summary>
            cbo,
            /// <summary>
            /// Bricks
            /// </summary>
            bri,
            /// <summary>
            /// Top Gradiant
            /// (Solid at top and gone at bottom)
            /// </summary>
            gra,
            /// <summary>
            /// Bottom Gradiant
            /// (Solid at bottom and gone at top)
            /// </summary>
            gru,
            /// <summary>
            /// Creeper
            /// </summary>
            cre,
            /// <summary>
            /// Skull
            /// </summary>
            sku,
            /// <summary>
            /// Flower
            /// </summary>
            flo,
            /// <summary>
            /// Mojang
            /// </summary>
            moj
        }
#pragma warning restore 1591
    }
}
