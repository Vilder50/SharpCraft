using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Command used for stopping an execute chain
    /// </summary>
    public class StopExecuteCommand : BaseCommand
    {
        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>NULL</returns>
        public override string? GetCommandString()
        {
            return null;
        }
    }
}
