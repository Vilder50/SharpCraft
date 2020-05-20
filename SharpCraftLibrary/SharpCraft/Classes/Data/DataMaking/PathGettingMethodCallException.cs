using System;
using System.Runtime.Serialization;

namespace SharpCraft.Data
{
    /// <summary>
    /// Exception for when a datapath getting method was called directly.
    /// </summary>
    public class PathGettingMethodCallException : Exception
    {
        /// <summary>
        /// Intializes a new <see cref="PathGettingMethodCallException"/>
        /// </summary>
        public PathGettingMethodCallException() : base("This method is used for generating datapaths. Do not call this method. Use " + nameof(DataPathCreator) + " to use this method.")
        {
        }
    }
}