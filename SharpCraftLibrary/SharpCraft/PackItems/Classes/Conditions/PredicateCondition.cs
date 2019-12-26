using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Condition which returns true if the given predicate returns true
    /// </summary>
    public class PredicateCondition : BaseCondition
    {
        private IPredicate predicate;

        /// <summary>
        /// Intializes a new <see cref="PredicateCondition"/>
        /// </summary>
        /// <param name="predicate">The predicate to check</param>
        public PredicateCondition(IPredicate predicate) : base("minecraft:reference")
        {
            Predicate = predicate;
        }

        /// <summary>
        /// The predicate to check
        /// </summary>
        [DataTag("name", JsonTag = true)]
        public IPredicate Predicate { get => predicate; set => predicate = value ?? throw new ArgumentNullException(nameof(Predicate), "Predicate may not be empty"); }
    }
}
