using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Condition which returns true if the item is correct
    /// </summary>
    public class ToolCondition : BaseCondition
    {
        private JSONObjects.Item item;

        /// <summary>
        /// Intializes a new <see cref="ToolCondition"/>
        /// </summary>
        /// <param name="item">The item to test for</param>
        public ToolCondition(JSONObjects.Item item) : base("minecraft:match_tool")
        {
            Item = item;
        }

        /// <summary>
        /// The location to test for
        /// </summary>
        [DataTag("predicate", JsonTag = true)]
        public JSONObjects.Item Item { get => item; set => item = value ?? throw new ArgumentNullException(nameof(Item), "Item may not be null"); }
    }
}
