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
    public interface ICommandChanger : ICommand
    {
        /// <summary>
        /// Changes the given command
        /// </summary>
        /// <param name="command">The command to change</param>
        /// <returns>The changed command. Returning null will mean the command won't be changed anymore and also wont be added to the function</returns>
        ICommand? ChangeCommand(ICommand command);

        /// <summary>
        /// Bool marking if the <see cref="ICommandChanger"/> is done chaning commands
        /// </summary>
        bool DoneChanging { get; }
    }
}
