using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

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
        List<(string path, IDisposable writer)> GetWriters();

        /// <summary>
        /// Returns a list of all the created directories
        /// </summary>
        /// <returns>A list of all the created directories</returns>
        List<string> GetDirectories();

        /// <summary>
        /// Creates a new binary writer and returns it
        /// </summary>
        /// <param name="path">The path of the file to write to</param>
        /// <param name="compress">If the data should be compressed</param>
        /// <returns>The writer</returns>
        BinaryWriter CreateBinaryWriter(string path, bool compress);
    }

    /// <summary>
    /// Class for normal file creation
    /// </summary>
    public class FileCreator : IFileCreator
    {
        private readonly List<string> directories;
        private readonly List<(string path, IDisposable writer)> writers;

        /// <summary>
        /// Intializes a new <see cref="FileCreator"/>
        /// </summary>
        public FileCreator()
        {
            directories = new List<string>();
            writers = new List<(string path, IDisposable writer)>();
        }

        /// <summary>
        /// Creates a new stream writer and returns it
        /// </summary>
        /// <param name="path">The path to write at</param>
        /// <returns>Stream writer for writing at the path</returns>
        public TextWriter CreateWriter(string path)
        {
            TextWriter writer = new StreamWriter(new FileStream(path, FileMode.Create)) { AutoFlush = true };
            writers.Add((path, writer));
            return writer;
        }

        /// <summary>
        /// Creates a new binary writer and returns it
        /// </summary>
        /// <param name="path">The path of the file to write to</param>
        /// <param name="compress">If the data should be compressed</param>
        /// <returns>The writer</returns>
        public BinaryWriter CreateBinaryWriter(string path, bool compress)
        {
            FileStream fileStream = new FileStream(path, FileMode.Create);
            GZipStream compressStream = new GZipStream(fileStream, compress ? CompressionLevel.Optimal : CompressionLevel.NoCompression);
            BinaryWriter writeStream = new BinaryWriter(compressStream);

            writers.Add((path, writeStream));
            return writeStream;
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
        public List<(string path, IDisposable writer)> GetWriters()
        {
            return new List<(string path, IDisposable writer)>(writers);
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
        private readonly List<(string path, IDisposable writer)> writers;

        /// <summary>
        /// Intializes a new <see cref="NoneFileCreator"/>
        /// </summary>
        public NoneFileCreator()
        {
            directories = new List<string>();
            writers = new List<(string path, IDisposable writer)>();
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
        /// Creates a new binary writer and returns it
        /// </summary>
        /// <param name="path">The path of the file to write to</param>
        /// <param name="compress">If the data should be compressed</param>
        /// <returns>The writer</returns>
        public BinaryWriter CreateBinaryWriter(string path, bool compress)
        {
            MemoryStream stream = new MemoryStream();
            writers.Add((path, stream));
            return new BinaryWriter(stream);
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
        public List<(string path, IDisposable writer)> GetWriters()
        {
            return new List<(string path, IDisposable writer)>(writers);
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
