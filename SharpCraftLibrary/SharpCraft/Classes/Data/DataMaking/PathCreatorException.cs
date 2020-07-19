using System;
using System.Runtime.Serialization;

namespace SharpCraft.Data
{
    /// <summary>
    /// Exception for when <see cref="DataPathCreator{T}"/> fails to create a path
    /// </summary>
    public class PathCreatorException : Exception
    {
        /// <summary>
        /// Intalizes a new <see cref="PathCreatorException"/>
        /// </summary>
        public PathCreatorException()
        {
        }

        /// <summary>
        /// Intalizes a new <see cref="PathCreatorException"/>
        /// </summary>
        /// <param name="message">The exception message</param>
        public PathCreatorException(string? message) : base(message)
        {
        }

        /// <summary>
        /// Intalizes a new <see cref="PathCreatorException"/>
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        public PathCreatorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}