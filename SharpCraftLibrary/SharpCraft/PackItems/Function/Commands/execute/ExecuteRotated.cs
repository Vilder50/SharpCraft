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
    public class ExecuteRotated : BaseExecuteCommand
    {
        private Rotation rotation = null!;

        /// <summary>
        /// Intializes a new <see cref="ExecuteRotated"/> command
        /// </summary>
        /// <param name="rotation">The rotation the command should be executed in</param>
        public ExecuteRotated(Rotation rotation)
        {
            Rotation = rotation;
        }

        /// <summary>
        /// The rotation the command should be executed in
        /// </summary>
        public Rotation Rotation
        {
            get => rotation;
            set
            {
                rotation = value ?? throw new ArgumentNullException(nameof(Rotation), "Rotation may not be null.");
            }
        }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>rotated [rotation]</returns>
        protected override string GetExecutePart()
        {
            return "rotated " + Rotation.GetRotationString();
        }
    }
}
