//
//This file was generated by SharpCraft.Generator.
//Do not make changes directly to this file. Change the template file instead.
//

using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// All the different ID's/Types/States things in the game can have
    /// </summary>
    public static partial class ID
    {
		#pragma warning disable 1591
        public class Entity : NamespacedEnumLike<string>, IEntityType
        {
            public Entity(string value, BasePackNamespace? @namespace = null) : base(value, @namespace)
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

            public static readonly Entity area_effect_cloud = new Entity("area_effect_cloud");
            public static readonly Entity armor_stand = new Entity("armor_stand");
            public static readonly Entity arrow = new Entity("arrow");
            public static readonly Entity bat = new Entity("bat");
            public static readonly Entity bee = new Entity("bee");
            public static readonly Entity blaze = new Entity("blaze");
            public static readonly Entity boat = new Entity("boat");
            public static readonly Entity cat = new Entity("cat");
            public static readonly Entity cave_spider = new Entity("cave_spider");
            public static readonly Entity chicken = new Entity("chicken");
            public static readonly Entity cod = new Entity("cod");
            public static readonly Entity cow = new Entity("cow");
            public static readonly Entity creeper = new Entity("creeper");
            public static readonly Entity dolphin = new Entity("dolphin");
            public static readonly Entity donkey = new Entity("donkey");
            public static readonly Entity dragon_fireball = new Entity("dragon_fireball");
            public static readonly Entity drowned = new Entity("drowned");
            public static readonly Entity elder_guardian = new Entity("elder_guardian");
            public static readonly Entity end_crystal = new Entity("end_crystal");
            public static readonly Entity ender_dragon = new Entity("ender_dragon");
            public static readonly Entity enderman = new Entity("enderman");
            public static readonly Entity endermite = new Entity("endermite");
            public static readonly Entity evoker = new Entity("evoker");
            public static readonly Entity evoker_fangs = new Entity("evoker_fangs");
            public static readonly Entity experience_orb = new Entity("experience_orb");
            public static readonly Entity eye_of_ender = new Entity("eye_of_ender");
            public static readonly Entity falling_block = new Entity("falling_block");
            public static readonly Entity firework_rocket = new Entity("firework_rocket");
            public static readonly Entity fox = new Entity("fox");
            public static readonly Entity ghast = new Entity("ghast");
            public static readonly Entity giant = new Entity("giant");
            public static readonly Entity guardian = new Entity("guardian");
            public static readonly Entity hoglin = new Entity("hoglin");
            public static readonly Entity horse = new Entity("horse");
            public static readonly Entity husk = new Entity("husk");
            public static readonly Entity illusioner = new Entity("illusioner");
            public static readonly Entity iron_golem = new Entity("iron_golem");
            public static readonly Entity item = new Entity("item");
            public static readonly Entity item_frame = new Entity("item_frame");
            public static readonly Entity fireball = new Entity("fireball");
            public static readonly Entity leash_knot = new Entity("leash_knot");
            public static readonly Entity lightning_bolt = new Entity("lightning_bolt");
            public static readonly Entity llama = new Entity("llama");
            public static readonly Entity llama_spit = new Entity("llama_spit");
            public static readonly Entity magma_cube = new Entity("magma_cube");
            public static readonly Entity minecart = new Entity("minecart");
            public static readonly Entity chest_minecart = new Entity("chest_minecart");
            public static readonly Entity command_block_minecart = new Entity("command_block_minecart");
            public static readonly Entity furnace_minecart = new Entity("furnace_minecart");
            public static readonly Entity hopper_minecart = new Entity("hopper_minecart");
            public static readonly Entity spawner_minecart = new Entity("spawner_minecart");
            public static readonly Entity tnt_minecart = new Entity("tnt_minecart");
            public static readonly Entity mule = new Entity("mule");
            public static readonly Entity mooshroom = new Entity("mooshroom");
            public static readonly Entity ocelot = new Entity("ocelot");
            public static readonly Entity painting = new Entity("painting");
            public static readonly Entity panda = new Entity("panda");
            public static readonly Entity parrot = new Entity("parrot");
            public static readonly Entity phantom = new Entity("phantom");
            public static readonly Entity pig = new Entity("pig");
            public static readonly Entity piglin = new Entity("piglin");
            public static readonly Entity pillager = new Entity("pillager");
            public static readonly Entity polar_bear = new Entity("polar_bear");
            public static readonly Entity tnt = new Entity("tnt");
            public static readonly Entity pufferfish = new Entity("pufferfish");
            public static readonly Entity rabbit = new Entity("rabbit");
            public static readonly Entity ravager = new Entity("ravager");
            public static readonly Entity salmon = new Entity("salmon");
            public static readonly Entity sheep = new Entity("sheep");
            public static readonly Entity shulker = new Entity("shulker");
            public static readonly Entity shulker_bullet = new Entity("shulker_bullet");
            public static readonly Entity silverfish = new Entity("silverfish");
            public static readonly Entity skeleton = new Entity("skeleton");
            public static readonly Entity skeleton_horse = new Entity("skeleton_horse");
            public static readonly Entity slime = new Entity("slime");
            public static readonly Entity small_fireball = new Entity("small_fireball");
            public static readonly Entity snow_golem = new Entity("snow_golem");
            public static readonly Entity snowball = new Entity("snowball");
            public static readonly Entity spectral_arrow = new Entity("spectral_arrow");
            public static readonly Entity spider = new Entity("spider");
            public static readonly Entity squid = new Entity("squid");
            public static readonly Entity stray = new Entity("stray");
            public static readonly Entity strider = new Entity("strider");
            public static readonly Entity egg = new Entity("egg");
            public static readonly Entity ender_pearl = new Entity("ender_pearl");
            public static readonly Entity experience_bottle = new Entity("experience_bottle");
            public static readonly Entity potion = new Entity("potion");
            public static readonly Entity trident = new Entity("trident");
            public static readonly Entity trader_llama = new Entity("trader_llama");
            public static readonly Entity tropical_fish = new Entity("tropical_fish");
            public static readonly Entity turtle = new Entity("turtle");
            public static readonly Entity vex = new Entity("vex");
            public static readonly Entity villager = new Entity("villager");
            public static readonly Entity vindicator = new Entity("vindicator");
            public static readonly Entity wandering_trader = new Entity("wandering_trader");
            public static readonly Entity witch = new Entity("witch");
            public static readonly Entity wither = new Entity("wither");
            public static readonly Entity wither_skeleton = new Entity("wither_skeleton");
            public static readonly Entity wither_skull = new Entity("wither_skull");
            public static readonly Entity wolf = new Entity("wolf");
            public static readonly Entity zoglin = new Entity("zoglin");
            public static readonly Entity zombie = new Entity("zombie");
            public static readonly Entity zombie_horse = new Entity("zombie_horse");
            public static readonly Entity zombie_villager = new Entity("zombie_villager");
            public static readonly Entity zombified_piglin = new Entity("zombified_piglin");
            public static readonly Entity player = new Entity("player");
            public static readonly Entity fishing_bobber = new Entity("fishing_bobber");

        }
    }
}