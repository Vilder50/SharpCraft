using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Says some text with the executing entity's name and a * at the beginning
    /// </summary>
    public class SayMeCommand : BaseCommand
    {
        private string text;

        /// <summary>
        /// intializes a new <see cref="SayMeCommand"/>
        /// </summary>
        /// <param name="text">The text it should display</param>
        public SayMeCommand(string text)
        {
            Text = text;
        }

        /// <summary>
        /// The text it should display
        /// </summary>
        public string Text 
        {
            get => text;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Text may not be null or whitespace", nameof(Text));
                }
                text = value;
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>me [Text]</returns>
        public override string GetCommandString()
        {
            return $"me {Text}";
        }
    }

    /// <summary>
    /// A # comment
    /// </summary>
    public class Comment : BaseCommand
    {
        private string text;

        /// <summary>
        /// intializes a new <see cref="Comment"/>
        /// </summary>
        /// <param name="text">The comment</param>
        public Comment(string text)
        {
            Text = text;
        }

        /// <summary>
        /// The comment
        /// </summary>
        public string Text { get => text; set => text = value ?? throw new ArgumentNullException(nameof(Text), "Comment text may not be null"); }

        /// <summary>
        /// Returns the comment as a string
        /// </summary>
        /// <returns>#[Text]</returns>
        public override string GetCommandString()
        {
            return $"#{Text}";
        }
    }

    /// <summary>
    /// Says some text with the executing entity's name in [] at the beginning
    /// </summary>
    public class SayCommand : BaseCommand
    {
        private string text;

        /// <summary>
        /// intializes a new <see cref="SayCommand"/>
        /// </summary>
        /// <param name="text">The text it should display</param>
        public SayCommand(string text)
        {
            Text = text;
        }

        /// <summary>
        /// The text it should display
        /// </summary>
        public string Text
        {
            get => text;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Text may not be null or whitespace", nameof(Text));
                }
                text = value;
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>say [Text]</returns>
        public override string GetCommandString()
        {
            return $"say {Text}";
        }
    }

    /// <summary>
    /// Sends a private message to one or more players
    /// </summary>
    public class MsgCommand : BaseCommand
    {
        private string text;
        private BaseSelector selector;

        /// <summary>
        /// intializes a new <see cref="MsgCommand"/>
        /// </summary>
        /// <param name="text">The text it should display</param>
        /// <param name="selector">Selector for selecting players to private message</param>
        public MsgCommand(BaseSelector selector, string text)
        {
            Text = text;
            Selector = selector;
        }

        /// <summary>
        /// The text it should display
        /// </summary>
        public string Text
        {
            get => text;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Text may not be null or whitespace", nameof(Text));
                }
                text = value;
            }
        }

        /// <summary>
        /// Selector for selecting players to private message
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.");
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>msg [Selector] [Text]</returns>
        public override string GetCommandString()
        {
            return $"msg {Selector.GetSelectorString()} {Text}";
        }
    }

    /// <summary>
    /// Private messages players on the same team as the player executing this command
    /// </summary>
    public class TeamMsgCommand : BaseCommand
    {
        private string text;

        /// <summary>
        /// intializes a new <see cref="TeamMsgCommand"/>
        /// </summary>
        /// <param name="text">The text to private message</param>
        public TeamMsgCommand(string text)
        {
            Text = text;
        }

        /// <summary>
        /// The text to private message
        /// </summary>
        public string Text
        {
            get => text;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Text may not be null or whitespace", nameof(Text));
                }
                text = value;
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>say [Text]</returns>
        public override string GetCommandString()
        {
            return $"teammsg {Text}";
        }
    }

    /// <summary>
    /// Command which gets the seed of the world
    /// </summary>
    public class SeedCommand : BaseCommand
    {
        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>seed</returns>
        public override string GetCommandString()
        {
            return "seed";
        }
    }

    /// <summary>
    /// Command which locates where the closest structure of the given type is at
    /// </summary>
    public class LocateCommand : BaseCommand
    {
        /// <summary>
        /// Intializes a new <see cref="LocateCommand"/>
        /// </summary>
        /// <param name="structure">The structure to locate</param>
        public LocateCommand(ID.Structure structure)
        {
            Structure = structure;
        }

        /// <summary>
        /// The structure to locate
        /// </summary>
        public ID.Structure Structure { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>locate [Structure]</returns>
        public override string GetCommandString()
        {
            return $"locate {Structure}";
        }
    }

    /// <summary>
    /// Changes a trigger objective for the executing player.
    /// Player doesn't need op to run this command.
    /// </summary>
    public class TriggerCommand : BaseCommand
    {
        private Objective objective;

        /// <summary>
        /// intializes a new <see cref="TriggerCommand"/>
        /// </summary>
        /// <param name="objective">The objective to change</param>
        /// <param name="score">The number to modify it with</param>
        /// <param name="shouldSet">True if it should set the objective to the score. False if it should add the score to the objective</param>
        public TriggerCommand(Objective objective, bool shouldSet, int score)
        {
            Objective = objective;
            ShouldSet = shouldSet;
            Score = score;
        }


        /// <summary>
        /// The objective to change
        /// </summary>
        public Objective Objective
        {
            get => objective;
            set
            {
                objective = value ?? throw new ArgumentNullException(nameof(Objective), "Objective may not be null.");
            }
        }

        /// <summary>
        /// True if it should set the objective to the score. False if it should add the score to the objective
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// The number to modify it with
        /// </summary>
        public bool ShouldSet { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>trigger [Objective] [ShouldSet] [Score]</returns>
        public override string GetCommandString()
        {
            return $"trigger {Objective.Name} {(ShouldSet ? "set" : "add")} {Score}";
        }
    }

    /// <summary>
    /// Command which changes the default gamemode in the world
    /// </summary>
    public class DefaultGamemodeCommand : BaseCommand
    {
        /// <summary>
        /// intializes a new <see cref="DefaultGamemodeCommand"/>
        /// </summary>
        /// <param name="gamemode">The gamemode to change to be the default gamemode</param>
        public DefaultGamemodeCommand(ID.Gamemode gamemode)
        {
            Gamemode = gamemode;
        }

        /// <summary>
        /// The gamemode to change to be the default gamemode
        /// </summary>
        public ID.Gamemode Gamemode { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>defaultgamemode [Gamemode]</returns>
        public override string GetCommandString()
        {
            return $"defaultgamemode {Gamemode}";
        }
    }

    /// <summary>
    /// Command which changes the difficulty in the world
    /// </summary>
    public class DifficultyCommand : BaseCommand
    {
        /// <summary>
        /// intializes a new <see cref="DifficultyCommand"/>
        /// </summary>
        /// <param name="difficulty">The new difficulty</param>
        public DifficultyCommand(ID.Difficulty difficulty)
        {
            Difficulty = difficulty;
        }

        /// <summary>
        /// The new difficulty
        /// </summary>
        public ID.Difficulty Difficulty { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>difficulty [Difficulty]</returns>
        public override string GetCommandString()
        {
            return $"difficulty {Difficulty}";
        }
    }

    /// <summary>
    /// Command which reloads datapacks in the world
    /// </summary>
    public class ReloadCommand : BaseCommand
    {
        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>reload</returns>
        public override string GetCommandString()
        {
            return "reload";
        }
    }

    /// <summary>
    /// Command which enchants a player's selected item with an enchant
    /// </summary>
    public class EnchantCommand : BaseCommand
    {
        private BaseSelector selector;
        private int level;

        /// <summary>
        /// Intializes a new <see cref="EnchantCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting the player's whose item to enchant</param>
        /// <param name="level">The level of the enchant</param>
        /// <param name="enchant">The enchant to enchant the player's selected item with</param>
        public EnchantCommand(BaseSelector selector, int level, ID.Enchant enchant)
        {
            Selector = selector;
            Level = level;
            Enchant = enchant;
        }


        /// <summary>
        /// Selector selecting the player's whose item to enchant
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.");
            }
        }

        /// <summary>
        /// The level of the enchant
        /// </summary>
        public int Level
        {
            get => level;
            set
            {
                if (value < 1 || value > 5)
                {
                    throw new ArgumentOutOfRangeException(nameof(Level), "Level may not be less than 0 or higher than 5.");
                }
                level = value;
            }
        }

        /// <summary>
        /// The enchant to enchant the player's selected item with
        /// </summary>
        public ID.Enchant Enchant { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>enchant [Selector] [Enchant] [Level]</returns>
        public override string GetCommandString()
        {
            return $"enchant {Selector.GetSelectorString()} {Enchant} {Level}";
        }
    }

    /// <summary>
    /// Command for running a function
    /// </summary>
    public class RunFunctionCommand : BaseCommand
    {
        private IFunction function;

        /// <summary>
        /// Intializes a new <see cref="RunFunctionCommand"/>
        /// </summary>
        /// <param name="function">The function to run</param>
        public RunFunctionCommand(IFunction function)
        {
            Function = function;
        }

        /// <summary>
        /// The function to run
        /// </summary>
        public IFunction Function
        {
            get => function;
            set
            {
                function = value ?? throw new ArgumentNullException(nameof(Function), "Function may not be null.");
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>function [Function]</returns>
        public override string GetCommandString()
        {
            return $"function {Function.GetNamespacedName()}";
        }
    }

    /// <summary>
    /// Command which changes player's gamemode
    /// </summary>
    public class GamemodeCommand : BaseCommand
    {
        private BaseSelector selector;

        /// <summary>
        /// intializes a new <see cref="GamemodeCommand"/>
        /// </summary>
        /// <param name="gamemode">The gamemode to change the players to</param>
        /// <param name="selector">Selector selecting the player's whose gamemode to change</param>
        public GamemodeCommand(BaseSelector selector, ID.Gamemode gamemode)
        {
            Gamemode = gamemode;
            Selector = selector;
        }

        /// <summary>
        /// The gamemode to change the players to
        /// </summary>
        public ID.Gamemode Gamemode { get; set; }

        /// <summary>
        /// Selector selecting the player's whose gamemode to change
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.");
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>gamemode [Gamemode] [Selector]</returns>
        public override string GetCommandString()
        {
            return $"gamemode {Gamemode} {Selector.GetSelectorString()}";
        }
    }

    /// <summary>
    /// Command which changes player's gamemode
    /// </summary>
    public class KillCommand : BaseCommand
    {
        private BaseSelector selector;

        /// <summary>
        /// intializes a new <see cref="KillCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting the entities to kill</param>
        public KillCommand(BaseSelector selector)
        {
            Selector = selector;
        }

        /// <summary>
        /// Selector selecting the entities to kill
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.");
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>kill [Selector]</returns>
        public override string GetCommandString()
        {
            return $"kill {Selector.GetSelectorString()}";
        }
    }

    /// <summary>
    /// Command which places a block in the world
    /// </summary>
    public class SetblockCommand : BaseCommand
    {
        private Vector coordinates;
        private Block block;

        /// <summary>
        /// Intializes a new <see cref="SetblockCommand"/>
        /// </summary>
        /// <param name="coordinates">The coords to place the block at</param>
        /// <param name="block">The block to place</param>
        /// <param name="mode">The way to place the block</param>
        public SetblockCommand(Vector coordinates, Block block, ID.BlockAdd mode)
        {
            Coordinates = coordinates;
            Block = block;
            Mode = mode;
        }

        /// <summary>
        /// The coords to place the block at
        /// </summary>
        public Vector Coordinates
        {
            get => coordinates;
            set
            {
                coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null.");
            }
        }

        /// <summary>
        /// The block to place
        /// </summary>
        public Block Block
        {
            get => block;
            set
            {
                block = value ?? throw new ArgumentNullException(nameof(Block), "Block may not be null.");
            }
        }

        /// <summary>
        /// The way to place the block
        /// </summary>
        public ID.BlockAdd Mode { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>setblock [Coordinates] [Block] [Mode]</returns>
        public override string GetCommandString()
        {
            return $"setblock {Coordinates.GetVectorString()} {Block.GetBlockPlacementString()} {Mode}";
        }
    }

    /// <summary>
    /// Command which changes the world spawn
    /// </summary>
    public class SetWorldSpawnCommand : BaseCommand
    {
        private Vector coordinates;

        /// <summary>
        /// intializes a new <see cref="SetWorldSpawnCommand"/>
        /// </summary>
        /// <param name="coordinates">The coordinates to place the world spawn at</param>
        public SetWorldSpawnCommand(Vector coordinates)
        {
            Coordinates = coordinates;
        }

        /// <summary>
        /// The coordinates to place the world spawn at
        /// </summary>
        public Vector Coordinates
        {
            get => coordinates;
            set
            {
                coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null.");
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>setworldspawn [Coordinates]</returns>
        public override string GetCommandString()
        {
            return $"setworldspawn {Coordinates.GetVectorString()}";
        }
    }

    /// <summary>
    /// Command which spreads entities around a location
    /// </summary>
    public class SpreadPlayersCommand : BaseCommand
    {
        private BaseSelector selector;
        private Vector coordinates;
        private double distance;
        private double maxRange;
        private bool doTogetherCheck;

        /// <summary>
        /// Intializes a new <see cref="SpreadPlayersCommand"/>
        /// </summary>
        /// <param name="coordinates">The coordinates to spread the entities around</param>
        /// <param name="selector">Selector selecting the entities to spread</param>
        /// <param name="distance">The minimum distance between spreaded entities</param>
        /// <param name="maxRange">The max distance the entities can be spread</param>
        /// <param name="respectTeams">True if teams should be grouped together. False if they shouldn't</param>
        public SpreadPlayersCommand(Vector coordinates, BaseSelector selector, double distance, double maxRange, bool respectTeams)
        {
            Coordinates = coordinates;
            Selector = selector;
            ChangeRanges(distance, maxRange);
            RespectTeams = respectTeams;
        }

        /// <summary>
        /// Changes both distance and maxRange at the same time
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="maxRange"></param>
        public void ChangeRanges(double distance, double maxRange)
        {
            doTogetherCheck = false;
            if (!ValidateRanges(distance, maxRange))
            {
                throw new ArgumentException(nameof(Distance), "Distance may not be higher than MaxRange or 1 away from it");
            }
            Distance = distance;
            MaxRange = maxRange;
            doTogetherCheck = true;
        }

        /// <summary>
        /// The coordinates to spread the entities around
        /// </summary>
        public Vector Coordinates
        {
            get => coordinates;
            set
            {
                coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null.");
            }
        }

        /// <summary>
        /// Selector selecting the entities to spread
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.");
            }
        }

        /// <summary>
        /// The minimum distance between spreaded entities
        /// </summary>
        public double Distance
        {
            get => distance;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Distance), "Distance may not be less than 0");
                }
                if (doTogetherCheck && !ValidateRanges(value, MaxRange))
                {
                    throw new ArgumentException(nameof(Distance), "Distance may not be higher than MaxRange or 1 away from it");
                }
                distance = value;
            }
        }

        /// <summary>
        /// The max distance the entities can be spread
        /// </summary>
        public double MaxRange
        {
            get => maxRange;
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(MaxRange), "MaxRange may not be less than 1");
                }
                if (doTogetherCheck && !ValidateRanges(Distance, value))
                {
                    throw new ArgumentException(nameof(MaxRange), "MaxRange may not be less than Distance or 1 away from it");
                }
                maxRange = value;
            }
        }

        private static bool ValidateRanges(double distance, double maxRange)
        {
            return (distance + 1 < maxRange);
        }

        /// <summary>
        /// True if teams should be grouped together. False if they shouldn't
        /// </summary>
        public bool RespectTeams { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>spreadplayers [Coordinates] [Distance] [MaxRange] [RespectTeams] [Selector]</returns>
        public override string GetCommandString()
        {
            return $"spreadplayers {Coordinates.GetXString()} {Coordinates.GetZString()} {Distance.ToMinecraftDouble()} {MaxRange.ToMinecraftDouble()} {RespectTeams.ToMinecraftBool()} {Selector.GetSelectorString()}";
        }
    }

    /// <summary>
    /// Command which summons an entity at the given location
    /// </summary>
    public class SummonCommand : BaseCommand
    {
        private Entity.BaseEntity entity;
        private Vector coordinates;

        /// <summary>
        /// intializes a new <see cref="SummonCommand"/>
        /// </summary>
        /// <param name="entity">The entity to summon</param>
        /// <param name="coordinates">The coordinates the summon the entity at</param>
        public SummonCommand(Entity.BaseEntity entity, Vector coordinates)
        {
            Entity = entity;
            Coordinates = coordinates;
        }



        /// <summary>
        /// The entity to summon
        /// </summary>
        public Entity.BaseEntity Entity
        {
            get => entity;
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(Entity), "Entity may not be null.");
                }
                if (value.EntityType is null)
                {
                    throw new ArgumentNullException(nameof(Entity), "Entity type may not ne null.");
                }
                entity = value;
            }
        }

        /// <summary>
        /// The coordinates the summon the entity at
        /// </summary>
        public Vector Coordinates
        {
            get => coordinates;
            set
            {
                coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null.");
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>summon [Entity(type)] [Coordinates] [Entity(data)]</returns>
        public override string GetCommandString()
        {
            return $"summon {Entity.EntityType} {Coordinates.GetVectorString()} {Entity.GetDataWithoutID()}";
        }
    }

    /// <summary>
    /// Sends a private message to one or more players
    /// </summary>
    public class TellrawCommand : BaseCommand
    {
        private JsonText text;
        private BaseSelector selector;

        /// <summary>
        /// intializes a new <see cref="TellrawCommand"/>
        /// </summary>
        /// <param name="text">The text it should display</param>
        /// <param name="selector">Selector for selecting players to private message</param>
        public TellrawCommand(BaseSelector selector, JsonText text)
        {
            Text = text;
            Selector = selector;
        }

        /// <summary>
        /// The text it should display
        /// </summary>
        public JsonText Text
        {
            get => text;
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(Text), "Text may not be null");
                }
                text = value;
            }
        }

        /// <summary>
        /// Selector for selecting players to private message
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.");
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>tellraw [Selector] [Text]</returns>
        public override string GetCommandString()
        {
            return $"tellraw {Selector.GetSelectorString()} {Text.GetJsonString()}";
        }
    }

    /// <summary>
    /// Clears one or more items from some selected players' inventories
    /// </summary>
    public class ClearCommand : BaseCommand
    {
        private Item item;
        private BaseSelector selector;
        private int? maxCount;

        /// <summary>
        /// intializes a new <see cref="ClearCommand"/>
        /// </summary>
        /// <param name="selector">Selector for selecting players to clear</param>
        /// <param name="item">The item to clear. Leave null to clear all items</param>
        /// <param name="maxCount">The maximum amount of items to clear. An item has to be set to use this</param>
        public ClearCommand(BaseSelector selector, Item item = null, int? maxCount = null)
        {
            Selector = selector;
            Item = item;
            MaxCount = maxCount;
        }

        /// <summary>
        /// The item to clear. Leave null to clear all items
        /// </summary>
        public Item Item 
        { 
            get => item;
            set 
            { 
                if (!(MaxCount is null) && value is null)
                {
                    throw new ArgumentException("Item may not be null if a max count value is set.", nameof(Item));
                }
                item = value; 
            }
        }

        /// <summary>
        /// Selector for selecting players to clear
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.");
            }
        }

        /// <summary>
        /// The maximum amount of items to clear. An item has to be set to use this
        /// </summary>
        public int? MaxCount 
        { 
            get => maxCount;
            set 
            { 
                if (!(value is null) && value < 0)
                {
                    throw new ArgumentOutOfRangeException("MaxCount may not be less than 0.", nameof(MaxCount));
                }
                if (Item is null && !(value is null))
                {
                    throw new ArgumentException("MaxCount cannot have a value if item doesn't have a value", nameof(MaxCount));
                }
                maxCount = value; 
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>clear [Selector] ([Item]) ([MaxCount])</returns>
        public override string GetCommandString()
        {
            if (Item is null)
            {
                return $"clear {Selector.GetSelectorString()}";
            }
            else if (MaxCount is null)
            {
                return $"clear {Selector.GetSelectorString()} {Item.GetIDDataString()}";
            }
            else
            {
                return $"clear {Selector.GetSelectorString()} {Item.GetIDDataString()} {MaxCount}";
            }
        }
    }

    /// <summary>
    /// Commands which gives a player an item
    /// </summary>
    public class GiveCommand : BaseCommand
    {
        private Item item;
        private BaseSelector selector;
        private int count;

        /// <summary>
        /// Intializes a new <see cref="GiveCommand"/>
        /// </summary>
        /// <param name="item">The item to give</param>
        /// <param name="selector">Selector for selecting players to give the item to</param>
        /// <param name="count">The amount of the item to give</param>
        public GiveCommand(BaseSelector selector, Item item, int count)
        {
            Item = item;
            Selector = selector;
            Count = count;
        }

        /// <summary>
        /// The item to give
        /// </summary>
        public Item Item
        {
            get => item;
            set
            {
                item = value ?? throw new ArgumentNullException(nameof(Item), "Item may not be null.");
            }
        }

        /// <summary>
        /// Selector for selecting players to give the item to
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.");
            }
        }

        /// <summary>
        /// The amount of the item to give
        /// </summary>
        public int Count
        {
            get => count;
            set
            {
                if (value < 1 || value > 64)
                {
                    throw new ArgumentOutOfRangeException(nameof(Count), "Count may not be less than 1 or higher than 64");
                }
                count = value;
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>give [Selector] [Item] [Count]</returns>
        public override string GetCommandString()
        {
            if (count == 1)
            {
                return $"give {Selector.GetSelectorString()} {Item.GetIDDataString()}";
            }
            else
            {
                return $"give {Selector.GetSelectorString()} {Item.GetIDDataString()} {Count}";
            }
        }
    }

    /// <summary>
    /// Command which plays a sound
    /// </summary>
    public class PlaySoundCommand : BaseCommand
    {
        private string sound;
        private double volume;
        private double pitch;
        private double minimumVolume;
        private BaseSelector selector;
        private Vector coordinates;

        /// <summary>
        /// Intializes a new <see cref="PlaySoundCommand"/>
        /// </summary>
        /// <param name="sound">The sound to play</param>
        /// <param name="source">The category to play the music in</param>
        /// <param name="selector">Selector selecting players to play the sound for</param>
        /// <param name="volume">The volume of the sound. Everything above 2 won't sound louder, but will make the sound hearable from further away</param>
        /// <param name="pitch">The pitch of the sound</param>
        /// <param name="minimumVolume">The minimum value of the sound</param>
        /// <param name="coordinates">The coordinates to play the sound at</param>
        public PlaySoundCommand(string sound, ID.SoundSource source, BaseSelector selector, Vector coordinates, double volume, double pitch, double minimumVolume)
        {
            Sound = sound;
            Source = source;
            Selector = selector;
            Volume = volume;
            Pitch = pitch;
            MinimumVolume = minimumVolume;
            Coordinates = coordinates;
        }

        /// <summary>
        /// The sound to play
        /// </summary>
        public string Sound 
        { 
            get => sound;
            set 
            { 
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Sound may not be null or whitepsace", nameof(Sound));
                }
                sound = value; 
            }
        }

        /// <summary>
        /// The category to play the music in
        /// </summary>
        public ID.SoundSource Source { get; set; }

        /// <summary>
        /// Selector selecting players to play the sound for
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.");
            }
        }

        /// <summary>
        /// The coordinates to play the sound at
        /// </summary>
        public Vector Coordinates { get => coordinates; set => coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null"); }

        /// <summary>
        /// The volume of the sound. Everything above 2 won't sound louder, but will make the sound hearable from further away
        /// </summary>
        public double Volume 
        { 
            get => volume;
            set 
            { 
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Volume), "Volume may not be less than 0");
                }
                volume = value; 
            }
        }
        /// <summary>
        /// The pitch of the sound
        /// </summary>
        public double Pitch 
        { 
            get => pitch;
            set 
            {
                if (value < 0 || value > 2)
                {
                    throw new ArgumentOutOfRangeException(nameof(Pitch), "Pitch may not be less than 0 or higher than 2");
                }
                pitch = value;  
            }
        }
        /// <summary>
        /// The minimum value of the sound
        /// </summary>
        public double MinimumVolume 
        { 
            get => minimumVolume;
            set 
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(MinimumVolume), "MinimumVolume may not be less than 0 or higher than 2");
                }
                minimumVolume = value; 
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>playsound [Sound] [Source] [Selector] [Coordinates] [Volume] [Pitch] [MinimumVolume]</returns>
        public override string GetCommandString()
        {
            return $"playsound {Sound} {Source} {Selector.GetSelectorString()} {Coordinates.GetVectorString()} {Volume.ToMinecraftDouble()} {Pitch.ToMinecraftDouble()} {MinimumVolume.ToMinecraftDouble()}";
        }
    }

    /// <summary>
    /// Command which changes one or more player's spawnpoints
    /// </summary>
    public class SpawnPointCommand : BaseCommand
    {
        private Vector coordinates;
        private BaseSelector selector;

        /// <summary>
        /// Intializes a new <see cref="SpawnPointCommand"/>
        /// </summary>
        /// <param name="coordinates">The place to move the spawnpoint to</param>
        /// <param name="selector">Selector selecting the players whose spawn point to change</param>
        public SpawnPointCommand(Vector coordinates, BaseSelector selector)
        {
            Coordinates = coordinates;
            Selector = selector;
        }

        /// <summary>
        /// The place to move the spawnpoint to
        /// </summary>
        public Vector Coordinates { get => coordinates; set => coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null"); }

        /// <summary>
        /// Selector selecting the players whose spawn point to change
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.");
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>clear [Selector] ([Item]) ([MaxCount])</returns>
        public override string GetCommandString()
        {
            return $"spawnpoint {Selector.GetSelectorString()} {Coordinates.GetVectorString()}";
        }
    }

    /// <summary>
    /// Command which stops a sound for one or more players
    /// </summary>
    public class StopSoundCommand : BaseCommand
    {
        private BaseSelector selector;

        /// <summary>
        /// Intializes a new <see cref="StopSoundCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting the players to stop the sound for</param>
        /// <param name="sound">The sound to stop. Leave null to stop all sounds</param>
        /// <param name="source">The category to stop sounds in. Leave null to stop sound in all categories</param>
        public StopSoundCommand(BaseSelector selector, string sound, ID.SoundSource? source)
        {
            Selector = selector;
            Sound = sound;
            Source = source;
        }

        /// <summary>
        /// Selector selecting the players to stop the sound for
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.");
            }
        }

        /// <summary>
        /// The sound to stop. Leave null to stop all sounds
        /// </summary>
        public string Sound { get; set; }

        /// <summary>
        /// The category to stop sounds in. Leave null to stop sound in all categories
        /// </summary>
        public ID.SoundSource? Source { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>stopsound [Selector] ([Source]) ([Sound])</returns>
        public override string GetCommandString()
        {
            if (Sound is null)
            {
                if (Source is null)
                {
                    return $"stopsound {Selector.GetSelectorString()}";
                }
                else
                {
                    return $"stopsound {Selector.GetSelectorString()} {Source}";
                }
            }
            else
            {
                if (Source is null)
                {
                    return $"stopsound {Selector.GetSelectorString()} * {Sound}";
                }
                else
                {
                    return $"stopsound {Selector.GetSelectorString()} {Source} {Sound}";
                }
            }
        }
    }

    /// <summary>
    /// Command which changes the weather for some time
    /// </summary>
    public class WeatherCommand : BaseCommand
    {
        private Time time;

        /// <summary>
        /// Intializes a new <see cref="WeatherCommand"/>
        /// </summary>
        /// <param name="time">The amount of time the weather is there for</param>
        /// <param name="weather">The weather to change the weather to</param>
        public WeatherCommand(ID.WeatherType weather, Time time)
        {
            Time = time;
            Weather = weather;
        }

        /// <summary>
        /// The amount of time the weather is there for
        /// </summary>
        public Time Time { get => time; set => time = value; }

        /// <summary>
        /// The weather to change the weather to
        /// </summary>
        public ID.WeatherType Weather { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>weather [Weather] [Time]</returns>
        public override string GetCommandString()
        {
            if (Time is null)
            {
                return $"weather {Weather}";
            }
            else
            {
                return $"weather {Weather} {Time.AsTicks()}";
            }
        }
    }
}
