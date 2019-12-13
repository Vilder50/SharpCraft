using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.AdvancementObjects
{
    /// <summary>
    /// Gets triggered when the player's inventory is changed
    /// </summary>
    public class InventoryChangedTrigger : BaseTrigger
    {
        /// <summary>
        /// Intializes a new <see cref="InventoryChangedTrigger"/>
        /// </summary>
        public InventoryChangedTrigger() : base("inventory_changed") { }

        /// <summary>
        /// The items in the players inventory
        /// </summary>
        [DataTag("conditions.items", JsonTag = true)]
        public JSONObjects.Item[] Items { get; set; }

        /// <summary>
        /// The amount of empty slots
        /// </summary>
        [DataTag("conditions.slots.empty","min","max",ID.NBTTagType.TagInt,true, JsonTag = true)]
        public Range EmptySlots { get; set; }

        /// <summary>
        /// The amount of full slots
        /// </summary>
        [DataTag("conditions.slots.full", "min", "max", ID.NBTTagType.TagInt, true, JsonTag = true)]
        public Range FullSlots { get; set; }

        /// <summary>
        /// The amount of occupied slots
        /// </summary>
        [DataTag("conditions.slots.occupied", "min", "max", ID.NBTTagType.TagInt, true, JsonTag = true)]
        public Range OccupiedSlots { get; set; }
    }
}
