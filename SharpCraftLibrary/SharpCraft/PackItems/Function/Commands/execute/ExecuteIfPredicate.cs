using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command which only executes the following command if predicate is successfull
    /// </summary>
    public class ExecuteIfPredicate : BaseExecuteIfCommand
    {
        private IPredicate predicate;

        /// <summary>
        /// Intializes a new <see cref="ExecuteIfPredicate"/> command
        /// </summary>
        /// <param name="predicate">The predicate to run</param>
        /// <param name="executeIf">True to use execute if and false to use execute unless the given thing is true</param>
        public ExecuteIfPredicate(IPredicate predicate, bool executeIf = true) : base(executeIf)
        {
            Predicate = predicate;
        }

        /// <summary>
        /// The predicate to run
        /// </summary>
        public IPredicate Predicate { get => predicate; set => predicate = value ?? throw new ArgumentNullException(nameof(Predicate), "Predicate may not be null"); }


        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>predicate [Predicate]</returns>
        protected override string GetCheckPart()
        {
            return "predicate " + Predicate.GetNamespacedName();
        }
    }
}
