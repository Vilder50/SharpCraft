using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command used for changing the dimension the command is being executed in
    /// </summary>
    public class ExecuteDimension : BaseExecuteCommand
    {
        /// <summary>
        /// Intializes a new <see cref="ExecuteDimension"/> command
        /// </summary>
        /// <param name="dimension">The dimension the command should be executed in</param>
        public ExecuteDimension(ID.Dimension dimension)
        {
            Dimension = dimension;
        }

        /// <summary>
        /// The dimension the command should be executed in
        /// </summary>
        public ID.Dimension Dimension { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>in [Dimension]</returns>
        protected override string GetExecutePart()
        {
            return "in " + Dimension.ToString();
        }
    }
}
