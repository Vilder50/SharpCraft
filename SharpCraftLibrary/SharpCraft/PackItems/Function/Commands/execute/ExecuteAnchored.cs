using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command used for changing where the origin used by coordinates
    /// </summary>
    public class ExecuteAnchored : BaseExecuteCommand
    {
        /// <summary>
        /// Intializes a new <see cref="ExecuteAnchored"/> command
        /// </summary>
        /// <param name="anchor">The origin used by local coordinates</param>
        public ExecuteAnchored(ID.FacingAnchor anchor)
        {
            Anchor = anchor;
        }

        /// <summary>
        /// The origin used by local coordinates
        /// </summary>
        public ID.FacingAnchor Anchor { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>anchored eyes/feet</returns>
        protected override string GetExecutePart()
        {
            return "anchored " + Anchor.ToString();
        }
    }
}
