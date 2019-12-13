using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player gets a new effect or when an effect runs out
    /// </summary>
    public class EffectChangedTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="EffectChangedTrigger"/>
        /// </summary>
        public EffectChangedTrigger() : base("effects_changed") { }

        /// <summary>
        /// The effects the player has
        /// </summary>
        [DataTag("conditions.effects", JsonTag = true)]
        public JSONObjects.Effects Effects { get; set; }
    }
}
