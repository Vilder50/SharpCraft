namespace SharpCraft
{
    /// <summary>
    /// Interface for selectors
    /// </summary>
    public abstract class BaseSelector
    {
        /// <summary>
        /// Returns true if the selector is limited to selecting a single entity
        /// </summary>
        /// <returns>True if the selector is limited to selecting a single entity</returns>
        public abstract bool IsLimited();

        /// <summary>
        /// Forces the selector to only select 1 entity
        /// </summary>
        public abstract void LimitSelector();

        /// <summary>
        /// The selector string used by the game
        /// </summary>
        /// <returns>The selector string used by the game</returns>
        public abstract string GetSelectorString();

        /// <summary>
        /// Converts a selector type into a selector
        /// </summary>
        /// <param name="selector">the selector type to convert into a selector</param>
        public static implicit operator BaseSelector(ID.Selector selector)
        {
            return new Selector(selector);
        }

        /// <summary>
        /// Converts a string into a selector selecting a name
        /// </summary>
        /// <param name="name">The name to select</param>
        public static implicit operator BaseSelector(string name)
        {
            return new NameSelector(name);
        }
    }
}
