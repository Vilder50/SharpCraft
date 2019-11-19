using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Interface for commands which when added to a function file should be able to change commands added afterwards
    /// </summary>
    public interface ICommandChanger
    {
        /// <summary>
        /// Changes the given command
        /// </summary>
        /// <param name="command">The command to change</param>
        /// <returns>Information about the change</returns>
        CommandChange ChangeCommand(ICommand command);
    }

    /// <summary>
    /// Class containing change data returned from an object with the <see cref="ICommandChanger"/> interface
    /// </summary>
    public struct CommandChange
    {
        /// <summary>
        /// Stops the <see cref="ICommandChanger.ChangeCommand(ICommand)"/> from being ran if set to true
        /// </summary>
        public bool StopChanger { get; set; }

        /// <summary>
        /// Marks that the command put into <see cref="ICommandChanger.ChangeCommand(ICommand)"/> shouldn't be put into the function
        /// </summary>
        public bool StopCommand { get; set; }

        /// <summary>
        /// If set, changes the command which is about to be added to the function
        /// </summary>
        public ICommand ChangeCommandTo { get; set; }
    }
}
