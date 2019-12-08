using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Condition which returns true if all of the given conditions are true
    /// </summary>
    public class AllCondition : BaseCondition
    {
        BaseCondition[] conditions;

        /// <summary>
        /// Intializes a new <see cref="AlternativeCondition"/>
        /// </summary>
        /// <param name="conditions">The conditions to check if true</param>
        public AllCondition(BaseCondition[] conditions) : base("minecraft:inverted")
        {
            Conditions = conditions;
        }

        /// <summary>
        /// The conditions to check if true
        /// </summary>
        [DataTag("terms", JsonTag = true)]
        public BaseCondition[] Conditions { get => conditions; set => conditions = value ?? throw new ArgumentNullException(nameof(Conditions), "Conditions may not be null"); }

        /// <summary>
        /// Returns a tree structure containing all the data tags for this object
        /// </summary>
        /// <returns>the bottom of the tree</returns>
        public override DataPartObject GetDataTree()
        {
            List<BaseCondition> invertedConditions = Conditions.Select(c => !c).ToList();
            return new InvertedCondition(new AlternativeCondition(invertedConditions.ToArray())).GetDataTree();
        }
    }
}
