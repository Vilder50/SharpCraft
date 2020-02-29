using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player breaks a bee nest
    /// </summary>
    public class NestDestroyedTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="NestDestroyedTrigger"/>
        /// </summary>
        public NestDestroyedTrigger() : base("bee_nest_destroyed") { }

        /// <summary>
        /// The item the player used to break the nest with
        /// </summary>
        [DataTag("conditions.item", JsonTag = true)]
        public JsonObjects.Item? Item { get; set; }

        /// <summary>
        /// The block which was destroyed
        /// </summary>
        [DataTag("conditions.block", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
        public BlockType? Id { get; set; }

        /// <summary>
        /// The amount of bees in the nest
        /// </summary>
        [DataTag("conditions.num_bees_inside", JsonTag = true)]
        public int? Bees { get; set; }
    }
}
