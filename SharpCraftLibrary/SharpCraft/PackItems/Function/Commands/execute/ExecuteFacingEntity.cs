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
    public class ExecuteFacingEntity : BaseExecuteCommand
    {
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="ExecuteFacingEntity"/> command
        /// </summary>
        /// <param name="selector">Selector for selecting entities the command should execute in the direction of</param>
        /// <param name="facingPart">The part of the entities the command should face</param>
        public ExecuteFacingEntity(BaseSelector selector, ID.FacingAnchor facingPart)
        {
            Selector = selector;
            FacingPart = facingPart;
        }

        /// <summary>
        /// Selector for selecting entities the command should execute in the direction of
        /// </summary>
        public BaseSelector Selector
        {
            get => selector;
            set
            {
                selector = value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.");
            }
        }

        /// <summary>
        /// The part of the entities the command should face
        /// </summary>
        public ID.FacingAnchor FacingPart { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>facing entity [Selector] [FacingPart]</returns>
        protected override string GetExecutePart()
        {
            return "facing entity " + Selector.GetSelectorString() + " " + FacingPart;
        }
    }
}
