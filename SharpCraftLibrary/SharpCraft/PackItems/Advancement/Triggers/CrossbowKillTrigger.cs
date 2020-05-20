using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player kills an entity with using a crossbow
    /// </summary>
    public class CrossbowKillTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="CrossbowKillTrigger"/>
        /// </summary>
        public CrossbowKillTrigger() : base("killed_by_crossbow") { }

        /// <summary>
        /// The entities killed
        /// </summary>
        [DataTag("victims", JsonTag = true)]
        public Conditions.EntityCondition[][]? Victims { get; set; }

        /// <summary>
        /// The amount of entities killed
        /// </summary>
        [DataTag("conditions.unique_entity_types", "min", "max", ID.NBTTagType.TagInt, true, JsonTag = true)]
        public MCRange? Kills { get; set; }
    }
}
