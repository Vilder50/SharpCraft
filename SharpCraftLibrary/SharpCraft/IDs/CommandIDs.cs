namespace SharpCraft
{
    /// <summary>
    /// All the different ID's/Types/States things in the game can have
    /// </summary>
    public static partial class ID
    {
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
            s
        }
        public enum Sort { nearest, furthest, random, arbitrary }
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
        public enum BoolGamerule
        {
            /// <summary>
            /// If the "Player has gotten advancement" message should be displayed
            /// </summary>
            announceAdvancements,

            /// <summary>
            /// If command blocks should tell admins when they run a command
            /// </summary>
            commandBlockOutput,

            /// <summary>
            /// If the server should skip checking player speed when flying with an elytra.
            /// </summary>
            disableElytraMovementCheck,

            /// <summary>
            /// If raids should be disabled
            /// </summary>
            disableRaids,

            /// <summary>
            /// If the daylight cycle runs
            /// </summary>
            doDaylightCycle,

            /// <summary>
            /// If entities should drop anything
            /// </summary>
            doEntityDrops,

            /// <summary>
            /// If fire should be able to spread
            /// </summary>
            doFireTick,

            /// <summary>
            /// If players only should be able to craft recipes they own
            /// </summary>
            doLimitedCrafting,

            /// <summary>
            /// If mobs should drop their loot
            /// </summary>
            doMobLoot,

            /// <summary>
            /// If mobs should be able to spawn naturally
            /// </summary>
            doMobSpawning,

            /// <summary>
            /// If blocks should drop their loot
            /// </summary>
            doTileDrops,

            /// <summary>
            /// If the weather cycle runs
            /// </summary>
            doWeatherCycle,

            /// <summary>
            /// If players should keep their inventory on death
            /// </summary>
            keepInventory,

            /// <summary>
            /// If admin commands should be logged in the server log
            /// </summary>
            logAdminCommands,

            /// <summary>
            /// If mobs should be able to grief/change blocks
            /// </summary>
            mobGriefing,

            /// <summary>
            /// If players should be able to heal if they have enough food
            /// </summary>
            naturalRegeneration,

            /// <summary>
            /// If the debug screen (f3) should show limited information
            /// </summary>
            reducedDegubInfo,

            /// <summary>
            /// If commands should tell players if they were successfull or not
            /// </summary>
            sendCommandFeedback,

            /// <summary>
            /// If death messages should be displayed in chat
            /// </summary>
            showDeathMessages,

            /// <summary>
            /// If spectators are able to generate new chunks
            /// </summary>
            spectatorsGenerateChunks,

            /// <summary>
            /// If phantoms should spawn when player havent slept for some time
            /// </summary>
            doInsomnia,

            /// <summary>
            /// If players should respawn immediatly
            /// </summary>
            doImmediateRespawn,

            /// <summary>
            /// If players should take damage from drowning
            /// </summary>
            drowningDamage,

            /// <summary>
            /// If players should take fall damage
            /// </summary>
            fallDamage,

            /// <summary>
            /// If players should take fire damage
            /// </summary>
            fireDamage,

            /// <summary>
            /// If angered neutral mobs should stop being angry when the player dies
            /// </summary>
            forgiveDeadPlayers,

            /// <summary>
            /// If angered neutral mobs should attack everyone
            /// </summary>
            universalAnger,
        }
        public enum IntGamerule
        {
            /// <summary>
            /// The amount of entities there can be in a crammed space before they start taking damage
            /// </summary>
            maxEntityCramming,

            /// <summary>
            /// The amount of random block ticks which happens per chunk per tick
            /// </summary>
            randomTickSpeed,

            /// <summary>
            /// The max distance players can spawn away from the world spawn center
            /// </summary>
            spawnRadius,

            /// <summary>
            /// The maximum amount of commands which can run at once
            /// </summary>
            maxCommandChainLength
        }
        public enum HorseSlot
        {
            saddle,
            chest,
            armor
        }
        public enum TeamDisplayName
        {
            prefix,
            suffix,
            displayName
        }
        public enum QueryTime
        {
            /// <summary>
            /// Returns the amount of days which has passed in the world
            /// </summary>
            day,

            /// <summary>
            /// Returns the time of day
            /// </summary>
            daytime,

            /// <summary>
            /// Returns the total amount of time there has passed in this world (based of what time it is)
            /// </summary>
            gametime
        }
        public enum AddSetModifier
        {
            add,
            set
        }
        public enum ScoreChange
        {
            add,
            remove,
            set
        }
        public enum BlockClone { replace, filtered, masked }
        public enum BlockCloneWay { normal, move, force }
        public enum SoundSource { ambient, block, hostile, master, music, neutral, player, record, voice, weather }
        public enum RelativeAdvancement
        {
            /// <summary>
            /// Selects all the child advancements of this advancement
            /// </summary>
            from,
            /// <summary>
            /// Selects all advancements on the same branch as this advancement
            /// </summary>
            through,
            /// <summary>
            /// Selects all the ancestor advancements of this advancement
            /// </summary>
            until
        }
        public enum BossBarValue
        {
            /// <summary>
            /// The max value the boss bar can have
            /// </summary>
            max,
            /// <summary>
            /// The amount of players who can see the boss bar
            /// </summary>
            players,
            /// <summary>
            /// The value displayed in the boss bar
            /// </summary>
            value,
            /// <summary>
            /// 1 if visible. 0 if not visible
            /// </summary>
            visible
        }
        public enum DatapackList
        {
            all,
            enabled,
            disabled
        }
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
        public enum FacingAnchor { feet, eyes }
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
        public enum DatapackPlace { after, before, first, last }
        public enum Difficulty { peaceful, easy, normal, hard }
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
        public enum StoreTypes { Byte, Double, Float, Int, Long, Short }
        public enum ArmorSlot { feed, legs, chest, head }
        public enum WeatherType { rain, thunder, clear }
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
        public enum EntityDataModifierType { append, merge, prepend, set }
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
#pragma warning restore 1591
    }
}
