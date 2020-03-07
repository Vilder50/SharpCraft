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
        #region DatapackListener
        private readonly static List<BaseDatapack> datapacks = new List<BaseDatapack>();
        private readonly static List<DatapackListener> datapackListeners = new List<DatapackListener>();

        /// <summary>
        /// Used for calling methods when a new datapack is added or when datapacks already has been added
        /// </summary>
        /// <param name="datapack">the added datapack</param>
        public delegate void DatapackListener(BaseDatapack datapack);

        /// <summary>
        /// Makes the given <see cref="DatapackListener"/> get called every time a new datapack is made and called with all existing datapacks
        /// </summary>
        /// <param name="listener">The listener to add</param>
        public static void AddDatapackListener(DatapackListener listener)
        {
            foreach(BaseDatapack datapack in datapacks)
            {
                listener(datapack);
            }
            datapackListeners.Add(listener);
        }
        #endregion

        #region BaseDatapack
        private const string pathPattern = @"^[\s\S]*[^\\/]{1}$";
        private const string namePattern = @"^[0-9a-zA-Z_]+$";

        private string name = null!;
        private string path = null!;
        private readonly List<BasePackNamespace> namespaces = null!;
        private BaseFile.FileListener fileListeners = null!;

        /// <summary>
        /// Creates a new <see cref="BaseDatapack"/> with the given parameters
        /// </summary>
        /// <param name="path">The path to the folder to create this datapack in</param>
        /// <param name="packName">The datapack's name</param>
        protected BaseDatapack(string path, string packName) : this(path, packName, new FileCreator())
        {
            
        }

        /// <summary>
        /// Creates a new <see cref="BaseDatapack"/> with the given parameters
        /// </summary>
        /// <param name="path">The path to the folder to create this datapack in</param>
        /// <param name="packName">The datapack's name</param>
        /// <param name="fileCreator">Class for creating files and directories</param>
        protected BaseDatapack(string path, string packName, IFileCreator fileCreator)
        {
            Path = path;
            Name = packName.ToLower();
            namespaces = new List<BasePackNamespace>();
            FileCreator = fileCreator;
        }

        /// <summary>
        /// Call when constructors are done
        /// </summary>
        protected virtual void FinishedConstructing()
        {
            datapacks.Add(this);
            foreach (DatapackListener listener in datapackListeners)
            {
                listener(this);
            }
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
                    throw new ArgumentException("Pack name is not valid.", nameof(Name));
                }
                name = value;
            }
        }

        /// <summary>
        /// The name of the datapack used for refering to the datapack in game
        /// </summary>
        public virtual string IngameName
        {
            get
            {
                return "\"file/" + Name + "\""; ;
            }
        }

        /// <summary>
        /// If the datapack has been disposed
        /// </summary>
        public bool Disposed { get; private set; }

        /// <summary>
        /// Used for getting text streams and create directories
        /// </summary>
        public IFileCreator FileCreator { get; private set; }

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
                space.AddNewFileListener(NewFileAdded);
                return space;
            }
        }

        private void NewFileAdded(BaseFile file)
        {
            fileListeners?.Invoke(file);
        }

        /// <summary>
        /// Calls the given method when a new file is added to a namespace in this datapack
        /// </summary>
        /// <param name="listener">The method to call</param>
        public void AddNewFileListener(BaseFile.FileListener listener)
        {
            fileListeners += listener ?? throw new ArgumentNullException(nameof(listener), "The given file listener may not be null");
        }

        /// <summary>
        /// Disposes the namespaces inside this pack
        /// </summary>
        public void Dispose()
        {
            if (!Disposed)
            {
                bool disposing = false;
                try
                {
                    foreach (BasePackNamespace packNamespace in namespaces)
                    {
                        disposing = true;
                        packNamespace.Dispose();
                        disposing = false;
                    }
                }
                catch (InvalidOperationException) when (!disposing)
                {
                    throw new InvalidOperationException("A new namespace was added while the datapack was trying to dispose. (Might be possible a WriteOnDispose file created a namespace (eg. needed and made the sharpcraft namespace))");
                }
                AfterDispose();
                datapacks.Remove(this);
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
        #endregion
    }
}
