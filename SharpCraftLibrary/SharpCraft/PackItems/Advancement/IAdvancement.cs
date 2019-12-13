using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    /// <summary>
    /// Interface for advancements
    /// </summary>
    public interface IAdvancement
    {
        /// <summary>
        /// The file name of the advancement
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// The namespace the advancement is in
        /// </summary>
        BasePackNamespace PackNamespace { get; }

        /// <summary>
        /// Should return the string used for evoking the advancement
        /// </summary>
        /// <returns>string for running the advancement</returns>
        string GetNamespacedName();
    }
}
