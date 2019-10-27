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
    public class ExecuteStoreEntity : BaseExecuteStoreCommand
    {
        private Selector selector;
        private string dataPath;

        /// <summary>
        /// Intializes a new <see cref="ExecuteStoreEntity"/> command
        /// </summary>
        /// <param name="selector">selector for selecting the entity to store the result on</param>
        /// <param name="dataPath">The path to store the result at</param>
        /// <param name="valueType">The type of number to store the result as</param>
        /// <param name="scale">A number to multiply the result by before storing it</param>
        /// <param name="storeResult">True if it should store the result. False if it should store success</param>
        public ExecuteStoreEntity(Selector selector, string dataPath, ID.StoreTypes valueType, double scale, bool storeResult = true) : base(storeResult)
        {
            Selector = selector;
            DataPath = dataPath;
            ValueType = valueType;
            Scale = scale;
        }

        /// <summary>
        /// selector for selecting the entity to store the result on
        /// </summary>
        public Selector Selector
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
        /// <returns>entity [Selector] [Path] [ValueType] [Scale]</returns>
        protected override string GetStorePart()
        {
            return "entity " + Selector + " " + DataPath + " " + ValueType.ToString().ToLower() + " " + Scale.ToMinecraftDouble();
        }
    }
}
