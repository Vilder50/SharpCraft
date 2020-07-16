using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft
{
    /// <summary>
    /// Interface for structures
    /// </summary>
    public interface IStructure : Data.IConvertableToDataTag
    {
        /// <summary>
        /// The file name of the structure
        /// </summary>
        string FileId { get; }

        /// <summary>
        /// The namespace the structure is in
        /// </summary>
        BasePackNamespace PackNamespace { get; }

        /// <summary>
        /// Should return the string used for using the structure
        /// </summary>
        /// <returns>string for using the structure</returns>
        string GetNamespacedName();
    }
}
