using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player switches dimension
    /// </summary>
    public class ChangedDimensionTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="ChangedDimensionTrigger"/>
        /// </summary>
        public ChangedDimensionTrigger() : base("changed_dimension") { }

        /// <summary>
        /// The dimension the player is coming from
        /// </summary>
        [DataTag("conditions.from", JsonTag = true)]
        public DimensionObjects.IDimension? From { get; set; }

        /// <summary>
        /// The dimension the player is going to
        /// </summary>
        [DataTag("conditions.to", JsonTag = true)]
        public DimensionObjects.IDimension? To { get; set; }
    }
}
