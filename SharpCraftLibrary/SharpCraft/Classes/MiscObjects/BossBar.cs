namespace SharpCraft
{
    /// <summary>
    /// An object used for boss bars
    /// </summary>
    public class BossBar
    {
        readonly string Name;
        /// <summary>
        /// Creates a new boss bar object.
        /// Note that this doesnt add the boss bar to the world
        /// </summary>
        /// <param name="BossBarName">The name of the bossbar</param>
        public BossBar(string BossBarName)
        {
            Name = BossBarName;
        }

        /// <summary>
        /// Outputs the name of this boss bar
        /// </summary>
        /// <returns>the name of this boss bar</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
