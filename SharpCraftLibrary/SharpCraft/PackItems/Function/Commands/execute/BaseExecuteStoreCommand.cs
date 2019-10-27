using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// base class for execute store commands
    /// </summary>
    public abstract class BaseExecuteStoreCommand : BaseExecuteCommand
    {
        /// <summary>
        /// Intializes a new <see cref="BaseExecuteStoreCommand"/>
        /// </summary>
        /// <param name="storeResult">True if it should store the result. False if it should store success</param>
        public BaseExecuteStoreCommand(bool storeResult = true)
        {
            StoreResult = storeResult;
        }

        /// <summary>
        /// True if it should store the result. False if it should store success
        /// </summary>
        public bool StoreResult { get; set; }

        /// <summary>
        /// Returns the command string without the "execute" part at the beginning
        /// </summary>
        /// <returns>The command string without the "execute" part at the beginning</returns>
        protected override string GetExecutePart()
        {
            return "store " + (StoreResult ? "result" : "success") + " " + GetStorePart();
        }

        /// <summary>
        /// Returns the command part of the execute command without the store result/success part
        /// </summary>
        /// <returns>The command part of the execute command without the result/success part</returns>
        /// <remarks>
        /// eg: with the "/execute store score [selector] [objective] run ..." command this would return the "score [selector] [objective]" part
        /// </remarks>
        protected abstract string GetStorePart();
    }
}
