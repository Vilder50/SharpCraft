using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Condition which returns true if the block would survive the explosion (1/explosion radius chance for the block to survive)
    /// </summary>
    public class SurvivesExplosionCondition : BaseCondition
    {
        /// <summary>
        /// Intializes a new <see cref="SurvivesExplosionCondition"/>
        /// </summary>
        public SurvivesExplosionCondition() : base("minecraft:survives_explosion")
        {
            
        }
    }
}
