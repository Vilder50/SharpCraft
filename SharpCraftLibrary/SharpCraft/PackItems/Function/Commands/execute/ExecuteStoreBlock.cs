using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command used for storing the result of the command
    /// </summary>
    public class ExecuteStoreBlock : BaseExecuteStoreCommand
    {
        private Coords coordinates;
        private string dataPath;

        /// <summary>
        /// Intializes a new <see cref="ExecuteStoreBlock"/> command
        /// </summary>
        /// <param name="coordinates">The position of the block to store the result in</param>
        /// <param name="dataPath">The path to store the result at</param>
        /// <param name="valueType">The type of number to store the result as</param>
        /// <param name="scale">A number to multiply the result by before storing it</param>
        /// <param name="storeResult">True if it should store the result. False if it should store success</param>
        public ExecuteStoreBlock(Coords coordinates, string dataPath, ID.StoreTypes valueType, double scale, bool storeResult = true) : base(storeResult)
        {
            Coordinates = coordinates;
            DataPath = dataPath;
            ValueType = valueType;
            Scale = scale;
        }

        /// <summary>
        /// The position of the block to store the result in
        /// </summary>
        public Coords Coordinates
        {
            get => coordinates;
            set
            {
                coordinates = value ?? throw new ArgumentNullException(nameof(Coordinates), "Coordinates may not be null.");
            }
        }

        /// <summary>
        /// The path to store the result at
        /// </summary>
        public string DataPath
        {
            get => dataPath;
            set
            {
                dataPath = value ?? throw new ArgumentNullException(nameof(DataPath), "DataPath may not be null.");
            }
        }

        /// <summary>
        /// The type of number to store the result as
        /// </summary>
        public ID.StoreTypes ValueType { get; set; }

        /// <summary>
        /// A number to multiply the result by before storing it
        /// </summary>
        public double Scale { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>block [Coordinates] [Path] [ValueType] [Scale]</returns>
        protected override string GetStorePart()
        {
            return "block " + Coordinates + " " + DataPath + " " + ValueType.ToString().ToLower() + " " + Scale.ToMinecraftDouble();
        }
    }
}
