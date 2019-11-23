using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SharpCraft
{
    /// <summary>
    /// Interface for file creators
    /// </summary>
    public interface IFileCreator 
    {
        /// <summary>
        /// Creates a new text writer and returns it
        /// </summary>
        /// <param name="path">The path to write at</param>
        /// <returns>Text writer for writing at the path</returns>
        TextWriter CreateWriter(string path);

        /// <summary>
        /// Creates a directory with the given path
        /// </summary>
        /// <param name="directory">The path of the directory</param>
        void CreateDirectory(string directory);

        /// <summary>
        /// Returns a list of all created text writers
        /// </summary>
        /// <returns>A list of all created text writers</returns>
        List<(string path, TextWriter writer)> GetWriters();

        /// <summary>
        /// Returns a list of all the created directories
        /// </summary>
        /// <returns>A list of all the created directories</returns>
        List<string> GetDirectories();
    }

    /// <summary>
    /// Class for normal file creation
    /// </summary>
    public class FileCreator : IFileCreator
    {
        private readonly List<string> directories;
        private readonly List<(string path, TextWriter writer)> writers;

        /// <summary>
        /// Intializes a new <see cref="FileCreator"/>
        /// </summary>
        public FileCreator()
        {
            directories = new List<string>();
            writers = new List<(string path, TextWriter writer)>();
        }

        /// <summary>
        /// Creates a new stream writer and returns it
        /// </summary>
        /// <param name="path">The path to write at</param>
        /// <returns>Stream writer for writing at the path</returns>
        public TextWriter CreateWriter(string path)
        {
            TextWriter writer = new StreamWriter(new FileStream(path, FileMode.Create)) { AutoFlush = true }; ;
            writers.Add((path, writer));
            return writer;
        }

        /// <summary>
        /// Creates a directory with the given path
        /// </summary>
        /// <param name="directory">The path of the directory</param>
        public void CreateDirectory(string directory)
        {
            if (!directories.Any(d => d.ToLower() == directory.ToLower()))
            {
                directories.Add(directory);
            }
            Directory.CreateDirectory(directory);
        }

        /// <summary>
        /// Returns a list of all created stream writers
        /// </summary>
        /// <returns>A list of all created stream writers</returns>
        public List<(string path, TextWriter writer)> GetWriters()
        {
            return new List<(string path, TextWriter writer)>(writers);
        }

        /// <summary>
        /// Returns a list of all the created directories
        /// </summary>
        /// <returns>A list of all the created directories</returns>
        public List<string> GetDirectories()
        {
            return new List<string>(directories);
        }
    }

    /// <summary>
    /// Class for simulating file creation
    /// </summary>
    public class NoneFileCreator : IFileCreator
    {
        private readonly List<string> directories;
        private readonly List<(string path, TextWriter writer)> writers;

        /// <summary>
        /// Intializes a new <see cref="NoneFileCreator"/>
        /// </summary>
        public NoneFileCreator()
        {
            directories = new List<string>();
            writers = new List<(string path, TextWriter writer)>();
        }

        /// <summary>
        /// Creates a new text writer and returns it
        /// </summary>
        /// <param name="path">The path to write at</param>
        /// <returns>text writer for writing at the path</returns>
        public TextWriter CreateWriter(string path)
        {
            TextWriter writer = new StringWriter();
            writers.Add((path, writer));
            return writer;
        }

        /// <summary>
        /// Creates a directory with the given path
        /// </summary>
        /// <param name="directory">The path of the directory</param>
        public void CreateDirectory(string directory)
        {
            if (!directories.Any(d => d.ToLower() == directory.ToLower()))
            {
                directories.Add(directory);
            }
        }

        /// <summary>
        /// Returns a list of all created text writers
        /// </summary>
        /// <returns>A list of all created text writers</returns>
        public List<(string path, TextWriter writer)> GetWriters()
        {
            return new List<(string path, TextWriter writer)>(writers);
        }

        /// <summary>
        /// Returns a list of all the created directories
        /// </summary>
        /// <returns>A list of all the created directories</returns>
        public List<string> GetDirectories()
        {
            return new List<string>(directories);
        }
    }
}
