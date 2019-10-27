using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command which only executes the following command if the block at the location has the datapath
    /// </summary>
    public class ExecuteIfBlockData : BaseExecuteIfCommand
    {
        private Coords coordinates;
        private string dataPath;

        /// <summary>
        /// Intializes a new <see cref="ExecuteIfBlockData"/> command
        /// </summary>
        /// <param name="coordinates">The coordinates of the block to check for</param>
        /// <param name="dataPath">The datapath to check if exists</param>
        /// <param name="executeIf">True to use execute if and false to use execute unless the given thing is true</param>
        public ExecuteIfBlockData(Coords coordinates, string dataPath, bool executeIf = true) : base(executeIf)
        {
            Coordinates = coordinates;
            DataPath = dataPath;
        }

        /// <summary>
        /// The coordinates of the block to check for
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
        /// The datapath to check if exists
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
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>data block [Coordinates] [DataPath]</returns>
        protected override string GetCheckPart()
        {
            return "data block " + Coordinates + " " + DataPath;
        }
    }
}
