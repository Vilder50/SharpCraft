using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// base class for execute if/unless commands
    /// </summary>
    public abstract class BaseExecuteIfCommand : BaseExecuteCommand
    {
        /// <summary>
        /// Intializes a new <see cref="BaseExecuteCommand"/>
        /// </summary>
        /// <param name="executeIf">True to use execute if and false to use execute unless the given thing is true</param>
        public BaseExecuteIfCommand(bool executeIf = true)
        {
            ExecuteIf = executeIf;
        }

        /// <summary>
        /// True to use execute if and false to use execute unless the given thing is true
        /// </summary>
        public bool ExecuteIf { get; set; }

        /// <summary>
        /// Returns the command string without the "execute" part at the beginning
        /// </summary>
        /// <returns>The command string without the "execute" part at the beginning</returns>
        protected override string GetExecutePart()
        {
            return (ExecuteIf ? "if" : "unless") + " " + GetCheckPart();
        }

        /// <summary>
        /// Returns the command part of the execute command without the if/unless part
        /// </summary>
        /// <returns>The command part of the execute command without the if/unless part</returns>
        /// <remarks>
        /// eg: with the "/execute if entity [selector] run ..." command this would return the "entity [selector]" part
        /// </remarks>
        protected abstract string GetCheckPart();
    }
}
