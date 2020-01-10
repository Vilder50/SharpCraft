namespace SharpCraft
{
    /// <summary>
    /// An object used for scoreboard objectives
    /// </summary>
    public class ScoreObject
    {
        readonly string Name;
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
        /// Outputs the name of this score objective
        /// </summary>
        /// <returns>the name of this score objective</returns>
        public override string ToString()
        {
            return Name;
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
