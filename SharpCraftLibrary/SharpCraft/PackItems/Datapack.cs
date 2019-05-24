using System.IO;
using System;

namespace SharpCraft
{
    /// <summary>
    /// An <see cref="object"/> used to define a datapack
    /// </summary>
    public class Datapack
    {
        readonly string _WorldPath;
        readonly string _PackName;
        readonly private string _description;
        readonly private int _packVersion;
        internal string WorldPath { get { return _WorldPath; } }

        /// <summary>
        /// The name of the namespace
        /// </summary>
        public string PackName { get { return _PackName; } }

        /// <summary>
        /// Creates a new <see cref="Datapack"/> with the given parameters
        /// </summary>
        /// <param name="worldPath">The path to the world's save file (not the datapack folder)</param>
        /// <param name="packName">The datapack's name</param>
        /// <param name="description">The datapack's description</param>
        /// <param name="packFormat">The datapack's format</param>
        public Datapack(string worldPath, string packName, string description = "Generated with Sharpcraft", int packFormat = 0)
        {
            _WorldPath = worldPath.ToLower();
            _PackName = packName.ToLower();
            this._description = description;
            _packVersion = packFormat;

            if (!File.Exists(_WorldPath + "\\datapacks\\" + _PackName + "\\pack.mcmeta"))
            {
                Directory.CreateDirectory(_WorldPath + "\\datapacks\\" + _PackName);
                StreamWriter WriteMeta = new StreamWriter(new FileStream(_WorldPath + "\\datapacks\\" + _PackName + "\\pack.mcmeta", FileMode.Create)) { AutoFlush = true };
                WriteMeta.Write("{\"pack\": {\"pack_format\": " + packFormat + ",\"description\": \"" + description + "\"}}");
                WriteMeta.Dispose();
            }
        }

        /// <summary>
        /// Creates a new <see cref="Datapack"/> object with the given name.
        /// (This is used for enable/disable datapack commands)
        /// </summary>
        /// <param name="datapackName">A <see cref="Datapack"/> with the name</param>
        public Datapack(string datapackName)
        {
            _PackName = datapackName.ToLower();
        }

        /// <summary>
        /// Outputs a <see cref="PackNamespace"/> for this datapack
        /// </summary>
        /// <param name="packNamespace">The namespace the namespace has</param>
        /// <returns>A new <see cref="PackNamespace"/> with the given namespace</returns>
        public PackNamespace Namespace(string packNamespace)
        {
            if (string.IsNullOrWhiteSpace(WorldPath))
            {
                throw new ArgumentException("Cannot create namespaces in basic datapack");
            }
            return new PackNamespace(this, packNamespace);
        }

        /// <summary>
        /// Gets the path to the data folder in the datapack
        /// </summary>
        /// <returns>the path to the data folder in the datapack</returns>
        public string GetDataPath()
        {
            return WorldPath + "\\datapacks\\" + PackName + "\\data\\";
        }
    }
}
