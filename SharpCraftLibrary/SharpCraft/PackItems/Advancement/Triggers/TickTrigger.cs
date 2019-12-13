using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered 20 times a second
    /// </summary>
    public class TickTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="TickTrigger"/>
        /// </summary>
        public TickTrigger() : base("tick") { }
    }
}
