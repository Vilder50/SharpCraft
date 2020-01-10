using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command which changes one or more players' experience
    /// </summary>
    public class ExperienceModifyCommand : BaseCommand
    {
        private BaseSelector selector;

        /// <summary>
        /// Intializes a new <see cref="ExperienceModifyCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting the players whose experience to change</param>
        /// <param name="changeLevels">True if the players' levels should change. False if the players' points should change</param>
        /// <param name="modifier">The way to modify the players experience</param>
        /// <param name="value">The value to modify with</param>
        public ExperienceModifyCommand(BaseSelector selector, bool changeLevels, ID.AddSetModifier modifier, int value)
        {
            Selector = selector;
            ChangeLevels = changeLevels;
            Modifier = modifier;
            Value = value;
        }

        /// <summary>
        /// Selector selecting the players whose experience to change
        /// </summary>
        public BaseSelector Selector { get => selector; set => selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null"); }

        /// <summary>
        /// True if the players' levels should change. False if the players' points should change
        /// </summary>
        public bool ChangeLevels { get; set; }

        /// <summary>
        /// The way to modify the players experience
        /// </summary>
        public ID.AddSetModifier Modifier { get; set; }

        /// <summary>
        /// The value to modify with
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>experience [Modifier] [Selector] [Value] [ChangeLevels]</returns>
        public override string GetCommandString()
        {
            return $"xp {Modifier} {Selector.GetSelectorString()} {Value} {(ChangeLevels ? "levels" : "points")}";
        }
    }

    /// <summary>
    /// Command which gets a players experience
    /// </summary>
    public class ExperienceGetCommand : BaseCommand
    {
        private BaseSelector selector;

        /// <summary>
        /// Intializes a new <see cref="ExperienceGetCommand"/>
        /// </summary>
        /// <param name="selector">Selector selecting the player whose experience to get</param>
        /// <param name="getLevels">True to get the player's levels. False to get the player's points</param>
        public ExperienceGetCommand(BaseSelector selector, bool getLevels)
        {
            Selector = selector;
            GetLevels = getLevels;
        }

        /// <summary>
        /// Selector selecting the player whose experience to get
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                if (!(value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.")).IsLimited())
                {
                    throw new ArgumentException("Command doesn't allow selectors which selects multiple entities", nameof(Selector));
                }
                selector = value;
            }
        }

        /// <summary>
        /// True to get the player's levels. False to get the player's points
        /// </summary>
        public bool GetLevels { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>experience query [Selector] [ChangeLevels]</returns>
        public override string GetCommandString()
        {
            return $"xp query {Selector.GetSelectorString()} {(GetLevels ? "levels" : "points")}";
        }
    }
}
