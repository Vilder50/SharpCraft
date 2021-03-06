﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command for changing where and what direction the command is getting executed
    /// </summary>
    public class ExecuteAt : BaseExecuteCommand
    {
        private BaseSelector selector = null!;

        /// <summary>
        /// Intializes a new <see cref="ExecuteAt"/> command
        /// </summary>
        /// <param name="selector">The selector for selecting entities to run the command at</param>
        public ExecuteAt(BaseSelector selector)
        {
            Selector = selector;
        }

        /// <summary>
        /// The selector for selecting entities to run the command as
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
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>at [selector]</returns>
        protected override string GetExecutePart()
        {
            return "at " + Selector.GetSelectorString();
        }
    }
}
