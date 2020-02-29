using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player slides down a block
    /// </summary>
    public class SlideBlockTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="SlideBlockTrigger"/>
        /// </summary>
        public SlideBlockTrigger() : base("slide_down_block") { }

        /// <summary>
        /// The block the player slid down of
        /// </summary>
        [DataTag("conditions.block", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
        public BlockType? Block { get; set; }
    }
}
