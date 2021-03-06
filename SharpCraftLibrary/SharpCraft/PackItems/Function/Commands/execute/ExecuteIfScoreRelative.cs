﻿using System;
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
        private BaseSelector selector1 = null!;
        private Objective objective1 = null!;
        private BaseSelector selector2 = null!;
        private Objective objective2 = null!;

        /// <summary>
        /// Intializes a new <see cref="ExecuteIfScoreRelative"/> command
        /// </summary>
        /// <param name="selector1">Selector selecting the thing to get a score from</param>
        /// <param name="objective1">The <see cref="Objective"/> to get the score for <see cref="selector1"/> from</param>
        /// <param name="selector2">Selector selecting the thing to get a score from</param>
        /// <param name="objective2">The <see cref="Objective"/> to get the score for <see cref="selector2"/> from</param>
        /// <param name="operator">How the scores should be relative to each other</param>
        /// <param name="executeIf">True to use execute if and false to use execute unless the given thing is true</param>
        public ExecuteIfScoreRelative(BaseSelector selector1, Objective objective1, ID.IfScoreOperation @operator, BaseSelector selector2, Objective objective2, bool executeIf = true) : base(executeIf)
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
                selector1 = Validators.ValidateSingleSelectSelector(value, nameof(Selector1), nameof(ExecuteIfScoreRelative));
            }
        }

        /// <summary>
        /// The <see cref="Objective"/> to get the score for <see cref="Selector1"/> from
        /// </summary>
        public Objective Objective1
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
                selector2 = Validators.ValidateSingleSelectSelector(value, nameof(Selector2), nameof(ExecuteIfScoreRelative));
            }
        }

        /// <summary>
        /// The <see cref="Objective"/> to get the score for <see cref="Selector2"/> from
        /// </summary>
        public Objective Objective2
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
            string OperationString = Operator switch
            {
                ID.IfScoreOperation.Equel => "=",
                ID.IfScoreOperation.Higher => ">",
                ID.IfScoreOperation.HigherOrEquel => ">=",
                ID.IfScoreOperation.Smaller => "<",
                ID.IfScoreOperation.SmallerOrEquel => "<=",

                _ => "=",
            };
            return "score " + Selector1.GetSelectorString() + " " + Objective1.Name + " " + OperationString + " " + Selector2.GetSelectorString() + " " + Objective2.Name;
        }
    }
}
