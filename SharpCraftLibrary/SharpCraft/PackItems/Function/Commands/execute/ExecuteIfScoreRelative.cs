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
    public class ExecuteIfScoreRelative : BaseExecuteIfCommand
    {
        private BaseSelector selector1;
        private ScoreObject objective1;
        private BaseSelector selector2;
        private ScoreObject objective2;

        /// <summary>
        /// Intializes a new <see cref="ExecuteIfScoreRelative"/> command
        /// </summary>
        /// <param name="selector1">Selector selecting the thing to get a score from</param>
        /// <param name="objective1">The <see cref="ScoreObject"/> to get the score for <see cref="selector1"/> from</param>
        /// <param name="selector2">Selector selecting the thing to get a score from</param>
        /// <param name="objective2">The <see cref="ScoreObject"/> to get the score for <see cref="selector2"/> from</param>
        /// <param name="operator">How the scores should be relative to each other</param>
        /// <param name="executeIf">True to use execute if and false to use execute unless the given thing is true</param>
        public ExecuteIfScoreRelative(BaseSelector selector1, ScoreObject objective1, ID.IfScoreOperation @operator, BaseSelector selector2, ScoreObject objective2, bool executeIf = true) : base(executeIf)
        {
            Selector1 = selector1;
            Objective1 = objective1;
            Selector2 = selector2;
            Objective2 = objective2;
            Operator = @operator;
        }

        /// <summary>
        /// Selector selecting the thing to get a score from
        /// </summary>
        public BaseSelector Selector1
        {
            get => selector1;
            set
            {
                if (!(value ?? throw new ArgumentNullException(nameof(Selector1), "Selector1 may not be null.")).IsLimited())
                {
                    throw new ArgumentException("Command doesn't allow selectors which selects multiple entities", nameof(Selector1));
                }
                selector1 = value;
            }
        }

        /// <summary>
        /// The <see cref="ScoreObject"/> to get the score for <see cref="Selector1"/> from
        /// </summary>
        public ScoreObject Objective1
        {
            get => objective1;
            set
            {
                objective1 = value ?? throw new ArgumentNullException(nameof(Objective1), "Objective1 may not be null.");
            }
        }

        /// <summary>
        /// Selector selecting the thing to get a score from
        /// </summary>
        public BaseSelector Selector2
        {
            get => selector2;
            set
            {
                if (!(value ?? throw new ArgumentNullException(nameof(Selector2), "Selector2 may not be null.")).IsLimited())
                {
                    throw new ArgumentException("Command doesn't allow selectors which selects multiple entities", nameof(Selector2));
                }
                selector2 = value;
            }
        }

        /// <summary>
        /// The <see cref="ScoreObject"/> to get the score for <see cref="Selector2"/> from
        /// </summary>
        public ScoreObject Objective2
        {
            get => objective2;
            set
            {
                objective2 = value ?? throw new ArgumentNullException(nameof(Objective2), "Objective2 may not be null.");
            }
        }

        /// <summary>
        /// How the scores should be relative to each other
        /// </summary>
        public ID.IfScoreOperation Operator { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>score [Selector1] [Objective1] [Operator] [Selector2] [Objective2]</returns>
        protected override string GetCheckPart()
        {
            string OperationString;

            switch (Operator)
            {
                case ID.IfScoreOperation.Equel:
                    OperationString = "=";
                    break;
                case ID.IfScoreOperation.Higher:
                    OperationString = ">";
                    break;
                case ID.IfScoreOperation.HigherOrEquel:
                    OperationString = ">=";
                    break;
                case ID.IfScoreOperation.Smaller:
                    OperationString = "<";
                    break;
                case ID.IfScoreOperation.SmallerOrEquel:
                    OperationString = "<=";
                    break;

                default:
                    OperationString = "=";
                    break;
            }

            return "score " + Selector1 + " " + Objective1 + " " + OperationString + " " + Selector2 + " " + Objective2;
        }
    }
}
