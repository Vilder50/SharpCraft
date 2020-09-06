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
    public abstract class BaseExecuteCommand : BaseCommand, ICommandChanger
    {
        /// <summary>
        /// Returns the execute command string
        /// </summary>
        /// <returns>The execute command string</returns>
        public override string GetCommandString()
        {
            return "execute " + GetExecuteCommandPart();
        }

        /// <summary>
        /// The command this execute command is going to execute
        /// </summary>
        public ICommand? ExecuteCommand { get; set; }

        /// <summary>
        /// True if this execute command is done changing commands.
        /// </summary>
        public bool DoneChanging { get; private set; }

        /// <summary>
        /// Returns the <see cref="GetCommandString"/> without the execute part at the beginning
        /// </summary>
        /// <returns>The command without the execute part at the beginning</returns>
        public string GetExecuteCommandPart()
        {
            string part = GetExecutePart();

            if (ExecuteCommand is null || ExecuteCommand is StopExecuteCommand)
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
        /// Adds the given command to the end of the execute line
        /// </summary>
        /// <param name="command">The command to add</param>
        /// <returns>Information about the adding</returns>
        public ICommand? ChangeCommand(ICommand command)
        {
            if (!HasEndCommand())
            {
                AddCommand(command);
                if (!(command is BaseExecuteCommand))
                {
                    DoneChanging = true;
                }
                return null;
            }

            DoneChanging = true;
            return command;
        }

        /// <summary>
        /// Returns true if the command at the end of the execute chain isn't an execute command. False if it is
        /// </summary>
        /// <returns>True if the command at the end of the execute chain isn't an execute command. False if it is</returns>
        public bool HasEndCommand()
        {
            if (ExecuteCommand is null)
            {
                return false;
            }

            if (ExecuteCommand is BaseExecuteCommand execute)
            {
                return execute.HasEndCommand();
            } 
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Adds the given command to the execute chain
        /// </summary>
        /// <param name="command">The command to add</param>
        /// <returns>This execute command</returns>
        public BaseExecuteCommand AddCommand(ICommand command)
        {
            if (ExecuteCommand is null)
            {
                ExecuteCommand = command;
            }
            else if (ExecuteCommand is BaseExecuteCommand execute)
            {
                execute.AddCommand(command);
            }
            else
            {
                throw new InvalidOperationException("Cannot add command to execute chain since the chain already ends in a command");
            }
            DoneChanging = HasEndCommand();
            return this;
        }

        /// <summary>
        /// Returns the command part of the execute command
        /// </summary>
        /// <returns>The command part of the execute command</returns>
        /// <remarks>
        /// eg: with the "/execute as [selector] run ..." command this would return the "as [selector]" part
        /// </remarks>
        protected abstract string GetExecutePart();

        /// <summary>
        /// Returns a shallow clone of this command
        /// </summary>
        /// <returns>A shallow clone of this command</returns>
        public override BaseCommand ShallowClone()
        {
            BaseExecuteCommand clone = (BaseExecuteCommand)MemberwiseClone();
            if (!(clone.ExecuteCommand is null))
            {
                clone.ExecuteCommand = clone.ExecuteCommand.ShallowClone();
            }

            return clone;
        }
    }
}
