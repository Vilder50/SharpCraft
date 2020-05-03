using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player activates a raid
    /// </summary>
    public class CausedRaidTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="CausedRaidTrigger"/>
        /// </summary>
        public CausedRaidTrigger() : base("voluntary_exile") { }
    }
}
