using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// base interface for commands
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>The command as a string</returns>
        string? GetCommandString();

        /// <summary>
        /// Returns a shallow clone of the command
        /// </summary>
        /// <returns>A shallow clone of the command</returns>
        BaseCommand ShallowClone();
    }

    /// <summary>
    /// base class for commands
    /// </summary>
    public abstract class BaseCommand : ICommand
    {
        /// <summary>
        /// Returns the command as a string
        /// </summary>
        /// <returns>The command as a string</returns>
        public abstract string? GetCommandString();

        /// <summary>
        /// Returns a shallow clone of this command
        /// </summary>
        /// <returns>A shallow clone of this command</returns>
        public virtual BaseCommand ShallowClone()
        {
            return (BaseCommand)MemberwiseClone();
        }
    }
}
