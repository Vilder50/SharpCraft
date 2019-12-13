using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player makes lightning using a trident
    /// </summary>
    public class ChanneledLightningTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="ChanneledLightningTrigger"/>
        /// </summary>
        public ChanneledLightningTrigger() : base("channeled_lightning") { }

        /// <summary>
        /// The dimension the player is coming from
        /// </summary>
        [DataTag("conditions.victims", JsonTag = true)]
        public JSONObjects.Entity[] HitEntities { get; set; }
    }
}
