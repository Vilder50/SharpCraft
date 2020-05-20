using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// The base class for advancement triggers
    /// </summary>
    public abstract class BasePlayerTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="BasePlayerTrigger"/>
        /// </summary>
        /// <param name="type">The type of trigger</param>
        protected BasePlayerTrigger(string type) : base(type)
        {
        }

        /// <summary>
        /// The player triggering the trigger
        /// </summary>
        [DataTag("conditions.player", JsonTag = true)]
        public JsonObjects.Entity? Player { get; set; }
    }
}
