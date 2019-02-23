namespace SharpCraft
{
    /// <summary>
    /// All the different ID's/Types/States things in the game can have
    /// </summary>
    public static partial class ID
    {
        /*
         Thanks to PepijnMC for making lists over some of the minecraft ID's:
        https://github.com/PepijnMC/Minecraft/tree/master/Data
        */

#pragma warning disable 1591
        public enum CoordType
        {
            /// <summary>
            /// A coordinate relative to another place
            /// </summary>
            Relative,
            /// <summary>
            /// A coordinate in a direction
            /// </summary>
            Local,
            /// <summary>
            /// A coordinate in the world
            /// </summary>
            Normal,
            /// <summary>
            /// Not used ingame. This coordinate type is for math.
            /// </summary>
            Vector,
            /// <summary>
            /// Used to mark a coordinate as a mix between relative and normal coords
            /// </summary>
            Mixed
        }
        public enum Gamemode
        {
            creative,
            survival,
            adventure,
            spectator
        }
        public enum Entity
        {
            fox,
            trader_llama,
            wandering_trader,
            cat,
            ravager,
            panda,
            pillager,
            lightning_bolt,
            dolphin,
            drowned,
            cod_mob,
            turtle,
            phantom,
            salmon_mob,
            pufferfish,
            tropical_fish,
            player,
            area_effect_cloud,
            armor_stand,
            arrow,
            bat,
            blaze,
            boat,
            cave_spider,
            chest_minecart,
            chicken,
            command_block_minecart,
            cow,
            creeper,
            donkey,
            dragon_fireball,
            egg,
            elder_guardian,
            end_crystal,
            ender_dragon,
            ender_pearl,
            enderman,
            endermite,
            evoker_fangs,
            evoker,
            eye_of_ender,
            falling_block,
            fireball,
            firework_rocket,
            furnace_minecart,
            ghast,
            giant,
            guardian,
            hopper_minecart,
            horse,
            husk,
            illusioner,
            item,
            item_frame,
            leash_knot,
            llama,
            llama_spit,
            magma_cube,
            minecart,
            mooshroom,
            mule,
            ocelot,
            painting,
            parrot,
            pig,
            polar_bear,
            potion,
            rabbit,
            sheep,
            shulker,
            shulker_bullet,
            silverfish,
            skeleton,
            skeleton_horse,
            slime,
            small_fireball,
            snowball,
            snow_golem,
            spawner_minecart,
            spectral_arrow,
            spider,
            squid,
            stray,
            tnt,
            tnt_minecart,
            vex,
            villager,
            iron_golem,
            vindicator,
            witch,
            wither,
            wither_skeleton,
            wither_skull,
            wolf,
            experience_bottle,
            experience_orb,
            zombie,
            zombie_horse,
            zombie_pigman,
            zombie_villager,
            EntityEnumEnd
        }
        public enum Block
        {
            composter,
            campfire,
            sweet_berry_bush,
            barrel,
            smoker,
            blast_furnace,
            cartography_table,
            fletching_table,
            grindstone,
            smithing_table,
            stonecutter,
            bell,
            lantern,
            lectern,
            jigsaw,
            scaffolding,
            polished_diorute_stairs,
            mossy_cobblestone_stairs,
            end_stone_brick_stairs,
            stone_stairs,
            smooth_sandstone_stairs,
            smooth_quartz_stairs,
            granite_stairs,
            andesite_stairs,
            red_nether_brick_stairs,
            polished_andesite_stairs,
            diorite_stairs,
            mossy_stone_brick_stairs,
            smooth_red_sandstone_stairs,
            polished_granite_stairs,
            polished_diorute_slab,
            mossy_cobblestone_slab,
            end_stone_brick_slab,
            stone_slab,
            smooth_sandstone_slab,
            smooth_quartz_slab,
            granite_slab,
            andesite_slab,
            red_nether_brick_slab,
            polished_andesite_slab,
            diorite_slab,
            mossy_stone_brick_slab,
            smooth_red_sandstone_slab,
            polished_granite_slab,
            smooth_stone_slab,
            polished_diorute_wall,
            mossy_cobblestone_wall,
            end_stone_brick_wall,
            stone_wall,
            smooth_sandstone_wall,
            smooth_quartz_wall,
            granite_wall,
            andesite_wall,
            red_nether_brick_wall,
            polished_andesite_wall,
            diorite_wall,
            mossy_stone_brick_wall,
            smooth_red_sandstone_wall,
            polished_granite_wall,
            prismarine_wall,
            nether_brick_wall,
            oak_sign,
            spruce_sign,
            birch_sign,
            acacia_sign,
            jungle_sign,
            dark_oak_sign,
            bamboo,
            cornflower,
            lily_of_the_valley,
            wither_rose,
            loom,

            oak_wall_sign,
            spruce_wall_sign,
            birch_wall_sign,
            acacia_wall_sign,
            jungle_wall_sign,
            dark_oak_wall_sign,
            bamboo_sapling,

            dead_tube_coral,
            dead_brain_coral,
            dead_bubble_coral,
            dead_fire_coral,
            dead_horn_coral,
            dead_tube_coral_fan,
            dead_brain_coral_fan,
            dead_bubble_coral_fan,
            dead_fire_coral_fan,
            dead_horn_coral_fan,
            dead_tube_coral_wall_fan,
            dead_brain_coral_wall_fan,
            dead_bubble_coral_wall_fan,
            dead_fire_coral_wall_fan,
            dead_horn_coral_wall_fan,
            tube_coral_wall_fan,
            brain_coral_wall_fan,
            bubble_coral_wall_fan,
            fire_coral_wall_fan,
            horn_coral_wall_fan,
            stripped_oak_wood,
            stripped_spruce_wood,
            stripped_birch_wood,
            stripped_jungle_wood,
            stripped_acacia_wood,
            stripped_dark_oak_wood,
            tall_seagrass,
            shulker_box,
            sea_pickle,
            conduit,
            dead_tube_coral_block,
            dead_brain_coral_block,
            dead_bubble_coral_block,
            dead_fire_coral_block,
            dead_horn_coral_block,
            tube_coral_block,
            brain_coral_block,
            bubble_coral_block,
            fire_coral_block,
            horn_coral_block,
            tube_coral,
            brain_coral,
            bubble_coral,
            fire_coral,
            horn_coral,
            tube_coral_fan,
            brain_coral_fan,
            bubble_coral_fan,
            fire_coral_fan,
            horn_coral_fan,
            blue_ice,
            kelp,
            kelp_plant,
            turtle_egg,
            seagrass,
            stripped_oak_log,
            stripped_spruce_log,
            stripped_birch_log,
            stripped_jungle_log,
            stripped_acacia_log,
            stripped_dark_oak_log,
            dried_kelp_block,
            cave_air,
            void_air,
            air,
            stone,
            granite,
            polished_granite,
            diorite,
            polished_diorite,
            andesite,
            polished_andesite,
            grass_block,
            dirt,
            coarse_dirt,
            podzol,
            cobblestone,
            oak_planks,
            spruce_planks,
            birch_planks,
            jungle_planks,
            acacia_planks,
            dark_oak_planks,
            oak_sapling,
            spruce_sapling,
            birch_sapling,
            jungle_sapling,
            acacia_sapling,
            dark_oak_sapling,
            bedrock,
            water,
            lava,
            sand,
            red_sand,
            gravel,
            gold_ore,
            iron_ore,
            coal_ore,
            oak_log,
            spruce_log,
            birch_log,
            jungle_log,
            acacia_log,
            dark_oak_log,
            oak_wood,
            spruce_wood,
            birch_wood,
            jungle_wood,
            acacia_wood,
            dark_oak_wood,
            oak_leaves,
            spruce_leaves,
            birch_leaves,
            jungle_leaves,
            acacia_leaves,
            dark_oak_leaves,
            sponge,
            wet_sponge,
            glass,
            lapis_ore,
            lapis_block,
            dispenser,
            sandstone,
            chiseled_sandstone,
            cut_sandstone,
            note_block,
            white_bed,
            orange_bed,
            magenta_bed,
            light_blue_bed,
            yellow_bed,
            lime_bed,
            pink_bed,
            gray_bed,
            light_gray_bed,
            cyan_bed,
            purple_bed,
            blue_bed,
            brown_bed,
            green_bed,
            red_bed,
            black_bed,
            powered_rail,
            detector_rail,
            sticky_piston,
            cobweb,
            grass,
            fern,
            dead_bush,
            piston,
            piston_head,
            white_wool,
            orange_wool,
            magenta_wool,
            light_blue_wool,
            yellow_wool,
            lime_wool,
            pink_wool,
            gray_wool,
            light_gray_wool,
            cyan_wool,
            purple_wool,
            blue_wool,
            brown_wool,
            green_wool,
            red_wool,
            black_wool,
            moving_piston,
            dandelion,
            poppy,
            blue_orchid,
            allium,
            azure_bluet,
            red_tulip,
            orange_tulip,
            white_tulip,
            pink_tulip,
            oxeye_daisy,
            brown_mushroom,
            red_mushroom,
            gold_block,
            iron_block,
            bricks,
            tnt,
            bookshelf,
            mossy_cobblestone,
            obsidian,
            torch,
            wall_torch,
            fire,
            spawner,
            oak_stairs,
            chest,
            redstone_wire,
            diamond_ore,
            diamond_block,
            crafting_table,
            wheat,
            farmland,
            furnace,
            oak_door,
            ladder,
            rail,
            cobblestone_stairs,
            lever,
            stone_pressure_plate,
            iron_door,
            oak_pressure_plate,
            spruce_pressure_plate,
            birch_pressure_plate,
            jungle_pressure_plate,
            acacia_pressure_plate,
            dark_oak_pressure_plate,
            redstone_ore,
            redstone_torch,
            redstone_wall_torch,
            stone_button,
            snow,
            ice,
            snow_block,
            cactus,
            clay,
            sugar_cane,
            jukebox,
            oak_fence,
            pumpkin,
            netherrack,
            soul_sand,
            glowstone,
            nether_portal,
            carved_pumpkin,
            jack_o_lantern,
            cake,
            repeater,
            white_stained_glass,
            orange_stained_glass,
            magenta_stained_glass,
            light_blue_stained_glass,
            yellow_stained_glass,
            lime_stained_glass,
            pink_stained_glass,
            gray_stained_glass,
            light_gray_stained_glass,
            cyan_stained_glass,
            purple_stained_glass,
            blue_stained_glass,
            brown_stained_glass,
            green_stained_glass,
            red_stained_glass,
            black_stained_glass,
            oak_trapdoor,
            spruce_trapdoor,
            birch_trapdoor,
            jungle_trapdoor,
            acacia_trapdoor,
            dark_oak_trapdoor,
            infested_stone,
            infested_cobblestone,
            infested_stone_bricks,
            infested_mossy_stone_bricks,
            infested_cracked_stone_bricks,
            infested_chiseled_stone_bricks,
            stone_bricks,
            mossy_stone_bricks,
            cracked_stone_bricks,
            chiseled_stone_bricks,
            brown_mushroom_block,
            red_mushroom_block,
            mushroom_stem,
            iron_bars,
            glass_pane,
            melon,
            attached_pumpkin_stem,
            attached_melon_stem,
            pumpkin_stem,
            melon_stem,
            vine,
            oak_fence_gate,
            brick_stairs,
            stone_brick_stairs,
            mycelium,
            lily_pad,
            nether_bricks,
            nether_brick_fence,
            nether_brick_stairs,
            nether_wart,
            enchanting_table,
            brewing_stand,
            cauldron,
            end_portal,
            end_portal_frame,
            end_stone,
            dragon_egg,
            redstone_lamp,
            cocoa,
            sandstone_stairs,
            emerald_ore,
            ender_chest,
            tripwire_hook,
            tripwire,
            emerald_block,
            spruce_stairs,
            birch_stairs,
            jungle_stairs,
            command_block,
            beacon,
            cobblestone_wall,
            flower_pot,
            potted_oak_sapling,
            potted_spruce_sapling,
            potted_birch_sapling,
            potted_jungle_sapling,
            potted_acacia_sapling,
            potted_dark_oak_sapling,
            potted_fern,
            potted_dandelion,
            potted_poppy,
            potted_blue_orchid,
            potted_allium,
            potted_azure_bluet,
            potted_red_tulip,
            potted_orange_tulip,
            potted_white_tulip,
            potted_pink_tulip,
            potted_oxeye_daisy,
            potted_red_mushroom,
            potted_brown_mushroom,
            potted_dead_bush,
            potted_cactus,
            carrots,
            potatoes,
            oak_button,
            spruce_button,
            birch_button,
            jungle_button,
            acacia_button,
            dark_oak_button,
            skeleton_wall_skull,
            skeleton_skull,
            wither_skeleton_wall_skull,
            wither_skeleton_skull,
            zombie_wall_head,
            zombie_head,
            player_wall_head,
            player_head,
            creeper_wall_head,
            creeper_head,
            dragon_wall_head,
            dragon_head,
            anvil,
            chipped_anvil,
            damaged_anvil,
            trapped_chest,
            light_weighted_pressure_plate,
            heavy_weighted_pressure_plate,
            comparator,
            daylight_detector,
            redstone_block,
            nether_quartz_ore,
            hopper,
            quartz_block,
            chiseled_quartz_block,
            quartz_pillar,
            quartz_stairs,
            activator_rail,
            dropper,
            white_terracotta,
            orange_terracotta,
            magenta_terracotta,
            light_blue_terracotta,
            yellow_terracotta,
            lime_terracotta,
            pink_terracotta,
            gray_terracotta,
            light_gray_terracotta,
            cyan_terracotta,
            purple_terracotta,
            blue_terracotta,
            brown_terracotta,
            green_terracotta,
            red_terracotta,
            black_terracotta,
            white_stained_glass_pane,
            orange_stained_glass_pane,
            magenta_stained_glass_pane,
            light_blue_stained_glass_pane,
            yellow_stained_glass_pane,
            lime_stained_glass_pane,
            pink_stained_glass_pane,
            gray_stained_glass_pane,
            light_gray_stained_glass_pane,
            cyan_stained_glass_pane,
            purple_stained_glass_pane,
            blue_stained_glass_pane,
            brown_stained_glass_pane,
            green_stained_glass_pane,
            red_stained_glass_pane,
            black_stained_glass_pane,
            acacia_stairs,
            dark_oak_stairs,
            slime_block,
            barrier,
            iron_trapdoor,
            prismarine,
            prismarine_bricks,
            dark_prismarine,
            sea_lantern,
            hay_block,
            white_carpet,
            orange_carpet,
            magenta_carpet,
            light_blue_carpet,
            yellow_carpet,
            lime_carpet,
            pink_carpet,
            gray_carpet,
            light_gray_carpet,
            cyan_carpet,
            purple_carpet,
            blue_carpet,
            brown_carpet,
            green_carpet,
            red_carpet,
            black_carpet,
            terracotta,
            coal_block,
            packed_ice,
            sunflower,
            lilac,
            rose_bush,
            peony,
            tall_grass,
            large_fern,
            white_banner,
            orange_banner,
            magenta_banner,
            light_blue_banner,
            yellow_banner,
            lime_banner,
            pink_banner,
            gray_banner,
            light_gray_banner,
            cyan_banner,
            purple_banner,
            blue_banner,
            brown_banner,
            green_banner,
            red_banner,
            black_banner,
            white_wall_banner,
            orange_wall_banner,
            magenta_wall_banner,
            light_blue_wall_banner,
            yellow_wall_banner,
            lime_wall_banner,
            pink_wall_banner,
            gray_wall_banner,
            light_gray_wall_banner,
            cyan_wall_banner,
            purple_wall_banner,
            blue_wall_banner,
            brown_wall_banner,
            green_wall_banner,
            red_wall_banner,
            black_wall_banner,
            red_sandstone,
            chiseled_red_sandstone,
            cut_red_sandstone,
            red_sandstone_stairs,
            oak_slab,
            spruce_slab,
            birch_slab,
            jungle_slab,
            acacia_slab,
            dark_oak_slab,
            sandstone_slab,
            petrified_oak_slab,
            cobblestone_slab,
            brick_slab,
            stone_brick_slab,
            nether_brick_slab,
            quartz_slab,
            red_sandstone_slab,
            purpur_slab,
            smooth_stone,
            smooth_sandstone,
            smooth_quartz,
            smooth_red_sandstone,
            spruce_fence_gate,
            birch_fence_gate,
            jungle_fence_gate,
            acacia_fence_gate,
            dark_oak_fence_gate,
            spruce_fence,
            birch_fence,
            jungle_fence,
            acacia_fence,
            dark_oak_fence,
            spruce_door,
            birch_door,
            jungle_door,
            acacia_door,
            dark_oak_door,
            end_rod,
            chorus_plant,
            chorus_flower,
            purpur_block,
            purpur_pillar,
            purpur_stairs,
            end_stone_bricks,
            beetroots,
            grass_path,
            end_gateway,
            repeating_command_block,
            chain_command_block,
            frosted_ice,
            magma_block,
            nether_wart_block,
            red_nether_bricks,
            bone_block,
            structure_void,
            observer,
            white_shulker_box,
            orange_shulker_box,
            magenta_shulker_box,
            light_blue_shulker_box,
            yellow_shulker_box,
            lime_shulker_box,
            pink_shulker_box,
            gray_shulker_box,
            light_gray_shulker_box,
            cyan_shulker_box,
            purple_shulker_box,
            blue_shulker_box,
            brown_shulker_box,
            green_shulker_box,
            red_shulker_box,
            black_shulker_box,
            white_glazed_terracotta,
            orange_glazed_terracotta,
            magenta_glazed_terracotta,
            light_blue_glazed_terracotta,
            yellow_glazed_terracotta,
            lime_glazed_terracotta,
            pink_glazed_terracotta,
            gray_glazed_terracotta,
            light_gray_glazed_terracotta,
            cyan_glazed_terracotta,
            purple_glazed_terracotta,
            blue_glazed_terracotta,
            brown_glazed_terracotta,
            green_glazed_terracotta,
            red_glazed_terracotta,
            black_glazed_terracotta,
            white_concrete,
            orange_concrete,
            magenta_concrete,
            light_blue_concrete,
            yellow_concrete,
            lime_concrete,
            pink_concrete,
            gray_concrete,
            light_gray_concrete,
            cyan_concrete,
            purple_concrete,
            blue_concrete,
            brown_concrete,
            green_concrete,
            red_concrete,
            black_concrete,
            white_concrete_powder,
            orange_concrete_powder,
            magenta_concrete_powder,
            light_blue_concrete_powder,
            yellow_concrete_powder,
            lime_concrete_powder,
            pink_concrete_powder,
            gray_concrete_powder,
            light_gray_concrete_powder,
            cyan_concrete_powder,
            purple_concrete_powder,
            blue_concrete_powder,
            brown_concrete_powder,
            green_concrete_powder,
            red_concrete_powder,
            black_concrete_powder,
            structure_block,
            BlockEnumEnd,
        }
        public enum Item
        {
            fox_spawn_egg,
            composter,
            campfire,
            sweet_berries,
            barrel,
            smoker,
            blast_furnace,
            cartography_table,
            fletching_table,
            grindstone,
            smithing_table,
            stonecutter,
            bell,
            lantern,
            lectern,
            jigsaw,
            scaffolding,
            dead_tube_coral,
            dead_brain_coral,
            dead_bubble_coral,
            dead_fire_coral,
            dead_horn_coral,
            cat_spawn_egg,
            illager_beast_spawn_egg,
            panda_spawn_egg,
            pillager_spawn_egg,
            suspicious_stew,
            crossbow,
            blue_dye,
            black_dye,
            brown_dye,
            white_dye,
            flower_banner_pattern,
            creeper_banner_pattern,
            skull_banner_pattern,
            mojang_banner_pattern,
            polished_diorute_stairs,
            mossy_cobblestone_stairs,
            end_stone_brick_stairs,
            stone_stairs,
            smooth_sandstone_stairs,
            smooth_quartz_stairs,
            granite_stairs,
            andesite_stairs,
            red_nether_brick_stairs,
            polished_andesite_stairs,
            diorite_stairs,
            mossy_stone_brick_stairs,
            smooth_red_sandstone_stairs,
            polished_granite_stairs,
            polished_diorute_slab,
            mossy_cobblestone_slab,
            end_stone_brick_slab,
            stone_slab,
            smooth_sandstone_slab,
            smooth_quartz_slab,
            granite_slab,
            andesite_slab,
            red_nether_brick_slab,
            polished_andesite_slab,
            diorite_slab,
            mossy_stone_brick_slab,
            smooth_red_sandstone_slab,
            polished_granite_slab,
            smooth_stone_slab,
            polished_diorute_wall,
            mossy_cobblestone_wall,
            end_stone_brick_wall,
            stone_wall,
            smooth_sandstone_wall,
            smooth_quartz_wall,
            granite_wall,
            andesite_wall,
            red_nether_brick_wall,
            polished_andesite_wall,
            diorite_wall,
            mossy_stone_brick_wall,
            smooth_red_sandstone_wall,
            polished_granite_wall,
            prismarine_wall,
            nether_brick_wall,
            oak_sign,
            spruce_sign,
            birch_sign,
            acacia_sign,
            jungle_sign,
            dark_oak_sign,
            bamboo,
            cornflower,
            lily_of_the_valley,
            wither_rose,
            loom,
            stripped_oak_wood,
            stripped_spruce_wood,
            stripped_birch_wood,
            stripped_jungle_wood,
            stripped_acacia_wood,
            stripped_dark_oak_wood,
            dead_tube_coral_fan,
            dead_brain_coral_fan,
            dead_bubble_coral_fan,
            dead_fire_coral_fan,
            dead_horn_coral_fan,
            shulker_box,
            phantom_membrane,
            sea_pickle,
            nautilus_shell,
            heart_of_the_sea,
            drowned_spawn_egg,
            dolphin_spawn_egg,
            conduit,
            dead_tube_coral_block,
            dead_brain_coral_block,
            dead_bubble_coral_block,
            dead_fire_coral_block,
            dead_horn_coral_block,
            tube_coral_block,
            brain_coral_block,
            bubble_coral_block,
            fire_coral_block,
            horn_coral_block,
            tube_coral,
            brain_coral,
            bubble_coral,
            fire_coral,
            horn_coral,
            tube_coral_fan,
            brain_coral_fan,
            bubble_coral_fan,
            fire_coral_fan,
            horn_coral_fan,
            blue_ice,
            kelp,
            turtle_egg,
            seagrass,
            stripped_oak_log,
            stripped_spruce_log,
            stripped_birch_log,
            stripped_jungle_log,
            stripped_acacia_log,
            stripped_dark_oak_log,
            dried_kelp_block,
            tropical_fish_bucket,
            cod_bucket,
            salmon_bucket,
            pufferfish_bucket,
            cod_spawn_egg,
            salmon_spawn_egg,
            pufferfish_spawn_egg,
            tropical_fish_spawn_egg,
            phantom_spawn_egg,
            turtle_spawn_egg,
            turtle_helmet,
            scute,
            trident,
            dried_kelp,
            air,
            String,
            stone,
            granite,
            polished_granite,
            diorite,
            polished_diorite,
            andesite,
            polished_andesite,
            grass_block,
            dirt,
            coarse_dirt,
            podzol,
            cobblestone,
            oak_planks,
            spruce_planks,
            birch_planks,
            jungle_planks,
            acacia_planks,
            dark_oak_planks,
            oak_sapling,
            spruce_sapling,
            birch_sapling,
            jungle_sapling,
            acacia_sapling,
            dark_oak_sapling,
            bedrock,
            sand,
            red_sand,
            gravel,
            gold_ore,
            iron_ore,
            coal_ore,
            oak_log,
            spruce_log,
            birch_log,
            jungle_log,
            acacia_log,
            dark_oak_log,
            oak_wood,
            spruce_wood,
            birch_wood,
            jungle_wood,
            acacia_wood,
            dark_oak_wood,
            oak_leaves,
            spruce_leaves,
            birch_leaves,
            jungle_leaves,
            acacia_leaves,
            dark_oak_leaves,
            sponge,
            wet_sponge,
            glass,
            lapis_ore,
            lapis_block,
            dispenser,
            sandstone,
            chiseled_sandstone,
            cut_sandstone,
            note_block,
            powered_rail,
            detector_rail,
            sticky_piston,
            cobweb,
            grass,
            fern,
            dead_bush,
            piston,
            white_wool,
            orange_wool,
            magenta_wool,
            light_blue_wool,
            yellow_wool,
            lime_wool,
            pink_wool,
            gray_wool,
            light_gray_wool,
            cyan_wool,
            purple_wool,
            blue_wool,
            brown_wool,
            green_wool,
            red_wool,
            black_wool,
            dandelion,
            poppy,
            blue_orchid,
            allium,
            azure_bluet,
            red_tulip,
            orange_tulip,
            white_tulip,
            pink_tulip,
            oxeye_daisy,
            brown_mushroom,
            red_mushroom,
            gold_block,
            iron_block,
            oak_slab,
            spruce_slab,
            birch_slab,
            jungle_slab,
            acacia_slab,
            dark_oak_slab,
            sandstone_slab,
            petrified_oak_slab,
            cobblestone_slab,
            brick_slab,
            stone_brick_slab,
            nether_brick_slab,
            quartz_slab,
            red_sandstone_slab,
            purpur_slab,
            smooth_quartz,
            smooth_red_sandstone,
            smooth_sandstone,
            smooth_stone,
            bricks,
            tnt,
            bookshelf,
            mossy_cobblestone,
            obsidian,
            torch,
            end_rod,
            chorus_plant,
            chorus_flower,
            purpur_block,
            purpur_pillar,
            purpur_stairs,
            spawner,
            oak_stairs,
            chest,
            diamond_ore,
            diamond_block,
            crafting_table,
            farmland,
            furnace,
            ladder,
            rail,
            cobblestone_stairs,
            lever,
            stone_pressure_plate,
            oak_pressure_plate,
            spruce_pressure_plate,
            birch_pressure_plate,
            jungle_pressure_plate,
            acacia_pressure_plate,
            dark_oak_pressure_plate,
            redstone_ore,
            redstone_torch,
            stone_button,
            snow,
            ice,
            snow_block,
            cactus,
            clay,
            jukebox,
            oak_fence,
            spruce_fence,
            birch_fence,
            jungle_fence,
            acacia_fence,
            dark_oak_fence,
            pumpkin,
            carved_pumpkin,
            netherrack,
            soul_sand,
            glowstone,
            jack_o_lantern,
            oak_trapdoor,
            spruce_trapdoor,
            birch_trapdoor,
            jungle_trapdoor,
            acacia_trapdoor,
            dark_oak_trapdoor,
            infested_stone,
            infested_cobblestone,
            infested_stone_bricks,
            infested_mossy_stone_bricks,
            infested_cracked_stone_bricks,
            infested_chiseled_stone_bricks,
            stone_bricks,
            mossy_stone_bricks,
            cracked_stone_bricks,
            chiseled_stone_bricks,
            brown_mushroom_block,
            red_mushroom_block,
            mushroom_stem,
            iron_bars,
            glass_pane,
            melon,
            vine,
            oak_fence_gate,
            spruce_fence_gate,
            birch_fence_gate,
            jungle_fence_gate,
            acacia_fence_gate,
            dark_oak_fence_gate,
            brick_stairs,
            stone_brick_stairs,
            mycelium,
            lily_pad,
            nether_bricks,
            nether_brick_fence,
            nether_brick_stairs,
            enchanting_table,
            end_portal_frame,
            end_stone,
            end_stone_bricks,
            dragon_egg,
            redstone_lamp,
            sandstone_stairs,
            emerald_ore,
            ender_chest,
            tripwire_hook,
            emerald_block,
            spruce_stairs,
            birch_stairs,
            jungle_stairs,
            command_block,
            beacon,
            cobblestone_wall,
            oak_button,
            spruce_button,
            birch_button,
            jungle_button,
            acacia_button,
            dark_oak_button,
            anvil,
            chipped_anvil,
            damaged_anvil,
            trapped_chest,
            light_weighted_pressure_plate,
            heavy_weighted_pressure_plate,
            daylight_detector,
            redstone_block,
            nether_quartz_ore,
            hopper,
            chiseled_quartz_block,
            quartz_block,
            quartz_pillar,
            quartz_stairs,
            activator_rail,
            dropper,
            white_terracotta,
            orange_terracotta,
            magenta_terracotta,
            light_blue_terracotta,
            yellow_terracotta,
            lime_terracotta,
            pink_terracotta,
            gray_terracotta,
            light_gray_terracotta,
            cyan_terracotta,
            purple_terracotta,
            blue_terracotta,
            brown_terracotta,
            green_terracotta,
            red_terracotta,
            black_terracotta,
            barrier,
            iron_trapdoor,
            hay_block,
            white_carpet,
            orange_carpet,
            magenta_carpet,
            light_blue_carpet,
            yellow_carpet,
            lime_carpet,
            pink_carpet,
            gray_carpet,
            light_gray_carpet,
            cyan_carpet,
            purple_carpet,
            blue_carpet,
            brown_carpet,
            green_carpet,
            red_carpet,
            black_carpet,
            terracotta,
            coal_block,
            packed_ice,
            acacia_stairs,
            dark_oak_stairs,
            slime_block,
            grass_path,
            sunflower,
            lilac,
            rose_bush,
            peony,
            tall_grass,
            large_fern,
            white_stained_glass,
            orange_stained_glass,
            magenta_stained_glass,
            light_blue_stained_glass,
            yellow_stained_glass,
            lime_stained_glass,
            pink_stained_glass,
            gray_stained_glass,
            light_gray_stained_glass,
            cyan_stained_glass,
            purple_stained_glass,
            blue_stained_glass,
            brown_stained_glass,
            green_stained_glass,
            red_stained_glass,
            black_stained_glass,
            white_stained_glass_pane,
            orange_stained_glass_pane,
            magenta_stained_glass_pane,
            light_blue_stained_glass_pane,
            yellow_stained_glass_pane,
            lime_stained_glass_pane,
            pink_stained_glass_pane,
            gray_stained_glass_pane,
            light_gray_stained_glass_pane,
            cyan_stained_glass_pane,
            purple_stained_glass_pane,
            blue_stained_glass_pane,
            brown_stained_glass_pane,
            green_stained_glass_pane,
            red_stained_glass_pane,
            black_stained_glass_pane,
            prismarine,
            prismarine_bricks,
            dark_prismarine,
            sea_lantern,
            red_sandstone,
            chiseled_red_sandstone,
            cut_red_sandstone,
            red_sandstone_stairs,
            repeating_command_block,
            chain_command_block,
            magma_block,
            nether_wart_block,
            red_nether_bricks,
            bone_block,
            structure_void,
            observer,
            white_shulker_box,
            orange_shulker_box,
            magenta_shulker_box,
            light_blue_shulker_box,
            yellow_shulker_box,
            lime_shulker_box,
            pink_shulker_box,
            gray_shulker_box,
            light_gray_shulker_box,
            cyan_shulker_box,
            purple_shulker_box,
            blue_shulker_box,
            brown_shulker_box,
            green_shulker_box,
            red_shulker_box,
            black_shulker_box,
            white_glazed_terracotta,
            orange_glazed_terracotta,
            magenta_glazed_terracotta,
            light_blue_glazed_terracotta,
            yellow_glazed_terracotta,
            lime_glazed_terracotta,
            pink_glazed_terracotta,
            gray_glazed_terracotta,
            light_gray_glazed_terracotta,
            cyan_glazed_terracotta,
            purple_glazed_terracotta,
            blue_glazed_terracotta,
            brown_glazed_terracotta,
            green_glazed_terracotta,
            red_glazed_terracotta,
            black_glazed_terracotta,
            white_concrete,
            orange_concrete,
            magenta_concrete,
            light_blue_concrete,
            yellow_concrete,
            lime_concrete,
            pink_concrete,
            gray_concrete,
            light_gray_concrete,
            cyan_concrete,
            purple_concrete,
            blue_concrete,
            brown_concrete,
            green_concrete,
            red_concrete,
            black_concrete,
            white_concrete_powder,
            orange_concrete_powder,
            magenta_concrete_powder,
            light_blue_concrete_powder,
            yellow_concrete_powder,
            lime_concrete_powder,
            pink_concrete_powder,
            gray_concrete_powder,
            light_gray_concrete_powder,
            cyan_concrete_powder,
            purple_concrete_powder,
            blue_concrete_powder,
            brown_concrete_powder,
            green_concrete_powder,
            red_concrete_powder,
            black_concrete_powder,
            iron_door,
            oak_door,
            spruce_door,
            birch_door,
            jungle_door,
            acacia_door,
            dark_oak_door,
            repeater,
            comparator,
            structure_block,
            iron_shovel,
            iron_pickaxe,
            iron_axe,
            flint_and_steel,
            apple,
            bow,
            arrow,
            coal,
            charcoal,
            diamond,
            iron_ingot,
            gold_ingot,
            iron_sword,
            wooden_sword,
            wooden_shovel,
            wooden_pickaxe,
            wooden_axe,
            stone_sword,
            stone_shovel,
            stone_pickaxe,
            stone_axe,
            diamond_sword,
            diamond_shovel,
            diamond_pickaxe,
            diamond_axe,
            stick,
            bowl,
            mushroom_stew,
            golden_sword,
            golden_shovel,
            golden_pickaxe,
            golden_axe,
            feather,
            gunpowder,
            wooden_hoe,
            stone_hoe,
            iron_hoe,
            diamond_hoe,
            golden_hoe,
            wheat_seeds,
            wheat,
            bread,
            leather_helmet,
            leather_chestplate,
            leather_leggings,
            leather_boots,
            chainmail_helmet,
            chainmail_chestplate,
            chainmail_leggings,
            chainmail_boots,
            iron_helmet,
            iron_chestplate,
            iron_leggings,
            iron_boots,
            diamond_helmet,
            diamond_chestplate,
            diamond_leggings,
            diamond_boots,
            golden_helmet,
            golden_chestplate,
            golden_leggings,
            golden_boots,
            flint,
            porkchop,
            cooked_porkchop,
            painting,
            golden_apple,
            enchanted_golden_apple,
            bucket,
            water_bucket,
            lava_bucket,
            minecart,
            saddle,
            redstone,
            snowball,
            oak_boat,
            leather,
            milk_bucket,
            brick,
            clay_ball,
            sugar_cane,
            paper,
            book,
            slime_ball,
            chest_minecart,
            furnace_minecart,
            egg,
            compass,
            fishing_rod,
            clock,
            glowstone_dust,
            cod,
            salmon,
            tropical_fish,
            pufferfish,
            cooked_cod,
            cooked_salmon,
            ink_sac,
            rose_red,
            cactus_green,
            cocoa_beans,
            lapis_lazuli,
            purple_dye,
            cyan_dye,
            light_gray_dye,
            gray_dye,
            pink_dye,
            lime_dye,
            dandelion_yellow,
            light_blue_dye,
            magenta_dye,
            orange_dye,
            bone_meal,
            bone,
            sugar,
            cake,
            white_bed,
            orange_bed,
            magenta_bed,
            light_blue_bed,
            yellow_bed,
            lime_bed,
            pink_bed,
            gray_bed,
            light_gray_bed,
            cyan_bed,
            purple_bed,
            blue_bed,
            brown_bed,
            green_bed,
            red_bed,
            black_bed,
            cookie,
            filled_map,
            shears,
            melon_slice,
            pumpkin_seeds,
            melon_seeds,
            beef,
            cooked_beef,
            chicken,
            cooked_chicken,
            rotten_flesh,
            ender_pearl,
            blaze_rod,
            ghast_tear,
            gold_nugget,
            nether_wart,
            potion,
            glass_bottle,
            spider_eye,
            fermented_spider_eye,
            blaze_powder,
            magma_cream,
            brewing_stand,
            cauldron,
            ender_eye,
            glistering_melon_slice,
            bat_spawn_egg,
            blaze_spawn_egg,
            cave_spider_spawn_egg,
            chicken_spawn_egg,
            cow_spawn_egg,
            creeper_spawn_egg,
            donkey_spawn_egg,
            elder_guardian_spawn_egg,
            enderman_spawn_egg,
            endermite_spawn_egg,
            evoker_spawn_egg,
            ghast_spawn_egg,
            guardian_spawn_egg,
            horse_spawn_egg,
            husk_spawn_egg,
            llama_spawn_egg,
            magma_cube_spawn_egg,
            mooshroom_spawn_egg,
            mule_spawn_egg,
            ocelot_spawn_egg,
            parrot_spawn_egg,
            pig_spawn_egg,
            polar_bear_spawn_egg,
            rabbit_spawn_egg,
            sheep_spawn_egg,
            shulker_spawn_egg,
            silverfish_spawn_egg,
            skeleton_spawn_egg,
            skeleton_horse_spawn_egg,
            slime_spawn_egg,
            spider_spawn_egg,
            squid_spawn_egg,
            stray_spawn_egg,
            vex_spawn_egg,
            villager_spawn_egg,
            vindicator_spawn_egg,
            witch_spawn_egg,
            wither_skeleton_spawn_egg,
            wolf_spawn_egg,
            zombie_spawn_egg,
            zombie_horse_spawn_egg,
            zombie_pigman_spawn_egg,
            zombie_villager_spawn_egg,
            experience_bottle,
            fire_charge,
            writable_book,
            written_book,
            emerald,
            item_frame,
            flower_pot,
            carrot,
            potato,
            baked_potato,
            poisonous_potato,
            map,
            golden_carrot,
            skeleton_skull,
            wither_skeleton_skull,
            player_head,
            zombie_head,
            creeper_head,
            dragon_head,
            carrot_on_a_stick,
            nether_star,
            pumpkin_pie,
            firework_rocket,
            firework_star,
            enchanted_book,
            nether_brick,
            quartz,
            tnt_minecart,
            hopper_minecart,
            prismarine_shard,
            prismarine_crystals,
            rabbit,
            cooked_rabbit,
            rabbit_stew,
            rabbit_foot,
            rabbit_hide,
            armor_stand,
            iron_horse_armor,
            golden_horse_armor,
            diamond_horse_armor,
            lead,
            name_tag,
            command_block_minecart,
            mutton,
            cooked_mutton,
            white_banner,
            orange_banner,
            magenta_banner,
            light_blue_banner,
            yellow_banner,
            lime_banner,
            pink_banner,
            gray_banner,
            light_gray_banner,
            cyan_banner,
            purple_banner,
            blue_banner,
            brown_banner,
            green_banner,
            red_banner,
            black_banner,
            end_crystal,
            chorus_fruit,
            popped_chorus_fruit,
            beetroot,
            beetroot_seeds,
            beetroot_soup,
            dragon_breath,
            splash_potion,
            spectral_arrow,
            tipped_arrow,
            lingering_potion,
            shield,
            elytra,
            spruce_boat,
            birch_boat,
            jungle_boat,
            acacia_boat,
            dark_oak_boat,
            totem_of_undying,
            shulker_shell,
            iron_nugget,
            knowledge_book,
            debug_stick,
            music_disc_13,
            music_disc_cat,
            music_disc_blocks,
            music_disc_chirp,
            music_disc_far,
            music_disc_mall,
            music_disc_mellohi,
            music_disc_stal,
            music_disc_strad,
            music_disc_ward,
            music_disc_11,
            music_disc_wait,
            TtemEnumEnd
        }
        public enum Color
        {
            white,
            orange,
            magenta,
            light_blue,
            yellow,
            lime,
            pink,
            gray,
            silver,
            cyan,
            purple,
            blue,
            brown,
            green,
            red,
            black
        }
        public enum Particle
        {
            campfire_cosy_smoke,
            campfire_signal_smoke,
            ambient_entity_effect,
            angry_villager,
            barrier,
            bubble,
            cloud,
            crit,
            damage_indicator,
            dragon_breath,
            dripping_lava,
            dripping_water,
            effect,
            elder_guardian,
            enchant,
            enchanted_hit,
            end_rod,
            entity_effect,
            explosion,
            explosion_emitter,
            firework,
            fishing,
            flame,
            happy_villager,
            heart,
            instant_effect,
            item_slime,
            item_snowball,
            large_smoke,
            lava,
            mycelium,
            note,
            poof,
            portal,
            rain,
            smoke,
            spit,
            splash,
            sweep_attack,
            totem_of_undying,
            underwater,
            witch,
            bubble_column_up,
            bubble_pop,
            current_down,
            squid_ink,
            nautilus
        }
        public enum Selector
        {
            /// <summary>
            /// Selects all players
            /// </summary>
            a,
            /// <summary>
            /// Selects the closest player
            /// </summary>
            p,
            /// <summary>
            /// Selects a random player
            /// </summary>
            r,
            /// <summary>
            /// Selects all entities
            /// </summary>
            e,
            /// <summary>
            /// Selects the executing player
            /// </summary>
            s,
            /// <summary>
            /// Selects everything, even things which aren't online. Not all commands support this selector
            /// </summary>
            All
        }
        public enum Sort { nearest, furthest, random, arbitrary }
        public enum Enchant
        {
            protection = 0,
            fire_protection = 1,
            feather_falling = 2,
            blast_protection = 3,
            projectile_protection = 4,
            respiration = 5,
            aqua_infinity = 6,
            thorns = 7,
            depth_strider = 8,
            frost_walker = 9,
            binding_curse = 10,
            sharpness = 16,
            smite = 17,
            bane_of_arthropods = 18,
            knockback = 19,
            fire_aspect = 20,
            looting = 21,
            sweeping = 22,
            efficiency = 32,
            silk_touch = 33,
            unbreaking = 34,
            fortune = 35,
            power = 48,
            punch = 49,
            flame = 50,
            infinity = 51,
            multishot = 52,
            quick_charge = 53,
            piercing = 54,
            luck_of_the_sea = 61,
            lure = 62,
            mending = 70,
            vanishing_curse = 71,
            channeling = 68,
            impaling = 66,
            loyalty = 65,
            riptide = 67,
            EnchantEnumEnd
        }
        public enum Effect
        {
            speed,
            slowness,
            haste,
            mining_fatigue,
            strength,
            instant_health,
            instant_damage,
            jump_boost,
            nausea,
            regeneration,
            resistance,
            fire_resistance,
            water_breathing,
            invisibility,
            blindness,
            night_vision,
            hunger,
            weakness,
            poison,
            wither,
            health_boost,
            absorption,
            saturation,
            glowing,
            levitation,
            luck,
            unluck,
            slow_falling,
            dolphins_grace,
            conduit_power,
            EffectEnumEnd
        }
        public enum Keys
        {
            forward,
            left,
            back,
            right,
            jump,
            sneak,
            sprint,
            inventory,
            swapHands,
            drop,
            use,
            attack,
            pickItem,
            chat,
            playerlist,
            command,
            screenshot,
            togglePerspective,
            smoothCamera,
            fullscreen,
            spectatorOutlines,
            hotbar_1,
            hotbar_2,
            hotbar_3,
            hotbar_4,
            hotbar_5,
            hotbar_6,
            hotbar_7,
            hotbar_8,
            hotbar_9,
            saveToolbarActivator,
            loadToolbarActivator
        }
        public enum MinecraftColor { black, dark_blue, dark_green, dark_aqua, dark_red, dark_purple, gold, gray, dark_gray, blue, green, aqua, red, light_purple, yellow, white, }
        public enum Biome
        {
            deep_frozen_ocean,
            deep_cold_ocean,
            deep_lukewarm_ocean,
            deep_warm_ocean,
            modified_badlands_plateau,
            modified_wooded_badlands_plateau,
            eroded_badlands,
            shattered_savanna_plateau,
            shattered_savanna,
            modified_gravelly_mountains,
            giant_spruce_taiga_hills,
            giant_spruce_taiga,
            snowy_taiga_mountains,
            dark_forest_hills,
            tall_birch_hills,
            tall_birch_forest,
            modified_jungle_edge,
            modified_jungle,
            bamboo_forest,
            ice_spikes,
            swamp_hills,
            taiga_mountains,
            flower_forest,
            gravelly_mountains,
            desert_lakes,
            sunflower_plains,
            the_void,
            badlands_plateau,
            wooded_badlands_plateau,
            badlands,
            savanna_plateau,
            wooded_mountains,
            giant_tree_taiga_hills,
            giant_tree_taiga,
            snowy_taiga_hills,
            snowy_taiga,
            dark_forest,
            snowy_beach,
            stone_shore,
            small_end_islands,
            end_midlands,
            end_highlands,
            end_barrens,
            mountain_edge,
            warm_ocean,
            lukewarm_ocean,
            cold_ocean,
            beach,
            birch_forest,
            birch_wooded_hills,
            deep_ocean,
            desert, desert_hills,
            mountains,
            mountains_with_trees,
            forest,
            wooded_hills,
            frozen_ocean, frozen_river,
            nether,
            snowy_tundra,
            snowy_mountains,
            jungle,
            jungle_edge,
            jungle_hills,
            mushroom_fields,
            mushroom_field_shore,
            mutated_birch_wooded_hills,
            mutated_mountains,
            mutated_mountains_with_trees,
            mutated_snowy_tundra,
            ocean, plains,
            river,
            savanna,
            the_end,
            smaller_mountains,
            swamp,
            taiga,
            taiga_hills,
            empty,
            BiomeEnumEnd
        }
        public enum Dimension {overworld, the_end, the_nether = -1 }
        public enum Structure {
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
            Shipwreck
        }

        public enum Potion
        {
            empty,
            water,
            mundane,
            thick,
            awkward,
            night_vision,
            long_night_vision,
            invisibility,
            long_invisibility,
            leaping,
            strong_leaping,
            long_leaping,
            fire_resistance,
            long_fire_resistance,
            swiftness,
            strong_swiftness,
            long_swiftness,
            water_breathing,
            long_water_breathing,
            healing,
            strong_healing,
            harming,
            strong_harming,
            poison,
            strong_poison,
            long_poison,
            regeneration,
            strong_regeneration,
            long_regeneration,
            strength,
            strong_strength,
            long_strength,
            weakness,
            long_weakness,
            luck
        }
        public enum Firework { Small, Large, Star, Creeper, Burst }
        public enum BookGeneration { Original, Copy, CopyCopy, Tattered }
        public enum MapMarker { player, frame, red_marker, blue_marker, target_x, target_point, player_off_map, player_off_limits, mansion, monument, banner_white, banner_orange, banner_magenta, banner_light_blue, banner_yellow, banner_lime, banner_pink, banner_gray, banner_light_gray, banner_cyan, banner_purple, banner_blue, banner_brown, banner_green, banner_red, banner_black, red_x }
        public enum AttributeType
        {
            generic_maxHealth,
            generic_followRange,
            generic_knockbackResistance,
            generic_movementSpeed,
            generic_attackDamage,
            generic_armor,
            generic_armorToughness,
            generic_attackSpeed,
            generic_luck,
            horse_jumpStrenght,
            generic_flyingSpeed,
            zombie_spawnReinforcements
        }
        public enum AttributeSlot
        {
            mainhand,
            offhand,
            feet,
            legs,
            chest,
            head
        }
        public enum AttributeOperation
        {
            /// <summary>
            /// Adds the number to the base
            /// </summary>
            addition,
            /// <summary>
            /// Multiplies the base number with all the given multiply_base modifiers at once
            /// </summary>
            multiply_base,
            /// <summary>
            /// Multiplies the total attribute value with the given number
            /// </summary>
            multiply_total
        }

        public enum HorseMarkings
        {
            Normal,
            WhiteLegs,
            WhiteFields,
            BigWhiteDots,
            SmallBlackDots
        }
        public enum HorseColor
        {
            White,
            Creamy,
            Chestnut,
            Brown,
            Black,
            Gray,
            DarkBrown
        }
        public enum FishSize
        {
            Small,
            Large,
            Invisible,
        }
        public enum FishPattern
        {
            Flopper,
            Stripey,
            Flitter,
            Blockfish,
            Betty,
            Clayfish,
            NoPatter,
        }
        public enum DragonPhase
        {
            /// <summary>
            /// The ender dragon will circle around the island
            /// </summary>
            Circling,
            /// <summary>
            /// Flies to a player and fires a fireball
            /// </summary>
            Strafing,
            /// <summary>
            /// Flies to the portal
            /// </summary>
            FlyingToPortal,
            /// <summary>
            /// Lands on the portal
            /// </summary>
            LandingOnPortal,
            /// <summary>
            /// Taking of from the portal
            /// </summary>
            TakingOffFromPortal,
            /// <summary>
            /// Performs a breath attack while standing still
            /// </summary>
            LandedBreathAttack,
            /// <summary>
            /// Gets ready to perform a breath attack on a player
            /// </summary>
            LandedReadyBreathAttack,
            /// <summary>
            /// Roars before going to <see cref="DragonPhase.LandedReadyBreathAttack"/>
            /// </summary>
            LandedRoar,
            /// <summary>
            /// Charges a player
            /// </summary>
            ChargingPlayer,
            /// <summary>
            /// Flies to the portal to die there
            /// </summary>
            FlyingToPortalToDie,
            /// <summary>
            /// Enderdragon will not have any AI
            /// </summary>
            NoAI
        }

        public enum VillagerProffession 
        {
            none,
            armorer,
            butcher,
            cartographer,
            cleric,
            farmer,
            fisherman,
            fletcher,
            leatherworker,
            librarian,
            mason,
            nitwit,
            shepherd,
            toolsmith,
            weaponsmith
        }
        public enum VillagerType 
        {
            desert,
            jungle,
            plains,
            savanna,
            snow,
            swamp,
            tiaga
        }
        public enum Panda {Lazy,Worried,Playful,Aggresive,Weak,Brown,Normal }
        public enum Cat { Tabby, Tuxedo, Red, Siamese, BritishShorthair, Calico, Persian, Ragdoll, White, Jellie, Black }
        public enum Fox { red, snow}
        public enum Parrot { red, blue, green, cyan, silver }
        public enum Rabbit { Brown, White, Black, Gray, Yellow, Light_Brown, Killer = 99 }
        public enum ShulkerDirection { down, up, north, south, west, east }
        public enum Boat { oak, spruce, birch, jungle, acacia, dark_oak }
        public enum Facing { north, south, east, west }
        public enum Painting
        {
            Kekab,
            Aztec,
            Alban,
            Aztec2,
            Bomb,
            Plant,
            Wasteland,
            Wanderer,
            Graham,
            Courbet,
            Sunset,
            Sea,
            Creebet,
            Match,
            Bust,
            Stage,
            Void,
            SkullAndRoses,
            Wither,
            Fighters,
            Skeleton,
            DonkeyKong,
            Pointer,
            Pigscene,
            BurningSkull
        }
        public enum ArrowPickup
        {
            /// <summary>
            /// Players can't pick up the arrow
            /// </summary>
            CantPickUp,
            /// <summary>
            /// Players can pick up the arrow
            /// </summary>
            CanPickUp,
            /// <summary>
            /// Players in creative mode can pick the arrow
            /// </summary>
            CreativePickUp
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
        public enum StructureRotation { NONE, CLOCKWISE_90, CLOCKWISE_180, COUNTERCLOCKWISE_90 }
        public enum StructureMirror { NONE, LEFT_RIGHT, FRONT_BACK }
        public enum StructureMode { data, save, load, corner }
        public enum FacingFull { down, up, north, south, west, east }
        public enum PistonType { normal, sticky }

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
        public enum StateDoorHinge {left, right }
        public enum StatePlaced { wall, ceiling, floor }
        public enum StateBambooLeave { none, small, large }
        public enum StateBedPart {foot, head }
        public enum StateBellAttachment {ceiling, double_wall, floor, single_wall }
        public enum StatePart {lower, upper }
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
        public enum StateRailShape {east_west, north_east, north_south, north_west, south_east, south_west, ascending_east, ascending_north, ascending_south, ascending_west }
        public enum StateSpecailRailShape { east_west, north_south, ascending_east, ascending_north, ascending_south, ascending_west }
        public enum StateRedstoneConnection {none, side, up }
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
        public enum StateSimplePlaced {bottom, top }

        public enum SmeltType {Furnace, Campfire, Smoker, BlastFurnace }
        public enum BlockAdd { destroy, keep, replace }
        public enum BlockFill
        {
            /// <summary>
            /// Destroys all blocks and makes their items drop
            /// </summary>
            destroy,
            /// <summary>
            /// Only replaces air blocks
            /// </summary>
            keep,
            /// <summary>
            /// The inside of the fill is filled with air with a square of the chosen block around
            /// </summary>
            hollow,
            /// <summary>
            /// The inside of the fill isn't effected. Only the frame is.
            /// </summary>
            outline,
            /// <summary>
            /// Replaces blocks with the specified block
            /// </summary>
            replace
        }
        public enum BlockClone { replace, filtered, masked }
        public enum BlockCloneWay { normal, move, force }
        public enum SoundSource { ambient, block, hostile, master, music, neutral, player, record, voice, weather }
        public enum Operation
        {
            /// <summary>
            /// Add (+) the 2 numbers and saves the result in the first <see cref="Selector"/>
            /// </summary>
            Add,
            /// <summary>
            /// subtract (-) the 2 numbers and saves the result in the first <see cref="Selector"/>
            /// </summary>
            Subtract,
            /// <summary>
            /// multiply (*) the 2 numbers and saves the result in the first <see cref="Selector"/>
            /// </summary>
            Multiply,
            /// <summary>
            /// divide (/) the 2 numbers and saves the result in the first <see cref="Selector"/>
            /// </summary>
            Divide,
            /// <summary>
            /// finds the remainder (%) when dividing the 2 numbers and saves the result in the first <see cref="Selector"/>
            /// </summary>
            Remainder,
            /// <summary>
            /// Switches the 2 numbers. First <see cref="Selector"/> gets <see cref="Selector"/> 2's number and 2 gets 1's number
            /// </summary>
            Switch,
            /// <summary>
            /// Finds the highest number of the given numbers and saves it in the first <see cref="Selector"/>
            /// </summary>
            GetHigher,
            /// <summary>
            /// Finds the lowest number of the given numbers and saves it in the first <see cref="Selector"/>
            /// </summary>
            GetLowest,
            /// <summary>
            /// Sets the first <see cref="Selector"/>'s score to the 2nd <see cref="Selector"/>'s score
            /// </summary>
            Equel
        }
        public enum FacingAnchor { feet,eyes }
        public enum TeamCollision
        {
            /// <summary>
            /// Makes the team collide with everyone
            /// </summary>
            always,
            /// <summary>
            /// Makes the team never collide
            /// </summary>
            never,
            /// <summary>
            /// Makes the team only collide with players not on their team
            /// </summary>
            pushOtherTeams,
            /// <summary>
            /// Makes the team only collide with players on their team
            /// </summary>
            PushOwnTeam
        }
        public enum TeamVisibility
        {
            /// <summary>
            /// Makes the team visible to everyone
            /// </summary>
            always,
            /// <summary>
            /// Makes the team hidden for everyone
            /// </summary>
            never,
            /// <summary>
            /// Makes the team hidden for players not on their team
            /// </summary>
            hideForOtherTeams,
            /// <summary>
            /// Makes the team hidden for players on their own team
            /// </summary>
            hideForOwnTeam
        }
        public enum DatapackPlace { after,before,first,last}
        public enum Difficulty { peaceful,easy,normal,hard}
        public enum IfScoreOperation
        {
            /// <summary>
            /// &lt;
            /// </summary>
            Smaller,
            /// <summary>
            /// &lt;=
            /// </summary>
            SmallerOrEquel,
            /// <summary>
            /// ==
            /// </summary>
            Equel,
            /// <summary>
            /// &gt;
            /// </summary>
            Higher,
            /// <summary>
            /// &gt;=
            /// </summary>
            HigherOrEquel
        }
        public enum StoreTypes { Byte,Double,Float,Int,Long,Short }
        public enum ArmorSlot {feed, legs, chest, head}
        public enum WeatherType {rain,thunder,clear }
        public enum BossBarColor { blue, green, pink, purple, red, white, yellow }
        public enum BossBarStyle
        {
            /// <summary>
            /// The bar is one big bar
            /// </summary>
            progress,
            /// <summary>
            /// The bar is split into 6 parts
            /// </summary>
            notched_6,
            /// <summary>
            /// The bar is split into 10 parts
            /// </summary>
            notched_10,
            /// <summary>
            /// The bar is split into 12 parts
            /// </summary>
            notched_12,
            /// <summary>
            /// The bar is split into 20 parts
            /// </summary>
            notched_20
        }
        public enum ObjectiveRender
        {
            /// <summary>
            /// If each number should be displayed as a half heart
            /// </summary>
            hearts,
            /// <summary>
            /// If the number should be displayed as a number
            /// </summary>
            integer
        }
        public enum EntityDataModifierType { append, merge, prepend, set}
        public enum TimeType
        {
            /// <summary>
            /// Makes the time be in ticks
            /// </summary>
            ticks,
            /// <summary>
            /// Makes the time be in seconds
            /// (20 ticks)
            /// </summary>
            seconds,
            /// <summary>
            /// Makes the time be in Minecraft days
            /// (24000 ticks)
            /// </summary>
            days
        }
        public enum Axis { x, y, z }

        public enum LootCheckEntity
        {
            /// <summary>
            /// Checks as the killed entity
            /// </summary>
            This,

            /// <summary>
            /// Checks as the killer
            /// </summary>
            Killer,

            /// <summary>
            /// Checks as the killer if the killer is a player
            /// </summary>
            Killer_player
        }

        public enum AdvancementFrame
        {
            /// <summary>
            /// Normal basic advancement
            /// </summary>
            task,
            /// <summary>
            /// A star advancement which is supposed to be hard to get
            /// It makes a sounds and makes the name purple
            /// </summary>
            challenge,
            /// <summary>
            /// A advancement which is round instead of being square
            /// </summary>
            goal
        }
        public enum JSONEntityFlags
        {
            is_on_fire,
            is_sneaking,
            is_sprinting,
            is_swimming,
            is_baby
        }
        #pragma warning restore 1591
    }
}
