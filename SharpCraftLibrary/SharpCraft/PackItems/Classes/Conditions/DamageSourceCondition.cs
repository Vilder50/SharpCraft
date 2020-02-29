using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.Conditions
{
    /// <summary>
    /// Condition which returns true if the entity data is correct
    /// </summary>
    public class DamageSourceCondition : BaseCondition
    {
        private JSONObjects.Damage damage = null!;

        /// <summary>
        /// Intializes a new <see cref="DamageSourceCondition"/>
        /// </summary>
        /// <param name="damage">The type of damage</param>
        public DamageSourceCondition(JSONObjects.Damage damage) : base("minecraft:damage_source_properties")
        {
            Damage = damage;
        }

        /// <summary>
        /// The type of damage
        /// </summary>
        [DataTag("properties", JsonTag = true)]
        public JSONObjects.Damage Damage { get => damage; set => damage = value ?? throw new ArgumentNullException(nameof(Damage), "Damage may not be null"); }
    }
}
