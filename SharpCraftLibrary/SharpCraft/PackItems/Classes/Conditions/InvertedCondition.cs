using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Condition which returns true if the given condition returns false
    /// </summary>
    public class InvertedCondition : BaseCondition
    {
        private BaseCondition condition = null!;

        /// <summary>
        /// Intializes a new <see cref="InvertedCondition"/>
        /// </summary>
        /// <param name="condition">The condition to invert</param>
        public InvertedCondition(BaseCondition condition) : base("minecraft:inverted")
        {
            Condition = condition;
        }

        /// <summary>
        /// The condition to invert
        /// </summary>
        [DataTag("term", JsonTag = true)]
        public BaseCondition Condition { get => condition; set => condition = value ?? throw new ArgumentNullException(nameof(Condition), "Condition may not be null"); }
    }
}
