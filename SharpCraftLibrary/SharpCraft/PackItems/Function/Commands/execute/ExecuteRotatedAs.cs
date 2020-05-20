using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command for changing the rotation the command is using when being executed
    /// </summary>
    public class ExecuteRotatedAs : BaseExecuteCommand
    {
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="ExecuteRotatedAs"/> command
        /// </summary>
        /// <param name="selector">The selector for selecting the entity whose rotation to use for executing</param>
        public ExecuteRotatedAs(BaseSelector selector)
        {
            Selector = selector;
        }

        /// <summary>
        /// The selector for selecting the entity whose rotation to use for executing
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
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>rotated as [selector]</returns>
        protected override string GetExecutePart()
        {
            return "rotated as " + Selector.GetSelectorString();
        }
    }
}
