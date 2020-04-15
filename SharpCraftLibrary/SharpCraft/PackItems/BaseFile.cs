using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace SharpCraft
{
    /// <summary>
    /// The base class used for files in a datapack
    /// </summary>
    public abstract class BaseFile : IDisposable
    {
        private const string FileLocationPattern = @"^(([a-zA-Z0-9_-]+)([\\/]{1}[a-zA-Z0-9_-])*)*$";

        /// <summary>
        /// Used for running things on new files and soon to be disposed files
        /// </summary>
        /// <param name="file">the file</param>
        public delegate void FileListener(BaseFile file);

        /// <summary>
        /// Settings for accessing this file
        /// </summary>
        public enum WriteSetting
        {
            /// <summary>
            /// Writes the file automatically as the file gets new things to write. It's not possible to change things already written to the file.
            /// </summary>
            Auto,

            /// <summary>
            /// Writes the file when gets disposed. Changes can be made to the file since is it's first get written when it isn't in use.
            /// </summary>
            OnDispose,

            /// <summary>
            /// Writes the file automatically as the file gets new things to write. It's not possible to change things already written to the file. The namespace the file is in will cast an exception if another source tries to access a file at the same location as this file.
            /// </summary>
            LockedAuto,

            /// <summary>
            /// Writes the file when gets disposed. Changes can be made to the file since is it's first get written when it isn't in use. The namespace the file is in will cast an exception if another source tries to access a file at the same location as this file.
            /// </summary>
            LockedOnDispose,
        }

#pragma warning disable IDE0069
        BasePackNamespace packNamespace = null!;
#pragma warning restore IDE0069
        string fileId = null!;
        string writePath = null!;
        private string fileType = null!;

        /// <summary>
        /// Listeners to call when the file gets disposed
        /// </summary>
        protected FileListener? disposeListener;

        /// <summary>
        /// Intializes a new <see cref="BaseFile"/> with the given values
        /// </summary>
        /// <param name="packNamespace">The namespace this file is for</param>
        /// <param name="fileName">The name of the file</param>
        /// <param name="writeSetting">The setting for the file</param>
        /// <param name="fileType">The type of file</param>
        protected BaseFile(BasePackNamespace packNamespace, string? fileName, WriteSetting writeSetting, string fileType)
        {
            PackNamespace = packNamespace;
            Setting = writeSetting;
            FileType = fileType;

            string useName = fileName!;
            if (string.IsNullOrWhiteSpace(useName))
            {
                useName = PackNamespace.GetID(this);
            }

            FileId = useName;
            if (PackNamespace.IsSettingSet(new NamespaceSettings().GenerateNames()) && useName == fileName)
            {
                WritePath = PackNamespace.GetID(this);
            }
            else
            {
                WritePath = useName;
            }
        }

        /// <summary>
        /// Call when constructors are done
        /// </summary>
        protected virtual void FinishedConstructing()
        {
            PackNamespace.AddFile(this);
            if (IsAuto())
            {
                StreamWriter = GetStream();
                WriteFile(StreamWriter);
                Dispose();
            }
        }

        /// <summary>
        /// The namespace this file is for
        /// </summary>
        public BasePackNamespace PackNamespace
        {
            get => packNamespace;
            private set => packNamespace = value ?? throw new ArgumentNullException(nameof(PackNamespace), "Packnamespace may not be null");
        }

        /// <summary>
        /// The name of this file
        /// </summary>
        public string FileId
        {
            get => fileId;
            private set
            {
                //validate file
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(FileId), "FileId may not be null");
                }

                //fix name and validate
                string fixedName = value.ToLower().Replace("/", "\\");

                if (!ValidateFileName(fixedName))
                {
                    throw new ArgumentException("FileId is an invalid file name. Make sure it only contains letters, numbers - and / or \\", nameof(fileId));
                }

                fileId = fixedName;
            }
        }

        /// <summary>
        /// The place the file will be written to
        /// </summary>
        public string WritePath
        {
            get => writePath;
            private set
            {
                //validate file
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(WritePath), "WritePath may not be null");
                }

                //fix name and validate
                string fixedName = value.ToLower().Replace("/", "\\");

                if (!ValidateFileName(fixedName))
                {
                    throw new ArgumentException("WritePath is an invalid file path and name. Make sure it only contains letters, numbers - and / or \\", nameof(WritePath));
                }

                writePath = fixedName;
            }
        }

        /// <summary>
        /// The type of file
        /// </summary>
        public string FileType { get => fileType; private set => fileType = value ?? throw new ArgumentNullException(nameof(FileType), "FileType may not be null"); }

        /// <summary>
        /// The setting used for this file
        /// </summary>
        public WriteSetting Setting { get; }

        /// <summary>
        /// Returns true if <see cref="Setting"/> is either <see cref="WriteSetting.Auto"/> or <see cref="WriteSetting.LockedAuto"/>
        /// </summary>
        /// <returns>True if the file setting is auto</returns>
        public bool IsAuto()
        {
            return Setting == WriteSetting.Auto || Setting == WriteSetting.LockedAuto;
        }

        /// <summary>
        /// If the file has been disposed
        /// </summary>
        public bool Disposed { get; protected set; }

        /// <summary>
        /// The stream writer used for writing the file. Is null if the writeSetting isn't Auto. Might be null if it is auto
        /// </summary>
        protected TextWriter? StreamWriter { get; set; }

        /// <summary>
        /// States if the file has been written and shouldn't be able to be written again.
        /// </summary>
        protected bool FileIsWritten { get; set; }

        /// <summary>
        /// Validates the given file name
        /// </summary>
        /// <param name="name">The name for the file</param>
        /// <returns>True if the name is valid</returns>
        public static bool ValidateFileName(string name)
        {
            if (name is null)
            {
                return false;
            }
            return Regex.IsMatch(name, FileLocationPattern);
        }

        /// <summary>
        /// Returns the namespaced name of this file
        /// </summary>
        /// <returns>The namespaced name of this file</returns>
        public string GetNamespacedName()
        {
            return PackNamespace.Name + ":" + WritePath.Replace("\\", "/");
        }

        /// <summary>
        /// Returns the stream this file is going to use for writing it's file
        /// </summary>
        /// <returns>The stream for this file</returns>
        protected abstract TextWriter GetStream();

        /// <summary>
        /// Disposes this file. If the write setting is OnDispose it will write the file
        /// </summary>
        public virtual void Dispose()
        {
            if (!Disposed)
            {
                disposeListener?.Invoke(this);
                if (IsAuto())
                {
                    StreamWriter?.Dispose();
                }
                else
                {
                    using TextWriter writer = GetStream();
                    WriteFile(writer);
                }
                AfterDispose();
                Disposed = true;
            }
        }

        /// <summary>
        /// Extra things to do after dispose was ran. (Clear the file for none needed things)
        /// </summary>
        protected virtual void AfterDispose()
        {

        }

        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected abstract void WriteFile(TextWriter stream);

        /// <summary>
        /// Finalizer which makes sure dispose was ran
        /// </summary>
        ~BaseFile()
        {
            if (!Disposed && (Setting == WriteSetting.OnDispose || Setting == WriteSetting.LockedOnDispose))
            {
                throw new Exception("File was never disposed and never written");
            }
        }

        /// <summary>
        /// Checks if this file can be changed
        /// </summary>
        /// <returns>True if it can be changed</returns>
        public bool CanDoChanges()
        {
            return !(Setting == WriteSetting.Auto || Setting == WriteSetting.LockedAuto || Disposed);
        }

        /// <summary>
        /// Adds a listener to this file which will be called right before the file is disposed
        /// </summary>
        /// <param name="listener">The listener to add</param>
        public void AddDisposeListener(FileListener listener)
        {
            disposeListener += listener ?? throw new ArgumentNullException(nameof(listener), "File dispose listener may not be null.");
        }

        /// <summary>
        /// Throws an exception if the file isn't allowed to be changed
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        protected void ThrowExceptionOnInvalidChange()
        {
            if (!CanDoChanges())
            {
                throw new InvalidOperationException("Cannot do the following change since the file doesn't allow it.");
            }
        }

        /// <summary>
        /// Creates the directory for the given file
        /// </summary>
        /// <param name="folderPath">The base folder the file should be in</param>
        protected void CreateDirectory(string folderPath)
        {
            if (WritePath.Contains("\\"))
            {
                PackNamespace.Datapack.FileCreator.CreateDirectory(PackNamespace.GetPath() + folderPath + "\\" + WritePath.Substring(0, WritePath.LastIndexOf("\\")) + "\\");
            }
            else
            {
                PackNamespace.Datapack.FileCreator.CreateDirectory(PackNamespace.GetPath() + folderPath + "\\");
            }
        }
    }
}
