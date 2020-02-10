using System.Text.RegularExpressions;

namespace SharpCraft
{
    /// <summary>
    /// An object used for boss bars
    /// </summary>
    public class BossBar
    {
        private const string namePattern = @"^[a-z\-_\./0-9]+$";
        private string name;

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
                if (!ValidateName(loweredString))
                {
                    throw new System.ArgumentException("BossBar name is invalid. Only accepts: \"" + namePattern + "\"", nameof(Name));
                }
                name = loweredString;
            }
        }

        /// <summary>
        /// The namespace the bossbar is in. Null = minecraft namespace.
        /// </summary>
        public BasePackNamespace Namespace { get; private set; }

        /// <summary>
        /// Get string used for refering this bossbar
        /// </summary>
        /// <returns>String used for refering this bossbar</returns>
        public string GetFullName()
        {
            return (Namespace?.Name ?? "minecraft") + ":" + Name;
        }

        /// <summary>
        /// Checks if the given name is valid or not for a bossbar
        /// </summary>
        /// <param name="name">The name to check</param>
        /// <returns>True if the name is valid</returns>
        public static bool ValidateName(string name)
        {
            return Regex.IsMatch(name, namePattern);
        }
    }
}
