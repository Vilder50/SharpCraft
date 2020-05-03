using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player cures a zombie villager
    /// </summary>
    public class CuredVillagerTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="CuredVillagerTrigger"/>
        /// </summary>
        public CuredVillagerTrigger() : base("cured_zombie_villager") { }

        /// <summary>
        /// The new cured villager
        /// </summary>
        [DataTag("conditions.villager", JsonTag = true)]
        public Conditions.EntityCondition[] Villager { get; set; }

        /// <summary>
        /// The zombie which was cured
        /// </summary>
        [DataTag("conditions.zombie", JsonTag = true)]
        public Conditions.EntityCondition[] Zombie { get; set; }
    }
}
