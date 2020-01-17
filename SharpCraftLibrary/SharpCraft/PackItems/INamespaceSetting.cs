using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    /// <summary>
    /// Interface for settings a <see cref="BasePackNamespace"/> can have
    /// </summary>
    public interface INamespaceSetting
    {
    }

    /// <summary>
    /// Holds settings <see cref="BasePackNamespace"/> can have
    /// </summary>
    public class NamespaceSettings
    {
        /// <summary>
        /// Setting used for making functions write a comment on where they were called from and how.
        /// </summary>
        /// <returns>The setting</returns>
        public INamespaceSetting WriteFunctionCalls()
        {
            return new WriteFunctionCalls();
        }

        /// <summary>
        /// Settting used for making file names only be numbers.
        /// </summary>
        /// <returns>The setting</returns>
        public INamespaceSetting ShortNames()
        {
            return new ShortNames();
        }

        /// <summary>
        /// Settting used for making the custom command group method make a function instead of using execute commands.
        /// </summary>
        /// <returns>The setting</returns>
        public INamespaceSetting FunctionGroupedCommands()
        {
            return new FunctionGroupedCommands();
        }
    }

    class WriteFunctionCalls : INamespaceSetting
    {

    }

    class ShortNames : INamespaceSetting
    {

    }

    class FunctionGroupedCommands : INamespaceSetting
    {

    }
}
