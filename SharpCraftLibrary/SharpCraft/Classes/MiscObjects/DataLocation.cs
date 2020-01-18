using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft
{
    /// <summary>
    /// Interface for classes which marks a place there is data
    /// </summary>
    public interface IDataLocation
    {
        /// <summary>
        /// The path to the data to get
        /// </summary>
        string DataPath { get; set; }

        /// <summary>
        /// Returns a string used in commands for getting the data
        /// </summary>
        /// <returns>A string used in commands for getting the data</returns>
        string GetLocationString();
    }

    /// <summary>
    /// Used for holding a block location and a datapath for getting data
    /// </summary>
    public class BlockDataLocation : IDataLocation
    {
        private Vector coordinates;
        private string dataPath;

        /// <summary>
        /// Intializes a new <see cref="BlockDataLocation"/>
        /// </summary>
        /// <param name="coordinates">The location of the block holding the data</param>
        /// <param name="dataPath">The path to the data to get</param>
        public BlockDataLocation(Vector coordinates, string dataPath)
        {
            Coordinates = coordinates;
            DataPath = dataPath;
        }

        /// <summary>
        /// The location of the block holding the data
        /// </summary>
        public Vector Coordinates
        {
            get => coordinates;
            set
            {
                coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null.");
            }
        }

        /// <summary>
        /// The path to the data to get
        /// </summary>
        public string DataPath
        {
            get => dataPath;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("DataPath may not be null or whitespace",nameof(DataPath));
                }
                dataPath = value;
            }
        }

        /// <summary>
        /// Returns a string used in commands for getting the data
        /// </summary>
        /// <returns>A string used in commands for getting the data</returns>
        public string GetLocationString()
        {
            return $"block {Coordinates.GetVectorString()} {DataPath}";
        }
    }

    /// <summary>
    /// Used for holding an entity selector and a datapath for getting data
    /// </summary>
    public class EntityDataLocation : IDataLocation
    {
        private BaseSelector selector;
        private string dataPath;

        /// <summary>
        /// Intializes a new <see cref="BlockDataLocation"/>
        /// </summary>
        /// <param name="selector">Selector selecting the entity holding the data</param>
        /// <param name="dataPath">The path to the data to get</param>
        public EntityDataLocation(BaseSelector selector, string dataPath)
        {
            Selector = selector;
            DataPath = dataPath;
        }

        /// <summary>
        /// Selector selecting the entity holding the data
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                if (!(value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.")).IsLimited())
                {
                    throw new ArgumentException("Command doesn't allow selectors which selects multiple entities", nameof(Selector));
                }
                selector = value;
            }
        }

        /// <summary>
        /// The path to the data to get
        /// </summary>
        public string DataPath
        {
            get => dataPath;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("DataPath may not be null or whitespace", nameof(DataPath));
                }
                dataPath = value;
            }
        }

        /// <summary>
        /// Returns a string used in commands for getting the data
        /// </summary>
        /// <returns>A string used in commands for getting the data</returns>
        public string GetLocationString()
        {
            return $"entity {Selector.GetSelectorString()} {DataPath}";
        }
    }

    /// <summary>
    /// Used for holding a storage location and a datapath for getting data
    /// </summary>
    public class StorageDataLocation : IDataLocation
    {
        private Storage storage;
        private string dataPath;

        /// <summary>
        /// Intializes a new <see cref="StorageDataLocation"/>
        /// </summary>
        /// <param name="storage">The storage holding the data</param>
        /// <param name="dataPath">The path to the data to get</param>
        public StorageDataLocation(Storage storage, string dataPath)
        {
            Storage = storage;
            DataPath = dataPath;
        }

        /// <summary>
        /// The storage holding the data
        /// </summary>
        public Storage Storage
        {
            get => storage;
            set
            {
                storage = value ?? throw new ArgumentNullException(nameof(Storage), "Storage may not be null");
            }
        }

        /// <summary>
        /// The path to the data to get
        /// </summary>
        public string DataPath
        {
            get => dataPath;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("DataPath may not be null or whitespace", nameof(DataPath));
                }
                dataPath = value;
            }
        }

        /// <summary>
        /// Returns a string used in commands for getting the data
        /// </summary>
        /// <returns>A string used in commands for getting the data</returns>
        public string GetLocationString()
        {
            return $"storage {Storage.GetNamespacedName()} {DataPath}";
        }
    }
}
