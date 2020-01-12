using System.IO;
using System;
using System.Collections.Generic;
using SharpCraft.Commands;

namespace SharpCraft
{
    /// <summary>
    /// Class for function files
    /// </summary>
    public class Function : BaseFile, IFunction
    {
        /// <summary>
        /// Used to add commands to the given function
        /// </summary>
        /// <param name="function">the function to give the commands to</param>
        public delegate void FunctionCreater(Function function);

        private List<ICommand> commands;

        /// <summary>
        /// Intializes a new <see cref="Function"/> with the given values. Inherite from this constructor.
        /// </summary>
        /// <param name="space">The namespace the function is in</param>
        /// <param name="fileName">The name of the function</param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <param name="_">Unused parameter used for specifing you want to use this constructor</param>
        protected Function(bool _, BasePackNamespace space, string fileName, WriteSetting writeSetting = WriteSetting.LockedAuto) : base(space, fileName, writeSetting, "function")
        {
            if (IsAuto())
            {
                StreamWriter = GetStream();
            }

            Block = new FunctionWriters.BlockCommands(this);
            Entity = new FunctionWriters.EntityCommands(this);
            Execute = new FunctionWriters.ExecuteCommands(this);
            World = new FunctionWriters.WorldCommands(this);
            Player = new FunctionWriters.PlayerCommands(this);
            Custom = new FunctionWriters.CustomCommands(this);

            Commands = new List<ICommand>();
        }

        /// <summary>
        /// Intializes a new <see cref="Function"/> with the given values
        /// </summary>
        /// <param name="space">The namespace the function is in</param>
        /// <param name="fileName">The name of the function</param>
        /// <param name="writeSetting">The setting for writing the file</param>
        public Function(BasePackNamespace space, string fileName, WriteSetting writeSetting = WriteSetting.LockedAuto) : this(true, space, fileName, writeSetting)
        {
            FinishedConstructing();
        }

        /// <summary>
        /// Call when constructors are done
        /// </summary>
        protected override void FinishedConstructing()
        {
            PackNamespace.AddFile(this);
        }

        /// <summary>
        /// The commands in the function. If the function file is auto then this list only contains all commands there are after an active <see cref="ICommandChanger"/>. Please use <see cref="AddCommand"/> for actually adding commands
        /// </summary>
        public List<ICommand> Commands { get => commands; set => commands = value ?? throw new ArgumentNullException(nameof(Commands), "Commands may not be null"); }

        /// <summary>
        /// Adds a command to this function
        /// </summary>
        /// <param name="command">The command to add</param>
        public void AddCommand(ICommand command)
        {
            if (Disposed)
            {
                throw new InvalidOperationException("Cannot add more commands since file is disposed");
            }
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command), "Command may not be null");
            }
            ICommand addCommand = command;

            //run changers
            for (int i = 0; i < commands.Count; i++)
            {
                if (commands[i] is ICommandChanger changer && !changer.DoneChanging)
                {
                    addCommand = changer.ChangeCommand(addCommand);
                }
            }

            if (!(addCommand is null))
            {
                commands.Add(addCommand);
            }

            //write unwritten commands up to next changer if auto
            if (IsAuto())
            {
                for (int i = 0; i < commands.Count; i++)
                {
                    if (commands[i] is ICommandChanger nextChanger && !nextChanger.DoneChanging)
                    {
                        break;
                    }
                    string commandString = commands[i].GetCommandString();
                    if (!(commandString is null))
                    {
                        StreamWriter.WriteLine(commandString);
                    }
                    commands.RemoveAt(i);
                    i--;
                }
            }
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
            CreateDirectory("functions");
            if (StreamWriter is null)
            {
                StreamWriter = PackNamespace.Datapack.FileCreator.CreateWriter(PackNamespace.GetPath() + "functions\\" + FileName + ".mcfunction");
            }
            return StreamWriter;
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
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewChild(string name = null, WriteSetting writeSetting = WriteSetting.LockedAuto)
        {
            string functionName = name;
            if (string.IsNullOrWhiteSpace(functionName))
            {
                if (PackNamespace is PackNamespace space)
                {
                    functionName = space.NextFileID.ToString();
                }
                else
                {
                    throw new InvalidOperationException("Cannot create function without a name without the namespace being a PackNamespace");
                }
            }
            return new Function(PackNamespace, FileName + "\\" + functionName.ToLower(), writeSetting);
        }
        /// <summary>
        /// Creates a folder with this function's name and creates a new <see cref="Function"/> inside of it with the specified name and commands
        /// </summary>
        /// <param name="name">The name of the new <see cref="Function"/></param>
        /// <param name="creater">a method creating the new <see cref="Function"/></param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewChild(string name, FunctionCreater creater, WriteSetting writeSetting = WriteSetting.LockedAuto)
        {
            Function function = NewChild(name, writeSetting);
            creater(function);
            return function;
        }
        /// <summary>
        /// Creates a folder with this function's name and creates a new <see cref="Function"/> inside of it with the specified commands
        /// </summary>
        /// <param name="creater">a method creating the new <see cref="Function"/></param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewChild(FunctionCreater creater, WriteSetting writeSetting = WriteSetting.LockedAuto)
        {
            Function function = NewChild((string)null, writeSetting);
            creater(function);
            return function;
        }

        /// <summary>
        /// Creates a new <see cref="Function"/> with the specified name in the same folder as this function
        /// </summary>
        /// <param name="name">The name of the new <see cref="Function"/></param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewSibling(string name = null, WriteSetting writeSetting = WriteSetting.LockedAuto)
        {
            string functionName = name;
            if (string.IsNullOrWhiteSpace(functionName))
            {
                if (PackNamespace is PackNamespace space)
                {
                    functionName = space.NextFileID.ToString();
                }
                else
                {
                    throw new InvalidOperationException("Cannot create function without a name without the namespace being a PackNamespace");
                }
            }
            if (FileName.Contains("\\"))
            {
                return new Function(PackNamespace, FileName.Substring(0, FileName.LastIndexOf("\\") + 1) + functionName.ToLower(), writeSetting);
            }
            else
            {
                return new Function(PackNamespace, functionName.ToLower(), writeSetting);
            }
        }
        /// <summary>
        /// Creates a new <see cref="Function"/> with the specified name and commands in the same folder as this function
        /// </summary>
        /// <param name="name">The name of the new <see cref="Function"/></param>
        /// <param name="creater">a method creating the new <see cref="Function"/></param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewSibling(string name, FunctionCreater creater, WriteSetting writeSetting = WriteSetting.LockedAuto)
        {
            Function function = NewSibling(name, writeSetting);
            creater(function);
            return function;
        }
        /// <summary>
        /// Creates a new <see cref="Function"/> with the specified commands in the same folder as this function
        /// </summary>
        /// <param name="creater">a method creating the new <see cref="Function"/></param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewSibling(FunctionCreater creater, WriteSetting writeSetting = WriteSetting.LockedAuto)
        {
            Function function = NewSibling((string)null, writeSetting);
            creater(function);
            return function;
        }

        /// <summary>
        /// Disposes this file. If the write setting is OnDispose it will write the file
        /// </summary>
        public override void Dispose()
        {
            if (!Disposed)
            {
                GetStream();
                WriteFile(StreamWriter);
                AfterDispose();
                StreamWriter.Dispose();
                Disposed = true;
            }
        }

        /// <summary>
        /// Writes the file
        /// </summary>
        /// <param name="stream">The stream used for writing the file</param>
        protected override void WriteFile(TextWriter stream)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                string command = commands[i].GetCommandString();
                if (!(command is null))
                {
                    stream.WriteLine(command);
                }
            }
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

        /// <summary>
        /// Converts a string of the format NAMESPACE:FUNCTION into an <see cref="EmptyFunction"/>
        /// </summary>
        /// <param name="function">The string to convert</param>
        public static implicit operator EmptyFunction(string function)
        {
            string[] parts = function.Split(':');
            if (parts.Length != 2)
            {
                throw new InvalidCastException("String for creating empty function has to contain a single :");
            }
            return new EmptyFunction(EmptyDatapack.GetPack().Namespace(parts[0]), parts[1]);
        }
    }
}
