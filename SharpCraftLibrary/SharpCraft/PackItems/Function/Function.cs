using System.IO;

namespace SharpCraft
{
    /// <summary>
    /// A object used to create <see cref="Function"/>s
    /// </summary>
    public partial class Function
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

        private Packspace Namespace;
        private string Name;
        private string Path;
        /// <summary>
        /// null if the function isnt a group of functions
        /// The name of the group which contaions functions
        /// </summary>
        public string FunctionGroup;

        /// <summary>
        /// The parent function which made this function using <see cref="NewChild(string)"/> or <see cref="NewCousin(string)"/>
        /// </summary>
        public Function Parent;

        /// <summary>
        /// Creates a <see cref="Function"/> object with the given string
        /// Used to run <see cref="Function"/> which doesnt have an object
        /// use <see cref="Packspace.NewFunction(string)"/> to create a new <see cref="Function"/> or <see cref="NewChild(string)"/> or <see cref="NewCousin(string)"/>
        /// </summary>
        /// <param name="function">An string path to and <see cref="Function"/></param>
        public Function(string function)
        {
            Path = function.ToLower().Replace("\\", "/");
        }
        internal Function(Packspace space, string name)
        {
            this.Name = name;
            this.Namespace = space;
            if (name.Contains("\\"))
            {
                Directory.CreateDirectory(space.WorldPath + "\\datapacks\\" + space.PackName + "\\data\\" + space.Name + "\\functions\\" + name.Substring(0, name.LastIndexOf("\\")));
            }
            else
            {
                Directory.CreateDirectory(space.WorldPath + "\\datapacks\\" + space.PackName + "\\data\\" + space.Name + "\\functions\\");
            }
            Path = space.Name + ":" + name.Replace("\\", "/");
            Writer.LineWriter = new StreamWriter(new FileStream(space.WorldPath + "\\datapacks\\" + space.PackName + "\\data\\" + space.Name + "\\functions\\" + name + ".mcfunction", FileMode.Create)) { AutoFlush = true };
            Writer.NameSpaceName = space.Name;
            Writer.FunctionName = ToString();

            Block = new BlockCommands(Writer);
            Entity = new EntityCommands(Writer);
            Execute = new ExecuteCommands(Writer);
            World = new WorldCommands(Writer);
            Player = new PlayerCommands(Writer);
        }

        /// <summary>
        /// Converts a group of functions into a function which runs all the group's functions
        /// </summary>
        /// <param name="FunctionGroup">the function group to convert</param>
        public Function(Group FunctionGroup)
        {
            this.FunctionGroup = "#" + FunctionGroup;
        }

        /// <summary>
        /// Returns the namespace path of this <see cref="Function"/>
        /// </summary>
        /// <returns>this <see cref="Function"/>'s name</returns>
        public override string ToString()
        {
            if (FunctionGroup == null)
            {
                return Path;
            }
            else
            {
                return FunctionGroup;
            }
        }

        /// <summary>
        /// Commands run on blocks
        /// </summary>
        public BlockCommands Block;

        /// <summary>
        /// Commands run on entities
        /// </summary>
        public EntityCommands Entity;

        /// <summary>
        /// Execute commands
        /// </summary>
        public ExecuteCommands Execute;

        /// <summary>
        /// Commands run on players
        /// </summary>
        public PlayerCommands Player;

        /// <summary>
        /// Commands run on the world
        /// </summary>
        public WorldCommands World;

        /// <summary>
        /// Creates a folder with this function's name and creates a new <see cref="Function"/> inside of it with the specified name
        /// </summary>
        /// <param name="Name">The name of the new <see cref="Function"/></param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewChild(string Name)
        {
            Name = Name.Replace("/", "\\");
            return new Function(Namespace, this.Name + "\\" + Name.ToLower().Replace("/", "\\")) {Parent = this };
        }
        /// <summary>
        /// Creates a folder with this function's name and creates a new <see cref="Function"/> inside of it with the specified name
        /// </summary>
        /// <param name="Name">The name of the new <see cref="Function"/></param>
        /// <param name="creater">a method creating the new <see cref="Function"/></param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewChild(string Name, FunctionCreater creater)
        {
            Function function = NewChild(Name);
            creater(function);
            return function;
        }

        /// <summary>
        /// Creates a new <see cref="Function"/> with the specified name in the same folder as this function
        /// </summary>
        /// <param name="Name">The name of the new <see cref="Function"/></param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewCousin(string Name)
        {
            Name = Name.Replace("/", "\\");
            if (this.Name.Contains("\\"))
            {
                return new Function(Namespace, this.Name.Substring(0, this.Name.LastIndexOf("\\") + 1) + Name.ToLower()) { Parent = this };
            }
            else
            {
                return new Function(Namespace, Name.ToLower()) { Parent = this };
            }
        }
        /// <summary>
        /// Creates a new <see cref="Function"/> with the specified name in the same folder as this function
        /// </summary>
        /// <param name="Name">The name of the new <see cref="Function"/></param>
        /// <param name="creater">a method creating the new <see cref="Function"/></param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewCousin(string Name, FunctionCreater creater)
        {
            Function function = NewCousin(Name);
            creater(function);
            return function;
        }
    }
}
