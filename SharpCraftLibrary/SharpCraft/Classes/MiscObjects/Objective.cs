using System.Text.RegularExpressions;

namespace SharpCraft
{
    /// <summary>
    /// An object used for scoreboard objectives
    /// </summary>
    public class Objective
    {
        private const string namePattern = @"^[a-zA-Z\-_\./0-9]+$";
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
                if (!ValidateName(value))
                {
                    throw new System.ArgumentException("Objective name is invalid. Only accepts: \"" + namePattern + "\"", nameof(Name));
                }
                name = value;
            }
        }

        /// <summary>
        /// Checks if the given name is valid or not for a <see cref="Objective"/>
        /// </summary>
        /// <param name="name">The name to check</param>
        /// <returns>True if the name is valid</returns>
        public static bool ValidateName(string name)
        {
            return Regex.IsMatch(name, namePattern);
        }
    }
}
