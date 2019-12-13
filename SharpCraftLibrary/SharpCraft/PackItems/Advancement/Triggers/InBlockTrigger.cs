using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player is in a block (Triggers for each block the player is in)
    /// </summary>
    public class InBlockTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="InBlockTrigger"/>
        /// </summary>
        public InBlockTrigger() : base("enter_block") { }

        /// <summary>
        /// The block the player is in.
        /// Note: block data is not supported.
        /// </summary>
        [DataTag("conditions.block", "conditions.block", "conditions.state", true, JsonTag = true, Merge = true)]
        public Block Block { get; set; }
    }
}
