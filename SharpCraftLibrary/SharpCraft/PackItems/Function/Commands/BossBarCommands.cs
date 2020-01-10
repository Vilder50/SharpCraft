using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which adds the given boss bar to the world
    /// </summary>
    public class BossBarAddCommand : BaseCommand
    {
        private BossBar bossBar;
        private JsonText[] name;

        /// <summary>
        /// Intializes a new <see cref="BossBarAddCommand"/>
        /// </summary>
        /// <param name="bossBar">The boss bar to add</param>
        /// <param name="name">The shown name of the boss bar</param>
        public BossBarAddCommand(BossBar bossBar, JsonText[] name)
        {
            BossBar = bossBar;
            Name = name;
        }

        /// <summary>
        /// The boss bar to add
        /// </summary>
        public BossBar BossBar
        {
            get => bossBar;
            set
            {
                bossBar = value ?? throw new ArgumentNullException(nameof(BossBar), "BossBar may not be null.");
            }
        }

        /// <summary>
        /// The shown name of the boss bar
        /// </summary>
        public JsonText[] Name
        {
            get => name;
            set
            {
                name = value ?? throw new ArgumentNullException(nameof(Name), "Name may not be null.");
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>bossbar add [Bossbar] [Name]</returns>
        public override string GetCommandString()
        {
            return $"bossbar add {BossBar.Name} {Name.GetString(true)}";
        }
    }

    /// <summary>
    /// Command which gets a value from a boss bar
    /// </summary>
    public class BossBarGetValueCommand : BaseCommand
    {
        private BossBar bossBar;

        /// <summary>
        /// Intializes a new <see cref="BossBarGetValueCommand"/>
        /// </summary>
        /// <param name="bossBar">The boss bar to the value from</param>
        /// <param name="getValue">The value to get from the boss bar</param>
        public BossBarGetValueCommand(BossBar bossBar, ID.BossBarValue getValue)
        {
            BossBar = bossBar;
            GetValue = getValue;
        }

        /// <summary>
        /// The boss bar to the value from
        /// </summary>
        public BossBar BossBar
        {
            get => bossBar;
            set
            {
                bossBar = value ?? throw new ArgumentNullException(nameof(BossBar), "BossBar may not be null.");
            }
        }

        /// <summary>
        /// The value to get from the boss bar
        /// </summary>
        public ID.BossBarValue GetValue { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>bossbar add [Bossbar] [Name]</returns>
        public override string GetCommandString()
        {
            return $"bossbar get {BossBar.Name} {GetValue}";
        }
    }

    /// <summary>
    /// Command which returns the amount of existing boss bars
    /// </summary>
    public class BossBarGetAllCommand : BaseCommand
    {
        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>bossbar list</returns>
        public override string GetCommandString()
        {
            return "bossbar list";
        }
    }

    /// <summary>
    /// Command which removes a boss bar
    /// </summary>
    public class BossBarRemoveCommand : BaseCommand
    {
        private BossBar bossBar;

        /// <summary>
        /// Intializes a new <see cref="BossBarRemoveCommand"/>
        /// </summary>
        /// <param name="bossBar">The boss bar to remove</param>
        public BossBarRemoveCommand(BossBar bossBar)
        {
            BossBar = bossBar;
        }

        /// <summary>
        /// The boss bar to remove
        /// </summary>
        public BossBar BossBar
        {
            get => bossBar;
            set
            {
                bossBar = value ?? throw new ArgumentNullException(nameof(BossBar), "BossBar may not be null.");
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>bossbar remove [Bossbar]</returns>
        public override string GetCommandString()
        {
            return $"bossbar remove {BossBar.Name}";
        }
    }

    /// <summary>
    /// Command which changes a boss bar's color
    /// </summary>
    public class BossBarChangeColorCommand : BaseCommand
    {
        private BossBar bossBar;

        /// <summary>
        /// Intializes a new <see cref="BossBarGetValueCommand"/>
        /// </summary>
        /// <param name="bossBar">The boss bar to change color on</param>
        /// <param name="color">The new color for the bar</param>
        public BossBarChangeColorCommand(BossBar bossBar, ID.BossBarColor color)
        {
            BossBar = bossBar;
            Color = color;
        }

        /// <summary>
        /// The boss bar to change color on
        /// </summary>
        public BossBar BossBar
        {
            get => bossBar;
            set
            {
                bossBar = value ?? throw new ArgumentNullException(nameof(BossBar), "BossBar may not be null.");
            }
        }

        /// <summary>
        /// The new color for the bar
        /// </summary>
        public ID.BossBarColor Color { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>bossbar set [Bossbar] color [Color]</returns>
        public override string GetCommandString()
        {
            return $"bossbar set {BossBar.Name} color {Color}";
        }
    }

    /// <summary>
    /// Command which changes a boss bar's max value
    /// </summary>
    public class BossBarChangeMaxValueCommand : BaseCommand
    {
        private BossBar bossBar;
        private int maxValue;

        /// <summary>
        /// Intializes a new <see cref="BossBarChangeMaxValueCommand"/>
        /// </summary>
        /// <param name="bossBar">The boss bar to change max value on</param>
        /// <param name="maxValue">The new max value for the bar</param>
        public BossBarChangeMaxValueCommand(BossBar bossBar, int maxValue)
        {
            BossBar = bossBar;
            MaxValue = maxValue;
        }

        /// <summary>
        /// The boss bar to change max value on
        /// </summary>
        public BossBar BossBar
        {
            get => bossBar;
            set
            {
                bossBar = value ?? throw new ArgumentNullException(nameof(BossBar), "BossBar may not be null.");
            }
        }

        /// <summary>
        /// The new max value for the bar
        /// </summary>
        public int MaxValue
        {
            get => maxValue;
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(MaxValue), "MaxValue may not be less than 1");
                }
                maxValue = value;
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>bossbar set [Bossbar] max [MaxValue]</returns>
        public override string GetCommandString()
        {
            return $"bossbar set {BossBar.Name} max {MaxValue}";
        }
    }

    /// <summary>
    /// Command which changes a boss bar's value
    /// </summary>
    public class BossBarChangeValueCommand : BaseCommand
    {
        private BossBar bossBar;
        private int barValue;

        /// <summary>
        /// Intializes a new <see cref="BossBarChangeValueCommand"/>
        /// </summary>
        /// <param name="bossBar">The boss bar to change value on</param>
        /// <param name="value">The new value for the bar</param>
        public BossBarChangeValueCommand(BossBar bossBar, int value)
        {
            BossBar = bossBar;
            Value = value;
        }

        /// <summary>
        /// The boss bar to change value on
        /// </summary>
        public BossBar BossBar
        {
            get => bossBar;
            set
            {
                bossBar = value ?? throw new ArgumentNullException(nameof(BossBar), "BossBar may not be null.");
            }
        }

        /// <summary>
        /// The new value for the bar
        /// </summary>
        public int Value
        {
            get => barValue;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Value), "Value may not be less than 0");
                }
                barValue = value;
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>bossbar set [Bossbar] value [Value]</returns>
        public override string GetCommandString()
        {
            return $"bossbar set {BossBar.Name} value {Value}";
        }
    }

    /// <summary>
    /// Command which changes a boss bar's name
    /// </summary>
    public class BossBarChangeNameCommand : BaseCommand
    {
        private BossBar bossBar;
        private JsonText[] name;

        /// <summary>
        /// Intializes a new <see cref="BossBarChangeNameCommand"/>
        /// </summary>
        /// <param name="bossBar">The boss bar to change the name of</param>
        /// <param name="name">The name the bar should change to</param>
        public BossBarChangeNameCommand(BossBar bossBar, JsonText[] name)
        {
            BossBar = bossBar;
            Name = name;
        }

        /// <summary>
        /// The boss bar to change the name of
        /// </summary>
        public BossBar BossBar
        {
            get => bossBar;
            set
            {
                bossBar = value ?? throw new ArgumentNullException(nameof(BossBar), "BossBar may not be null.");
            }
        }

        /// <summary>
        /// The name the bar should change to
        /// </summary>
        public JsonText[] Name
        {
            get => name;
            set
            {
                name = value ?? throw new ArgumentNullException(nameof(Name), "Name may not be null.");
            }
        }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>bossbar set [Bossbar] name [Name]</returns>
        public override string GetCommandString()
        {
            return $"bossbar set {BossBar.Name} name {Name.GetString(true)}";
        }
    }

    /// <summary>
    /// Command which changes who can see a boss bar
    /// </summary>
    public class BossBarChangePlayersCommand : BaseCommand
    {
        private BossBar bossBar;
        private BaseSelector selector;

        /// <summary>
        /// Intializes a new <see cref="BossBarChangePlayersCommand"/>
        /// </summary>
        /// <param name="bossBar">The boss bar to change the name of</param>
        /// <param name="selector">Selector selecting players who should see the bar</param>
        public BossBarChangePlayersCommand(BossBar bossBar, BaseSelector selector)
        {
            BossBar = bossBar;
            Selector = selector;
        }

        /// <summary>
        /// The boss bar to change the name of
        /// </summary>
        public BossBar BossBar
        {
            get => bossBar;
            set
            {
                bossBar = value ?? throw new ArgumentNullException(nameof(BossBar), "BossBar may not be null.");
            }
        }

        /// <summary>
        /// Selector selecting players who should see the bar
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
        /// <returns>bossbar set [Bossbar] players [Selector]</returns>
        public override string GetCommandString()
        {
            return $"bossbar set {BossBar.Name} players {Selector.GetSelectorString()}";
        }
    }

    /// <summary>
    /// Command which changes a boss bar's style
    /// </summary>
    public class BossBarChangeStyleCommand : BaseCommand
    {
        private BossBar bossBar;

        /// <summary>
        /// Intializes a new <see cref="BossBarGetValueCommand"/>
        /// </summary>
        /// <param name="bossBar">The boss bar to change style on</param>
        /// <param name="style">The new style for the bar</param>
        public BossBarChangeStyleCommand(BossBar bossBar, ID.BossBarStyle style)
        {
            BossBar = bossBar;
            Style = style;
        }

        /// <summary>
        /// The boss bar to change style on
        /// </summary>
        public BossBar BossBar
        {
            get => bossBar;
            set
            {
                bossBar = value ?? throw new ArgumentNullException(nameof(BossBar), "BossBar may not be null.");
            }
        }

        /// <summary>
        /// The new style for the bar
        /// </summary>
        public ID.BossBarStyle Style { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>bossbar set [Bossbar] style [Style]</returns>
        public override string GetCommandString()
        {
            return $"bossbar set {BossBar.Name} style {Style}";
        }
    }

    /// <summary>
    /// Command which changes a boss bar's style
    /// </summary>
    public class BossBarChangeVisibilityCommand : BaseCommand
    {
        private BossBar bossBar;

        /// <summary>
        /// Intializes a new <see cref="BossBarChangeVisibilityCommand"/>
        /// </summary>
        /// <param name="bossBar">The boss bar to change visibility on</param>
        /// <param name="visible">The new visibility for the bar</param>
        public BossBarChangeVisibilityCommand(BossBar bossBar, bool visible)
        {
            BossBar = bossBar;
            Visible = visible;
        }

        /// <summary>
        /// The boss bar to change visibility on
        /// </summary>
        public BossBar BossBar
        {
            get => bossBar;
            set
            {
                bossBar = value ?? throw new ArgumentNullException(nameof(BossBar), "BossBar may not be null.");
            }
        }

        /// <summary>
        /// The new visibility for the bar
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>bossbar set [Bossbar] visible [Visible]</returns>
        public override string GetCommandString()
        {
            return $"bossbar set {BossBar.Name} visible {Visible.ToMinecraftBool()}";
        }
    }
}
