#pragma warning disable 1591
namespace SharpCraft
{
    public partial class ID
    {
        /// <summary>
        /// A list of all sounds in the game
        /// This list can be used in /playsound and /stopsound commands
        /// <see cref="Function.PlayerCommands.ClassSound.Play(SharpCraft.Selector, string, SoundSource, Coords, double, double, double)"/> and <see cref="Function.PlayerCommands.ClassSound.Stop(SharpCraft.Selector, SoundSource?, string)"/>
        /// </summary>
        public static class Sounds
        {
            /// <summary>
            /// Ambient sounds
            /// </summary>
            public static class Ambient
            {
                public static string Cave { get; } = "ambient.cave";

                /// <summary>
                /// sounds from underwater
                /// </summary>
                public static class Underwater
                {

                    public static string Enter { get; } = "ambient.underwater.enter";
                    public static string Exit { get; } = "ambient.underwater.exit";
                    public static string Loop { get; } = "ambient.underwater.loop";
                    public static string ExtraLoop1 { get; } = "ambient.underwater.loop.additions";
                    public static string ExtraLoop2 { get; } = "ambient.underwater.loop.additions.rare";
                    public static string ExtraLoop3 { get; } = "ambient.underwater.loop.additions.ultra_rare";
                }
            }

            /// <summary>
            /// block sounds
            /// </summary>
            public static class Block
            {
                public static class Coral
                {
                    public static string Break { get; } = "block.coral_block.break";
                    public static string Fall { get; } = "block.coral_block.fall";
                    public static string Hit { get; } = "block.coral_block.hit";
                    public static string Place { get; } = "block.coral_block.place";
                    public static string Step { get; } = "block.coral_block.step";
                }
                public static class WetGrass
                {
                    public static string Break { get; } = "block.wet_grass.break";
                    public static string Fall { get; } = "block.wet_grass.fall";
                    public static string Hit { get; } = "block.wet_grass.hit";
                    public static string Place { get; } = "block.wet_grass.place";
                    public static string Step { get; } = "block.wet_grass.step";
                }
                public static class BubbleColumn
                {
                    public static string BubblePop { get; } = "block.bubble_column.bubble_pop";
                    public static string UpwardsAmbient { get; } = "block.bubble_column.upwards_ambient";
                    public static string UpwardsInside { get; } = "block.bubble_column.upwards_inside";
                    public static string WhirlAmbient { get; } = "block.block.bubble_column.whirlpool_ambient";
                    public static string WhirlInside { get; } = "block.bubble_column.whirlpool_inside";
                }
                public static class Anvil
                {
                    public static string Break { get; } = "block.anvil.break";
                    public static string Destroy { get; } = "block.anvil.destroy";
                    public static string Fall { get; } = "block.anvil.fall";
                    public static string Hit { get; } = "block.anvil.hit";
                    public static string Land { get; } = "block.anvil.land";
                    public static string Place { get; } = "block.anvil.place";
                    public static string Step { get; } = "block.anvil.step";
                    public static string Use { get; } = "block.anvil.use";
                }
                public static string Brewing { get; } = "block.brewing_stand.brew";
                public static string Enchant { get; } = "block.enchantment_table.use";
                public static string Campfire { get; } = "block.campfire.crackle";
                public static string Bell { get; } = "block.bell.use";
                public static class Chest
                {
                    public static string Close { get; } = "block.chest.close";
                    public static string Locked { get; } = "block.chest.locked";
                    public static string Open { get; } = "block.chest.open";
                }
                public static class Chorus
                {
                    public static string Death { get; } = "block.chorus_flower.death";
                    public static string Grow { get; } = "block.chorus_flower.grow";
                }
                public static class Cloth
                {
                    public static string Break { get; } = "block.wool.break";
                    public static string Fall { get; } = "block.wool.fall";
                    public static string Hit { get; } = "block.wool.hit";
                    public static string Place { get; } = "block.wool.place";
                    public static string Step { get; } = "block.wool.step";
                }
                public static string Comparator { get; } = "block.comparator.click";
                public static class Dispenser
                {
                    public static string Dispense { get; } = "block.dispenser.dispense";
                    public static string Fail { get; } = "block.dispenser.fail";
                    public static string Launch { get; } = "block.dispenser.launch";
                }
                public static string EndGate { get; } = "block.end_gateway.spawn";
                public static class EndPortal
                {
                    public static string Spawn { get; } = "block.end_portal.spawn";
                    public static string Fill { get; } = "block.end_portal.fill";
                }
                public static class EnderChest
                {
                    public static string Close { get; } = "block.ender_chest.close";
                    public static string Open { get; } = "block.ender_chest.open";
                }
                public static class FenceGate
                {
                    public static string Close { get; } = "block.fence_gate.close";
                    public static string Open { get; } = "block.fence_gate.open";
                }
                public static string Furnace { get; } = "block.furnace.fire_cracle";
                public static class Glass
                {
                    public static string Break { get; } = "block.glass.break";
                    public static string Fall { get; } = "block.glass.fall";
                    public static string Hit { get; } = "block.glass.hit";
                    public static string Place { get; } = "block.glass.place";
                    public static string Step { get; } = "block.glass.step";
                }
                public static class Grass
                {
                    public static string Break { get; } = "block.grass.break";
                    public static string Fall { get; } = "block.grass.fall";
                    public static string Hit { get; } = "block.grass.hit";
                    public static string Place { get; } = "block.grass.place";
                    public static string Step { get; } = "block.grass.step";
                }
                public static class Gravel
                {
                    public static string Break { get; } = "block.gravel.break";
                    public static string Fall { get; } = "block.gravel.fall";
                    public static string Hit { get; } = "block.gravel.hit";
                    public static string Place { get; } = "block.gravel.place";
                    public static string Step { get; } = "block.gravel.step";
                }
                public static class IronDoor
                {
                    public static string Close { get; } = "block.iron_door.close";
                    public static string Open { get; } = "block.iron_door.open";
                }
                public static class IronTrapdoor
                {
                    public static string Close { get; } = "block.iron_trapdoor.close";
                    public static string Open { get; } = "block.iron_trapdoor.open";
                }
                public static class Ladder
                {
                    public static string Break { get; } = "block.ladder.break";
                    public static string Fall { get; } = "block.ladder.fall";
                    public static string Hit { get; } = "block.ladder.hit";
                    public static string Place { get; } = "block.ladder.place";
                    public static string Step { get; } = "block.ladder.step";
                }
                public static class Lava
                {
                    public static string Ambient { get; } = "block.lava.ambiant";
                    public static string Extinguish { get; } = "block.lava.extinguish";
                    public static string Pop { get; } = "block.lava.pop";
                }
                public static string Lever { get; } = "block.lever.click";
                public static class Metal
                {
                    public static string Break { get; } = "block.metal.break";
                    public static string Fall { get; } = "block.metal.fall";
                    public static string Hit { get; } = "block.metal.hit";
                    public static string Place { get; } = "block.metal.place";
                    public static string Step { get; } = "block.metal.step";
                }
                public static class MetalPlate
                {
                    public static string ClickOn { get; } = "block.metal_pressure_plate.click_on";
                    public static string ClickOff { get; } = "block.metal_pressure_plate.click_off";
                }
                public static class Note
                {
                    public static string Basedrum { get; } = "block.note_block.basedrum";
                    public static string Bass { get; } = "block.note_block.bass";
                    public static string Bell { get; } = "block.note_block.bell";
                    public static string Chime { get; } = "block.note_block.chime";
                    public static string Flute { get; } = "block.note_block.flute";
                    public static string Guitar { get; } = "block.note_block.guitar";
                    public static string Harp { get; } = "block.note_block.harp";
                    public static string Hat { get; } = "block.note_block.hat";
                    public static string Pling { get; } = "block.note_block.pling";
                    public static string Snare { get; } = "block.note_block.snare";
                    public static string Xylophone { get; } = "block.note_block.xylophone";
                }
                public static class Piston
                {
                    public static string Extend { get; } = "block.piston.extend";
                    public static string Contract { get; } = "block.piston.contract";
                }
                public static class Portal
                {
                    public static string Travel { get; } = "block.portal.travel";
                    public static string Trigger { get; } = "block.portal.trigger";
                }
                public static class Sand
                {
                    public static string Break { get; } = "block.sand.break";
                    public static string Fall { get; } = "block.sand.fall";
                    public static string Hit { get; } = "block.sand.hit";
                    public static string Place { get; } = "block.sand.place";
                    public static string Step { get; } = "block.sand.step";
                }
                public static class Shulker
                {
                    public static string Close { get; } = "block.shulker.close";
                    public static string Open { get; } = "block.shulker.open";
                }
                public static class Slime
                {
                    public static string Break { get; } = "block.slime_block.break";
                    public static string Fall { get; } = "block.slime_block.fall";
                    public static string Hit { get; } = "block.slime_block.hit";
                    public static string Place { get; } = "block.slime_block.place";
                    public static string Step { get; } = "block.slime_block.step";
                }
                public static class Snow
                {
                    public static string Break { get; } = "block.snow.break";
                    public static string Fall { get; } = "block.snow.fall";
                    public static string Hit { get; } = "block.snow.hit";
                    public static string Place { get; } = "block.snow.place";
                    public static string Step { get; } = "block.snow.step";
                }
                public static class Stone
                {
                    public static string Break { get; } = "block.stone.break";
                    public static string Fall { get; } = "block.stone.fall";
                    public static string Hit { get; } = "block.stone.hit";
                    public static string Place { get; } = "block.stone.place";
                    public static string Step { get; } = "block.stone.step";
                }
                public static class StoneButton
                {
                    public static string ClickOn { get; } = "block.stone_button.click_on";
                    public static string ClickOff { get; } = "block.stone_button.click_off";
                }
                public static class StonePlate
                {
                    public static string ClickOn { get; } = "block.stone_pressureplate.click_on";
                    public static string ClickOff { get; } = "block.stone_pressureplate.click_off";
                }
                public static class Tripwire
                {
                    public static string ClickOn { get; } = "block.tripwire.click_on";
                    public static string ClickOff { get; } = "block.tripwire.click_off";
                    public static string Detach { get; } = "block.tripwire.detach";
                }
                public static string Water { get; } = "block.water.ambient";
                public static string LilyPad { get; } = "block.lily_pad.place";
                public static class Wood
                {
                    public static string Break { get; } = "block.wood.break";
                    public static string Fall { get; } = "block.wood.fall";
                    public static string Hit { get; } = "block.wood.hit";
                    public static string Place { get; } = "block.wood.place";
                    public static string Step { get; } = "block.wood.step";
                }
                public static class WoodenButton
                {
                    public static string ClickOn { get; } = "block.wooden_button.click_on";
                    public static string ClickOff { get; } = "block.wooden_button.click_off";
                }
                public static class WoodenPlate
                {
                    public static string ClickOn { get; } = "block.wooden_pressure_plate.click_on";
                    public static string ClickOff { get; } = "block.wooden_pressure_plate.click_off";
                }
                public static class WoodenDoor
                {
                    public static string ClickOn { get; } = "block.wooden_door.click_on";
                    public static string ClickOff { get; } = "block.wooden_door.click_off";
                }
                public static class WoodenTrapdoor
                {
                    public static string ClickOn { get; } = "block.wooden_trapdoor.click_on";
                    public static string ClickOff { get; } = "block.wooden_trapdoor.click_off";
                }
            }

            public static string EnchantThorns { get; } = "enchant.thorns.hit";

            /// <summary>
            /// entity sounds
            /// </summary>
            public static class Entity
            {
                public static string Fish { get; } = "entity.fish.swim";
                public static class Armorstand
                {
                    public static string Break { get; } = "entity.armor_stand.break";
                    public static string Fall { get; } = "entity.armor_stand.fall";
                    public static string Hit { get; } = "entity.armor_stand.hit";
                    public static string Place { get; } = "entity.armor_stand.place";
                }
                public static class Arrow
                {
                    public static string Hit { get; } = "entity.arrow.hit";
                    public static string HitPlayer { get; } = "entity.arrow.hit_player";
                    public static string Shoot { get; } = "entity.arrow.shoot";
                }
                public static class Bat
                {
                    public static string Ambient { get; } = "entity.bat.ambient";
                    public static string Death { get; } = "entity.bat.death";
                    public static string Hurt { get; } = "entity.bat.hurt";
                    public static string Loop { get; } = "entity.bat.loop";
                    public static string Takeoff { get; } = "entity.bat.takeoff";
                }
                public static class Turtle
                {
                    public static string AmbientLand { get; } = "entity.turtle.ambient_land";
                    public static string Death { get; } = "entity.turtle.death";
                    public static string BabyDeath { get; } = "entity.turtle.death_baby";
                    public static string EggBreak { get; } = "entity.turtle.egg_break";
                    public static string EggCrack { get; } = "entity.turtle.egg_crack";
                    public static string EggHatch { get; } = "entity.turtle.egg_hatch";
                    public static string Hurt { get; } = "entity.turtle.hurt";
                    public static string BabyHurt { get; } = "entity.turtle.hurt_baby";
                    public static string LayEgg { get; } = "entity.turtle.lay_egg";
                    public static string Shamble { get; } = "entity.turtle.shamble";
                    public static string ShambleBaby { get; } = "entity.turtle.shamble_baby";
                    public static string Swim { get; } = "entity.turtle.swim";
                }
                public static class Blaze
                {
                    public static string Ambient { get; } = "entity.blaze.ambient";
                    public static string Death { get; } = "entity.blaze.death";
                    public static string Hurt { get; } = "entity.blaze.hurt";
                    public static string Burn { get; } = "entity.blaze.burn";
                    public static string Shoot { get; } = "entity.blaze.shoot";
                }
                public static class Phantom
                {
                    public static string Ambient { get; } = "entity.phantom.ambient";
                    public static string Bite { get; } = "entity.phantom.bite";
                    public static string Death { get; } = "entity.phantom.death";
                    public static string Hurt { get; } = "entity.phantom.hurt";
                    public static string Swoop { get; } = "entity.phantom.swoop";
                    public static string Flap { get; } = "entity.phantom.flap";
                }
                public static class Drowned
                {
                    public static string Ambient { get; } = "entity.Drowned.ambient";
                    public static string AmbientWater { get; } = "entity.Drowned.ambient_water";
                    public static string Death { get; } = "entity.Drowned.death";
                    public static string DeathWater { get; } = "entity.Drowned.death_water";
                    public static string Hurt { get; } = "entity.Drowned.hurt";
                    public static string HurtWater { get; } = "entity.Drowned.hurt_water";
                    public static string Shoot { get; } = "entity.Drowned.shoot";
                    public static string Step { get; } = "entity.Drowned.step";
                    public static string Swim { get; } = "entity.Drowned.swim";
                }
                public static class Dolphin
                {
                    public static string Ambient { get; } = "entity.Dolphin.ambient";
                    public static string AmbientWater { get; } = "entity.Dolphin.ambient_water";
                    public static string Attack { get; } = "entity.Dolphin.attack";
                    public static string Death { get; } = "entity.Dolphin.death";
                    public static string Eat { get; } = "entity.Dolphin.eat";
                    public static string Hurt { get; } = "entity.Dolphin.hurt";
                    public static string Jump { get; } = "entity.Dolphin.jump";
                    public static string Play { get; } = "entity.Dolphin.play";
                    public static string Splash { get; } = "entity.Dolphin.splash";
                    public static string Swim { get; } = "entity.Dolphin.swim";
                }
                public static class Boat
                {
                    public static string Water { get; } = "entity.boat.paddle_water";
                    public static string Land { get; } = "entity.boat.paddle_land";
                }
                public static class Bobber
                {
                    public static string Retrieve { get; } = "entity.fishing_bobber.retrieve";
                    public static string Splash { get; } = "entity.fishing_bobber.splash";
                    public static string Throw { get; } = "entity.fishing_bobber.throw";
                }
                public static class Cat
                {
                    public static string Ambient { get; } = "entity.cat.ambient";
                    public static string StrayAmbient { get; } = "entity.cat.stray_ambient";
                    public static string BegFood { get; } = "entity.cat.beg_for_food";
                    public static string Death { get; } = "entity.cat.death";
                    public static string Eat { get; } = "entity.cat.eat";
                    public static string Hurt { get; } = "entity.cat.hurt";
                    public static string Hiss { get; } = "entity.cat.hiss";
                    public static string Purr { get; } = "entity.cat.purr";
                    public static string Purrow { get; } = "entity.cat.purrow";
                }
                public static class Ocelot
                {
                    public static string Ambient { get; } = "entity.ocelot.ambient";
                    public static string Death { get; } = "entity.ocelot.death";
                    public static string Hurt { get; } = "entity.ocelot.hurt";
                }
                public static class Chicken
                {
                    public static string Ambient { get; } = "entity.chicken.ambient";
                    public static string Death { get; } = "entity.chicken.death";
                    public static string Hurt { get; } = "entity.chicken.hurt";
                    public static string Egg { get; } = "entity.chicken.egg";
                    public static string Step { get; } = "entity.chicken.step";
                }
                public static class Cow
                {
                    public static string Ambient { get; } = "entity.cow.ambient";
                    public static string Death { get; } = "entity.cow.death";
                    public static string Hurt { get; } = "entity.cow.hurt";
                    public static string Milk { get; } = "entity.cow.egg";
                    public static string Step { get; } = "entity.cow.step";
                }
                public static class Creeper
                {
                    public static string Death { get; } = "entity.creeper.death";
                    public static string Hurt { get; } = "entity.creeper.hurt";
                    public static string Primed { get; } = "entity.creeper.primed";
                }
                public static class Donkey
                {
                    public static string Ambient { get; } = "entity.donkey.ambient";
                    public static string Death { get; } = "entity.donkey.death";
                    public static string Hurt { get; } = "entity.donkey.hurt";
                    public static string Angry { get; } = "entity.donkey.angry";
                    public static string Chest { get; } = "entity.donkey.chest";
                }
                public static string Egg { get; } = "entity.egg.throw";
                public static class ElderGuardian
                {
                    public static string Ambient { get; } = "entity.elder_guardian.ambient";
                    public static string Death { get; } = "entity.elder_guardian.death";
                    public static string Hurt { get; } = "entity.elder_guardian.hurt";
                    public static string Curse { get; } = "entity.elder_guardian.curse";
                    public static string DeathLand { get; } = "entity.elder_guardian.death_land";
                    public static string Flop { get; } = "entity.elder_guardian.flop";
                    public static string HurtLand { get; } = "entity.elder_guardian.hurt_land";
                }
                public static class EnderDragon
                {
                    public static string Ambient { get; } = "entity.ender_dragon.ambient";
                    public static string Death { get; } = "entity.ender_dragon.death";
                    public static string Hurt { get; } = "entity.ender_dragon.hurt";
                    public static string Flap { get; } = "entity.ender_dragon.flap";
                    public static string Growl { get; } = "entity.ender_dragon.growl";
                    public static string Shoot { get; } = "entity.ender_dragon.shoot";
                }
                public static class IllagerBeast
                {
                    public static string Ambient { get; } = "entity.illager_beast.ambient";
                    public static string Death { get; } = "entity.illager_beast.death";
                    public static string Hurt { get; } = "entity.illager_beast.hurt";
                    public static string Attack { get; } = "entity.illager_beast.attack";
                    public static string Roar { get; } = "entity.illager_beast.roar";
                    public static string Step { get; } = "entity.illager_beast.step";
                    public static string Stunned { get; } = "entity.illager_beast.stunned";
                }
                public static class Pillager
                {
                    public static string Ambient { get; } = "entity.pillager.ambient";
                    public static string Death { get; } = "entity.pillager.death";
                    public static string Hurt { get; } = "entity.pillager.hurt";
                }
                public static string EnderBall { get; } = "entity.dragon_fireball.explode";
                public static class EnderEye
                {
                    public static string Launch { get; } = "entity.ender_eye.launch";
                    public static string Death { get; } = "entity.ender_eye.death";
                }
                public static class Enderman
                {
                    public static string Ambient { get; } = "entity.enderman.ambient";
                    public static string Death { get; } = "entity.enderman.death";
                    public static string Hurt { get; } = "entity.enderman.hurt";
                    public static string Scream { get; } = "entity.enderman.scream";
                    public static string Stare { get; } = "entity.enderman.stare";
                    public static string Teleport { get; } = "entity.enderman.teleport";
                }
                public static class Endermite
                {
                    public static string Ambient { get; } = "entity.endermite.ambient";
                    public static string Death { get; } = "entity.endermite.death";
                    public static string Hurt { get; } = "entity.endermite.hurt";
                    public static string Step { get; } = "entity.endermite.step";
                }
                public static string EvocationFangs { get; } = "entity.evocation_fangs.attack";
                public static class EvocationIllager
                {
                    public static string Ambient { get; } = "entity.evoker.ambient";
                    public static string Death { get; } = "entity.evoker.death";
                    public static string Hurt { get; } = "entity.evoker.hurt";
                    public static string CastSpell { get; } = "entity.evoker.cast_spell";
                    public static string PrepareAttack { get; } = "entity.evoker.prepare_attack";
                    public static string PrepareSummon { get; } = "entity.evoker.prepare_summon";
                    public static string PrepareWololo { get; } = "entity.evoker.prepare_wololo";
                }
                public static string XPBottle { get; } = "entity.experience_bottle.throw";
                public static string XPOrb { get; } = "entity.experience_orb.pickup";
                public static string EnderPearl { get; } = "entity.ender_pearl.throw";
                public static class Firework
                {
                    public static string Blast { get; } = "entity.firework_rocket.blass";
                    public static string BlastFar { get; } = "entity.firework_rocket.blast_far";
                    public static string LargeBlast { get; } = "entity.firework_rocket.large_blast";
                    public static string LargeBlastFar { get; } = "entity.firework_rocket.large_blast_far";
                    public static string Launch { get; } = "entity.firework_rocket.launch";
                    public static string Shoot { get; } = "entity.firework_rocket.shoot";
                    public static string Twinkle { get; } = "entity.firework_rocket.twinkle";
                    public static string TwinkleFar { get; } = "entity.firework_rocket.twinklefar";
                }
                public static class Generic
                {
                    public static string BigFall { get; } = "entity.generic.big_fall";
                    public static string Burn { get; } = "entity.generic.burn";
                    public static string Death { get; } = "entity.generic.death";
                    public static string Drink { get; } = "entity.generic.drink";
                    public static string Eat { get; } = "entity.generic.eat";
                    public static string Explode { get; } = "entity.generic.explode";
                    public static string Extinguish { get; } = "entity.generic.extinguish_fire";
                    public static string Hurt { get; } = "entity.generic.hurt";
                    public static string SmallFall { get; } = "entity.generic.small_fall";
                    public static string Splash { get; } = "entity.generic.splash";
                    public static string Swim { get; } = "entity.generic.swim";
                }
                public static class Ghast
                {
                    public static string Ambient { get; } = "entity.ghast.ambient";
                    public static string Death { get; } = "entity.ghast.death";
                    public static string Hurt { get; } = "entity.ghast.hurt";
                    public static string Scream { get; } = "entity.ghast.scream";
                    public static string Shoot { get; } = "entity.ghast.shoot";
                    public static string Warn { get; } = "entity.ghast.warn";
                }
                public static class Guardian
                {
                    public static string Ambient { get; } = "entity.guardian.ambient";
                    public static string Death { get; } = "entity.guardian.death";
                    public static string Hurt { get; } = "entity.guardian.hurt";
                    public static string DeathLand { get; } = "entity.guardian.death_land";
                    public static string Flop { get; } = "entity.guardian.flop";
                    public static string HurtLand { get; } = "entity.guardian.hurt_land";
                    public static string Attack { get; } = "entity.guardian.attack";
                }
                public static class Horse
                {
                    public static string Ambient { get; } = "entity.horse.ambient";
                    public static string Death { get; } = "entity.horse.death";
                    public static string Hurt { get; } = "entity.horse.hurt";
                    public static string Angry { get; } = "entity.horse.angry";
                    public static string Breathe { get; } = "entity.horse.breathe";
                    public static string Eat { get; } = "entity.horse.eat";
                    public static string Armor { get; } = "entity.horse.armor";
                    public static string Gallop { get; } = "entity.horse.gallop";
                    public static string Jump { get; } = "entity.horse.jump";
                    public static string Land { get; } = "entity.horse.land";
                    public static string Saddle { get; } = "entity.horse.saddle";
                    public static string Step { get; } = "entity.horse.step";
                    public static string StepWood { get; } = "entity.horse.step_wood";
                }
                public static class Hostile
                {
                    public static string BigFall { get; } = "entity.hostile.big_fall";
                    public static string Death { get; } = "entity.hostile.death";
                    public static string Hurt { get; } = "entity.hostile.hurt";
                    public static string SmallFall { get; } = "entity.hostile.small_fall";
                    public static string Splash { get; } = "entity.hostile.splash";
                    public static string Swim { get; } = "entity.hostile.swim";
                }
                public static class Husk
                {
                    public static string Ambient { get; } = "entity.husk.ambient";
                    public static string Death { get; } = "entity.husk.death";
                    public static string Hurt { get; } = "entity.husk.hurt";
                    public static string Step { get; } = "entity.husk.step";
                    public static string Convert { get; } = "entity.husk.converted_to_zombie";
                }
                public static class IllusionIllager
                {
                    public static string Ambient { get; } = "entity.illusioner.ambient";
                    public static string Death { get; } = "entity.illusioner.death";
                    public static string Hurt { get; } = "entity.illusioner.hurt";
                    public static string CastSpell { get; } = "entity.illusioner.cast_spell";
                    public static string MirrorMove { get; } = "entity.illusioner.mirror_move";
                    public static string PrepareBlindness { get; } = "entity.illusioner.prepare_blindness";
                    public static string PrepareMirror { get; } = "entity.illusioner.prepare_mirror";
                }
                public static class Irongolem
                {
                    public static string Ambient { get; } = "entity.iron_golem.ambient";
                    public static string Death { get; } = "entity.iron_golem.death";
                    public static string Hurt { get; } = "entity.iron_golem.hurt";
                    public static string Step { get; } = "entity.iron_golem.step";
                    public static string Attack { get; } = "entity.iron_golem.attack";
                }
                public static class Item
                {
                    public static string Break { get; } = "entity.item.break";
                    public static string Pickup { get; } = "entity.item.pickup";
                }
                public static class ItemFrame
                {
                    public static string Break { get; } = "entity.item_frame.break";
                    public static string AddItem { get; } = "entity.item_frame.add_item";
                    public static string Place { get; } = "entity.item_frame.place";
                    public static string RemoveIem { get; } = "entity.item_frame.remove_item";
                    public static string RotateItem { get; } = "entity.item_frame.rotate_item";
                }
                public static class Leashknot
                {
                    public static string Place { get; } = "entity.leash_knot.place";
                    public static string Break { get; } = "entity.leash_knot.break";
                }
                public static class Lightning
                {
                    public static string Impact { get; } = "entity.lightning_bolt.impact";
                    public static string Thunder { get; } = "entity.ligning_bolt.thunder";
                }
                public static string LingeringPotion { get; } = "entity.lingering_potion.throw";
                public static class Llama
                {
                    public static string Ambient { get; } = "entity.llama.ambient";
                    public static string Death { get; } = "entity.llama.death";
                    public static string Hurt { get; } = "entity.llama.hurt";
                    public static string Step { get; } = "entity.llama.step";
                    public static string Angry { get; } = "entity.llama.angry";
                    public static string Eat { get; } = "entity.llama.eat";
                    public static string Chest { get; } = "entity.llama.chest";
                    public static string Spit { get; } = "entity.llama.spit";
                    public static string Swag { get; } = "entity.llama.swag";
                }
                public static class MagmaCube
                {
                    public static string Death { get; } = "entity.magma_cube.death";
                    public static string Hurt { get; } = "entity.magma_cube.hurt";
                    public static string Jump { get; } = "entity.magma_cube.jump";
                    public static string Squish { get; } = "entity.magma_cube.squish";
                    public static string SmallDeath { get; } = "entity.magma_cube.death_small";
                    public static string SmallHurt { get; } = "entity.magma_cube.hurt_small";
                    public static string SmallSquish { get; } = "entity.magma_cube.squish_small";
                }
                public static class Minecart
                {
                    public static string Inside { get; } = "entity.minecart.inside";
                    public static string Riding { get; } = "entity.minecart.riding";
                }
                public static class Mule
                {
                    public static string Ambient { get; } = "entity.mule.ambient";
                    public static string Death { get; } = "entity.mule.death";
                    public static string Hurt { get; } = "entity.mule.hurt";
                    public static string Chest { get; } = "entity.mule.chest";
                }
                public static class Painting
                {
                    public static string Break { get; } = "entity.painting.break";
                    public static string Place { get; } = "entity.painting.place";
                }
                public static class Parrot
                {
                    public static string Ambient { get; } = "entity.parrot.ambient";
                    public static string Death { get; } = "entity.parrot.death";
                    public static string Hurt { get; } = "entity.parrot.Hurt";
                    public static string Eat { get; } = "entity.parrot.Eat";
                    public static string Step { get; } = "entity.parrot.step";
                    public static string Fly { get; } = "entity.parrot.Fly";
                    public static class Imitate
                    {
                        public static string Blaze { get; } = "entity.parrot.imitate.blaze";
                        public static string Creeper { get; } = "entity.parrot.imitate.creeper";
                        public static string Drowned { get; } = "entity.parrot.imitate.drowned";
                        public static string ElderGuardian { get; } = "entity.parrot.imitate.elder_guardian";
                        public static string EnderDragon { get; } = "entity.parrot.imitate.ender_dragon";
                        public static string Enderman { get; } = "entity.parrot.imitate.enderman";
                        public static string EnderMite { get; } = "entity.parrot.imitate.endermite";
                        public static string EvocationIllager { get; } = "entity.parrot.imitate.evoker";
                        public static string Ghast { get; } = "entity.parrot.imitate.ghast";
                        public static string Husk { get; } = "entity.parrot.imitate.husk";
                        public static string IllusionIllager { get; } = "entity.parrot.imitate.illusioner";
                        public static string MagmaCube { get; } = "entity.parrot.imitate.magma_cube";
                        public static string PolarBear { get; } = "entity.parrot.imitate.polar_bear";
                        public static string Phantom { get; } = "entity.parrot.imitate.phantom";
                        public static string Shulker { get; } = "entity.parrot.imitate.shulker";
                        public static string Silverfish { get; } = "entity.parrot.imitate.silverfish";
                        public static string Skeleton { get; } = "entity.parrot.imitate.skeleton";
                        public static string Slime { get; } = "entity.parrot.imitate.slime";
                        public static string Spider { get; } = "entity.parrot.imitate.spider";
                        public static string Stray { get; } = "entity.parrot.imitate.stray";
                        public static string Vex { get; } = "entity.parrot.imitate.vex";
                        public static string VindicationIllager { get; } = "entity.parrot.imitate.vindicator";
                        public static string Witch { get; } = "entity.parrot.imitate.witch";
                        public static string Wither { get; } = "entity.parrot.imitate.wither";
                        public static string WitherSkeleton { get; } = "entity.parrot.imitate.wither_skeleton";
                        public static string Wolf { get; } = "entity.parrot.imitate.wolf";
                        public static string Zombie { get; } = "entity.parrot.imitate.zombie";
                        public static string ZombiePigman { get; } = "entity.parrot.imitate.zombie_pigman";
                        public static string ZombieVillager { get; } = "entity.parrot.imitate.zombie_villager";
                    }
                }
                public static class Pig
                {
                    public static string Ambient { get; } = "entity.pig.ambient";
                    public static string Death { get; } = "entity.pig.death";
                    public static string Hurt { get; } = "entity.pig.hurt";
                    public static string Saddle { get; } = "entity.pig.egg";
                    public static string Step { get; } = "entity.pig.step";
                }
                public static class Player
                {
                    public static class Attack
                    {
                        public static string Crit { get; } = "entity.player.attack.crit";
                        public static string Knockback { get; } = "entity.player.attack.knockback";
                        public static string NoDamage { get; } = "entity.player.attack.nodamage";
                        public static string Strong { get; } = "entity.player.attack.strong";
                        public static string Sweep { get; } = "entity.player.attack.sweep";
                        public static string Weak { get; } = "entity.player.attack.weak";
                    }
                    public static string BigFall { get; } = "entity.player.big_fall";
                    public static string Breath { get; } = "entity.player.breath";
                    public static string Burp { get; } = "entity.player.burp";
                    public static string Death { get; } = "entity.player.death";
                    public static string Hurt { get; } = "entity.player.hurt";
                    public static string HurtDrown { get; } = "entity.player.hurt_drown";
                    public static string HurtOnFire { get; } = "entity.player.hurt_on_fire";
                    public static string LevelUp { get; } = "entity.player.levelup";
                    public static string SmallFall { get; } = "entity.player.small_fall";
                    public static string Splash { get; } = "entity.player.splash";
                    public static string SplashHighSpeed { get; } = "entity.player.splash.high_speed";
                    public static string Swim { get; } = "entity.player.swim";
                }
                public static class PolarBear
                {
                    public static string Ambient { get; } = "entity.polar_bear.ambient";
                    public static string Death { get; } = "entity.polar_bear.death";
                    public static string Hurt { get; } = "entity.polar_bear.hurt";
                    public static string BabyAmbient { get; } = "entity.polar_bear.ambient_baby";
                    public static string Step { get; } = "entity.polar_bear.step";
                    public static string Warning { get; } = "entity.polar_bear.warning";
                }
                public static class Rabbit
                {
                    public static string Ambient { get; } = "entity.rabbit.ambient";
                    public static string Death { get; } = "entity.rabbit.death";
                    public static string Hurt { get; } = "entity.rabbit.hurt";
                    public static string Attack { get; } = "entity.rabbit.attack";
                    public static string Jump { get; } = "entity.rabbit.jump";
                }
                public static class Sheep
                {
                    public static string Ambient { get; } = "entity.sheep.ambient";
                    public static string Death { get; } = "entity.sheep.death";
                    public static string Hurt { get; } = "entity.sheep.hurt";
                    public static string Shear { get; } = "entity.sheep.shear";
                    public static string Step { get; } = "entity.sheep.step";
                }
                public static class Shulker
                {
                    public static string Ambient { get; } = "entity.shulker.ambient";
                    public static string Death { get; } = "entity.shulker.death";
                    public static string Hurt { get; } = "entity.shulker.hurt";
                    public static string HurtClosed { get; } = "entity.shulker.hurt_closed";
                    public static string Close { get; } = "entity.shulker.close";
                    public static string Open { get; } = "entity.shulker.open";
                    public static string Shoot { get; } = "entity.shulker.shoot";
                    public static string Teleport { get; } = "entity.shulker.teleport";
                }
                public static class ShulkerBullet
                {
                    public static string Hit { get; } = "entity.shulker_bullet.hit";
                    public static string Hurt { get; } = "entity.shulker_bullet.hurt";
                }
                public static class Silverfish
                {
                    public static string Ambient { get; } = "entity.silverfish.ambient";
                    public static string Death { get; } = "entity.silverfish.death";
                    public static string Hurt { get; } = "entity.silverfish.hurt";
                    public static string Step { get; } = "entity.silverfish.step";
                }
                public static class Skeleton
                {
                    public static string Ambient { get; } = "entity.skeleton.ambient";
                    public static string Death { get; } = "entity.skeleton.death";
                    public static string Hurt { get; } = "entity.skeleton.hurt";
                    public static string Step { get; } = "entity.skeleton.step";
                    public static string Shoot { get; } = "entity.skeleton.shoot";
                }
                public static class SkeletonHorse
                {
                    public static string Ambient { get; } = "entity.skeleton_horse.ambient";
                    public static string Death { get; } = "entity.skeleton_horse.death";
                    public static string Hurt { get; } = "entity.skeleton_horse.hurt";
                    public static string Step { get; } = "entity.skeleton_horse.step";
                }
                public static class Slime
                {
                    public static string Death { get; } = "entity.slime.death";
                    public static string Hurt { get; } = "entity.slime.hurt";
                    public static string Jump { get; } = "entity.slime.jump";
                    public static string Squish { get; } = "entity.slime.squish";

                    public static string SmallDeath { get; } = "entity.slime.death_small";
                    public static string SmallHurt { get; } = "entity.slime.hurt_small";
                    public static string SmallJump { get; } = "entity.slime.jump_small";
                    public static string SmallSquish { get; } = "entity.slime.squish_small";
                }
                public static string Snowball { get; } = "entity.snowball.throw";
                public static class Snowman
                {
                    public static string Ambient { get; } = "entity.snow_golem.ambient";
                    public static string Death { get; } = "entity.snow_golem.death";
                    public static string Hurt { get; } = "entity.snow_golem.hurt";
                    public static string Jump { get; } = "entity.snow_golem.jump";
                    public static string Shoot { get; } = "entity.snow_golem.shoot";
                }
                public static class Spider
                {
                    public static string Ambient { get; } = "entity.spider.ambient";
                    public static string Death { get; } = "entity.spider.death";
                    public static string Hurt { get; } = "entity.spider.hurt";
                    public static string Jump { get; } = "entity.spider.jump";
                    public static string Step { get; } = "entity.spider.step";
                }
                public static class SplashPotion
                {
                    public static string Throw { get; } = "entity.splash_potion.throw";
                    public static string Break { get; } = "entity.splash_potion.break";
                }
                public static class Squid
                {
                    public static string Ambient { get; } = "entity.squid.ambient";
                    public static string Death { get; } = "entity.squid.death";
                    public static string Hurt { get; } = "entity.squid.hurt";
                    public static string Squirt { get; } = "entity.squid.squirt";
                }
                public static class Stray
                {
                    public static string Ambient { get; } = "entity.stray.ambient";
                    public static string Death { get; } = "entity.stray.death";
                    public static string Hurt { get; } = "entity.stray.hurt";
                    public static string Step { get; } = "entity.stray.step";
                }
                public static string TNT { get; } = "entity.tnt.primed";
                public static class Vex
                {
                    public static string Ambient { get; } = "entity.vex.ambient";
                    public static string Death { get; } = "entity.vex.death";
                    public static string Hurt { get; } = "entity.vex.hurt";
                    public static string Charge { get; } = "entity.vex.step";
                }
                public static class Villager
                {
                    public static string Ambient { get; } = "entity.villager.ambient";
                    public static string Death { get; } = "entity.villager.death";
                    public static string Hurt { get; } = "entity.villager.hurt";
                    public static string No { get; } = "entity.villager.no";
                    public static string Yes { get; } = "entity.villager.yes";
                    public static string Trading { get; } = "entity.villager.trading";
                }
                public static class VindicationIllager
                {
                    public static string Ambient { get; } = "entity.vindicator.ambient";
                    public static string Death { get; } = "entity.vindicator.death";
                    public static string Hurt { get; } = "entity.vindicator.hurt";
                }
                public static class Witch
                {
                    public static string Ambient { get; } = "entity.witch.ambient";
                    public static string Death { get; } = "entity.witch.death";
                    public static string Hurt { get; } = "entity.witch.hurt";
                    public static string Drink { get; } = "entity.witch.drink";
                    public static string Throw { get; } = "entity.witch.throw";
                }
                public static class Wither
                {
                    public static string Ambient { get; } = "entity.wither.ambient";
                    public static string Death { get; } = "entity.wither.death";
                    public static string Hurt { get; } = "entity.wither.hurt";
                    public static string BreakBlock { get; } = "entity.wither.break_block";
                    public static string Shoot { get; } = "entity.wither.shoot";
                    public static string Spawn { get; } = "entity.wither.spawn";
                }
                public static class WitherSkeleton
                {
                    public static string Ambient { get; } = "entity.wither_skeleton.ambient";
                    public static string Death { get; } = "entity.wither_skeleton.death";
                    public static string Hurt { get; } = "entity.wither_skeleton.hurt";
                    public static string Step { get; } = "entity.wither_skeleton.step";
                }
                public static class Wolf
                {
                    public static string Ambient { get; } = "entity.wolf.ambient";
                    public static string Death { get; } = "entity.wolf.death";
                    public static string Hurt { get; } = "entity.wolf.hurt";
                    public static string Growl { get; } = "entity.wolf.growl";
                    public static string Howl { get; } = "entity.wolf.howl";
                    public static string Pant { get; } = "entity.wolf.pant";
                    public static string Shake { get; } = "entity.wolf.shake";
                    public static string Whine { get; } = "entity.wolf.whine";
                    public static string Step { get; } = "entity.wolf.step";
                }
                public static class Zombie
                {
                    public static string Ambient { get; } = "entity.zombie.ambient";
                    public static string WoodenDoor { get; } = "entity.zombie.attack_wooden_door";
                    public static string IronDoor { get; } = "entity.zombie.atack_iron_door";
                    public static string BreakWoodenDoor { get; } = "entity.zombie.break_wooden_door";
                    public static string Death { get; } = "entity.zombie.death";
                    public static string Hurt { get; } = "entity.zombie.hurt";
                    public static string Infect { get; } = "entity.zombie.infect";
                    public static string Step { get; } = "entity.zombie.step";
                }
                public static class Panda
                {
                    public static string Ambient { get; } = "entity.panda.ambient";
                    public static string Death { get; } = "entity.panda.death";
                    public static string Hurt { get; } = "entity.panda.hurt";
                    public static string Step { get; } = "entity.panda.step";
                    public static string AggressiveAmbient { get; } = "entity.panda.aggressive_ambient";
                    public static string Bite { get; } = "entity.panda.bite";
                    public static string CantBreed { get; } = "entity.panda.cant_breed";
                    public static string Eat { get; } = "entity.panda.eat";
                    public static string PreSneeze { get; } = "entity.panda.pre_sneeze";
                    public static string Sneeze { get; } = "entity.panda.seeze";
                    public static string WorriedAmbient { get; } = "entity.panda.worried_ambient";
                }
                public static class ZombieHorse
                {
                    public static string Ambient { get; } = "entity.zombie_horse.ambient";
                    public static string Death { get; } = "entity.zombie_horse.death";
                    public static string Hurt { get; } = "entity.zombie_horse.hurt";
                }
                public static class ZombiePigman
                {
                    public static string Ambient { get; } = "entity.zombie_pigman.ambient";
                    public static string Death { get; } = "entity.zombie_pigman.death";
                    public static string Hurt { get; } = "entity.zombie_pigman.hurt";
                    public static string Angry { get; } = "entity.zombie_pigman.angry";
                }
                public static class ZombieVillager
                {
                    public static string Ambient { get; } = "entity.zombie_villager.ambient";
                    public static string Converted { get; } = "entity.zombie_villager.converted";
                    public static string Cure { get; } = "entity.zombie_villager.cure";
                    public static string Death { get; } = "entity.zombie_villager.death";
                    public static string Hurt { get; } = "entity.zombie_villager.hurt";
                    public static string Step { get; } = "entity.zombie_villager.step";
                }
            }

            /// <summary>
            /// item sounds
            /// </summary>
            public static class Item
            {
                public static class Armor
                {
                    public static string Chain { get; } = "item.armor.equip_chain";
                    public static string Diamond { get; } = "item.armor.equip_diamond";
                    public static string Elytra { get; } = "item.armor.equip_elytra";
                    public static string Generic { get; } = "item.armor.equip_generic";
                    public static string Gold { get; } = "item.armor.equip_gold";
                    public static string Leather { get; } = "item.armor.equip_leather";
                    public static string Iron { get; } = "item.armor.equip_iron";
                }
                public static class Bottle
                {
                    public static string Empty { get; } = "item.bottle.fill";
                    public static string Fill { get; } = "item.bottle.fill";
                    public static string FillDragonBreath { get; } = "item.bottle.fill_dragonbreath";
                }
                public static class Book
                {
                    public static string TurnPage { get; } = "item.book.page_turn";
                    public static string Place { get; } = "item.book.put";
                }
                public static class Bucket
                {
                    public static string Empty { get; } = "item.bucket.empty";
                    public static string EmptyLava { get; } = "item.bucket.empty_lava";
                    public static string Fill { get; } = "item.bucket.fill";
                    public static string FillLava { get; } = "item.bucket.fill_lava";
                }
                public static string ChorusFruit { get; } = "item.chorus_fruit.teleport";
                public static string ElytraFly { get; } = "item.elytra.flying";
                public static string FireCharge { get; } = "item.firecharge.use";
                public static string FlintAndSteel { get; } = "item.flintandsteel.use";
                public static string Hoe { get; } = "item.hoe.till";
                public static class Shield
                {
                    public static string Block { get; } = "item.shield.block";
                    public static string Break { get; } = "item.shield.break";
                }
                public static string Shovel { get; } = "item.shovel.flattern";
                public static string Totem { get; } = "item.totem.use";
            }

            /// <summary>
            /// music
            /// </summary>
            public static class Music
            {
                public static string Creative { get; } = "music.creative";
                public static string Credits { get; } = "music.credits";
                public static string Dragon { get; } = "music.dragon";
                public static string End { get; } = "music.end";
                public static string Game { get; } = "music.game";
                public static string Menu { get; } = "music.menu";
                public static string Nether { get; } = "music.nether";
                public static string UnderWater { get; } = "music.under_water";
            }

            /// <summary>
            /// music discs music
            /// </summary>
            public static class MusicDisc
            {
                public static string Eleven { get; } = "music_disc.11";
                public static string Thirteen { get; } = "music_disc.13";
                public static string Blocks { get; } = "music_disc.blocks";
                public static string Cat { get; } = "music_disc.cat";
                public static string Chirp { get; } = "music_disc.chirp";
                public static string Far { get; } = "music_disc.far";
                public static string Mall { get; } = "music_disc.mall";
                public static string Mellohi { get; } = "music_disc.mellohi";
                public static string Stal { get; } = "music_disc.stall";
                public static string Strad { get; } = "music_disc.strad";
                public static string Wait { get; } = "music_disc.wait";
                public static string Ward { get; } = "music_disc.ward";
            }

            /// <summary>
            /// advancement/new recipe toast sounds
            /// </summary>
            public static class Toast
            {
                public static string In { get; } = "ui.toast.in";
                public static string Out { get; } = "ui.toast.out";
                public static string Complete { get; } = "ui.toast.challenge_complete";
            }

            /// <summary>
            /// weather sounds
            /// </summary>
            public static class Weather
            {
                public static string Rain { get; } = "weather.rain";
                public static string RainAbove { get; } = "weather.rain.above";
            }
        }
    }
}