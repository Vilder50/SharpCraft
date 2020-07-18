namespace SharpCraft
{
    /// <summary>
    /// All the different ID's/Types/States things in the game can have
    /// </summary>
    public static partial class ID
    {
        #pragma warning disable 1591
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
            soul,
            ash,
            crimson_spore,
            soul_fire_flame,
            warped_spore,
            dripping_obsidian_tear,
            falling_obsidian_tear,
            landing_obsidian_tear,
            dripping_honey,
            falling_honey,
            falling_nectar,
            landing_honey,
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
            nautilus,
        }
            
        public class Effect : NamespacedEnumLike<string>
        {
            public Effect(string value, BasePackNamespace? @namespace = null) : base(value, @namespace)
            {
            }

            public static readonly Effect speed = new Effect("speed");
            public static readonly Effect slowness = new Effect("slowness");
            public static readonly Effect haste = new Effect("haste");
            public static readonly Effect mining_fatigue = new Effect("mining_fatigue");
            public static readonly Effect strength = new Effect("strength");
            public static readonly Effect instant_health = new Effect("instant_health");
            public static readonly Effect instant_damage = new Effect("instant_damage");
            public static readonly Effect jump_boost = new Effect("jump_boost");
            public static readonly Effect nausea = new Effect("nausea");
            public static readonly Effect regeneration = new Effect("regeneration");
            public static readonly Effect resistance = new Effect("resistance");
            public static readonly Effect fire_resistance = new Effect("fire_resistance");
            public static readonly Effect water_breathing = new Effect("water_breathing");
            public static readonly Effect invisibility = new Effect("invisibility");
            public static readonly Effect blindness = new Effect("blindness");
            public static readonly Effect night_vision = new Effect("night_vision");
            public static readonly Effect hunger = new Effect("hunger");
            public static readonly Effect weakness = new Effect("weakness");
            public static readonly Effect poison = new Effect("poison");
            public static readonly Effect wither = new Effect("wither");
            public static readonly Effect health_boost = new Effect("health_boost");
            public static readonly Effect absorption = new Effect("absorption");
            public static readonly Effect saturation = new Effect("saturation");
            public static readonly Effect glowing = new Effect("glowing");
            public static readonly Effect levitation = new Effect("levitation");
            public static readonly Effect luck = new Effect("luck");
            public static readonly Effect unluck = new Effect("unluck");
            public static readonly Effect slow_falling = new Effect("slow_falling");
            public static readonly Effect conduit_power = new Effect("conduit_power");
            public static readonly Effect dolphins_grace = new Effect("dolphins_grace");
            public static readonly Effect bad_omen = new Effect("bad_omen");
            public static readonly Effect hero_of_the_village = new Effect("hero_of_the_village");
        }
        public enum Key
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
        #pragma warning restore 1591
    }
}
