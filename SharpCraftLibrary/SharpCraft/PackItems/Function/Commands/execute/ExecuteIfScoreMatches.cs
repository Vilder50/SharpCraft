using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command which only executes the following command if the entity chosen by the selector has the datapath
    /// </summary>
    public class ExecuteIfScoreMatches : BaseExecuteIfCommand
    {
        private BaseSelector selector = null!;
        private Objective objective = null!;
        private MCRange range = null!;

        /// <summary>
        /// Intializes a new <see cref="ExecuteIfScoreMatches"/> command
        /// </summary>
        /// <param name="selector">Selector selecting the thing to get a score from</param>
        /// <param name="objective">The <see cref="SharpCraft.Objective"/> to get the score for <see cref="Selector"/> from</param>
        /// <param name="range">The range the score should be inside</param>
        /// <param name="executeIf">True to use execute if and false to use execute unless the given thing is true</param>
        public ExecuteIfScoreMatches(BaseSelector selector, Objective objective, MCRange range, bool executeIf = true) : base(executeIf)
        {
            Selector = selector;
            Objective = objective;
            Range = range;
        }

        /// <summary>
        /// Selector selecting the thing to get a score from
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                selector = Validators.ValidateSingleSelectSelector(value, nameof(Selector), nameof(ExecuteIfScoreMatches));
            }
        }

        /// <summary>
        /// The <see cref="SharpCraft.Objective"/> to get the score for <see cref="Selector"/> from
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
        /// The range the score should be inside
        /// </summary>
        public MCRange Range
        {
            get => range;
            set
            {
                range = value ?? throw new ArgumentNullException(nameof(Range), "Range may not be null.");
            }
        }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>score [Selector] [Objective] matches [Range]</returns>
        protected override string GetCheckPart()
        {
            return "score " + Selector.GetSelectorString() + " " + Objective.Name + " matches " + Range.SelectorString();
        }
    }
}
