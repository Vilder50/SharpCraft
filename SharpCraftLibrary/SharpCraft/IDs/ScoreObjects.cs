namespace SharpCraft
{
    public partial class ID
    {
        /// <summary>
        /// Places to show scores
        /// </summary>
        public enum ScoreDisplay
        {
            /// <summary>
            /// To display the player's score under the player's nametag ingame
            /// </summary>
            belowName,

            /// <summary>
            /// To display the player's score besides the player's name in the tab list
            /// </summary>
            list,

            /// <summary>
            /// To display the whole scoreboard in the sidebar list
            /// (Highest numbers are displayed first. This list only supports up to 15 scores) 
            /// </summary>
            sidebar
        }

        /// <summary>
        /// A list of all scoreboard objectives
        /// </summary>
        public static class Objective
        {
            /// <summary>
            /// Goes up by one when the player was killed by another player with the specified team color
            /// </summary>
            /// <param name="TeamColor">The team color to check for</param>
            public static string KilledByTeam(MinecraftColor TeamColor) { return "killedByTeam." + TeamColor; }

            /// <summary>
            /// Goes up by one when the player kills another with the specified team color
            /// </summary>
            /// <param name="TeamColor">The team color to check for</param>
            public static string KillTeam(MinecraftColor TeamColor) { return "teamkill." + TeamColor; }

            /// <summary>
            /// Goes up by one when the player breaks the specified item
            /// (As in uses the last durability)
            /// </summary>
            /// <param name="Item">the item to check for</param>
            public static string Broken(Item Item) { return "minecraft.broken:minecraft." + Item; }

            /// <summary>
            /// Goes up by one when the player crafts the specified item
            /// </summary>
            /// <param name="Item">the item to check for</param>
            public static string Crafted(Item Item) { return "minecraft.crafted:minecraft." + Item; }

            /// <summary>
            /// Goes up by one for each of the specified item the player drops
            /// </summary>
            /// <param name="Item">the item to check for</param>
            public static string Dropped(Item Item) { return "minecraft.dropped:minecraft." + Item; }

            /// <summary>
            /// Goes up by one when the player kills an entity of the specified type
            /// </summary>
            /// <param name="Entity">the entity to check for</param>
            public static string Killed(Entity Entity) { return "minecraft.killed:minecraft." + Entity; }

            /// <summary>
            /// Goes up by one when the player gets killed by an entity of the specified type
            /// </summary>
            /// <param name="Entity">the entity to check for</param>
            public static string KilledBy(Entity Entity) { return "minecraft.killed_by:minecraft." + Entity; }

            /// <summary>
            /// Goes up by one when the player breaks the specified block
            /// </summary>
            /// <param name="Block">the block to check for</param>
            public static string Mined(Block Block) { return "minecraft.mined:minecraft." + Block; }

            /// <summary>
            /// Goes up by one when the player picks up the specified item
            /// </summary>
            /// <param name="Item">the item to check for</param>
            public static string PickedUp(Item Item) { return "minecraft.picked_up:minecraft." + Item; }

            /// <summary>
            /// Goes up by one when the player uses the specified item
            /// </summary>
            /// <param name="Item">the item to check for</param>
            public static string Used(Item Item) { return "minecraft.used:minecraft." + Item; }

            /// <summary>
            /// The number of ticks since the player last slept
            /// (This is a readonly objective type)
            /// </summary>
            public static string LastSlept { get; } = "minecraft.custom:minecraft.time_since_rest";

            /// <summary>
            /// Goes up by one when the player opens the specified block's gui
            /// </summary>
            public static class Open
            {
                #pragma warning disable 1591
                public static string Dispenser { get; } = "minecraft.custom:minecraft.inspect_dispenser";
                public static string Dropper { get; } = "minecraft.custom:minecraft.inspect_dropper";
                public static string Hopper { get; } = "minecraft.custom:minecraft.inspect_hopper";
                public static string Beacon { get; } = "minecraft.custom:minecraft.interact_with_beacon";
                public static string BrewingStand { get; } = "minecraft.custom:minecraft.interact_with_brewing_stand";
                public static string CraftingTable { get; } = "minecraft.custom:minecraft.interact_with_crafting_table";
                public static string Furnace { get; } = "minecraft.custom:minecraft.interact_with_furnace";
                public static string Chest { get; } = "minecraft.custom:minecraft.open_chest";
                public static string Enderchest { get; } = "minecraft.custom:minecraft.open_enderchest";
                public static string ShulkerBox { get; } = "minecraft.custom:minecraft.open_shulker_box";
                public static string Villager { get; } = "minecraft.custom:minecraft.talked_to_villager";
                public static string TrappedChest { get; } = "minecraft.custom:minecraft.trigger_trapped_chest";
                public static string Barrel { get; } = "minecraft.custom:minecraft.open_barrel";
                #pragma warning restore 1591
            }

            /// <summary>
            /// Goes up one per cm the player moves in the specified way
            /// </summary>
            public static class Movement
            {
                /// <summary>
                /// Distance flown with Elytra
                /// </summary>
                public static string Aviate { get; } = "minecraft.custom:minecraft.aviate_one_cm";

                /// <summary>
                /// Distance ridden in boats
                /// </summary>
                public static string RideBoat { get; } = "minecraft.custom:minecraft.boat_one_cm";

                /// <summary>
                /// Distance climbed on ladders/leaves
                /// </summary>
                public static string Climb { get; } = "minecraft.custom:minecraft.climb_one_cm";

                /// <summary>
                /// Distance crouched / shifted / sneaking
                /// </summary>
                public static string Crouch { get; } = "minecraft.custom:minecraft.crouch_one_cm";

                /// <summary>
                /// Distance walked under water (not swum)
                /// </summary>
                public static string Dive { get; } = "minecraft.custom:minecraft.dive_one_cm";

                /// <summary>
                /// Distance falled
                /// </summary>
                public static string Fall { get; } = "minecraft.custom:minecraft.fall_one_cm";

                /// <summary>
                /// Distance flown in creative/spectator mode
                /// </summary>
                public static string Fly { get; } = "minecraft.custom:minecraft.fly_one_cm";

                /// <summary>
                /// Distance ridden on horses
                /// </summary>
                public static string RideHorse { get; } = "minecraft.custom:minecraft.horse_one_cm";

                /// <summary>
                /// Distance ridden on striders
                /// </summary>
                public static string RideStrider { get; } = "minecraft.custom:minecraft.strider_one_cm";

                /// <summary>
                /// Distance ridden minecarts
                /// </summary>
                public static string RideMinecart { get; } = "minecraft.custom:minecraft.minecart_one_cm";

                /// <summary>
                /// Distance ridden on pigs
                /// </summary>
                public static string RidePig { get; } = "minecraft.custom:minecraft.pig_one_cm";

                /// <summary>
                /// Distance sprinted/ran
                /// </summary>
                public static string Sprint { get; } = "minecraft.custom:minecraft.sprint_one_cm";

                /// <summary>
                /// Distance swum
                /// </summary>
                public static string Swim { get; } = "minecraft.custom:minecraft.swim_one_cm";

                /// <summary>
                /// Distance walked
                /// </summary>
                public static string Walk { get; } = "minecraft.custom:minecraft.walk_one_cm";
            }

            /// <summary>
            /// Goes up by one everytime the player dies
            /// </summary>
            public static string DeathCount { get; } = "deathCount";

            /// <summary>
            /// Goes up by one everytime the player breds two animals
            /// </summary>
            public static string AnimalsBred { get; } = "minecraft.custom:minecraft.animals_bred";

            /// <summary>
            /// Goes up by one everytime the player cleans leather armor in a cauldron
            /// </summary>
            public static string CleanArmor { get; } = "minecraft.custom:minecraft.clean_armor";

            /// <summary>
            /// Goes up by one everytime the player cleans a banner in a cauldron
            /// </summary>
            public static string CleanBanner { get; } = "minecraft.custom:minecraft.clean_banner";

            /// <summary>
            /// Goes up by however much damage the player deals
            /// </summary>
            public static string DamageDealt { get; } = "minecraft.custom:minecraft.damage_dealt";

            /// <summary>
            /// Goes up by however much damage the player takes
            /// </summary>
            public static string DamageTaken { get; } = "minecraft.custom:minecraft.damage_taken";

            /// <summary>
            /// Goes up by one everytime the player dies
            /// ...Yep... there are 2 objectives for this
            /// </summary>
            public static string Deaths { get; } = "minecraft.custom:minecraft.deaths";

            /// <summary>
            /// Goes up by one everytime the player drops something
            /// Note: dragging an item out of inventory doesnt count
            /// </summary>
            public static string Drop { get; } = "minecraft.custom:minecraft.drop";

            /// <summary>
            /// Goes up by one everytime the player eats a cake slice
            /// </summary>
            public static string EatCake { get; } = "minecraft.custom:minecraft.eat_cake_slice";

            /// <summary>
            /// Goes up by one everytime the player enchants an item
            /// </summary>
            public static string Enchant { get; } = "minecraft.custom:minecraft.enchant_item";

            /// <summary>
            /// Goes up by one everytime the player fills a cauldron with water
            /// </summary>
            public static string FillCauldron { get; } = "minecraft.custom:minecraft.fill_cauldron";

            /// <summary>
            /// Goes up by one for every fish the player caughts
            /// Note: "fish" is determined by the fish item group
            /// </summary>
            public static string FishCaught { get; } = "minecraft.custom:minecraft.fish_caught";

            /// <summary>
            /// Goes up by one everytime the player jumps
            /// </summary>
            public static string Jump { get; } = "minecraft.custom:minecraft.jump";

            /// <summary>
            /// Goes up by one when the player leaves the world
            /// Note: You can ofcourse first check for the score once the player has logged back into the world
            /// </summary>
            public static string Leave { get; } = "minecraft.custom:minecraft.leave_game";

            /// <summary>
            /// Goes up by one when the player kills an entity
            /// </summary>
            public static string MobKills { get; } = "minecraft.custom:minecraft.mob_kills";

            /// <summary>
            /// Goes up by one everytime the player plays a note on a noteblock
            /// </summary>
            public static string PlayNote { get; } = "minecraft.custom:minecraft.play_noteblock";

            /// <summary>
            /// Goes up by one every minute
            /// </summary>
            public static string PlayMinute { get; } = "minecraft.custom:minecraft.play_one_minute";

            /// <summary>
            /// Goes up by one when the player inserts a record into a jukebox
            /// </summary>
            public static string PlayRecord { get; } = "minecraft.custom:minecraft.play_record";

            /// <summary>
            /// Goes up by one everytime the player kills another player
            /// </summary>
            public static string PlayerKills { get; } = "minecraft.custom:minecraft.player_kills";

            /// <summary>
            /// Goes up by one everytime the player pots a flower in a flower pot
            /// </summary>
            public static string PotFlower { get; } = "minecraft.custom:minecraft.pot_flower";

            /// <summary>
            /// Goes up by one per tick the player has been in a bed
            /// </summary>
            public static string Sleep { get; } = "minecraft.custom:minecraft.sleep_in_bed";

            /// <summary>
            /// Goes up by one per tick while the player is sneaking / shifting / crouching
            /// </summary>
            public static string SneakTime { get; } = "minecraft.custom:minecraft.sneak_time";

            /// <summary>
            /// Goes up by one everytime the player tunes a noteblock
            /// </summary>
            public static string TuneNote { get; } = "minecraft.custom:minecraft.tune_noteblock";

            /// <summary>
            /// Goes up by one everytime the player uses a cauldron
            /// </summary>
            public static string UseCauldron { get; } = "minecraft.custom:minecraft.use_cauldron";

            /// <summary>
            /// Goes up by one everyime the player kills an entity
            /// ...Yep... there are 2 scoreboard objectives for this
            /// </summary>
            public static string TotalKills { get; } = "totalKillCount";

            /// <summary>
            /// The number of xp the player has
            /// (This is a readonly objective type)
            /// </summary>
            public static string XP { get; } = "xp";

            /// <summary>
            /// The number of air bubbles the player has
            /// (This is a readonly objective type)
            /// </summary>
            public static string Air { get; } = "air";

            /// <summary>
            /// The number of armor bars the player has
            /// (This is a readonly objective type)
            /// </summary>
            public static string Armor { get; } = "armor";

            /// <summary>
            /// The number of hearts the player has
            /// (Measured in half hearts)
            /// (This is a readonly objective type)
            /// </summary>
            public static string Health { get; } = "health";

            /// <summary>
            /// An objective the player self can change with /trigger
            /// </summary>
            public static string Trigger { get; } = "trigger";

            /// <summary>
            /// An objective which doesnt go up/down on its own.
            /// Its value can only be altered with commands
            /// </summary>
            public static string Dummy { get; } = "dummy";

            /// <summary>
            /// The number of food bars the player has
            /// (Measured in half food bars)
            /// (This is a readonly objective type)
            /// </summary>
            public static string Food { get; } = "food";

            /// <summary>
            /// The number of levels the player has
            /// (This is a readonly objective type)
            /// </summary>
            public static string Level { get; } = "level";
        }
    }
}
