using System;
using System.Collections.Generic;
using System.Text;

namespace SharpCraft.DimensionObjects
{
    /// <summary>
    /// Interface for dimension types
    /// </summary>
    public interface IDimensionType
    {
        /// <summary>
        /// Returns the string used for chosen the dimension type
        /// </summary>
        /// <returns>String for chosing the type</returns>
        public string GetDimensionTypeString();
    }

    /// <summary>
    /// Interface for dimension types
    /// </summary>
    public interface IDimensionTypeFile : IDimensionType
    {
        /// <summary>
        /// The file name of the dimension type
        /// </summary>
        string FileId { get; }

        /// <summary>
        /// The namespace the dimension type is in
        /// </summary>
        BasePackNamespace PackNamespace { get; }

        /// <summary>
        /// Should return the string used for using the dimension type
        /// </summary>
        /// <returns>string for using the dimension type</returns>
        string GetNamespacedName();
    }
}
