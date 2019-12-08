using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Condition which checks if an entity was killed by a player
    /// </summary>
    public class KilledByPlayerCondition : BaseCondition
    {
        /// <summary>
        /// Intializes a new <see cref="KilledByPlayerCondition"/>
        /// </summary>
        /// <param name="ifTrue">True if the entity has to have been killed by a player. False it shouldn't have been by a player</param>
        public KilledByPlayerCondition(bool ifTrue) : base("minecraft:killed_by_player")
        {
            IfTrue = ifTrue;
        }

        /// <summary>
        /// True if the entity has to have been killed by a player. False it shouldn't have been by a player
        /// </summary>
        [DataTag("inverse", JsonTag = true)]
        public bool IfTrue { get; set; }
    }
}
