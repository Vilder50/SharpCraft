using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player places a block
    /// </summary>
    public class PlacedBlockTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="PlacedBlockTrigger"/>
        /// </summary>
        public PlacedBlockTrigger() : base("placed_block") { }

        /// <summary>
        /// The block the player is in.
        /// Note: block data is not supported.
        /// </summary>
        [DataTag("conditions.block", "conditions.block", "conditions.state", true, JsonTag = true, Merge = true)]
        public Block Block { get; set; }

        /// <summary>
        /// The item the player used to place the block
        /// </summary>
        [DataTag("conditions.item", JsonTag = true)]
        public JSONObjects.Item PlacedItem { get; set; }

        /// <summary>
        /// The location the block was placed
        /// </summary>
        [DataTag("conditions.location", JsonTag = true)]
        public JSONObjects.Location Location { get; set; }
    }
}
