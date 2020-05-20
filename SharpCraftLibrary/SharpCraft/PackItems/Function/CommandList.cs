using SharpCraft.Commands;

namespace SharpCraft.FunctionWriters
{
    /// <summary>
    /// Base class for command lists
    /// </summary>
    public abstract class CommandList
    {
        /// <summary>
        /// The function to write onto
        /// </summary>
        public Function ForFunction { get; private set; }

        /// <summary>
        /// Intializes a new <see cref="CommandList"/>
        /// </summary>
        /// <param name="function">The function to write onto</param>
        protected CommandList(Function function)
        {
            ForFunction = function;
        }
    }
}
