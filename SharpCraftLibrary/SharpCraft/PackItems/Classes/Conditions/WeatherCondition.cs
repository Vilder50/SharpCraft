using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Condition which returns true if the item has the given enchants
    /// </summary>
    public class WeatherCondition : BaseCondition
    {
        /// <summary>
        /// Intializes a new <see cref="WeatherCondition"/>
        /// </summary>
        public WeatherCondition(bool raining, bool? thundering) : base("minecraft:tool_enchantment")
        {
            Raining = raining;
            Thundering = thundering;
        }

        /// <summary>
        /// If its raining
        /// </summary>
        [DataTag("raining", JsonTag = true)]
        public bool Raining { get; set; }

        /// <summary>
        /// If its thundering
        /// </summary>
        [DataTag("thundering", JsonTag = true)]
        public bool? Thundering { get; set; }
    }
}