using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command which only executes the following command if the datapath exists
    /// </summary>
    public class ExecuteIfData : BaseExecuteIfCommand
    {
        private IDataLocation dataLocation = null!;

        /// <summary>
        /// Intializes a new <see cref="ExecuteIfData"/> command
        /// </summary>
        /// <param name="executeIf">True to use execute if and false to use execute unless the given thing is true</param>
        /// <param name="dataLocation">The location and datapath to check for</param>
        public ExecuteIfData(IDataLocation dataLocation, bool executeIf = true) : base(executeIf)
        {
            DataLocation = dataLocation;
        }

        /// <summary>
        /// The location and datapath to check for
        /// </summary>
        public IDataLocation DataLocation { get => dataLocation; set => dataLocation = value ?? throw new ArgumentNullException(nameof(DataLocation), "DataLocation may not be null"); }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>data [DataLocation]</returns>
        protected override string GetCheckPart()
        {
            return $"data {DataLocation.GetLocationString()}";
        }
    }
}
