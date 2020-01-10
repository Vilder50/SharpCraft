namespace SharpCraft
{
    /// <summary>
    /// An object used for scoreboard objectives
    /// </summary>
    public class ScoreObject
    {
        private string name;
        /// <summary>
        /// Creates a new scoreboard objective object.
        /// Note that this doesnt add the objective to the world
        /// </summary>
        /// <param name="ScoreName">The name of the score</param>
        public ScoreObject(string ScoreName)
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

        /// <summary>
        /// Displays a text and a score in this <see cref="ScoreObject"/> in the chat
        /// </summary>
        /// <param name="text">The text to display</param>
        /// <param name="selector">The selector's score to show</param>
        /// <param name="writeOn">The function to write the command on</param>
        public void ScoreSayDebug(string text, BaseSelector selector, Function writeOn)
        {
            writeOn.Player.Tellraw(ID.Selector.a, new JsonText[] { text + " ", new JsonText.Score(selector,this) });
        }
    }
}
