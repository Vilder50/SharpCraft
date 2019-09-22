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
        /// Settings for accessing this file
        /// </summary>
        public enum WriteSetting {
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

        BasePackNamespace packNamespace;
        string fileName;

        /// <summary>
        /// Intializes a new <see cref="BaseFile"/> with the given values
        /// </summary>
        /// <param name="packNamespace">The namespace this file is for</param>
        /// <param name="fileName">The name of the file</param>
        /// <param name="writeSetting">The setting for the file</param>
        public BaseFile(BasePackNamespace packNamespace, string fileName, WriteSetting writeSetting)
        {
            PackNamespace = packNamespace;
            FileName = fileName;
            Setting = writeSetting;

            PackNamespace.AddFile(this);
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
        public string FileName
        {
            get => fileName;
            set
            {
                //don't allow changing name if file is in auto mode
                if (!(fileName is null) && Setting == WriteSetting.Auto)
                {
                    throw new ArgumentException("Cannot change the file name when the file write mode is Auto", nameof(FileName));
                }

                //validate file
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(FileName), "FileName may not be null");
                }

                //fix name
                string fixedName = value.ToLower().Replace("/","\\");

                if (!ValidateFileName(fixedName))
                {
                    throw new ArgumentException("Name is an invalid file name. Make sure it only contains letters, numbers - and / or \\", nameof(FileName));
                }

                fileName = fixedName;
            }
        }

        /// <summary>
        /// The setting used for this file
        /// </summary>
        public WriteSetting Setting { get; }

        /// <summary>
        /// If the file has been disposed
        /// </summary>
        public bool Disposed { get; private set; }

        /// <summary>
        /// The stream writer used for writing the file. Is null if the writeSetting isn't Auto
        /// </summary>
        protected TextWriter StreamWriter { get; set; }

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
            return PackNamespace.Name + ":" + FileName.Replace("\\","/");
        }

        /// <summary>
        /// Returns the stream this file is going to use for writing it's file
        /// </summary>
        /// <returns>The stream for this file</returns>
        protected virtual TextWriter GetStream()
        {
            return new StreamWriter(PackNamespace.GetPath() + FileName);
        }

        /// <summary>
        /// Disposes this file. If the write setting is OnDispose it will write the file
        /// </summary>
        public void Dispose()
        {
            if (!Disposed)
            {
                if (Setting == WriteSetting.Auto || Setting == WriteSetting.LockedAuto)
                {
                    StreamWriter?.Dispose();
                }
                else
                {
                    using (TextWriter writer = GetStream())
                    {
                        WriteFile(writer);
                    }
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
    }
}
