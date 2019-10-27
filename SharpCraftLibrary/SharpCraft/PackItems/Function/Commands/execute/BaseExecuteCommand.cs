using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Base class for execute commands
    /// </summary>
    public abstract class BaseExecuteCommand : ICommand
    {
        /// <summary>
        /// Returns the execute command string
        /// </summary>
        /// <returns>The execute command string</returns>
        public string GetCommandString()
        {
            return "execute " + GetExecuteCommandPart();
        }

        /// <summary>
        /// The command this execute command is going to execute
        /// </summary>
        public ICommand ExecuteCommand { get; set; }

        /// <summary>
        /// Returns the <see cref="GetCommandString"/> without the execute part at the beginning
        /// </summary>
        /// <returns>The command without the execute part at the beginning</returns>
        public string GetExecuteCommandPart()
        {
            string part = GetExecutePart();

            if (ExecuteCommand is null)
            {
                return part;
            }
            else if (ExecuteCommand is BaseExecuteCommand executeCommand)
            {
                return part + " " + executeCommand.GetExecuteCommandPart();
            } 
            else
            {
                return part + " run " + ExecuteCommand.GetCommandString();
            }
        }

        /// <summary>
        /// Returns the command part of the execute command
        /// </summary>
        /// <returns>The command part of the execute command</returns>
        /// <remarks>
        /// eg: with the "/execute as [selector] run ..." command this would return the "as [selector]" part
        /// </remarks>
        protected abstract string GetExecutePart();
    }
}
