namespace SharpCraft.FunctionWriters
{
    /// <summary>
    /// All commands for the world
    /// </summary>
    public class WorldCommands
    {
        readonly Function.FunctionWriter Writer;
        readonly Function function;

        internal WorldCommands(Function.FunctionWriter CommandsList, Function parentFunction)
        {
            Writer = CommandsList;
            function = parentFunction;
            Rule = new ClassRules(CommandsList);
            Objective = new ClassObjective(CommandsList);
            Team = new ClassTeam(CommandsList);
            Datapack = new ClassDatapack(CommandsList);
            Time = new ClassTime(CommandsList);
            Border = new ClassBorder(CommandsList);
            BossBar = new ClassBossBar(CommandsList);
            LoadSquare = new ClassLoadSquare(Writer);
        }

        /// <summary>
        /// Changes the world's spawnpoint to the specified location
        /// </summary>
        /// <param name="coords">The new location of the world spawnpoint</param>
        public void Spawn(Coords coords)
        {
            Writer.Add("setworldspawn " + coords);
            Writer.NewLine();
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
            readonly Function.FunctionWriter Writer;
            internal ClassLoadSquare(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
            }

            /// <summary>
            /// Loads the chunks containing the coordinates from corner to oppesiteCorner
            /// </summary>
            /// <param name="corner">One of the corners of the square to load</param>
            /// <param name="oppesiteCorner">The oppesite corner in the square to load</param>
            public void ForceLoad(Coords corner, Coords oppesiteCorner)
            {
                Writer.Add($"forceload add {corner.StringX} {corner.StringZ} {oppesiteCorner.StringX} {oppesiteCorner.StringZ}");
                Writer.NewLine();
            }

            /// <summary>
            /// Loads the chunk containing the coordinate
            /// </summary>
            /// <param name="coordinate">Coordinate in the chunk to load</param>
            public void ForceLoad(Coords coordinate)
            {
                Writer.Add($"forceload add {coordinate.StringX} {coordinate.StringZ}");
                Writer.NewLine();
            }

            /// <summary>
            /// stops the chunks containing the coordinates from corner to oppesiteCorner from being forced loaded
            /// </summary>
            /// <param name="corner">One of the corners of the square to stop loading</param>
            /// <param name="oppesiteCorner">The oppesite corner in the square to stop loading</param>
            public void StopLoad(Coords corner, Coords oppesiteCorner)
            {
                Writer.Add($"forceload remove {corner.StringX} {corner.StringZ} {oppesiteCorner.StringX} {oppesiteCorner.StringZ}");
                Writer.NewLine();
            }

            /// <summary>
            /// Stops the chunk at the coordinate from being forcedloaded
            /// </summary>
            /// <param name="coordinate">Coordinate in the chunk to stop loading</param>
            public void StopLoad(Coords coordinate)
            {
                Writer.Add($"forceload remove {coordinate.StringX} {coordinate.StringZ}");
                Writer.NewLine();
            }

            /// <summary>
            /// Stops all chunks from being forced loaded
            /// </summary>
            public void StopLoad()
            {
                Writer.Add($"forceload remove all");
                Writer.NewLine();
            }

            /// <summary>
            /// Checks if the given coords are loaded
            /// </summary>
            /// <param name="coordinate">The coordinate to check if loaded</param>
            public void IsLoaded(Coords coordinate)
            {
                Writer.Add($"forceload query {coordinate.StringX} {coordinate.StringZ}");
                Writer.NewLine();
            }
        }


        /// <summary>
        /// Says a message in the chat
        /// </summary>
        /// <param name="text">the text to say in chat</param>
        public void Say(string text)
        {
            Writer.Add("say " + text);
            Writer.NewLine();
        }

        /// <summary>
        /// Changes the default gamemode players are in when they spawn for the first time
        /// </summary>
        /// <param name="gamemode">the new default gamemode</param>
        public void DefaultGamemode(ID.Gamemode gamemode)
        {
            Writer.Add("defaultgamemode " + gamemode);
            Writer.NewLine();
        }

        /// <summary>
        /// Changes the difficulty of the world
        /// </summary>
        /// <param name="difficulty">the new difficulty</param>
        public void Difficulty(ID.Difficulty difficulty)
        {
            Writer.Add("difficulty " + difficulty);
            Writer.NewLine();
        }

        /// <summary>
        /// Spawns loot into the world at the given location
        /// </summary>
        /// <param name="coords">The location to spawn the loot</param>
        /// <param name="loot">the <see cref="Loottable"/> to spawn in</param>
        public void Loot(Coords coords, Loottable loot)
        {
            Writer.Add($"loot spawn {coords} loot {loot}");
            Writer.NewLine();
        }
        /// <summary>
        /// Spawns loot into the world at the given location
        /// </summary>
        /// <param name="coords">The location to spawn the loot</param>
        /// <param name="kill">the entity whose "when killed loot" should be dropped</param>
        public void Loot(Coords coords, Selector kill)
        {
            Writer.Add($"loot spawn {coords} kill {kill}");
            Writer.NewLine();
        }
        /// <summary>
        /// Spawns loot into the world at the given location
        /// </summary>
        /// <param name="coords">The location to spawn the loot</param>
        /// <param name="breakBlock">the block whose "when mined loot" should be dropped</param>
        /// <param name="breakWith">the tool used to break the block</param>
        public void Loot(Coords coords, Coords breakBlock, Item breakWith)
        {
            Writer.Add($"loot spawn {coords} mine {breakBlock}");
            if (breakWith != null)
            {
                Writer.Add(" " + breakWith);
            }
            Writer.NewLine();
        }

        /// <summary>
        /// Runs the specified function
        /// </summary>
        /// <param name="runFunction">the function to run</param>
        /// <param name="delay">the amount of time to function execution should be delayed. null doesnt delay it. 
        /// (If value is other than null the function will ignore the arguments send in the execute command which executed it)</param>
        /// <returns>The ran function</returns>
        public Function Function(Function runFunction, Time delay = null)
        {
            if (!runFunction.ToString().Contains("#") && function.PackNamespace.IsSettingSet(BasePackNamespace.Settings.WriteFunctionCalls()))
            {
                runFunction.Writer.CopyState();
                runFunction.Writer.HasExecute = false;
                runFunction.Writer.TempCommand = "";

                runFunction.Writer.Add("#----------------------------------------#");
                runFunction.Writer.NewLine();
                runFunction.Writer.Add("#Function ran by " + Writer.FunctionName + " at line " + this.Writer.LineNumber);
                runFunction.Writer.NewLine();
                if (Writer.TempCommand != "")
                {
                    runFunction.Writer.Add("#Using: /" + Writer.TempCommand);
                    runFunction.Writer.NewLine();
                }
                runFunction.Writer.Add("#----------------------------------------#");
                runFunction.Writer.NewLine();

                runFunction.Writer.PasteState();
            }
            if (delay == null)
            {
                Writer.Add("function " + runFunction);
            }
            else
            {
                Writer.Add($"schedule function {runFunction} {delay}");
            }
            Writer.NewLine();
            return runFunction;
        }

        /// <summary>
        /// Changes the weather to the specified type
        /// </summary>
        /// <param name="SetTo">The new type of weather</param>
        /// <param name="WeatherTime">The number of ticks the weather should be going. Null means the game chose</param>
        public void Weather(ID.WeatherType SetTo, int? WeatherTime = null)
        {
            if (WeatherTime != null)
            {
                Writer.Add("weather " + SetTo + " " + WeatherTime);
            }
            else
            {
                Writer.Add("weather " + SetTo);
            }
            Writer.NewLine();
        }

        /// <summary>
        /// All commands for gamemodes
        /// </summary>
        public ClassRules Rule;
        /// <summary>
        /// All commands for gamemodes
        /// </summary>
        public class ClassRules
        {
            readonly Function.FunctionWriter Writer;
            internal ClassRules(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
            }
            /// <summary>
            /// Changes if advancements should be announced in chat or not
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void AnnounceAdvancements(bool? setRule = null)
            {
                Gamerule(setRule, "announceAdvancements");
            }
            /// <summary>
            /// Changes if command blocks should say their executed commands in chat
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void CommandBlockOutput(bool? setRule = null)
            {
                Gamerule(setRule, "commandBlockOutput");
            }
            /// <summary>
            /// Makes the server not double check the players' flying speeds
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void DisableElytraMovementCheck(bool? setRule = null)
            {
                Gamerule(setRule, "disableElytraMovementCheck");
            }
            /// <summary>
            /// Changes if the daylightcycle should be running or not
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void DoDaylightCycle(bool? setRule = null)
            {
                Gamerule(setRule, "doDaylightCycle");
            }
            /// <summary>
            /// Changes if entities which arent mobs should drop loot or not
            /// (so like armorstands and item frames)
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void DoEntityDrops(bool? setRule = null)
            {
                Gamerule(setRule, "doEntityDrops");
            }
            /// <summary>
            /// Changes if fire should spread or not
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void DoFireTick(bool? setRule = null)
            {
                Gamerule(setRule, "doFireTick");
            }
            /// <summary>
            /// Changes if players have to have the recipe to be able to craft the item
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void DoLimitedCrafting(bool? setRule = null)
            {
                Gamerule(setRule, "doLimitedCrafting");
            }
            /// <summary>
            /// Changes if mobs should drop loot or not
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void DoMobLoot(bool? setRule = null)
            {
                Gamerule(setRule, "doMobLoot");
            }
            /// <summary>
            /// Changes if mobs should spawn in the world or not
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void DoMobSpawning(bool? setRule = null)
            {
                Gamerule(setRule, "doMobSpawning");
            }
            /// <summary>
            /// Changes if blocks should drop loot or not
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void DoTileDrops(bool? setRule = null)
            {
                Gamerule(setRule, "doTileDrops");
            }
            /// <summary>
            /// Changes if the weather cycle should be going or not
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void DoWeatherCycle(bool? setRule = null)
            {
                Gamerule(setRule, "doWeatherCycle");
            }
            /// <summary>
            /// Changes if players should keep their inventory on death
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void KeepInventory(bool? setRule = null)
            {
                Gamerule(setRule, "keepInventory");
            }
            /// <summary>
            /// Changes if ran commands should be logged in the server log or not
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void LogAdminCommands(bool? setRule = null)
            {
                Gamerule(setRule, "logAdminCommands");
            }
            /// <summary>
            /// Changes if mobs should be able to grief or not
            /// (fx if creepers blowing up breaks blocks and endermans should be able to pick up blocks)
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void MobGriefing(bool? setRule = null)
            {
                Gamerule(setRule, "mobGriefing");
            }
            /// <summary>
            /// Changes if players health should regenerate or not
            /// (Players can still heal with effects)
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void NaturalRegeneration(bool? setRule = null)
            {
                Gamerule(setRule, "naturalRegeneration");
            }
            /// <summary>
            /// Changes if the debug screen (f3) only should show a few things or not
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void ReducedDebugInfo(bool? setRule = null)
            {
                Gamerule(setRule, "reducedDebugInfo");
            }
            /// <summary>
            /// Changes if executed commands should output their success in chat / in the command block's output text
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void SendCommandFeedback(bool? setRule = null)
            {
                Gamerule(setRule, "sendCommandFeedback");
            }
            /// <summary>
            /// Changes if death messages should be displayed in chat or not
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void ShowDeathMessages(bool? setRule = null)
            {
                Gamerule(setRule, "showDeathMessages");
            }
            /// <summary>
            /// Changes if players in spectator mode should be able to generate new chunks
            /// </summary>
            /// <param name="setRule">if it should or not. null if you want to get the gamerule's value</param>
            public void SpectatorsGenerateChunks(bool? setRule = null)
            {
                Gamerule(setRule, "spectatorsGenerateChunks");
            }
            /// <summary>
            /// Changes the maximum amount of commands to run each tick
            /// (default:65536)
            /// </summary>
            /// <param name="setRule">The new value. null if you want to get the gamerule's value</param>
            public void MaxCommandChainLength(int? setRule = null)
            {
                GameruleInt(setRule, "maxCommandChainLength");
            }
            /// <summary>
            /// Changes the amount of entities there has to be in one block before they start to take damage
            /// (default:24)
            /// </summary>
            /// <param name="setRule">The new value. null if you want to get the gamerule's value</param>
            public void MaxEntityCramming(int? setRule = null)
            {
                GameruleInt(setRule, "maxEntityCramming");
            }
            /// <summary>
            /// Changes the amount of random ticks which happens each tick
            /// (default: 3)
            /// (fx makes things grow faster and fire spreads faster)
            /// </summary>
            /// <param name="setRule">The new value. null if you want to get the gamerule's value</param>
            public void RandomTickSpeed(int? setRule = null)
            {
                GameruleInt(setRule, "randomTickSpeed");
            }
            /// <summary>
            /// Changes the radius players maximum can spawn away from the world's spawnpoint
            /// (default: 10)
            /// </summary>
            /// <param name="setRule">The new value. null if you want to get the gamerule's value</param>
            public void SpawnRadius(int? setRule = null)
            {
                GameruleInt(setRule, "spawnRadius");
            }

            private void Gamerule(bool? SetRule, string Name)
            {
                Writer.Add("gamerule " + Name);
                if (SetRule != null)
                {
                    Writer.Add(" " + SetRule.ToMinecraftBool());
                }
                Writer.NewLine();
            }
            private void GameruleInt(int? SetRule, string Name)
            {
                Writer.Add("gamerule " + Name);
                if (SetRule != null)
                {
                    Writer.Add(" " + SetRule);
                }
                Writer.NewLine();
            }
        }

        /// <summary>
        /// All commands for scoreboards
        /// </summary>
        public ClassObjective Objective;
        /// <summary>
        /// All commands for scoreboards
        /// </summary>
        public class ClassObjective
        {
            readonly Function.FunctionWriter Writer;
            internal ClassObjective(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
            }
            /// <summary>
            /// Adds the specified <see cref="ScoreObject"/> to the world
            /// </summary>
            /// <param name="ScoreName">The name of the <see cref="ScoreObject"/></param>
            /// <param name="Type">the type of the scoreboard. See <see cref="ID.Objective"/> for a list of types</param>
            /// <param name="DisplayName">The name to display when the scoreboard is viewed in the sidebar</param>
            /// <returns>the newly created <see cref="ScoreObject"/></returns>
            public ScoreObject Add(string ScoreName, string Type = "dummy", JSON[] DisplayName = null)
            {
                Writer.Add("scoreboard objectives add " + ScoreName + " " + Type);
                if (DisplayName != null) Writer.Add(" " + DisplayName.GetString());
                Writer.NewLine();
                return new ScoreObject(ScoreName);
            }

            /// <summary>
            /// Removes the specified <see cref="ScoreObject"/> from the world
            /// </summary>
            /// <param name="Object">the <see cref="ScoreObject"/> to remove</param>
            public void Remove(ScoreObject Object)
            {
                Writer.Add("scoreboard objectives remove " + Object);
                Writer.NewLine();
            }

            /// <summary>
            /// Displays the specified <see cref="ScoreObject"/> in the specified display slot
            /// Note: each slot can only display one <see cref="ScoreObject"/>
            /// </summary>
            /// <param name="Object">the <see cref="ScoreObject"/> to display</param>
            /// <param name="Display">the display slot to display it in</param>
            /// <param name="TeamColor">only teams with this color can see it. Null makes everyone see it. Note: this only works with <see cref="ID.ScoreDisplay.sidebar"/> as the <paramref name="Display"/></param>
            public void Display(ScoreObject Object, ID.ScoreDisplay Display, ID.MinecraftColor? TeamColor = null)
            {
                if (Display == ID.ScoreDisplay.sidebar && TeamColor != null)
                {
                    Writer.Add("scoreboard objectives setdisplay sidebar.team." + TeamColor + " " + Object);
                }
                else
                {
                    Writer.Add("scoreboard objectives setdisplay " + Display + " " + Object);
                }
                Writer.NewLine();
            }
            /// <summary>
            /// Clears a display slot
            /// </summary>
            /// <param name="Display">the display slot to clear</param>
            /// <param name="TeamColor">the team's sidebar slot to clear. Note: this only works with <see cref="ID.ScoreDisplay.sidebar"/> as the <paramref name="Display"/></param>
            public void ClearDisplay(ID.ScoreDisplay Display, ID.MinecraftColor? TeamColor = null)
            {
                if (Display == ID.ScoreDisplay.sidebar && TeamColor != null)
                {
                    Writer.Add("scoreboard objectives setdisplay sidebar.team." + TeamColor);
                }
                else
                {
                    Writer.Add("scoreboard objectives setdisplay " + Display);
                }
                Writer.NewLine();
            }
            /// <summary>
            /// Changes the <see cref="ScoreObject"/>'s displayed name
            /// </summary>
            /// <param name="Object">the <see cref="ScoreObject"/> to change</param>
            /// <param name="Name">The new display name</param>
            public void DisplayName(ScoreObject Object, JSON[] Name)
            {
                Writer.Add("scoreboard objectives modify " + Object + " displayname " + Name.GetString());
                Writer.NewLine();
            }
            /// <summary>
            /// Changes the way the <see cref="ScoreObject"/> is rendered in the <see cref="ID.ScoreDisplay.list"/> display slot
            /// </summary>
            /// <param name="Object">the <see cref="ScoreObject"/> to change</param>
            /// <param name="Render">The way it should be rendered</param>
            public void Render(ScoreObject Object, ID.ObjectiveRender Render)
            {
                Writer.Add("scoreboard objectives modify " + Object + " displayname " + Render);
                Writer.NewLine();
            }
        }

        /// <summary>
        /// All commands for teams
        /// </summary>
        public ClassTeam Team;
        /// <summary>
        /// All commands for teams
        /// </summary>
        public class ClassTeam
        {
            readonly Function.FunctionWriter Writer;
            internal ClassTeam(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
            }
            /// <summary>
            /// Adds the specified <see cref="Team"/> to the world
            /// </summary>
            /// <param name="TeamName">the <see cref="Team"/>'s name</param>
            /// <param name="DisplayName">the displayed name of the <see cref="Team"/></param>
            /// <param name="TeamColor">the color of the <see cref="Team"/>. If null the <see cref="Team"/> will have the default color (white)</param>
            /// <returns>the newly created <see cref="Team"/></returns>
            public Team Add(string TeamName, JSON[] DisplayName, ID.MinecraftColor? TeamColor = null)
            {
                Team Creating = new Team(TeamName);

                Writer.Add("team add " + TeamName);
                if (DisplayName != null) Writer.Add(" " + DisplayName.GetString());
                Writer.NewLine();
                if (TeamColor != null) { Color(Creating, (ID.MinecraftColor)TeamColor); }
                return Creating;
            }
            /// <summary>
            /// Removes the specified <see cref="Team"/> from the world
            /// </summary>
            /// <param name="RemoveTeam">the <see cref="Team"/> to remove</param>
            public void Remove(Team RemoveTeam)
            {
                Writer.Add("team remove " + RemoveTeam);
                Writer.NewLine();
            }
            /// <summary>
            /// Changes the color of the specified <see cref="Team"/>
            /// </summary>
            /// <param name="ChangeTeam">the <see cref="Team"/> to change</param>
            /// <param name="Color">The new color</param>
            public void Color(Team ChangeTeam, ID.MinecraftColor Color)
            {
                Writer.Add("team modify " + ChangeTeam + " color " + Color);
                Writer.NewLine();
            }
            /// <summary>
            /// Changes how the specified <see cref="Team"/>'s death messages are displayed
            /// </summary>
            /// <param name="ChangeTeam">the <see cref="Team"/> to change</param>
            /// <param name="Visible">the visibility rule</param>
            public void DeathMessage(Team ChangeTeam, ID.TeamVisibility Visible)
            {
                Writer.Add("team modify " + ChangeTeam + " deathMessageVisibility " + Visible);
                Writer.NewLine();
            }
            /// <summary>
            /// Changes how the specified <see cref="Team"/> collides things
            /// </summary>
            /// <param name="ChangeTeam">the <see cref="Team"/> to change</param>
            /// <param name="Collision">the collision rule</param>
            public void Collision(Team ChangeTeam, ID.TeamCollision Collision)
            {
                Writer.Add("team modify " + ChangeTeam + " collisionRule " + Collision);
                Writer.NewLine();
            }
            /// <summary>
            /// Changes if the specified <see cref="Team"/> can damage players on their own team or not
            /// </summary>
            /// <param name="ChangeTeam">the <see cref="Team"/> to change</param>
            /// <param name="FriendlyFire">If the team should be able to damage their own team or not</param>
            public void FriendlyFire(Team ChangeTeam, bool FriendlyFire)
            {
                Writer.Add("team modify " + ChangeTeam + " friendlyFire " + FriendlyFire.ToMinecraftBool());
                Writer.NewLine();
            }
            /// <summary>
            /// Changes how the specified <see cref="Team"/> is displayed when invisible
            /// </summary>
            /// <param name="ChangeTeam">the <see cref="Team"/> to change</param>
            /// <param name="See">the visibility rule</param>
            public void SeeInvisibleFriends(Team ChangeTeam, bool See)
            {
                Writer.Add("team modify " + ChangeTeam + " seeFriendlyInvisibles " + See.ToMinecraftBool());
                Writer.NewLine();
            }
            /// <summary>
            /// Changes how the specified <see cref="Team"/>'s nametags are visible
            /// </summary>
            /// <param name="ChangeTeam">the <see cref="Team"/> to change</param>
            /// <param name="Visible">the visibility tule</param>
            public void Nametag(Team ChangeTeam, ID.TeamVisibility Visible)
            {
                Writer.Add("team modify " + ChangeTeam + " nametagVisibility " + Visible);
                Writer.NewLine();
            }
            /// <summary>
            /// Changes the display name of the specified <see cref="Team"/>
            /// </summary>
            /// <param name="ChangeTeam">the <see cref="Team"/> to change</param>
            /// <param name="Name">The new name of the team</param>
            public void DisplayName(Team ChangeTeam, JSON[] Name)
            {
                Writer.Add("team modify " + ChangeTeam + " displayName " + Name.GetString());
                Writer.NewLine();
            }
            /// <summary>
            /// Removes all players from the specified <see cref="Team"/>
            /// </summary>
            /// <param name="ClearTeam">the <see cref="Team"/> to remove players from</param>
            public void Clear(Team ClearTeam)
            {
                Writer.Add("team empty " + ClearTeam);
                Writer.NewLine();
            }
            /// <summary>
            /// Changes the prefix shown before the name of players in the specified <see cref="Team"/>
            /// </summary>
            /// <param name="ChangeTeam">the <see cref="Team"/> to change</param>
            /// <param name="PrefixJson">The new prefix to show</param>
            public void Prefix(Team ChangeTeam, JSON[] PrefixJson)
            {
                Writer.Add("team modify " + ChangeTeam + " prefix " + PrefixJson.GetString());
                Writer.NewLine();
            }
            /// <summary>
            /// Changes the suffix shown after the name of players in the specified <see cref="Team"/>
            /// </summary>
            /// <param name="ChangeTeam">the <see cref="Team"/> to change</param>
            /// <param name="SuffixJson">the new prefix to show</param>
            public void Suffix(Team ChangeTeam, JSON[] SuffixJson)
            {
                Writer.Add("team modify " + ChangeTeam + " suffix " + SuffixJson.GetString());
                Writer.NewLine();
            }
        }

        /// <summary>
        /// All commands for datapacks
        /// </summary>
        public ClassDatapack Datapack;
        /// <summary>
        /// All commands for datapacks
        /// </summary>
        public class ClassDatapack
        {
            readonly Function.FunctionWriter Writer;
            internal ClassDatapack(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
            }
            /// <summary>
            /// Disables the specified <see cref="SharpCraft.Datapack"/>
            /// </summary>
            /// <param name="datapack">the <see cref="SharpCraft.Datapack"/> to disable</param>
            public void Disable(Datapack datapack)
            {
                Writer.Add("datapack disable " + datapack.Name);
                Writer.NewLine();
            }
            /// <summary>
            /// Enables the specified <see cref="SharpCraft.Datapack"/>
            /// </summary>
            /// <param name="datapack">the <see cref="SharpCraft.Datapack"/> to enable</param>
            /// <param name="PlaceAt">choses where the <see cref="SharpCraft.Datapack"/> should be placed relative to other enabled <see cref="SharpCraft.Datapack"/>s</param>
            /// <param name="OtherPack">the <see cref="SharpCraft.Datapack"/> the <paramref name="datapack"/> is placed relative to</param>
            public void Enable(Datapack datapack, ID.DatapackPlace? PlaceAt = null, Datapack OtherPack = null)
            {
                Writer.Add("datapack enable " + datapack.Name);
                if (PlaceAt != null) { Writer.Add(" " + PlaceAt); }
                if (PlaceAt.Value >= ID.DatapackPlace.first) { Writer.Add(" " + OtherPack.Name); }
                Writer.NewLine();
            }
        }

        /// <summary>
        /// All commands for time
        /// </summary>
        public ClassTime Time;
        /// <summary>
        /// All commands for time
        /// </summary>
        public class ClassTime
        {
            readonly Function.FunctionWriter Writer;
            internal ClassTime(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
            }
            /// <summary>
            /// Adds the specified amount of <see cref="Time"/> to the time of day
            /// </summary>
            /// <param name="time">the <see cref="Time"/> to add</param>
            public void Add(Time time)
            {
                Writer.Add("time add " + time);
                Writer.NewLine();
            }
            /// <summary>
            /// Sets the time of day to the specified <see cref="Time"/>
            /// </summary>
            /// <param name="time">the <see cref="Time"/> to set it to</param>
            public void Set(Time time)
            {
                Writer.Add("time set " + time);
                Writer.NewLine();
            }
            /// <summary>
            /// Gets the amount of days which has gone by
            /// </summary>
            public void GetDay()
            {
                Writer.Add("time query day");
                Writer.NewLine();
            }
            /// <summary>
            /// Gets the time of day it is in ticks
            /// (0-24000)
            /// </summary>
            public void GetDayTime()
            {
                Writer.Add("time query daytime");
                Writer.NewLine();
            }
            /// <summary>
            /// Gets the time of the world
            /// (Pretty much just <see cref="GetDay"/> * 24000 + <see cref="GetDayTime"/>)
            /// </summary>
            public void GetTime()
            {
                Writer.Add("time query gametime");
                Writer.NewLine();
            }
        }

        /// <summary>
        /// All commands for the world border
        /// </summary>
        public ClassBorder Border;
        /// <summary>
        /// All commands for the world border
        /// </summary>
        public class ClassBorder
        {
            readonly Function.FunctionWriter Writer;
            internal ClassBorder(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
            }
            /// <summary>
            /// Adds blocks to the world border size
            /// </summary>
            /// <param name="Add">The amount of blocks to add. 
            /// Note: the blocks are spread out from the center, so adding 1 block adds a half block to all sides
            /// Note: if the number is negative blocks will be removed</param>
            /// <param name="time">The amount of time it should take to add the blocks</param>
            public void Add(int Add, int time = 0)
            {
                if (Add >= 0)
                {
                    Writer.Add("worldborder add " + Add + " " + time);
                }
                else
                {
                    Writer.Add("worldborder remove " + (Add * -1) + " " + time);
                }
                Writer.NewLine();
            }
            /// <summary>
            /// Changes the center of the world border
            /// </summary>
            /// <param name="Coords">the location of the new center</param>
            public void Center(Coords Coords)
            {
                Writer.Add("worldborder center " + Coords.StringX + " " + Coords.StringZ);
                Writer.NewLine();
            }
            /// <summary>
            /// Changes the amount of damage the world border does
            /// </summary>
            /// <param name="AmountPerBlock">The amount of damage it does per block the player is too far outside</param>
            /// <param name="Buffer">The amount of blocks the player has to be outside the border before taking damage</param>
            public void Damage(int AmountPerBlock = -1, int Buffer = -1)
            {
                if (AmountPerBlock != -1) { Writer.Add("worldborder damage amount " + AmountPerBlock); }
                if (Buffer != -1) { Writer.Add("worldborder damage buffer " + Buffer); }

                if (AmountPerBlock != -1 || Buffer != -1) { Writer.NewLine(); }
            }
            /// <summary>
            /// Gets the worldborder's current size in blocks
            /// </summary>
            public void Get()
            {
                Writer.Add("worldborder get");
                Writer.NewLine();
            }
            /// <summary>
            /// Changes when the worldborder starts to show red on the players' screens
            /// </summary>
            /// <param name="Distance">the maximum distance in blocks the player has be away from the border for the red to show</param>
            /// <param name="Time">The maximum amount of time the player is away from the border for the red to show.
            /// (Time as in: "the world border will be at the player in X seconds")</param>
            public void Warning(int? Distance = null, int? Time = null)
            {
                if (Distance != null) { Writer.Add("worldborder warning distance " + Distance); }
                if (Time != null) { Writer.Add("worldborder warning time " + Time); }

                if (Distance != null || Time != null) { Writer.NewLine(); }
            }
            /// <summary>
            /// Sets the world border's size in blocks
            /// </summary>
            /// <param name="Set">The amount of blocks wide the border is</param>
            /// <param name="time">The time it should take for the border to get there</param>
            public void Set(int Set, int time = 0)
            {
                Writer.Add("worldborder set " + Set + " " + time);
                Writer.NewLine();
            }
        }

        /// <summary>
        /// All commands for boss bars
        /// </summary>
        public ClassBossBar BossBar;
        /// <summary>
        /// All commands for boss bars
        /// </summary>
        public class ClassBossBar
        {
            readonly Function.FunctionWriter Writer;
            internal ClassBossBar(Function.FunctionWriter CommandsList)
            {
                Writer = CommandsList;
            }
            /// <summary>
            /// Adds a <see cref="SharpCraft.BossBar"/> with the specified name to the world
            /// </summary>
            /// <param name="Name">the name of the <see cref="SharpCraft.BossBar"/></param>
            /// <param name="ShowName">The name to show ontop of the <see cref="SharpCraft.BossBar"/></param>
            /// <returns>the newly created <see cref="SharpCraft.BossBar"/></returns>
            public BossBar Add(string Name, JSON[] ShowName)
            {
                Writer.Add("bossbar add " + Writer.NameSpaceName + ":" + Name.ToLower() + " " + ShowName.GetString());
                Writer.NewLine();
                return new BossBar(Writer.NameSpaceName + ":" + Name.ToLower());
            }
            /// <summary>
            /// Removes the specified <see cref="SharpCraft.BossBar"/> from the world
            /// </summary>
            /// <param name="RemoveThis">the <see cref="SharpCraft.BossBar"/> to remove</param>
            public void Remove(BossBar RemoveThis)
            {
                Writer.Add("bossbar remove " + RemoveThis);
                Writer.NewLine();
            }


            /// <summary>
            /// Sets the specified <see cref="SharpCraft.BossBar"/>'s value
            /// </summary>
            /// <param name="BossBar">the <see cref="SharpCraft.BossBar"/> to change</param>
            /// <param name="SetTo">the new value</param>
            public void SetValue(BossBar BossBar, int SetTo)
            {
                Writer.Add("bossbar set " + BossBar + " value " + SetTo);
                Writer.NewLine();
            }
            /// <summary>
            /// Sets the maximum value the specified <see cref="SharpCraft.BossBar"/> can display
            /// </summary>
            /// <param name="BossBar">the <see cref="SharpCraft.BossBar"/> to change</param>
            /// <param name="SetTo">the new max value</param>
            public void SetMax(BossBar BossBar, int SetTo)
            {
                Writer.Add("bossbar set " + BossBar + " max " + SetTo);
                Writer.NewLine();
            }
            /// <summary>
            /// Changes the specified <see cref="SharpCraft.BossBar"/>'s display name
            /// </summary>
            /// <param name="BossBar">the <see cref="SharpCraft.BossBar"/> to change</param>
            /// <param name="NewName">the new display name for the bar</param>
            public void SetName(BossBar BossBar, JSON[] NewName)
            {
                Writer.Add("bossbar set " + BossBar + " name " + NewName.GetString());
                Writer.NewLine();
            }
            /// <summary>
            /// Changes if the specified <see cref="SharpCraft.BossBar"/> is visible
            /// </summary>
            /// <param name="BossBar">the <see cref="SharpCraft.BossBar"/> to change</param>
            /// <param name="Visible">If it should be visible or not</param>
            public void SetVisible(BossBar BossBar, bool Visible)
            {
                Writer.Add("bossbar set " + BossBar + " visible " + Visible.ToMinecraftBool());
                Writer.NewLine();
            }
            /// <summary>
            /// Changes the style of the specified <see cref="SharpCraft.BossBar"/>
            /// </summary>
            /// <param name="BossBar">the <see cref="SharpCraft.BossBar"/> to change</param>
            /// <param name="Style">the new style of the <see cref="SharpCraft.BossBar"/></param>
            public void SetStyle(BossBar BossBar, ID.BossBarStyle Style)
            {
                Writer.Add("bossbar set " + BossBar + " style " + Style);
                Writer.NewLine();
            }
            /// <summary>
            /// Changes the color of the specified <see cref="SharpCraft.BossBar"/>
            /// </summary>
            /// <param name="BossBar">the <see cref="SharpCraft.BossBar"/> to change</param>
            /// <param name="Color">the new color</param>
            public void SetColor(BossBar BossBar, ID.BossBarColor Color)
            {
                Writer.Add("bossbar set " + BossBar + " color " + Color);
                Writer.NewLine();
            }
            /// <summary>
            /// Makes the selected players see the specified <see cref="SharpCraft.BossBar"/>
            /// </summary>
            /// <param name="BossBar">the <see cref="SharpCraft.BossBar"/> to show</param>
            /// <param name="Players">the <see cref="Selector"/> to use</param>
            public void SetPlayers(BossBar BossBar, Selector Players)
            {
                Writer.Add("bossbar set " + BossBar + " players " + Players);
                Writer.NewLine();
            }


            /// <summary>
            /// Gets the specified <see cref="SharpCraft.BossBar"/>'s value
            /// </summary>
            /// <param name="BossBar">the <see cref="SharpCraft.BossBar"/> to get from</param>
            public void GetValue(BossBar BossBar)
            {
                Writer.Add("bossbar get " + BossBar + " value");
                Writer.NewLine();
            }
            /// <summary>
            /// Gets the specified <see cref="SharpCraft.BossBar"/>'s max value
            /// </summary>
            /// <param name="BossBar">the <see cref="SharpCraft.BossBar"/> to get from</param>
            public void GetMax(BossBar BossBar)
            {
                Writer.Add("bossbar get " + BossBar + " max");
                Writer.NewLine();
            }
            /// <summary>
            /// Gets the specified <see cref="SharpCraft.BossBar"/>'s visibility value
            /// </summary>
            /// <param name="BossBar">the <see cref="SharpCraft.BossBar"/> to get from</param>
            public void GetVisible(BossBar BossBar)
            {
                Writer.Add("bossbar get " + BossBar + " visible");
                Writer.NewLine();
            }
            /// <summary>
            /// Gets a number stating how many players can see the specified <see cref="SharpCraft.BossBar"/>
            /// </summary>
            /// <param name="BossBar">the <see cref="SharpCraft.BossBar"/> to get from</param>
            public void GetPlayers(BossBar BossBar)
            {
                Writer.Add("bossbar get " + BossBar + " players");
                Writer.NewLine();
            }
        }
    }
}
