using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Condition which returns true if any of the given conditions are true
    /// </summary>
    public class AlternativeCondition : BaseCondition
    {
        private BaseCondition[] conditions = null!;

        /// <summary>
        /// Intializes a new <see cref="AlternativeCondition"/>
        /// </summary>
        /// <param name="conditions">The conditions to check if true</param>
        public AlternativeCondition(BaseCondition[] conditions) : base("minecraft:alternative")
        {
            Conditions = conditions;
        }

        /// <summary>
        /// The conditions to check if true
        /// </summary>
        [DataTag("terms", JsonTag = true)]
        public BaseCondition[] Conditions { get => conditions; set => conditions = value ?? throw new ArgumentNullException(nameof(Conditions), "Conditions may not be null"); }
    }
}

