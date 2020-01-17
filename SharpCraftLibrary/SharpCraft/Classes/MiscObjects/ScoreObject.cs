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
        /// <param name="ScoreName">The name of the score</param>
        public Objective(string ScoreName)
        {
            Name = ScoreName;
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
                    throw new System.ArgumentException("ScoreObjective name may not be null or whitespace", nameof(Name));
                }
                name = value;
            }
        }
    }
}
