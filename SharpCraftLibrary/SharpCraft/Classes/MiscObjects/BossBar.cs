using System.Text.RegularExpressions;

namespace SharpCraft
{
    /// <summary>
    /// An object used for boss bars
    /// </summary>
    public class BossBar
    {
        private string name = null!;

        /// <summary>
        /// Creates a new boss bar object.
        /// Note that this doesnt add the boss bar to the world
        /// </summary>
        /// <param name="bossBarName">The name of the bossbar</param>
        public BossBar(string bossBarName)
        {
            Name = bossBarName;
        }

        /// <summary>
        /// Creates a new boss bar object.
        /// Note that this doesnt add the boss bar to the world
        /// </summary>
        /// <param name="bossBarName">The name of the bossbar</param>
        /// <param name="namespace">The namespace the bossbar is in. Null = minecraft namespace.</param>
        public BossBar(BasePackNamespace @namespace, string bossBarName) : this(bossBarName)
        {
            Namespace = @namespace;
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
                string loweredString = value.ToLower();
                if (!Utils.ValidateName(loweredString,false,true, null))
                {
                    throw new System.ArgumentException("BossBar name is invalid. Name only accepts letters, numbers and /-._");
                }
                name = loweredString;
            }
        }

        /// <summary>
        /// The namespace the bossbar is in. Null = minecraft namespace.
        /// </summary>
        public BasePackNamespace? Namespace { get; private set; }

        /// <summary>
        /// Get string used for refering this bossbar
        /// </summary>
        /// <returns>String used for refering this bossbar</returns>
        public string GetFullName()
        {
            return (Namespace?.Name ?? "minecraft") + ":" + Name;
        }
    }
}
