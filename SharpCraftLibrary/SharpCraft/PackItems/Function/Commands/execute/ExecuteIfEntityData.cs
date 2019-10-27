using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command which only executes the following command if the entity chosen by the selector has the datapath
    /// </summary>
    public class ExecuteIfEntityData : BaseExecuteIfCommand
    {
        private Selector selector;
        private string dataPath;

        /// <summary>
        /// Intializes a new <see cref="ExecuteIfEntityData"/> command
        /// </summary>
        /// <param name="selector">Selector selecting the entity to check for</param>
        /// <param name="dataPath">The datapath to check if exists</param>
        /// <param name="executeIf">True to use execute if and false to use execute unless the given thing is true</param>
        public ExecuteIfEntityData(Selector selector, string dataPath, bool executeIf = true) : base(executeIf)
        {
            Selector = selector;
            DataPath = dataPath;
        }

        /// <summary>
        /// Selector selecting the entity to check for
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
        /// <returns>data entity [Selector] [DataPath]</returns>
        protected override string GetCheckPart()
        {
            return "data entity " + selector + " " + DataPath;
        }
    }
}
