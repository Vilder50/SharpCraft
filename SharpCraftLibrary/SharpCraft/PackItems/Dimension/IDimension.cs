using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.DimensionObjects
{
    /// <summary>
    /// Interface for dimensions
    /// </summary>
    public interface IDimension : Data.IConvertableToDataTag
    {
        /// <summary>
        /// The file name of the dimension
        /// </summary>
        string FileId { get; }

        /// <summary>
        /// The namespace the dimension is in
        /// </summary>
        BasePackNamespace PackNamespace { get; }

        /// <summary>
        /// Should return the string used for using the dimension
        /// </summary>
        /// <returns>string for using the dimension</returns>
        string GetNamespacedName();
    }
}
