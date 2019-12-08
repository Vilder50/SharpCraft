﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpCraft.Commands
{
    /// <summary>
    /// Execute command used for storing the result of the command
    /// </summary>
    public class ExecuteStoreScore : BaseExecuteStoreCommand
    {
        private Selector selector;
        private ScoreObject objective;

        /// <summary>
        /// Intializes a new <see cref="ExecuteStoreScore"/> command
        /// </summary>
        /// <param name="selector">The selector selecting the place to store the value at</param>
        /// <param name="objective">The <see cref="ScoreObject"/> to store the value in</param>
        /// <param name="storeResult">True if it should store the result. False if it should store success</param>
        public ExecuteStoreScore(Selector selector, ScoreObject objective, bool storeResult = true) : base(storeResult)
        {
            Selector = selector;
            Objective = objective;
        }

        /// <summary>
        /// The selector selecting the place to store the value at
        /// </summary>
        public Selector Selector
        {
            get => selector;
            set
            {
                if (!(value ?? throw new ArgumentNullException(nameof(Selector), "Selector may not be null.")).IsLimited())
                {
                    throw new ArgumentException("Command doesn't allow selectors which selects multiple entities", nameof(Selector));
                }
                selector = value;
            }
        }

        /// <summary>
        /// The <see cref="ScoreObject"/> to store the value in
        /// </summary>
        public ScoreObject Objective
        {
            get => objective;
            set
            {
                objective = value ?? throw new ArgumentNullException(nameof(Objective), "Objective may not be null.");
            }
        }

        /// <summary>
        /// Returns the part of the execute command there is special for this command
        /// </summary>
        /// <returns>score [Selector] [Objective]</returns>
        protected override string GetStorePart()
        {
            return "score " + Selector + " " + Objective;
        }
    }
}