using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command which only executes the following command if the selector selects an entity
    /// </summary>
    public class ExecuteIfEntity : BaseExecuteIfCommand
    {
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="ExecuteIfEntity"/> command
        /// </summary>
        /// <param name="selector">Selector used to check if one or more entities exists</param>
        /// <param name="executeIf">True to use execute if and false to use execute unless the given thing is true</param>
        public ExecuteIfEntity(BaseSelector selector, bool executeIf = true) : base(executeIf)
        {
            Selector = selector;
        }

        /// <summary>
        /// Selector used to check if one or more entities exists
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
        /// <returns>entity [Selector]</returns>
        protected override string GetCheckPart()
        {
            return "entity " + Selector.GetSelectorString();
        }
    }
}
