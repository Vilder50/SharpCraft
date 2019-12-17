using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    /// <summary>
    /// Interface for predicates
    /// </summary>
    public interface IPredicate
    {
        /// <summary>
        /// The file name of the predicate
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// The namespace the predicate is in
        /// </summary>
        BasePackNamespace PackNamespace { get; }

        /// <summary>
        /// Should return the string used for using the predicate
        /// </summary>
        /// <returns>string for using the predicate</returns>
        string GetNamespacedName();
    }
}
