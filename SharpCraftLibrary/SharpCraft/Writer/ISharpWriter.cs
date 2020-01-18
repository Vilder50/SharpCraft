using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Writer
{
    /// <summary>
    /// Interface for classes with a method which should run using <see cref="SharpWriter"/> 
    /// </summary>
    public interface ISharpWriter
    {
        /// <summary>
        /// The index of this writer. Low indexed ISharpWriter will be called before high indexed ones
        /// </summary>
        int Index { get; }
    }

    /// <summary>
    /// Interface for classes with a method which should run using <see cref="SharpWriter"/> 
    /// </summary>
    public interface ISharpWriterNormal : ISharpWriter
    {
        /// <summary>
        /// Method to run using <see cref="SharpWriter.RunNormalWriters{TWriter}"/>
        /// </summary>
        void Write();
    }

    /// <summary>
    /// Interface for classes with a method which should run using <see cref="SharpWriter"/> 
    /// </summary>
    public interface ISharpWriterNamespace : ISharpWriter
    {
        /// <summary>
        /// Method to run using <see cref="SharpWriter.RunNormalWriters{TWriter}"/>
        /// </summary>
        void Write();

        /// <summary>
        /// The namespace to write to
        /// </summary>
        PackNamespace Namespace { get; set; }

        /// <summary>
        /// The name of the namespace this writer will get when called
        /// </summary>
        string NamespaceName { get; }
    }
}
