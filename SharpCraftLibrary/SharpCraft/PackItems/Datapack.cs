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
        public Datapack(string path, string packName, string description = "Generated with Sharpcraft", int packFormat = 5) : this(path, packName, description, packFormat, new FileCreator())
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
        public Datapack(string path, string packName, string description, int packFormat, IFileCreator fileCreator) : this(path, packName, description, packFormat, fileCreator, true)
        {
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
        protected Datapack(string path, string packName, string description, int packFormat, IFileCreator fileCreator, bool _) : base(path, packName, fileCreator)
        {
            FileCreator.CreateDirectory(Path + "\\" + Name);
            using TextWriter metaWriter = FileCreator.CreateWriter(Path + "\\" + Name + "\\pack.mcmeta");
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

    /// <summary>
    /// A datapack used for disabling/enabling datapacks.
    /// </summary>
    public class EmptyDatapack : BaseDatapack
    {
        private static EmptyDatapack emptyPack;

        /// <summary>
        /// Returns an empty datapack
        /// </summary>
        /// <returns>An empty datapack</returns>
        public static EmptyDatapack GetPack()
        {
            emptyPack ??= new EmptyDatapack("vanilla", false);
            return emptyPack;
        }

        /// <summary>
        /// Intializes a new <see cref="EmptyDatapack"/>
        /// </summary>
        /// <param name="name">The name of the datapack</param>
        /// <param name="fileDatapack">True this <see cref="EmptyDatapack"/> is refering to an installed datapack. False if its an inbuilt datapack</param>
        public EmptyDatapack(string name, bool fileDatapack = true) : base("NoneExistingPath", name)
        {
            FileDatapack = fileDatapack;
        }

        /// <summary>
        /// The name of the datapack used for refering to the datapack in game
        /// </summary>
        public override string IngameName
        {
            get
            {
                if (FileDatapack)
                {
                    return "\"file/" + Name + "\"";
                }
                else
                {
                    return Name;
                }
            }
        }

        /// <summary>
        /// True if this <see cref="EmptyDatapack"/> is refering to an installed datapack. False if its an inbuilt datapack
        /// </summary>
        public bool FileDatapack { get; set; }

        /// <summary>
        /// Returns a new empty namespace
        /// </summary>
        /// <param name="packNamespace">The name of the namespace</param>
        /// <returns>A new empty namespace</returns>
        public EmptyNamespace Namespace(string packNamespace)
        {
            return Namespace<EmptyNamespace>(packNamespace);
        }
    }
}
