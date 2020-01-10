using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command for changing the rotation the command is being executed in
    /// </summary>
    public class ExecuteFacingCoord : BaseExecuteCommand
    {
        private Coords coordinates;

        /// <summary>
        /// Intializes a new <see cref="ExecuteFacingCoord"/> command
        /// </summary>
        /// <param name="coordinates">The coordinates the command should execute in the direction of</param>
        public ExecuteFacingCoord(Coords coordinates)
        {
            Coordinates = coordinates;
        }

        /// <summary>
        /// The coordinates the command should execute in the direction of
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
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>facing [Coordinates]</returns>
        protected override string GetExecutePart()
        {
            return "facing " + Coordinates.GetCoordString(); 
        }
    }
}
