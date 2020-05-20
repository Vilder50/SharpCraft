using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command which only executes the following command if the blocks at one location are the same as the blocks in another location
    /// </summary>
    public class ExecuteIfBlocks : BaseExecuteIfCommand
    {
        private Vector corner1 = null!;
        private Vector corner2 = null!;
        private Vector location = null!;

        /// <summary>
        /// Intializes a new <see cref="ExecuteIfBlocks"/>
        /// </summary>
        /// <param name="corner1">One of the corners of the structure to check for</param>
        /// <param name="corner2">The oppesite corners of the structure to check for</param>
        /// <param name="location">The location to look for the structure</param>
        /// <param name="ignoreAir">If air blocks should be ignored when checking</param>
        /// <param name="executeIf">True to use execute if and false to use execute unless the given thing is true</param>
        public ExecuteIfBlocks(Vector corner1, Vector corner2, Vector location, bool ignoreAir, bool executeIf = true) : base(executeIf)
        {
            Corner1 = corner1;
            Corner2 = corner2;
            Location = location;
            IgnoreAir = ignoreAir;
        }

        /// <summary>
        /// One of the corners of the structure to check for
        /// </summary>
        public Vector Corner1
        {
            get => corner1;
            set
            {
                corner1 = value ?? throw new ArgumentNullException(nameof(Corner1), "Corner1 may not be null.");
            }
        }

        /// <summary>
        /// The oppesite corners of the structure to check for
        /// </summary>
        public Vector Corner2
        {
            get => corner2;
            set
            {
                corner2 = value ?? throw new ArgumentNullException(nameof(Corner2), "Corner2 may not be null.");
            }
        }

        /// <summary>
        /// The location to look for the structure
        /// </summary>
        public Vector Location
        {
            get => location;
            set
            {
                location = value ?? throw new ArgumentNullException(nameof(Location), "Location may not be null.");
            }
        }

        /// <summary>
        /// If air blocks should be ignored when checking
        /// </summary>
        public bool IgnoreAir { get; set; }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>blocks [Corner1] [Corner2] [Location] [IgnoreAir]</returns>
        protected override string GetCheckPart()
        {
            return "blocks " + Corner1.GetVectorString() + " " + Corner2.GetVectorString() + " " + Location.GetVectorString() + " " + (IgnoreAir ? "masked" : "all");
        }
    }
}
