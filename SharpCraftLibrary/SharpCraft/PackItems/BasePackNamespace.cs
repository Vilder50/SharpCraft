using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SharpCraft
{
    /// <summary>
    /// base class for pack namespaces
    /// </summary>
    public abstract class BasePackNamespace : IDisposable
    {
        /// <summary>
        /// List of settings namespaces can have
        /// </summary>
        public static readonly NamespaceSettings Settings = new NamespaceSettings();

        private string name = null!;
#pragma warning disable IDE0069
        private BaseDatapack datapack = null!;
#pragma warning restore IDE0069
        private BaseFile.FileListener fileListeners = null!;

        /// <summary>
        /// The files inside this namespace
        /// </summary>
        protected readonly List<BaseFile> files;
        /// <summary>
        /// The settings for this namespace
        /// </summary>
        protected readonly List<INamespaceSetting> settings;

        /// <summary>
        /// Intializes a new namespace. Make sure to call <see cref="Setup(BaseDatapack, string)"/> after using this
        /// </summary>
        public BasePackNamespace()
        {
            files = new List<BaseFile>();
            settings = new List<INamespaceSetting>();
        }

        /// <summary>
        /// Creates a new namespace in a datapack
        /// </summary>
        /// <param name="datapack">The datapack to add the namespace to</param>
        /// <param name="namespaceName">the name of the namespace</param>
        public BasePackNamespace(BaseDatapack datapack, string namespaceName) : this()
        {
            Setup(datapack, namespaceName);
        }

        /// <summary>
        /// Sets up the namespace
        /// </summary>
        /// <param name="datapack">The datapack to add the namespace to</param>
        /// <param name="namespaceName">the name of the namespace</param>
        public void Setup(BaseDatapack datapack, string namespaceName)
        {
            if (!IsSetup)
            {
                Name = namespaceName;
                Datapack = datapack;
                IsSetup = true;
                datapack.AddNamespace(this);
            }
            else
            {
                throw new InvalidOperationException("Setup has already been run.");
            }
        }

        /// <summary>
        /// Returns true if Setup has been run
        /// </summary>
        /// <returns>true if Setup has been run</returns>
        public bool IsSetup { get; private set; }

        /// <summary>
        /// The name of this namespace
        /// </summary>
        public string Name
        {
            get
            {
                if (!IsSetup)
                {
                    throw new InvalidOperationException("Setup hasn't been run yet.");
                }
                return name;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(nameof(Name) + " may not be null or empty");
                }
                if (!value.ToCharArray().All(c => char.IsLetter(c) || char.IsNumber(c) || c == '_'))
                {
                    throw new ArgumentException("namespace Name is invalid. Should only contain letters, numbers and _", nameof(Name));
                }

                name = value.ToLower();
            }
        }

        /// <summary>
        /// If the namespace has been disposed
        /// </summary>
        public bool Disposed { get; private set; }

        /// <summary>
        /// The datapack this namespace is a part of
        /// </summary>
        public BaseDatapack Datapack
        {
            get
            {
                if (!IsSetup)
                {
                    throw new InvalidOperationException("Setup hasn't been run yet.");
                }
                return datapack;
            }

            private set
            {
                if (value is null)
                {
                    throw new ArgumentException(nameof(Datapack) + " may not be null");
                }
                datapack = value;
            }
        }

        /// <summary>
        /// Returns the path to this namespace
        /// </summary>
        /// <returns>The path to this namespace</returns>
        public string GetPath()
        {
            if (!IsSetup)
            {
                throw new InvalidOperationException("Setup hasn't been run yet.");
            }
            return Datapack.GetDataPath() + Name + "\\";
        }

        /// <summary>
        /// Returns the file with the name and of the type. If it doesn't exist it returns null
        /// </summary>
        /// <param name="fileType">The type of file to get</param>
        /// <param name="fileName">The name of the file</param>
        /// <returns>The file with the name or null</returns>
        public BaseFile? GetFile(string fileType, string fileName)
        {
            string name = fileName.ToLower().Replace("/", "\\");

            if (!IsSetup)
            {
                throw new InvalidOperationException("Namespace setup hasn't been run yet.");
            }

            BaseFile file = files.SingleOrDefault(f => f.FileId == name && f.FileType == fileType);

            if (file is null)
            {
                return null;
            }

            if (file.Setting == BaseFile.WriteSetting.LockedAuto || file.Setting == BaseFile.WriteSetting.LockedOnDispose)
            {
                throw new InvalidOperationException($"Cannot get file \"{name}\" since it's locked.");
            }

            return file;
        }

        /// <summary>
        /// returns true if the given setting is on in this namespace
        /// </summary>
        /// <param name="setting">The setting to test for</param>
        /// <returns>True if the setting is on</returns>
        public bool IsSettingSet(INamespaceSetting setting)
        {
            if (!IsSetup)
            {
                throw new InvalidOperationException("Setup hasn't been run yet.");
            }
            return settings.Any(s => s.GetType() == setting.GetType());
        }

        /// <summary>
        /// Adds the given file to this namespace
        /// </summary>
        /// <param name="file">The file to add</param>
        public void AddFile(BaseFile file)
        {
            if (file is null)
            {
                throw new ArgumentNullException(nameof(file), "Cannot add null as a file to a namespace");
            }

            if (!IsSetup)
            {
                throw new InvalidOperationException("Setup hasn't been run yet.");
            }

            if (Disposed)
            {
                throw new InvalidOperationException("Cannot add files to a disposed namespace.");
            }

            if (file.PackNamespace != this)
            {
                throw new ArgumentException("Cannot add file which isn't made for this namespace", nameof(file));
            }

            if (files.Any(f => f == file))
            {
                return;
            }

            if (files.Any(f => f.FileId == file.FileId && f.FileType == file.FileType))
            {
                throw new ArgumentException("The namespace already contains a file with the given name (" + file.FileId + ")", nameof(file));
            }

            if (files.Any(f => f.WritePath == file.WritePath && f.FileType == file.FileType))
            {
                throw new ArgumentException("The namespace already contains a file which writes to the given path ("+file.WritePath+")", nameof(file));
            }

            fileListeners?.Invoke(file);
            files.Add(file);
        }

        /// <summary>
        /// Disposes all the files in this namespace
        /// </summary>
        public void Dispose()
        {
            if (!IsSetup)
            {
                throw new InvalidOperationException("Setup hasn't been run yet.");
            }
            if (!Disposed)
            {
                foreach(BaseFile file in files)
                {
                    file.Dispose();
                }
                AfterDispose();
                Disposed = true;
            }
        }

        /// <summary>
        /// Calls the given method when a new file is added to this namespace
        /// </summary>
        /// <param name="listener">The method to call</param>
        public void AddNewFileListener(BaseFile.FileListener listener)
        {
            fileListeners += listener ?? throw new ArgumentNullException(nameof(listener), "The given file listener may not be null");
        }

        /// <summary>
        /// Extra things to do after dispose is ran
        /// </summary>
        protected virtual void AfterDispose()
        {

        }

        /// <summary>
        /// Generates a random id for the given <see cref="object"/>
        /// </summary>
        /// <param name="getIdFor">The object to id</param>
        /// <returns>The id for the object</returns>
        public abstract string GetID(object getIdFor);
    }
}
