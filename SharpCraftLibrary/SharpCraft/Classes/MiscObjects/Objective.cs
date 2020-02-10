﻿using System.Text.RegularExpressions;

namespace SharpCraft
{
    /// <summary>
    /// An object used for scoreboard objectives
    /// </summary>
    public class Objective
    {
        private string name;

        /// <summary>
        /// Creates a new scoreboard objective object.
        /// Note that this doesnt add the objective to the world
        /// </summary>
        /// <param name="scoreName">The name of the score</param>
        public Objective(string scoreName)
        {
            Name = scoreName;
        }

        /// <summary>
        /// The name of this score objective
        /// </summary>
        public string Name
        {
            get => name;
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new System.ArgumentException("Objective name may not be null or whitespace", nameof(Name));
                }
                if (!Utils.ValidateName(value,true,false))
                {
                    throw new System.ArgumentException("Objective name is invalid. Only accepts letters, numbers and -._");
                }
                name = value;
            }
        }
    }
}
