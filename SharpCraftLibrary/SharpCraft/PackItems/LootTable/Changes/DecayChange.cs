using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Makes less items drop. 1/Explosion Radius chance an item won't survive
    /// </summary>
    public class DecayChange : BaseChange
    {
        /// <summary>
        /// Intializes a new <see cref="DecayChange"/>
        /// </summary>
        public DecayChange() : base("explosion_decay")
        {
            
        }
    }
}
