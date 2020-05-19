using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command for changing the rotation the command is using when being executed
    /// </summary>
    public class ExecutePosition : BaseExecuteCommand
    {
        private Vector coordinates = null!;

        /// <summary>
        /// Intializes a new <see cref="ExecutePosition"/> command
        /// </summary>
        /// <param name="coordinates">The position the command should be executed at</param>
        public ExecutePosition(Vector coordinates)
        {
            Coordinates = coordinates;
        }

        /// <summary>
        /// The position the command should be executed at
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
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>positioned [rotation]</returns>
        protected override string GetExecutePart()
        {
            return "positioned " + Coordinates.GetVectorString();
        }
    }
}
