using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player defeats a raid
    /// </summary>
    public class ImpossibleTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="ImpossibleTrigger"/>
        /// </summary>
        public ImpossibleTrigger() : base("impossible") { }
    }
}
