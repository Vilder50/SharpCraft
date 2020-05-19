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
        private static NamespaceSettings? singleton;

        /// <summary>
        /// Returns a list of namespace settings
        /// </summary>
        /// <returns>The list of settings</returns>
        public static NamespaceSettings GetSettings()
        {
            singleton ??= new NamespaceSettings();
            return singleton;
        }

        private NamespaceSettings()
        {

        }

        /// <summary>
        /// Use this setting to force files to get a generated name
        /// </summary>
        /// <returns>The setting</returns>
        public INamespaceSetting GenerateNames()
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

        /// <summary>
        /// Forces all files made by this namespace to be written when they are disposed instead of being auto files.
        /// </summary>
        /// <returns>The setting</returns>
        public INamespaceSetting ForceDisposeWriteFiles()
        {
            return new ForceDisposeWriteFiles();
        }
    }

    class ShortNames : INamespaceSetting
    {

    }

    class FunctionGroupedCommands : INamespaceSetting
    {

    }

    class ForceDisposeWriteFiles : INamespaceSetting
    {

    }
}
