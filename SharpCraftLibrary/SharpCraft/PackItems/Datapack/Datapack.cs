using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;

namespace SharpCraft
{
    /// <summary>
    /// An <see cref="object"/> used to define a datapack
    /// </summary>
    public class Datapack : BaseDatapack
    {
        /// <summary>
        /// Creates a new <see cref="Datapack"/> with the given parameters
        /// </summary>
        /// <param name="path">The path to the folder to create this datapack in</param>
        /// <param name="packName">The datapack's name</param>
        /// <param name="description">The datapack's description</param>
        /// <param name="packFormat">The datapack's format</param>
        /// <param name="settings">Datapack settings</param>
        public Datapack(string path, string packName, string description = "Generated with Sharpcraft", int packFormat = 5, IDatapackSetting[]? settings = null) : this(path, packName, description, packFormat, new FileCreator(), settings)
        {

        }

        /// <summary>
        /// Creates a new <see cref="Datapack"/> with the given parameters
        /// </summary>
        /// <param name="path">The path to the folder to create this datapack in</param>
        /// <param name="packName">The datapack's name</param>
        /// <param name="description">The datapack's description</param>
        /// <param name="packFormat">The datapack's format</param>
        /// <param name="fileCreator">Class for creating files and directories</param>
        /// <param name="settings">Datapack settings</param>
        public Datapack(string path, string packName, string description, int packFormat, IFileCreator fileCreator, IDatapackSetting[]? settings = null) : this(path, packName, description, packFormat, fileCreator, settings, true)
        {
            MakeForDatapack = this;
            FinishedConstructing();
        }

        /// <summary>
        /// Creates a new <see cref="Datapack"/> with the given parameters. Inherite from this constructor.
        /// </summary>
        /// <param name="path">The path to the folder to create this datapack in</param>
        /// <param name="packName">The datapack's name</param>
        /// <param name="description">The datapack's description</param>
        /// <param name="packFormat">The datapack's format</param>
        /// <param name="fileCreator">Class for creating files and directories</param>
        /// <param name="_">Unused parameter used for specifing you want to use this constructor</param>
        /// <param name="settings">Datapack settings</param>
        protected Datapack(string path, string packName, string description, int packFormat, IFileCreator fileCreator, IDatapackSetting[]? settings, bool _) : base(path, packName, fileCreator, settings)
        {
            FileCreator.CreateDirectory(Path + "/" + Name);
            using TextWriter metaWriter = FileCreator.CreateWriter(Path + "/" + Name + "/pack.mcmeta");
            metaWriter.Write("{\"pack\":{\"pack_format\":" + packFormat + ",\"description\":\"" + description + "\"}}");
        }

        /// <summary>
        /// The name of the datapack used for refering to the datapack in game
        /// </summary>
        public new string IngameName
        {
            get => "\"file/" + Name + "\"";
        }

        /// <summary>
        /// Outputs a <see cref="PackNamespace"/> for this datapack
        /// </summary>
        /// <param name="packNamespace">The namespace the namespace has</param>
        /// <returns>A new <see cref="PackNamespace"/> with the given namespace</returns>
        public PackNamespace Namespace(string packNamespace)
        {
            return Namespace<PackNamespace>(packNamespace);
        }
    }
}
