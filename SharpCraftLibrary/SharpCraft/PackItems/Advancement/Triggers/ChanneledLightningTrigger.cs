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
    public class ChanneledLightningTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="ChanneledLightningTrigger"/>
        /// </summary>
        public ChanneledLightningTrigger() : base("channeled_lightning") { }

        /// <summary>
        /// The entities hit by the lightning
        /// </summary>
        [DataTag("conditions.victims", JsonTag = true)]
        public Conditions.EntityCondition[][]? HitEntities { get; set; }
    }
}
