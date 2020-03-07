using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player shoots a <see cref="ID.Block.target"/>.
    /// </summary>
    public class TargetHitTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="TargetHitTrigger"/>
        /// </summary>
        public TargetHitTrigger() : base("bullseye") { }

        /// <summary>
        /// The redstone length the target outputs
        /// </summary>
        [DataTag("conditions.signal_strength", JsonTag = true)]
        public int SignalLength { get; set; }

        /// <summary>
        /// The entity which hit the target
        /// </summary>
        [DataTag("conditions.projectile", JsonTag = true)]
        public JSONObjects.Entity Projectile { get; set; }

        /// <summary>
        /// The entity which shot the target
        /// </summary>
        [DataTag("conditions.shooter", JsonTag = true)]
        public JSONObjects.Entity Shooter { get; set; }
    }
}
