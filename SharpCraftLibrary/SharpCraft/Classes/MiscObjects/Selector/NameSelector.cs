using System;

namespace SharpCraft
{
    /// <summary>
    /// Selector which selects a name
    /// </summary>
    public class NameSelector : BaseSelector
    {
        /// <summary>
        /// Intializes a new <see cref="NameSelector"/>
        /// </summary>
        /// <param name="name">The name to select</param>
        public NameSelector(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Intializes a new <see cref="NameSelector"/>
        /// </summary>
        /// <param name="name">The name to select</param>
        /// <param name="isHidden">If the name should be hidden</param>
        public NameSelector(string name, bool isHidden)
        {
            Name = name;
            IsHidden = isHidden;
        }

        private string name = null!;

        /// <summary>
        /// The name to select
        /// </summary>
        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Selector Name may not be null or whitespace", nameof(Name));
                }
                if (value.Contains(" ") || value.Contains("\n") || value.Contains("*"))
                {
                    throw new ArgumentException("Selector name may not contain spaces, newlines or *", nameof(Name));
                }

                if (value.StartsWith("#"))
                {
                    IsHidden = true;
                    name = value.Substring(1);
                }
                else
                {
                    name = value;
                }
            }
        }

        /// <summary>
        /// if the name should be hidden on scoreboard lists. (If the name should start with a #)
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// Returns true. Selector can only select 1 thing
        /// </summary>
        /// <returns>True. Selector can only select 1 thing</returns>
        public override bool IsLimited()
        {
            return true;
        }

        /// <summary>
        /// Does nothing. Selector is already limited
        /// </summary>
        public override void LimitSelector()
        {
            ;
        }

        /// <summary>
        /// The selector string used by the game
        /// </summary>
        /// <returns>The selector string used by the game</returns>
        public override string GetSelectorString()
        {
            if (IsHidden)
            {
                return "#" + Name;
            }
            return Name;
        }

        /// <summary>
        /// Converts a string into a selector selecting a name
        /// </summary>
        /// <param name="name">The name to select</param>
        public static implicit operator NameSelector(string name)
        {
            return new NameSelector(name);
        }
    }
}
