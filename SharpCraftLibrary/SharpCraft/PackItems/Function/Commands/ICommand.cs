using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Interface for commands
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>The command as a string</returns>
        string GetCommandString();
    }
}
