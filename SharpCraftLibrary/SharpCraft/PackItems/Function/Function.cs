using System.IO;
using System;

namespace SharpCraft
{
    /// <summary>
    /// A object used to create <see cref="Function"/>s
    /// </summary>
    public class Function : BaseFile, IFunction
    {
        /// <summary>
        /// Used to add commands to the given function
        /// </summary>
        /// <param name="function">the function to give the commands to</param>
        public delegate void FunctionCreater(Function function);

        /// <summary>
        /// A object used to write function files
        /// </summary>
        public class FunctionWriter
        {
            /// <summary>
            /// The streamwriter used to write the commands
            /// </summary>
            public StreamWriter LineWriter;

            /// <summary>
            /// the command line being written right now.
            /// execute commands are written onto here before the command to execute has been specified.
            /// </summary>
            public string TempCommand;

            /// <summary>
            /// If the tempcommand is an execute command
            /// (If this is false, the next time a execute command is added to the command being written right now, it will add "execute" to the command)
            /// (If this is true "run" will be added to the command the next time a none execute command is added)
            /// </summary>
            public bool HasExecute;

            /// <summary>
            /// The line being written to right now
            /// </summary>
            public int LineNumber;

            /// <summary>
            /// The name of the function this <see cref="FunctionWriter"/> is in
            /// </summary>
            public string FunctionName;

            /// <summary>
            /// The namespace of the function this <see cref="FunctionWriter"/> is in
            /// </summary>
            public string NameSpaceName;

            /// <summary>
            /// Adds the specifed command to the <see cref="TempCommand"/>
            /// </summary>
            /// <param name="Command">The command to add</param>
            /// <param name="Execute">If the command is an execute command or not</param>
            public void Add(string Command, bool Execute = false)
            {
                if (Execute)
                {
                    if (!HasExecute) { TempCommand += "execute "; }
                    HasExecute = true;
                }
                else
                {
                    if (HasExecute) { TempCommand += "run "; }
                    HasExecute = false;
                }
                TempCommand += Command;
            }

            /// <summary>
            /// Writes the <see cref="TempCommand"/> to the function file and gets ready for a new command
            /// </summary>
            public void NewLine()
            {
                HasExecute = false;

                LineWriter.WriteLine(TempCommand);
                LineNumber++;
                TempCommand = "";
            }


            private bool CopiedHasExecute;
            private string CopiedTempCommand;

            /// <summary>
            /// Copies the <see cref="FunctionWriter"/>'s <see cref="TempCommand"/> and <see cref="HasExecute"/>
            /// (This overwrites the last copied state)
            /// </summary>
            public void CopyState()
            {
                CopiedHasExecute = HasExecute;
                CopiedTempCommand = TempCommand;
            }

            /// <summary>
            /// Pastes the state copied from <see cref="CopyState"/>
            /// Note this overwrites the <see cref="FunctionWriter"/>'s <see cref="TempCommand"/> and <see cref="HasExecute"/> with the copied things
            /// </summary>
            public void PasteState()
            {
                HasExecute = CopiedHasExecute;
                TempCommand = CopiedTempCommand;
            }
        }

        /// <summary>
        /// The thing used to write this function
        /// </summary>
        public FunctionWriter Writer = new FunctionWriter();

        /// <summary>
        /// The parent function which made this function using <see cref="NewChild(string)"/> or <see cref="NewSibling(string)"/>
        /// </summary>
        public Function Parent;

        /// <summary>
        /// Intializes a new <see cref="Function"/> with the given values
        /// </summary>
        /// <param name="space">The namespace the function is in</param>
        /// <param name="fileName">The name of the function</param>
        public Function(BasePackNamespace space, string fileName) : base(space, fileName, WriteSetting.Auto)
        {
            if (FileName.Contains("\\"))
            {
                Directory.CreateDirectory(PackNamespace.GetPath() + "functions\\" + FileName.Substring(0, FileName.LastIndexOf("\\")));
            }
            else
            {
                Directory.CreateDirectory(PackNamespace.GetPath() + "functions\\");
            }
            Writer.LineWriter = (StreamWriter)GetStream();
            Writer.NameSpaceName = PackNamespace.Name;
            Writer.FunctionName = ToString();

            Block = new FunctionWriters.BlockCommands(Writer);
            Entity = new FunctionWriters.EntityCommands(Writer);
            Execute = new FunctionWriters.ExecuteCommands(Writer);
            World = new FunctionWriters.WorldCommands(Writer, this);
            Player = new FunctionWriters.PlayerCommands(Writer);
            Custom = new FunctionWriters.CustomCommands(this);
        }

        /// <summary>
        /// Returns <see cref="BaseFile.GetNamespacedName()"/>
        /// </summary>
        public string Name
        {
            get => GetNamespacedName();
        }

        /// <summary>
        /// Returns the streamwriter to use
        /// </summary>
        /// <returns>the streamwriter to use</returns>
        protected override TextWriter GetStream()
        {
            StreamWriter = new StreamWriter(new FileStream(PackNamespace.GetPath() + "functions\\" + FileName + ".mcfunction", FileMode.Create)) { AutoFlush = true };
            return StreamWriter;
        }

        /// <summary>
        /// Returns the namespace path of this <see cref="Function"/>
        /// </summary>
        /// <returns>this <see cref="Function"/>'s name</returns>
        public override string ToString()
        {
            return GetNamespacedName();
        }

        /// <summary>
        /// Commands run on blocks
        /// </summary>
        public FunctionWriters.BlockCommands Block;

        /// <summary>
        /// Commands run on entities
        /// </summary>
        public FunctionWriters.EntityCommands Entity;

        /// <summary>
        /// Execute commands
        /// </summary>
        public FunctionWriters.ExecuteCommands Execute;

        /// <summary>
        /// Commands run on players
        /// </summary>
        public FunctionWriters.PlayerCommands Player;

        /// <summary>
        /// Commands run on the world
        /// </summary>
        public FunctionWriters.WorldCommands World;

        /// <summary>
        /// Custom commands to make life easier
        /// </summary>
        public FunctionWriters.CustomCommands Custom;

        /// <summary>
        /// Creates a folder with this function's name and creates a new <see cref="Function"/> inside of it with the specified name
        /// </summary>
        /// <param name="name">The name of the new <see cref="Function"/></param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewChild(string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                if (PackNamespace is PackNamespace space)
                {
                    name = space.NextFileID.ToString();
                }
                else
                {
                    throw new InvalidOperationException("Cannot create function without a name without the namespace being a PackNamespace");
                }
            }
            return new Function(PackNamespace, FileName + "\\" + name.ToLower().Replace("/", "\\")) {Parent = this };
        }
        /// <summary>
        /// Creates a folder with this function's name and creates a new <see cref="Function"/> inside of it with the specified name and commands
        /// </summary>
        /// <param name="name">The name of the new <see cref="Function"/></param>
        /// <param name="creater">a method creating the new <see cref="Function"/></param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewChild(string name, FunctionCreater creater)
        {
            Function function = NewChild(name);
            creater(function);
            return function;
        }
        /// <summary>
        /// Creates a folder with this function's name and creates a new <see cref="Function"/> inside of it with the specified commands
        /// </summary>
        /// <param name="creater">a method creating the new <see cref="Function"/></param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewChild(FunctionCreater creater)
        {
            Function function = NewChild();
            creater(function);
            return function;
        }

        /// <summary>
        /// Creates a new <see cref="Function"/> with the specified name in the same folder as this function
        /// </summary>
        /// <param name="name">The name of the new <see cref="Function"/></param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewSibling(string name = null)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                if (PackNamespace is PackNamespace space)
                {
                    name = space.NextFileID.ToString();
                }
                else
                {
                    throw new InvalidOperationException("Cannot create function without a name without the namespace being a PackNamespace");
                }
            }
            if (FileName.Contains("\\"))
            {
                return new Function(PackNamespace, FileName.Substring(0, FileName.LastIndexOf("\\") + 1) + name.ToLower()) { Parent = this };
            }
            else
            {
                return new Function(PackNamespace, name.ToLower()) { Parent = this };
            }
        }
        /// <summary>
        /// Creates a new <see cref="Function"/> with the specified name and commands in the same folder as this function
        /// </summary>
        /// <param name="name">The name of the new <see cref="Function"/></param>
        /// <param name="creater">a method creating the new <see cref="Function"/></param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewSibling(string name, FunctionCreater creater)
        {
            Function function = NewSibling(name);
            creater(function);
            return function;
        }
        /// <summary>
        /// Creates a new <see cref="Function"/> with the specified commands in the same folder as this function
        /// </summary>
        /// <param name="creater">a method creating the new <see cref="Function"/></param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewSibling(FunctionCreater creater)
        {
            Function function = NewSibling();
            creater(function);
            return function;
        }

        /// <summary>
        /// Writes this function file
        /// </summary>
        /// <param name="stream">The stream used for writing</param>
        protected override void WriteFile(TextWriter stream)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Class for functions which should be callable.
    /// </summary>
    public class EmptyFunction : IFunction
    {
        /// <summary>
        /// Intializes a new <see cref="EmptyFunction"/>
        /// </summary>
        /// <param name="packNamespace">The namespace the function is in</param>
        /// <param name="name">The name of the function</param>
        public EmptyFunction(BasePackNamespace packNamespace, string name)
        {
            FileName = name;
            PackNamespace = packNamespace;
        }

        /// <summary>
        /// The name of the function
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// The namespace the function is in
        /// </summary>
        public BasePackNamespace PackNamespace { get; private set; }

        /// <summary>
        /// Returns the string used for running this function
        /// </summary>
        /// <returns>The string used for running this function</returns>
        public string GetNamespacedName()
        {
            return PackNamespace.Name + ":" + FileName;
        }

        /// <summary>
        /// Returns <see cref="BaseFile.GetNamespacedName()"/>
        /// </summary>
        public string Name
        {
            get => GetNamespacedName();
        }

        //TODO make way to convert from string into this
    }
}
