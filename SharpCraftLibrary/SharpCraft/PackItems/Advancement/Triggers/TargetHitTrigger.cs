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
        [DataTag("conditions.signalStrength", JsonTag = true)]
        public int SignalLength { get; set; }
    }
}
