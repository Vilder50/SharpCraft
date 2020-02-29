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
    public class ExecuteStoreData : BaseExecuteStoreCommand
    {
        private IDataLocation dataLocation = null!;

        /// <summary>
        /// Intializes a new <see cref="ExecuteStoreData"/> command
        /// </summary>
        /// <param name="dataLocation">The location to store the result in</param>
        /// <param name="valueType">The type of number to store the result as</param>
        /// <param name="scale">A number to multiply the result by before storing it</param>
        /// <param name="storeResult">True if it should store the result. False if it should store success</param>
        public ExecuteStoreData(IDataLocation dataLocation, ID.StoreTypes valueType, double scale, bool storeResult = true) : base(storeResult)
        {
            DataLocation = dataLocation;
            ValueType = valueType;
            Scale = scale;
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
        /// The location to store the result in
        /// </summary>
        public IDataLocation DataLocation { get => dataLocation; set => dataLocation = value ?? throw new ArgumentNullException(nameof(dataLocation), "DataLocation may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>[DataLocation.String] [ValueType] [Scale]</returns>
        protected override string GetStorePart()
        {
            return DataLocation.GetLocationString() + " " + ValueType.ToString().ToLower() + " " + Scale.ToMinecraftDouble();
        }
    }
}
