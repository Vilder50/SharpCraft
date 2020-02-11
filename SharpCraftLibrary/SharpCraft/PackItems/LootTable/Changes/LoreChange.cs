using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Changes the loot lore
    /// </summary>
    public class LoreChange : BaseChange
    {
        private JsonText[] lore;

        /// <summary>
        /// Intializes a new <see cref="LoreChange"/>
        /// </summary>
        /// <param name="lore">The lore</param>
        /// <param name="replace">True replaces the old lore. False appends the lore.</param>
        /// <param name="target">The entity @s referes to in the lore</param>
        public LoreChange(JsonText[] lore, ID.LootTarget? target = null, bool replace = true) : base("set_lore")
        {
            Lore = lore;
            Target = target;
            Replace = replace;
        }

        /// <summary>
        /// The entity @s referes to in the lore
        /// </summary>
        [DataTag("entity", ForceType = ID.NBTTagType.TagString, JsonTag = true)]
        public ID.LootTarget? Target { get; set; }

        /// <summary>
        /// The lore
        /// </summary>
        [DataTag("lore", ForceType = ID.NBTTagType.TagCompoundArray, JsonTag = true)]
        public JsonText[] Lore { get => lore; set => lore = value ?? throw new ArgumentNullException(nameof(Lore), "Lore may not be null"); }

        /// <summary>
        /// True replaces the old lore. False appends the lore.
        /// </summary>
        [DataTag("replace", JsonTag = true)]
        public bool Replace { get; set; }
    }
}
