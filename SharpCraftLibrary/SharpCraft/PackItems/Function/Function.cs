﻿using System.IO;
using System;
using System.Collections.Generic;
using SharpCraft.Commands;
using SharpCraft.Data;

namespace SharpCraft
{
    /// <summary>
    /// Class for function files
    /// </summary>
    public class Function : BaseFile<TextWriter>, IFunction
    {
        /// <summary>
        /// Used to add commands to the given function
        /// </summary>
        /// <param name="function">the function to give the commands to</param>
        public delegate void FunctionWriter(Function function);

        /// <summary>
        /// Used for running things on a function when it writes a command
        /// </summary>
        /// <param name="file">the function file which is writing the command</param>
        /// <param name="command">The command its writing</param>
        public delegate void CommandWriteListener(Function file, ICommand command);

        private List<ICommand> commands = null!;

        private CommandWriteListener? writeCommandListener;

        private int siblings = 0;

        /// <summary>
        /// Intializes a new <see cref="Function"/> with the given values. Inherite from this constructor.
        /// </summary>
        /// <param name="space">The namespace the function is in</param>
        /// <param name="fileName">The name of the function</param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <param name="_">Unused parameter used for specifing you want to use this constructor</param>
        protected Function(bool _, BasePackNamespace space, string? fileName, WriteSetting writeSetting = WriteSetting.LockedAuto) : base(space, fileName, writeSetting, "function")
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
        public Function(BasePackNamespace space, string? fileName, WriteSetting writeSetting = WriteSetting.LockedAuto) : this(true, space, fileName, writeSetting)
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
            ICommand? addCommand = command;

            //run changers
            for (int i = 0; i < commands.Count; i++)
            {
                if (commands[i] is ICommandChanger changer && !changer.DoneChanging)
                {
                    addCommand = changer.ChangeCommand(addCommand!);
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

                    writeCommandListener?.Invoke(this, commands[i]);
                    string? commandString = commands[i].GetCommandString();
                    if (!(commandString is null))
                    {
                        StreamWriter!.WriteLine(commandString);
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
                StreamWriter = PackNamespace.Datapack.FileCreator.CreateWriter(PackNamespace.GetPath() + "functions/" + WritePath + ".mcfunction");
            }
            return StreamWriter;
        }

        /// <summary>
        /// Commands run on blocks
        /// </summary>
        public FunctionWriters.BlockCommands Block { get; private set; }

        /// <summary>
        /// Commands run on entities
        /// </summary>
        public FunctionWriters.EntityCommands Entity { get; private set; }

        /// <summary>
        /// Execute commands
        /// </summary>
        public FunctionWriters.ExecuteCommands Execute { get; private set; }

        /// <summary>
        /// Commands run on players
        /// </summary>
        public FunctionWriters.PlayerCommands Player { get; private set; }

        /// <summary>
        /// Commands run on the world
        /// </summary>
        public FunctionWriters.WorldCommands World { get; private set; }

        /// <summary>
        /// Custom commands to make life easier
        /// </summary>
        public FunctionWriters.CustomCommands Custom { get; private set; }

        /// <summary>
        /// Marks this as not being a group
        /// </summary>
        public bool IsAGroup => false;

        /// <summary>
        /// Creates a folder with this function's name and creates a new <see cref="Function"/> inside of it with the specified name
        /// </summary>
        /// <param name="name">The name of the new <see cref="Function"/></param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewChild(string? name = null, WriteSetting writeSetting = WriteSetting.LockedAuto)
        {
            string? functionName = name;
            if (functionName is null)
            {
                return new Function(PackNamespace, null, writeSetting);
            }
            else
            {
                return new Function(PackNamespace, FileId + "/" + functionName.ToLower(), writeSetting);
            }
        }
        /// <summary>
        /// Creates a folder with this function's name and creates a new <see cref="Function"/> inside of it with the specified name and commands
        /// </summary>
        /// <param name="name">The name of the new <see cref="Function"/></param>
        /// <param name="creater">a method creating the new <see cref="Function"/></param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewChild(string? name, FunctionWriter creater, WriteSetting writeSetting = WriteSetting.LockedAuto)
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
        public Function NewChild(FunctionWriter creater, WriteSetting writeSetting = WriteSetting.LockedAuto)
        {
            Function function = NewChild((string?)null, writeSetting);
            creater(function);
            return function;
        }

        /// <summary>
        /// Creates a new <see cref="Function"/> with the specified name in the same folder as this function
        /// </summary>
        /// <param name="name">The name of the new <see cref="Function"/></param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewSibling(string? name = null, WriteSetting writeSetting = WriteSetting.LockedAuto)
        {
            if (FileId.Contains("/"))
            {
                if (name is null)
                {
                    return new Function(PackNamespace, null, writeSetting);
                }
                else
                {
                    return new Function(PackNamespace, FileId.Substring(0, FileId.LastIndexOf("/") + 1) + name.ToLower(), writeSetting);
                }
            }
            else
            {
                return new Function(PackNamespace, name?.ToLower(), writeSetting);
            }
        }
        /// <summary>
        /// Creates a new <see cref="Function"/> with the specified name and commands in the same folder as this function
        /// </summary>
        /// <param name="name">The name of the new <see cref="Function"/></param>
        /// <param name="creater">a method creating the new <see cref="Function"/></param>
        /// <param name="writeSetting">The setting for writing the file</param>
        /// <returns>The new <see cref="Function"/></returns>
        public Function NewSibling(string? name, FunctionWriter creater, WriteSetting writeSetting = WriteSetting.LockedAuto)
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
        public Function NewSibling(FunctionWriter creater, WriteSetting writeSetting = WriteSetting.LockedAuto)
        {
            Function function = new Function(PackNamespace, FileId + "-" + (siblings++), writeSetting);
            creater(function);
            return function;
        }

        /// <summary>
        /// Adds a listener to this file which will be called when the file writes a new command
        /// </summary>
        /// <param name="listener">The listener to add</param>
        public void AddCommandListener(CommandWriteListener listener)
        {
            writeCommandListener += listener ?? throw new ArgumentNullException(nameof(listener), "File dispose listener may not be null.");
        }

        /// <summary>
        /// Disposes this file. If the write setting is OnDispose it will write the file
        /// </summary>
        public override void Dispose()
        {
            if (!Disposed)
            {
                disposeListener?.Invoke(this);
                GetStream();
                WriteFile(StreamWriter!);
                AfterDispose();
                StreamWriter!.Dispose();
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
                writeCommandListener?.Invoke(this, commands[i]);
                string? command = commands[i].GetCommandString();
                if (!(command is null))
                {
                    stream.WriteLine(command);
                }
            }
        }

        /// <summary>
        /// Clears the things in the file.
        /// </summary>
        protected override void AfterDispose()
        {
            commands = null!;
            Block = null!;
            Entity = null!;
            Custom = null!;
            Execute = null!;
            World = null!;
            Player = null!;
        }

        /// <summary>
        /// Converts this type into a <see cref="DataPartObject"/>
        /// </summary>
        /// <param name="conversionData">0: tag name if id. 1: tag name if group. 2: if json</param>
        /// <returns></returns>
        public DataPartObject GetAsDataObject(object?[] conversionData)
        {
            return (this as IFunction).GetGroupData(conversionData);
        }
    }
}
