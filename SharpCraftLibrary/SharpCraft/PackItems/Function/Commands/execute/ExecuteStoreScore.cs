using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command used for storing the result of the command
    /// </summary>
    public class ExecuteStoreScore : BaseExecuteStoreCommand
    {
        private BaseSelector selector = null!;
        private Objective objective = null!;

        /// <summary>
        /// Intializes a new <see cref="ExecuteStoreScore"/> command
        /// </summary>
        /// <param name="selector">The selector selecting the place to store the value at</param>
        /// <param name="objective">The <see cref="SharpCraft.Objective"/> to store the value in</param>
        /// <param name="storeResult">True if it should store the result. False if it should store success</param>
        public ExecuteStoreScore(BaseSelector selector, Objective objective, bool storeResult = true) : base(storeResult)
        {
            Selector = selector;
            Objective = objective;
        }

        /// <summary>
        /// The selector selecting the place to store the value at
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                selector = Validators.ValidateSingleSelectSelector(value, nameof(Selector), nameof(ExecuteStoreScore));
            }
        }

        /// <summary>
        /// The <see cref="SharpCraft.Objective"/> to store the value in
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
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>score [Selector] [Objective]</returns>
        protected override string GetStorePart()
        {
            return "score " + Selector.GetSelectorString() + " " + Objective.Name;
        }
    }
}
