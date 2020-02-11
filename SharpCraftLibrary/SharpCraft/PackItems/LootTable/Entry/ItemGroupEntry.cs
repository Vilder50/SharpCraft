using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCraft.Data;

namespace SharpCraft.LootObjects
{
    /// <summary>
    /// Entry for dropping an item group
    /// </summary>
    public class ItemGroupEntry : BaseEntry
    {
        /// <summary>
        /// Intializes a new <see cref="ItemGroupEntry"/>
        /// </summary>
        /// <param name="itemGroup">The item group to drop</param>
        /// <param name="onlyDropOne">If true drops a random item from the tag. If false drops all items in the item tag.</param>
        public ItemGroupEntry(IGroup<ItemType> itemGroup, bool onlyDropOne) : base(ID.LootEntryType.tag)
        {
            ItemGroup = itemGroup;
            OnlyDropOne = onlyDropOne;
        }

        /// <summary>
        /// The item group to drop
        /// </summary>
        [DataTag("name", true, ForceType = ID.NBTTagType.TagString, JsonTag = true)]
        public IGroup<ItemType> ItemGroup { get; set; }

        /// <summary>
        /// If true drops a random item from the tag. If false drops all items in the item tag.
        /// </summary>
        [DataTag("expand", JsonTag = true)]
        public bool OnlyDropOne { get; set; }
    }
}