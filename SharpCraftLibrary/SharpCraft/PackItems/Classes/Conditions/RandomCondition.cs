using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Condition which returns true randomly
    /// </summary>
    public class RandomCondition : BaseCondition
    {
        /// <summary>
        /// Intializes a new <see cref="RandomCondition"/>
        /// </summary>
        /// <param name="chance">The chance for it to return true (0-1)</param>
        public RandomCondition(double chance) : base("minecraft:random_chance")
        {
            Chance = chance;
        }

        /// <summary>
        /// The chance for it to return true (0-1)
        /// </summary>
        [DataTag("chance", JsonTag = true)]
        public double Chance { get; set; }
    }
}
