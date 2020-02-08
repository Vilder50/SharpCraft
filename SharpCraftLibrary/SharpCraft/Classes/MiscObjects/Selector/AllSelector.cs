using System;

namespace SharpCraft
{
    /// <summary>
    /// Selector which selects everything
    /// </summary>
    public class AllSelector : BaseSelector
    {
        private static AllSelector singleton;

        /// <summary>
        /// Returns a singleton <see cref="AllSelector"/>
        /// </summary>
        /// <returns>A singleton <see cref="AllSelector"/></returns>
        public static BaseSelector GetSelector()
        {
            singleton ??= new AllSelector();
            return singleton;
        }

        /// <summary>
        /// Returns false. Selector selects everything
        /// </summary>
        /// <returns>False. Selector selects everything</returns>
        public override bool IsLimited()
        {
            return false;
        }

        /// <summary>
        /// Throws an exception since the selector can't be limited
        /// </summary>
        public override void LimitSelector()
        {
            throw new InvalidOperationException("Cannot limit a " + nameof(AllSelector));
        }

        /// <summary>
        /// The selector string used by the game
        /// </summary>
        /// <returns>The selector string used by the game</returns>
        public override string GetSelectorString()
        {
            return "*";
        }
    }
}
