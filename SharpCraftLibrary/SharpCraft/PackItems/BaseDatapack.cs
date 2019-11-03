using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SharpCraft
{
    /// <summary>
    /// base class for datapacks
    /// </summary>
    public abstract class BaseDatapack : IDisposable
    {
        private const string pathPattern = @"^[\s\S]+[^\\/]{1}$";
        private const string namePattern = @"^[0-9a-zA-Z_]+$";

        private string name;
        private string path;
        private readonly List<BasePackNamespace> namespaces;

        /// <summary>
        /// Creates a new <see cref="BaseDatapack"/> with the given parameters
        /// </summary>
        /// <param name="path">The path to the folder to create this datapack in</param>
        /// <param name="packName">The datapack's name</param>
        public BaseDatapack(string path, string packName)
        {
            Path = path;
            Name = packName.ToLower();
            namespaces = new List<BasePackNamespace>();
        }

        /// <summary>
        /// The path to the folder to create this datapack in
        /// </summary>
        public string Path
        {
            get => path;
            private set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(Path), "Path may not be null");
                }
                if (!ValidatePath(value))
                {
                    throw new ArgumentException("Path is not valid (Make sure it doesn't end with \\ or /)", nameof(Path));
                }
                path = value.Replace("/","\\");
            }
        }

        /// <summary>
        /// The name of the datapack
        /// </summary>
        public string Name
        {
            get => name;
            private set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(Name), "Path may not be null");
                }
                if (!ValidateName(value))
                {
                    throw new ArgumentException("Name is not valid.", nameof(Name));
                }
                name = value;
            }
        }

        /// <summary>
        /// If the datapack has been disposed
        /// </summary>
        public bool Disposed { get; private set; }

        /// <summary>
        /// Gets the path to the data folder in the datapack
        /// </summary>
        /// <returns>the path to the data folder in the datapack</returns>
        public virtual string GetDataPath()
        {
            return Path + "\\" + Name + "\\data\\";
        }

        /// <summary>
        /// Validates the given path
        /// </summary>
        /// <param name="path">The path to validate</param>
        /// <returns>True if the path is valid</returns>
        public static bool ValidatePath(string path)
        {
            return Regex.IsMatch(path, pathPattern);
        }

        /// <summary>
        /// Validates the given datapack name
        /// </summary>
        /// <param name="name">The name to validate</param>
        /// <returns>True if the name is valid</returns>
        public static bool ValidateName(string name)
        {
            return Regex.IsMatch(name, namePattern);
        }

        /// <summary>
        /// Outputs a namespace for this datapack
        /// </summary>
        /// <typeparam name="TNamespace">The type of namespace</typeparam>
        /// <param name="name">The namespace to get</param>
        /// <returns>A namespace</returns>
        public TNamespace Namespace<TNamespace>(string name) where TNamespace : BasePackNamespace, new()
        {
            if (Disposed)
            {
                throw new InvalidOperationException("Cannot get namespaces for a disposed datapack");
            }

            name = name.ToLower();
            TNamespace returnSpace = (TNamespace)namespaces.SingleOrDefault(n => n.Name == name);
            if (!(returnSpace is null))
            {
                return returnSpace;
            }
            else
            {
                TNamespace space = new TNamespace();
                space.Setup(this, name);
                return space;
            }
        }

        /// <summary>
        /// Disposes the namespaces inside this pack
        /// </summary>
        public void Dispose()
        {
            if (!Disposed)
            {
                foreach (BasePackNamespace packNamespace in namespaces)
                {
                    packNamespace.Dispose();
                }
                AfterDispose();
                Disposed = true;
            }
        }

        /// <summary>
        /// Extra things to do after dispose is ran
        /// </summary>
        protected virtual void AfterDispose()
        {

        }

        internal void AddNamespace(BasePackNamespace space)
        {
            if (namespaces.Any(n => n.Name == space.Name))
            {
                throw new ArgumentException("Cannot add a namespace to this datapack with the same name as another namespace in this pack");
            }

            namespaces.Add(space);
        }
    }
}
