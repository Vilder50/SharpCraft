using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered the player opens a container which generates loot from a loot table
    /// </summary>
    public class GeneratedContainerLootTrigger : BasePlayerTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="GeneratedContainerLootTrigger"/>
        /// </summary>
        public GeneratedContainerLootTrigger() : base("player_generates_container_loot") { }

        /// <summary>
        /// The loot table which generated the container's items
        /// </summary>
        [DataTag("conditions.loot_table", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
        public ILootTable? LootTable { get; set; }
    }
}
