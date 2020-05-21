using SharpCraft.Commands;
using System.Linq;

namespace SharpCraft.FunctionWriters
{
    /// <summary>
    /// All commands for the world
    /// </summary>
    public class WorldCommands : CommandList
    {

        internal WorldCommands(Function function) : base(function)
        {
            Objective = new ClassObjective(function);
            Team = new ClassTeam(function);
            Datapack = new ClassDatapack(function);
            Time = new ClassTime(function);
            Border = new ClassBorder(function);
            BossBar = new ClassBossBar(function);
            LoadSquare = new ClassLoadSquare(function);
            Data = new ClassData(function);
        }

        /// <summary>
        /// Changes the world's spawnpoint to the specified location
        /// </summary>
        /// <param name="coords">The new location of the world spawnpoint</param>
        public void Spawn(Vector coords)
        {
            ForFunction.AddCommand(new SetWorldSpawnCommand(coords));
        }

        /// <summary>
        /// Commands for loading chunks
        /// </summary>
        public readonly ClassLoadSquare LoadSquare;

        /// <summary>
        /// Commands for loading chunks
        /// </summary>
        public class ClassLoadSquare
        {
            readonly Function function;
            internal ClassLoadSquare(Function function)
            {
                this.function = function;
            }

            /// <summary>
            /// Loads the chunks containing the coordinates from corner to oppesiteCorner
            /// </summary>
            /// <param name="corner">One of the corners of the square to load</param>
            /// <param name="oppesiteCorner">The oppesite corner in the square to load</param>
            public void ForceLoad(Vector corner, Vector oppesiteCorner)
            {
                function.AddCommand(new ForceloadChunksCommand(corner, oppesiteCorner, true));
            }

            /// <summary>
            /// Loads the chunk containing the coordinate
            /// </summary>
            /// <param name="coordinate">Coordinate in the chunk to load</param>
            public void ForceLoad(Vector coordinate)
            {
                function.AddCommand(new ForceloadChunkCommand(coordinate, true));
            }

            /// <summary>
            /// stops the chunks containing the coordinates from corner to oppesiteCorner from being forced loaded
            /// </summary>
            /// <param name="corner">One of the corners of the square to stop loading</param>
            /// <param name="oppesiteCorner">The oppesite corner in the square to stop loading</param>
            public void StopLoad(Vector corner, Vector oppesiteCorner)
            {
                function.AddCommand(new ForceloadChunksCommand(corner, oppesiteCorner, false));
            }

            /// <summary>
            /// Stops the chunk at the coordinate from being forcedloaded
            /// </summary>
            /// <param name="coordinate">Coordinate in the chunk to stop loading</param>
            public void StopLoad(Vector coordinate)
            {
                function.AddCommand(new ForceloadChunkCommand(coordinate, false));
            }

            /// <summary>
            /// Stops all chunks from being forced loaded
            /// </summary>
            public void StopLoad()
            {
                function.AddCommand(new ForceloadRemoveAllCommand());
            }

            /// <summary>
            /// Checks if the given coords are loaded
            /// </summary>
            /// <param name="coordinate">The coordinate to check if loaded</param>
            public void IsLoaded(Vector coordinate)
            {
                function.AddCommand(new ForceloadQueryChunkCommand(coordinate));
            }
        }


        /// <summary>
        /// Says a message in the chat
        /// </summary>
        /// <param name="text">the text to say in chat</param>
        public void Say(string text)
        {
            ForFunction.AddCommand(new SayCommand(text));
        }

        /// <summary>
        /// Changes the default gamemode players are in when they spawn for the first time
        /// </summary>
        /// <param name="gamemode">the new default gamemode</param>
        public void DefaultGamemode(ID.Gamemode gamemode)
        {
            ForFunction.AddCommand(new DefaultGamemodeCommand(gamemode));
        }

        /// <summary>
        /// Changes the difficulty of the world
        /// </summary>
        /// <param name="difficulty">the new difficulty</param>
        public void Difficulty(ID.Difficulty difficulty)
        {
            ForFunction.AddCommand(new DifficultyCommand(difficulty));
        }

        /// <summary>
        /// Spawns loot into the world at the given location
        /// </summary>
        /// <param name="coords">The location to spawn the loot</param>
        /// <param name="loot">the <see cref="LootTable"/> to spawn in</param>
        public void Loot(Vector coords, ILootTable loot)
        {
            ForFunction.AddCommand(new LootCommand(new LootTargets.SpawnTarget(coords), new LootSources.LoottableSource(loot)));
        }
        /// <summary>
        /// Spawns loot into the world at the given location
        /// </summary>
        /// <param name="coords">The location to spawn the loot</param>
        /// <param name="kill">the entity whose "when killed loot" should be dropped</param>
        public void Loot(Vector coords, BaseSelector kill)
        {
            ForFunction.AddCommand(new LootCommand(new LootTargets.SpawnTarget(coords), new LootSources.KillSource(kill)));
        }
        /// <summary>
        /// Spawns loot into the world at the given location
        /// </summary>
        /// <param name="coords">The location to spawn the loot</param>
        /// <param name="breakBlock">the block whose "when mined loot" should be dropped</param>
        /// <param name="breakWith">the tool used to break the block</param>
        public void Loot(Vector coords, Vector breakBlock, Item breakWith)
        {
            if (breakWith is null)
            {
                ForFunction.AddCommand(new LootCommand(new LootTargets.SpawnTarget(coords), new LootSources.MineHandSource(breakBlock, true)));
            }
            else
            {
                ForFunction.AddCommand(new LootCommand(new LootTargets.SpawnTarget(coords), new LootSources.MineItemSource(breakBlock, breakWith)));
            }
        }

        /// <summary>
        /// Runs the specified function
        /// </summary>
        /// <param name="runFunction">the function to run</param>
        /// <param name="delay">the amount of time to function execution should be delayed. null doesnt delay it. 
        /// (If value is other than null the function will ignore the arguments send in the execute command which executed it)</param>
        /// <param name="append">If the function is being scheduled: if false replace the last time the function was scheduled</param>
        /// <returns>The ran function</returns>
        public IFunction Function(IFunction runFunction, NoneNegativeTime<int>? delay = null, bool append = true)
        {
            if (delay == null)
            {
                ForFunction.AddCommand(new RunFunctionCommand(runFunction));
            }
            else
            {
                ForFunction.AddCommand(new ScheduleAddCommand(runFunction, delay, append));
            }

            return runFunction;
        }

        /// <summary>
        /// Runs the given commands as a function (The function is made as a sibling function)
        /// </summary>
        /// <param name="writer">the commands to run</param>
        /// <param name="delay">the amount of time to function execution should be delayed. null doesnt delay it. 
        /// (If value is other than null the function will ignore the arguments send in the execute command which executed it)</param>
        /// <param name="append">If the function is being scheduled: if false replace the last time the function was scheduled</param>
        /// <returns>The function</returns>
        public Function Function(Function.FunctionWriter writer, NoneNegativeTime<int>? delay = null, bool append = true)
        {
            return (Function)Function(ForFunction.NewSibling(writer), delay, append);
        }

        /// <summary>
        /// Clears the schedule for the given function
        /// </summary>
        /// <param name="function">The function to clear the schedule for</param>
        public void StopSchedule(IFunction function)
        {
            ForFunction.AddCommand(new ScheduleClearCommand(function));
        }

        /// <summary>
        /// Changes the weather to the specified type
        /// </summary>
        /// <param name="SetTo">The new type of weather</param>
        /// <param name="WeatherTime">The number of ticks the weather should be going. Null means the game chose</param>
        public void Weather(ID.WeatherType SetTo, NoneNegativeTime<int> WeatherTime)
        {
            ForFunction.AddCommand(new WeatherCommand(SetTo, WeatherTime));
        }

        /// <summary>
        /// Changes or gets a gamerule
        /// </summary>
        /// <param name="gamerule">The gamerule to change or get</param>
        /// <param name="setValue">The value to change the gamerule to. Null returns the gamerule value instead</param>
        public void Gamerule(ID.BoolGamerule gamerule, bool? setValue)
        {
            ForFunction.AddCommand(new GameruleSetBoolCommand(gamerule, setValue));
        }

        /// <summary>
        /// Changes or gets a gamerule
        /// </summary>
        /// <param name="gamerule">The gamerule to change or get</param>
        /// <param name="setValue">The value to change the gamerule to. Null returns the gamerule value instead</param>
        public void Gamerule(ID.IntGamerule gamerule, int? setValue)
        {
            ForFunction.AddCommand(new GameruleSetIntCommand(gamerule, setValue));
        }

        /// <summary>
        /// The data commands
        /// </summary>
        public ClassData Data;

        /// <summary>
        /// The data commands
        /// </summary>
        public class ClassData
        {
            readonly Function function;
            internal ClassData(Function function)
            {
                this.function = function;
            }

            /// <summary>
            /// Adds the given data to the <see cref="Storage"/>
            /// </summary>
            /// <param name="data">The data to give to the <see cref="Storage"/></param>
            /// <param name="storage">the <see cref="Storage"/> to give the data to</param>
            public void Change(Storage storage, Data.SimpleDataHolder data)
            {
                function.AddCommand(new DataMergeStorageCommand(storage, data));
            }

            /// <summary>
            /// Copies data into the <see cref="SharpCraft.Block"/>
            /// </summary>
            /// <param name="storage">the <see cref="Storage"/> to copy the data to</param>
            /// <param name="toDataPath">the datapath to copy to</param>
            /// <param name="modifierType">The way to data should be copied in</param>
            /// <param name="copyData">The data to insert</param>
            public void Change(Storage storage, string toDataPath, ID.EntityDataModifierType modifierType, Data.SimpleDataHolder copyData)
            {
                function.AddCommand(new DataModifyWithDataCommand(new StorageDataLocation(storage, toDataPath), modifierType, copyData));
            }

            /// <summary>
            /// Copies data into the <see cref="SharpCraft.Block"/>
            /// </summary>
            /// <param name="storage">the <see cref="Storage"/> to copy the data to</param>
            /// <param name="toDataPath">the datapath to copy to</param>
            /// <param name="index">The index to copy the data to</param>
            /// <param name="copyData">The data to insert</param>
            public void Change(Storage storage, string toDataPath, int index, Data.SimpleDataHolder copyData)
            {
                function.AddCommand(new DataModifyInsertDataCommand(new StorageDataLocation(storage, toDataPath), index, copyData));
            }

            /// <summary>
            /// Removes the data from the <see cref="SharpCraft.Block"/> at the given datapath
            /// </summary>
            /// <param name="dataPath">The datapath</param>
            /// <param name="storage">the <see cref="Storage"/> to remove the data from</param>
            public void Remove(Storage storage, string dataPath)
            {
                function.AddCommand(new DataDeleteCommand(new StorageDataLocation(storage, dataPath)));
            }

            /// <summary>
            /// Gets the numeric data from the datapath from the <see cref="SharpCraft.Block"/>
            /// </summary>
            /// <param name="storage">the <see cref="Storage"/> to get the data from</param>
            /// <param name="dataPath">the datapath to the data</param>
            /// <param name="scale">the number to multiply the numeric value with</param>
            public void Get(Storage storage, string dataPath, double scale = 1)
            {
                function.AddCommand(new DataGetCommand(new StorageDataLocation(storage, dataPath), scale));
            }

            /// <summary>
            /// Copies data into the block at given location
            /// </summary>
            /// <param name="storage">the <see cref="Storage"/> to copy the data to</param>
            /// <param name="toDataPath">the datapath to copy to</param>
            /// <param name="modifierType">The way to data should be copied in</param>
            /// <param name="dataLocation">The place to copy the data from</param>
            public void Copy(Storage storage, string toDataPath, ID.EntityDataModifierType modifierType, IDataLocation dataLocation)
            {
                function.AddCommand(new DataModifyWithLocationCommand(new StorageDataLocation(storage, toDataPath), modifierType, dataLocation));
            }

            /// <summary>
            /// Copies data into the block at the given location
            /// </summary>
            /// <param name="storage">the <see cref="Storage"/> to copy the data to</param>
            /// <param name="toDataPath">the datapath to copy to</param>
            /// <param name="index">The index to copy the data to</param>
            /// <param name="dataLocation">The place to copy the data from</param>
            public void Copy(Storage storage, string toDataPath, int index, IDataLocation dataLocation)
            {
                function.AddCommand(new DataModifyInsertLocationCommand(new StorageDataLocation(storage, toDataPath), index, dataLocation));
            }
        }

        /// <summary>
        /// All commands for scoreboards
        /// </summary>
        public ClassObjective Objective;
        /// <summary>
        /// All commands for scoreboards
        /// </summary>
        public class ClassObjective : CommandList
        {
            internal ClassObjective(Function function) : base(function)
            {
                
            }
            /// <summary>
            /// Adds the specified <see cref="SharpCraft.Objective"/> to the world
            /// </summary>
            /// <param name="scoreName">The name of the <see cref="SharpCraft.Objective"/></param>
            /// <param name="type">the type of the scoreboard. See <see cref="ID.Objective"/> for a list of types</param>
            /// <param name="displayName">The name to display when the scoreboard is viewed in the sidebar</param>
            /// <returns>the newly created <see cref="SharpCraft.Objective"/></returns>
            public Objective Add(string scoreName, string type = "dummy", BaseJsonText? displayName = null)
            {
                Objective newObject = new Objective(scoreName);
                ForFunction.AddCommand(new ScoreboardObjectiveAddCommand(newObject, type, displayName));
                return newObject;
            }

            /// <summary>
            /// Removes the specified <see cref="SharpCraft.Objective"/> from the world
            /// </summary>
            /// <param name="objective">the <see cref="SharpCraft.Objective"/> to remove</param>
            public void Remove(Objective objective)
            {
                ForFunction.AddCommand(new ScoreboardObjectiveRemoveCommand(objective));
            }

            /// <summary>
            /// Displays the specified <see cref="SharpCraft.Objective"/> in the specified display slot
            /// Note: each slot can only display one <see cref="SharpCraft.Objective"/>
            /// </summary>
            /// <param name="objective">the <see cref="SharpCraft.Objective"/> to display</param>
            /// <param name="display">the display slot to display it in</param>
            /// <param name="teamColor">only teams with this color can see it. Null makes everyone see it. Note: this only works with <see cref="ID.ScoreDisplay.sidebar"/> as the <paramref name="display"/></param>
            public void Display(Objective objective, ID.ScoreDisplay display, ID.MinecraftColor? teamColor = null)
            {
                if (display == ID.ScoreDisplay.sidebar && !(teamColor is null))
                {
                    ForFunction.AddCommand(new ScoreboardSetTeamDisplayCommand(objective, teamColor.Value));
                }
                else
                {
                    ForFunction.AddCommand(new ScoreboardSetDisplayCommand(objective, display));
                }
            }
            /// <summary>
            /// Clears a display slot
            /// </summary>
            /// <param name="display">the display slot to clear</param>
            /// <param name="teamColor">the team's sidebar slot to clear. Note: this only works with <see cref="ID.ScoreDisplay.sidebar"/> as the <paramref name="display"/></param>
            public void ClearDisplay(ID.ScoreDisplay display, ID.MinecraftColor? teamColor = null)
            {
                if (display == ID.ScoreDisplay.sidebar && !(teamColor is null))
                {
                    ForFunction.AddCommand(new ScoreboardSetTeamDisplayCommand(null, teamColor.Value));
                }
                else
                {
                    ForFunction.AddCommand(new ScoreboardSetDisplayCommand(null, display));
                }
            }
            /// <summary>
            /// Changes the <see cref="SharpCraft.Objective"/>'s displayed name
            /// </summary>
            /// <param name="objective">the <see cref="SharpCraft.Objective"/> to change</param>
            /// <param name="name">The new display name</param>
            public void DisplayName(Objective objective, BaseJsonText name)
            {
                ForFunction.AddCommand(new ScoreboardObjectiveChangeNameCommand(objective, name));
            }
            /// <summary>
            /// Changes the way the <see cref="SharpCraft.Objective"/> is rendered in the <see cref="ID.ScoreDisplay.list"/> display slot
            /// </summary>
            /// <param name="objective">the <see cref="SharpCraft.Objective"/> to change</param>
            /// <param name="render">The way it should be rendered</param>
            public void Render(Objective objective, ID.ObjectiveRender render)
            {
                ForFunction.AddCommand(new ScoreboardObjectiveChangeRenderCommand(objective, render));
            }
        }

        /// <summary>
        /// All commands for teams
        /// </summary>
        public ClassTeam Team;
        /// <summary>
        /// All commands for teams
        /// </summary>
        public class ClassTeam : CommandList
        {
            internal ClassTeam(Function function) : base(function)
            {
                
            }
            /// <summary>
            /// Adds the specified <see cref="Team"/> to the world
            /// </summary>
            /// <param name="teamName">the <see cref="Team"/>'s name</param>
            /// <param name="displayName">the displayed name of the <see cref="Team"/></param>
            /// <param name="teamColor">the color of the <see cref="Team"/>. If null the <see cref="Team"/> will have the default color (white)</param>
            /// <returns>the newly created <see cref="Team"/></returns>
            public Team Add(string teamName, BaseJsonText displayName, ID.MinecraftColor? teamColor = null)
            {
                Team creating = new Team(teamName);

                if (teamColor is null)
                {
                    ForFunction.AddCommand(new TeamAddCommand(creating, displayName));
                }
                else
                {
                    ForFunction.Custom.GroupCommands((g) =>
                    {
                        g.AddCommand(new TeamAddCommand(creating, displayName));
                        g.AddCommand(new TeamModifyColorCommand(creating, teamColor.Value));
                    });
                }

                return creating;
            }
            /// <summary>
            /// Removes the specified <see cref="Team"/> from the world
            /// </summary>
            /// <param name="removeTeam">the <see cref="Team"/> to remove</param>
            public void Remove(Team removeTeam)
            {
                ForFunction.AddCommand(new TeamRemoveCommand(removeTeam));
            }
            /// <summary>
            /// Changes the color of the specified <see cref="Team"/>
            /// </summary>
            /// <param name="changeTeam">the <see cref="Team"/> to change</param>
            /// <param name="color">The new color</param>
            public void Color(Team changeTeam, ID.MinecraftColor color)
            {
                ForFunction.AddCommand(new TeamModifyColorCommand(changeTeam, color));
            }
            /// <summary>
            /// Changes how the specified <see cref="Team"/>'s death messages are displayed
            /// </summary>
            /// <param name="changeTeam">the <see cref="Team"/> to change</param>
            /// <param name="visible">the visibility rule</param>
            public void DeathMessage(Team changeTeam, ID.TeamVisibility visible)
            {
                ForFunction.AddCommand(new TeamModifyDeathMessageCommand(changeTeam, visible));
            }
            /// <summary>
            /// Changes how the specified <see cref="Team"/> collides things
            /// </summary>
            /// <param name="changeTeam">the <see cref="Team"/> to change</param>
            /// <param name="collision">the collision rule</param>
            public void Collision(Team changeTeam, ID.TeamCollision collision)
            {
                ForFunction.AddCommand(new TeamModifyCollisionCommand(changeTeam, collision));
            }
            /// <summary>
            /// Changes if the specified <see cref="Team"/> can damage players on their own team or not
            /// </summary>
            /// <param name="changeTeam">the <see cref="Team"/> to change</param>
            /// <param name="friendlyFire">If the team should be able to damage their own team or not</param>
            public void FriendlyFire(Team changeTeam, bool friendlyFire)
            {
                ForFunction.AddCommand(new TeamModifyFriendlyFireCommand(changeTeam, friendlyFire));
            }
            /// <summary>
            /// Changes how the specified <see cref="Team"/> is displayed when invisible
            /// </summary>
            /// <param name="changeTeam">the <see cref="Team"/> to change</param>
            /// <param name="see">the visibility rule</param>
            public void SeeInvisibleFriends(Team changeTeam, bool see)
            {
                ForFunction.AddCommand(new TeamModifyInvisibilityCommand(changeTeam, see));
            }
            /// <summary>
            /// Changes how the specified <see cref="Team"/>'s nametags are visible
            /// </summary>
            /// <param name="changeTeam">the <see cref="Team"/> to change</param>
            /// <param name="visible">the visibility tule</param>
            public void Nametag(Team changeTeam, ID.TeamVisibility visible)
            {
                ForFunction.AddCommand(new TeamModifyNameVisibilityCommand(changeTeam, visible));
            }
            /// <summary>
            /// Changes the display name of the specified <see cref="Team"/>
            /// </summary>
            /// <param name="changeTeam">the <see cref="Team"/> to change</param>
            /// <param name="name">The new name of the team</param>
            public void DisplayName(Team changeTeam, BaseJsonText name)
            {
                ForFunction.AddCommand(new TeamModifyDisplayCommand(changeTeam, ID.TeamDisplayName.displayName, name));
            }
            /// <summary>
            /// Removes all players from the specified <see cref="Team"/>
            /// </summary>
            /// <param name="clearTeam">the <see cref="Team"/> to remove players from</param>
            public void Clear(Team clearTeam)
            {
                ForFunction.AddCommand(new TeamEmptyCommand(clearTeam));
            }
            /// <summary>
            /// Changes the prefix shown before the name of players in the specified <see cref="Team"/>
            /// </summary>
            /// <param name="changeTeam">the <see cref="Team"/> to change</param>
            /// <param name="preffixJson">The new prefix to show</param>
            public void Prefix(Team changeTeam, BaseJsonText preffixJson)
            {
                ForFunction.AddCommand(new TeamModifyDisplayCommand(changeTeam, ID.TeamDisplayName.prefix, preffixJson));
            }
            /// <summary>
            /// Changes the suffix shown after the name of players in the specified <see cref="Team"/>
            /// </summary>
            /// <param name="changeTeam">the <see cref="Team"/> to change</param>
            /// <param name="suffixJson">the new prefix to show</param>
            public void Suffix(Team changeTeam, BaseJsonText suffixJson)
            {
                ForFunction.AddCommand(new TeamModifyDisplayCommand(changeTeam, ID.TeamDisplayName.suffix, suffixJson));
            }
        }

        /// <summary>
        /// All commands for datapacks
        /// </summary>
        public ClassDatapack Datapack;
        /// <summary>
        /// All commands for datapacks
        /// </summary>
        public class ClassDatapack : CommandList
        {
            internal ClassDatapack(Function function) : base(function)
            {
                
            }
            /// <summary>
            /// Disables the specified <see cref="SharpCraft.Datapack"/>
            /// </summary>
            /// <param name="datapack">the <see cref="SharpCraft.Datapack"/> to disable</param>
            public void Disable(BaseDatapack datapack)
            {
                ForFunction.AddCommand(new DatapackDisableCommand(datapack));
            }
            /// <summary>
            /// Enables the specified <see cref="SharpCraft.Datapack"/>
            /// </summary>
            /// <param name="datapack">the <see cref="SharpCraft.Datapack"/> to enable</param>
            /// <param name="placeAt">choses where the <see cref="SharpCraft.Datapack"/> should be placed relative to other enabled <see cref="SharpCraft.Datapack"/>s</param>
            /// <param name="otherPack">the <see cref="SharpCraft.Datapack"/> the <paramref name="datapack"/> is placed relative to</param>
            public void Enable(BaseDatapack datapack, ID.DatapackPlace placeAt, Datapack? otherPack = null)
            {
                if (otherPack is null)
                {
                    ForFunction.AddCommand(new DatapackEnableCommand(datapack, placeAt == ID.DatapackPlace.first));
                }
                else
                {
                    ForFunction.AddCommand(new DatapackEnableAtCommand(datapack, placeAt == ID.DatapackPlace.after, otherPack));
                }
            }
        }

        /// <summary>
        /// All commands for time
        /// </summary>
        public ClassTime Time;
        /// <summary>
        /// All commands for time
        /// </summary>
        public class ClassTime : CommandList
        {
            internal ClassTime(Function function) : base(function)
            {
                
            }
            /// <summary>
            /// Adds the specified amount of <see cref="Time"/> to the time of day
            /// </summary>
            /// <param name="time">the <see cref="Time"/> to add</param>
            public void Add(NoneNegativeTime<int> time)
            {
                ForFunction.AddCommand(new TimeModifyCommand(time, ID.AddSetModifier.add));
            }
            /// <summary>
            /// Sets the time of day to the specified <see cref="Time"/>
            /// </summary>
            /// <param name="time">the <see cref="Time"/> to set it to</param>
            public void Set(NoneNegativeTime<int> time)
            {
                ForFunction.AddCommand(new TimeModifyCommand(time, ID.AddSetModifier.set));
            }
            /// <summary>
            /// Gets the amount of days which has gone by
            /// </summary>
            public void GetDay()
            {
                ForFunction.AddCommand(new TimeQueryCommand(ID.QueryTime.day));
            }
            /// <summary>
            /// Gets the time of day it is in ticks
            /// (0-24000)
            /// </summary>
            public void GetDayTime()
            {
                ForFunction.AddCommand(new TimeQueryCommand(ID.QueryTime.daytime));
            }
            /// <summary>
            /// Gets the time of the world
            /// (Pretty much just <see cref="GetDay"/> * 24000 + <see cref="GetDayTime"/>)
            /// </summary>
            public void GetTime()
            {
                ForFunction.AddCommand(new TimeQueryCommand(ID.QueryTime.gametime));
            }
        }

        /// <summary>
        /// All commands for the world border
        /// </summary>
        public ClassBorder Border;
        /// <summary>
        /// All commands for the world border
        /// </summary>
        public class ClassBorder : CommandList
        {
            internal ClassBorder(Function function) : base(function)
            {
                
            }
            /// <summary>
            /// Adds blocks to the world border size
            /// </summary>
            /// <param name="add">The amount of blocks to add. 
            /// Note: the blocks are spread out from the center, so adding 1 block adds a half block to all sides
            /// Note: if the number is negative blocks will be removed</param>
            /// <param name="time">The amount of time it should take to add the blocks</param>
            public void Add(double add, NoneNegativeTime<int>? time = null)
            {
                ForFunction.AddCommand(new WorldborderSizeCommand(add, ID.AddSetModifier.add, time));
            }
            /// <summary>
            /// Changes the center of the world border
            /// </summary>
            /// <param name="coords">the location of the new center</param>
            public void Center(Vector coords)
            {
                ForFunction.AddCommand(new WorldborderCenterCommand(coords));
            }
            /// <summary>
            /// Changes the amount of damage the world border does
            /// </summary>
            /// <param name="amountPerBlock">The amount of damage it does per block the player is too far outside</param>
            /// <param name="buffer">The amount of blocks the player has to be outside the border before taking damage</param>
            public void Damage(double? amountPerBlock = null, double? buffer = null)
            {
                if (amountPerBlock is null && buffer is null)
                {
                    throw new System.ArgumentException("Both arguments may not be null at the same time");
                }

                if (amountPerBlock is null)
                {
                    ForFunction.AddCommand(new WorldborderDamageBufferCommand(buffer!.Value));
                }
                if (buffer is null)
                {
                    ForFunction.AddCommand(new WorldborderDamageAmountCommand(amountPerBlock!.Value));
                }
                ForFunction.Custom.GroupCommands((g) =>
                {
                    g.AddCommand(new WorldborderDamageAmountCommand(amountPerBlock!.Value));
                    g.AddCommand(new WorldborderDamageBufferCommand(buffer!.Value));
                });
            }
            /// <summary>
            /// Gets the worldborder's current size in blocks
            /// </summary>
            public void Get()
            {
                ForFunction.AddCommand(new WorldborderGetCommand());
            }
            /// <summary>
            /// Changes when the worldborder starts to show red on the players' screens
            /// </summary>
            /// <param name="distance">the maximum distance in blocks the player has be away from the border for the red to show</param>
            /// <param name="time">The maximum amount of time the player is away from the border for the red to show.
            /// (Time as in: "the world border will be at the player in X seconds")</param>
            public void Warning(int? distance = null, NoneNegativeTime<int>? time = null)
            {
                if (distance is null && time is null)
                {
                    throw new System.ArgumentException("Both arguments may not be null at the same time");
                }

                if (distance is null)
                {
                    ForFunction.AddCommand(new WorldborderWarningTimeCommand(time!));
                }
                if (time is null)
                {
                    ForFunction.AddCommand(new WorldborderWarningDistanceCommand(distance!.Value));
                }
                ForFunction.Custom.GroupCommands((g) =>
                {
                    g.AddCommand(new WorldborderWarningDistanceCommand(distance!.Value));
                    g.AddCommand(new WorldborderWarningTimeCommand(time!));
                });
            }
            /// <summary>
            /// Sets the world border's size in blocks
            /// </summary>
            /// <param name="set">The amount of blocks wide the border is</param>
            /// <param name="time">The time it should take for the border to get there</param>
            public void Set(double set, NoneNegativeTime<int>? time = null)
            {
                ForFunction.AddCommand(new WorldborderSizeCommand(set, ID.AddSetModifier.set, time));
            }
        }

        /// <summary>
        /// All commands for boss bars
        /// </summary>
        public ClassBossBar BossBar;
        /// <summary>
        /// All commands for boss bars
        /// </summary>
        public class ClassBossBar : CommandList
        {
            internal ClassBossBar(Function function) : base(function)
            {
                
            }
            /// <summary>
            /// Adds a <see cref="SharpCraft.BossBar"/> with the specified name to the world
            /// </summary>
            /// <param name="name">the name of the <see cref="SharpCraft.BossBar"/></param>
            /// <param name="showName">The name to show ontop of the <see cref="SharpCraft.BossBar"/></param>
            /// <returns>the newly created <see cref="SharpCraft.BossBar"/></returns>
            public BossBar Add(string name, BaseJsonText showName)
            {
                BossBar addBar = new BossBar(ForFunction.PackNamespace, name.ToLower());
                ForFunction.AddCommand(new BossBarAddCommand(addBar, showName));
                return addBar;
            }
            /// <summary>
            /// Removes the specified <see cref="SharpCraft.BossBar"/> from the world
            /// </summary>
            /// <param name="removeThis">the <see cref="SharpCraft.BossBar"/> to remove</param>
            public void Remove(BossBar removeThis)
            {
                ForFunction.AddCommand(new BossBarRemoveCommand(removeThis));
            }


            /// <summary>
            /// Sets the specified <see cref="SharpCraft.BossBar"/>'s value
            /// </summary>
            /// <param name="bossBar">the <see cref="SharpCraft.BossBar"/> to change</param>
            /// <param name="setTo">the new value</param>
            public void SetValue(BossBar bossBar, int setTo)
            {
                ForFunction.AddCommand(new BossBarChangeValueCommand(bossBar, setTo));
            }
            /// <summary>
            /// Sets the maximum value the specified <see cref="SharpCraft.BossBar"/> can display
            /// </summary>
            /// <param name="bossBar">the <see cref="SharpCraft.BossBar"/> to change</param>
            /// <param name="setTo">the new max value</param>
            public void SetMax(BossBar bossBar, int setTo)
            {
                ForFunction.AddCommand(new BossBarChangeMaxValueCommand(bossBar, setTo));
            }
            /// <summary>
            /// Changes the specified <see cref="SharpCraft.BossBar"/>'s display name
            /// </summary>
            /// <param name="bossBar">the <see cref="SharpCraft.BossBar"/> to change</param>
            /// <param name="newName">the new display name for the bar</param>
            public void SetName(BossBar bossBar, BaseJsonText newName)
            {
                ForFunction.AddCommand(new BossBarChangeNameCommand(bossBar, newName));
            }
            /// <summary>
            /// Changes if the specified <see cref="SharpCraft.BossBar"/> is visible
            /// </summary>
            /// <param name="bossBar">the <see cref="SharpCraft.BossBar"/> to change</param>
            /// <param name="visible">If it should be visible or not</param>
            public void SetVisible(BossBar bossBar, bool visible)
            {
                ForFunction.AddCommand(new BossBarChangeVisibilityCommand(bossBar, visible));
            }
            /// <summary>
            /// Changes the style of the specified <see cref="SharpCraft.BossBar"/>
            /// </summary>
            /// <param name="bossBar">the <see cref="SharpCraft.BossBar"/> to change</param>
            /// <param name="style">the new style of the <see cref="SharpCraft.BossBar"/></param>
            public void SetStyle(BossBar bossBar, ID.BossBarStyle style)
            {
                ForFunction.AddCommand(new BossBarChangeStyleCommand(bossBar, style));
            }
            /// <summary>
            /// Changes the color of the specified <see cref="SharpCraft.BossBar"/>
            /// </summary>
            /// <param name="bossBar">the <see cref="SharpCraft.BossBar"/> to change</param>
            /// <param name="color">the new color</param>
            public void SetColor(BossBar bossBar, ID.BossBarColor color)
            {
                ForFunction.AddCommand(new BossBarChangeColorCommand(bossBar, color));
            }
            /// <summary>
            /// Makes the selected players see the specified <see cref="SharpCraft.BossBar"/>
            /// </summary>
            /// <param name="bossBar">the <see cref="SharpCraft.BossBar"/> to show</param>
            /// <param name="players">the <see cref="BaseSelector"/> to use</param>
            public void SetPlayers(BossBar bossBar, BaseSelector players)
            {
                ForFunction.AddCommand(new BossBarChangePlayersCommand(bossBar, players));
            }


            /// <summary>
            /// Gets the specified <see cref="SharpCraft.BossBar"/>'s value
            /// </summary>
            /// <param name="bossBar">the <see cref="SharpCraft.BossBar"/> to get from</param>
            public void GetValue(BossBar bossBar)
            {
                ForFunction.AddCommand(new BossBarGetValueCommand(bossBar, ID.BossBarValue.value));
            }
            /// <summary>
            /// Gets the specified <see cref="SharpCraft.BossBar"/>'s max value
            /// </summary>
            /// <param name="bossBar">the <see cref="SharpCraft.BossBar"/> to get from</param>
            public void GetMax(BossBar bossBar)
            {
                ForFunction.AddCommand(new BossBarGetValueCommand(bossBar, ID.BossBarValue.max));
            }
            /// <summary>
            /// Gets the specified <see cref="SharpCraft.BossBar"/>'s visibility value
            /// </summary>
            /// <param name="bossBar">the <see cref="SharpCraft.BossBar"/> to get from</param>
            public void GetVisible(BossBar bossBar)
            {
                ForFunction.AddCommand(new BossBarGetValueCommand(bossBar, ID.BossBarValue.visible));
            }
            /// <summary>
            /// Gets a number stating how many players can see the specified <see cref="SharpCraft.BossBar"/>
            /// </summary>
            /// <param name="bossBar">the <see cref="SharpCraft.BossBar"/> to get from</param>
            public void GetPlayers(BossBar bossBar)
            {
                ForFunction.AddCommand(new BossBarGetValueCommand(bossBar, ID.BossBarValue.players));
            }
        }
    }
}
