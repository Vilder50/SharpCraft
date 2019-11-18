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
        public Datapack(string path, string packName, string description = "Generated with Sharpcraft", int packFormat = 0) : base(path, packName)
        {
            if (!File.Exists(Path + "\\" + Name + "\\pack.mcmeta"))
            {
                Directory.CreateDirectory(Path + "\\" + Name);
                StreamWriter WriteMeta = new StreamWriter(new FileStream(Path + "\\" + Name + "\\pack.mcmeta", FileMode.Create)) { AutoFlush = true };
                WriteMeta.Write("{\"pack\":{\"pack_format\":" + packFormat + ",\"description\":\"" + description + "\"}}");
                WriteMeta.Dispose();
            }
        }

        /// <summary>
        /// The name of the datapack used for refering to the datapack in game
        /// </summary>
        public new string IngameName
        {
            get => "\"file/" + base.Name + "\"";
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
        /// <summary>
        /// Intializes a new <see cref="EmptyDatapack"/>
        /// </summary>
        /// <param name="name">The name of the datapack</param>
        /// <param name="fileDatapack">True this <see cref="EmptyDatapack"/> is refering to an installed datapack. False if its an inbuilt datapack</param>
        public EmptyDatapack(string name, bool fileDatapack = true) : base("a", name)
        {
            FileDatapack = fileDatapack;
        }

        /// <summary>
        /// The name of the datapack used for refering to the datapack in game
        /// </summary>
        public new string IngameName
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
