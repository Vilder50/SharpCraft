using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    /// <summary>
    /// Interface for loot tables
    /// </summary>
    public interface ILootTable
    {
        /// <summary>
        /// The file name of the loot table
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// The namespace the loot table is in
        /// </summary>
        BasePackNamespace PackNamespace { get; }

        /// <summary>
        /// Should return the string used for giving the loot table
        /// </summary>
        /// <returns>string for running the loot table</returns>
        string GetNamespacedName();
    }
}
