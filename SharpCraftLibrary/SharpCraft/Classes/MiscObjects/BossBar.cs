namespace SharpCraft
{
    /// <summary>
    /// An object used for boss bars
    /// </summary>
    public class BossBar
    {
        private string name;
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
        /// The name of the bossbar
        /// </summary>
        public string Name 
        { 
            get => name;
            protected set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new System.ArgumentException("BossBar name may not be null or whitespace", nameof(Name));
                }
                name = value;
            }
        }
    }
}
